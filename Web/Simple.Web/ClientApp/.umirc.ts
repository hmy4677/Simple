import { defineConfig } from 'umi';

export default defineConfig({
  title: 'Simple',
  nodeModulesTransform: {
    type: 'none',
  },
  routes: [
    { path: '/login', component: '@/pages/user/Login', name: 'Login' },
    {
      path: '/',
      component: '@/layout',
      routes: [
        { path: '/', component: '@/pages/index' },
        { component: '@/pages/404' }
      ]
    },

  ],
  fastRefresh: {},
  proxy: {
    '/api': {
      target: "http://localhost:5000/",
      'changeOrigin': true,
      'pathRewrite': { '^/api': 'api' },
    }
  },
  theme: {
    '@primary-color': '#1DA57A',
    "border-radius-base": "6px" // 组件/浮层圆角
  },
  define: {
    initArr: [],
    dateStr: 'YYYY-MM-DD',
    dateTimeStr: 'YYYY-MM-DD HH:mm:ss',
    timeStr: 'HH:mm:ss',
    initPagination: {
      total: 0,
      page: 1,
      pageSize: 20
    },
    websiteTitle:'Simple'
  },
  chainWebpack(memo) {
    memo.module
      .rule('media')
      .test(/\.(mp3|4)$/)
      .use('file-loader')
      .loader(require.resolve('file-loader'))
  },
  headScripts: [""],
  outputPath: 'build'
});
