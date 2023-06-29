import { readFile } from "fs/promises";
import { EOL } from "os";
import type {
  DocumentConventions,
  IAuthOptions,
  SessionOptions,
} from "ravendb";
import { DocumentStore } from "ravendb";

let store: DocumentStore;
let initialized = false;

interface InitializeOptions {
  /**
   * Cluster Node URLs
   */
  urls: string[];

  /**
   * Default database
   */
  databaseName: string;

  /**
   * Relative or absolute path to .pfx file
   */
  dbCertPath?: string;

  /**
   * Optional password for .pfx file
   */
  dbCertPassword?: string;

  /**
   * The full PEM certificate contents (usually used in Azure).
   */
  dbCertPem?: string;

  /**
   * Customize DocumentConventions of store before initialization
   */
  customize?: (c: DocumentConventions) => void;
}

export async function initializeDb({
  urls,
  databaseName,
  dbCertPassword,
  dbCertPath,
  dbCertPem,
  customize,
}: InitializeOptions) {
  if (initialized) return;

  let authOptions: IAuthOptions = undefined;

  if (dbCertPath) {
    authOptions = await getAuthOptionsFromCertificatePath(
      dbCertPath,
      dbCertPassword
    );
  } else if (dbCertPem) {
    authOptions = getAuthOptionsFromCertPem(dbCertPem);
  }

  store = new DocumentStore(urls, databaseName, authOptions);

  if (customize) {
    customize(store.conventions);
  }

  store.initialize();

  initialized = true;

  return store;
}

async function getAuthOptionsFromCertificatePath(
  dbCertPath: string,
  dbCertPassword: string
): Promise<IAuthOptions | undefined> {
  return {
    certificate: await readFile(dbCertPath),
    password: dbCertPassword,
    type: "pfx",
  };
}

function getAuthOptionsFromCertPem(dbCertPem: string): IAuthOptions {
  let certificate = dbCertPem;
  const isMissingLineEndings = !dbCertPem.includes(EOL);

  if (isMissingLineEndings) {
    // Typically when pasting values into Azure env vars
    certificate = normalizePEM(certificate);
  }

  return {
    certificate,
    type: "pem",
  };
}

function normalizePEM(pem: string): string {
  return pem.replace(PEM_REGEX, (match, certSection, certSectionBody) => {
    const normalizedCertSectionBody = certSectionBody.replace(/\s/g, EOL);
    return `-----BEGIN ${certSection}-----${EOL}${normalizedCertSectionBody.trim()}${EOL}-----END ${certSection}-----${EOL}`;
  });
}

const PEM_REGEX =
  /-----BEGIN ([A-Z\s]+)-----(\s?[A-Za-z0-9+\/=\s]+?\s?)-----END \1-----/gm;

export function openDbSession(opts?: SessionOptions) {
  if (!initialized)
    throw new Error(
      "DocumentStore is not initialized yet. Must `initializeDb()` before calling `openDbSession()`."
    );
  return store.openSession(opts);
}

export function initializeWelcomeDb() {
  if (initialized) return store;

  store = new DocumentStore(["http://live-test.ravendb.net"], "(not set)");

  store.conventions.disableTopologyUpdates = true;
  store.initialize();

  initialized = true;

  return store;
}
