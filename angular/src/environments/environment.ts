import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'GraphTask',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44391/',
    redirectUri: baseUrl,
    clientId: 'GraphTask_App',
    responseType: 'code',
    scope: 'offline_access GraphTask',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44391',
      rootNamespace: 'GraphTask',
    },
  },
} as Environment;
