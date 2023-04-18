import { readFile } from "fs/promises";
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

  dbCertPath?: string;

  dbCertPassword?: string;

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
  customize,
}: InitializeOptions) {
  if (initialized) return;

  let authOptions: IAuthOptions = undefined;

  if (dbCertPath) {
    authOptions = await getAuthOptions(dbCertPath, dbCertPassword);
  }

  store = new DocumentStore(urls, databaseName, authOptions);

  if (customize) {
    customize(store.conventions);
  }

  store.initialize();

  initialized = true;

  return store;
}

async function getAuthOptions(
  dbCertPath: string,
  dbCertPassword: string
): Promise<IAuthOptions | undefined> {
  return {
    certificate: await readFile(dbCertPath),
    password: dbCertPassword,
    type: "pfx",
  };
}

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
