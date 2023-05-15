import { MiddlewareFunction } from "@senacor/azure-function-middleware";
import { initializeDb, initializeWelcomeDb } from ".";
import { DocumentConventions, IDocumentStore } from "ravendb";

export const createDbMiddleware =
  (customize?: (c: DocumentConventions) => void): MiddlewareFunction =>
  async (ctx, req) => {
    const isNewSetup = !process.env.DB_URLS || !process.env.DB_NAME;
    let dbStore: IDocumentStore;

    console.log("URLs", process.env.DB_URLS);

    if (isNewSetup) {
      dbStore = initializeWelcomeDb();
    } else {
      dbStore = await initializeDb({
        urls: process.env.DB_URLS.split(","),
        databaseName: process.env.DB_NAME,
        dbCertPath: process.env.DB_CERT_PATH,
        dbCertPassword: process.env.DB_CERT_PASSWORD,
        dbCertPem: process.env.DB_CERT_PEM,
        customize,
      });
    }

    ctx.db = dbStore.openSession();
  };
