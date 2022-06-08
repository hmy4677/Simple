// @ts-nocheck
import React from 'react';
import { ApplyPluginsType } from '/Users/hemingyu/项目/Simple/Web/Simple.Web/ClientApp/node_modules/umi/node_modules/@umijs/runtime';
import * as umiExports from './umiExports';
import { plugin } from './plugin';

export function getRoutes() {
  const routes = [
  {
    "path": "/login",
    "component": require('@/pages/user/Login').default,
    "name": "Login",
    "exact": true
  },
  {
    "path": "/",
    "component": require('@/layout').default,
    "routes": [
      {
        "path": "/",
        "component": require('@/pages/index').default,
        "exact": true
      },
      {
        "component": require('@/pages/404').default,
        "exact": true
      }
    ]
  }
];

  // allow user to extend routes
  plugin.applyPlugins({
    key: 'patchRoutes',
    type: ApplyPluginsType.event,
    args: { routes },
  });

  return routes;
}
