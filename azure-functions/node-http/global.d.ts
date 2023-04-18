import { IDocumentSession } from "ravendb";

declare module "@azure/functions" {
  export interface Context {
    db: IDocumentSession;
  }
}
