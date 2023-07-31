export class EnvService {
  /** The root url of the api. */
  apiUrl: string;

  /** Whether debug is enabled. */
  enableDebug: boolean;

  constructor(obj: any) {
    this.apiUrl = obj.apiUrl;
    this.enableDebug = obj.enableDebug;
  }
}

export const EnvServiceProvider = {
  provide: EnvService,
  useFactory: () => new EnvService((window as any).__env),
};
