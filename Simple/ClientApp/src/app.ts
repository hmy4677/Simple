import { history } from 'umi';
import type { ResponseError, RequestOptionsInit } from 'umi-request';
import type { RequestConfig } from 'umi';
import { notification, message } from 'antd';
import moment from 'moment';

/**
 * 启用initialState
 * @returns 用户信息
 */
export async function getInitialState() {
  let currentUser: API.ICurrentUser = {};
  const fetchUserInfo = () => {
    try {
      if (window.localStorage.getItem('user')) {
        currentUser = JSON.parse(window.localStorage.getItem('user') || '');
      }
      else {
        currentUser = JSON.parse(window.sessionStorage.getItem('user') || '');
      }
      return currentUser;
    }
    catch (error) {
      history.replace('/login');
    }
  };
  // 非登录页面
  if (history.location.pathname !== '/login') {
    const currentUser = fetchUserInfo();
    return { currentUser };
  }
  return { currentUser };
}

const codeMessage = {
  200: '服务器成功返回请求的数据。',
  201: '新建或修改数据成功。',
  202: '一个请求已经进入后台排队（异步任务）。',
  204: '删除数据成功。',
  400: '发出的请求有错误，服务器没有进行新建或修改数据的操作。',
  401: '用户没有权限（令牌、用户名、密码错误）。',
  403: '用户得到授权，但是访问是被禁止的。',
  404: '发出的请求针对的是不存在的记录，服务器没有进行操作。',
  405: '请求方法不被允许。',
  406: '请求的格式不可得。',
  410: '请求的资源被永久删除，且不会再得到的。',
  422: '当创建一个对象时，发生一个验证错误。',
  500: '服务器发生错误，请检查服务器。',
  502: '网关错误。',
  503: '服务不可用，服务器暂时过载或维护。',
  504: '网关超时。',
};

notification.config({
  maxCount: 1,
  duration: 3,
  rtl: false,
});

const openNotificationWithIcon = (type: string, content: string) => {
  notification[type]({
    message: '消息',
    description: content,
  });
};

/** 异常处理*/
const errorHandler = (error: ResponseError) => {
  const { response } = error;
  if (response && response.status === 401) {
    localStorage.removeItem('user');
    localStorage.removeItem('token');
    location.replace('/login');
    openNotificationWithIcon('info', '登录过期');
    return;
  }
  if (response && response.status) {
    const errorText = codeMessage[response.status] || response.statusText;
    openNotificationWithIcon('error', errorText);
    return;
  }
  if (!response) {
    openNotificationWithIcon('warning', '网络异常');
    return;
  }
  throw error;
};
//请求拦截器
const requestInterceptor = (url: string, options: RequestOptionsInit) => {
  const token = localStorage.getItem('token') ?? sessionStorage.getItem('token') ?? "";
  let authHeader = { 'Authorization': `Bearer ${token}`, };

  //如果token快要过期了，就带上刷新token 后台设置的20分钟过过期
  const tokenStamp = localStorage.getItem('tokenStamp') ?? sessionStorage.getItem('tokenStamp') ?? "";
  if (moment().unix() - parseInt(tokenStamp) >= 19 * 60) {
    const xToken = localStorage.getItem('xToken') ?? sessionStorage.getItem('xToken') ?? "";
    authHeader['X-Authorization'] = `Bearer ${xToken}`;
  }
  return {
    url: `${url}`,
    options: {
      ...options,
      interceptors: true,
      headers: authHeader,
      errorHandler
    },
  };
};
//响应拦截器
const responseInterceptor = (response: Response) => {
  const token = response.headers.get('access-token');
  const xToken = response.headers.get('x-access-token');

  if (token && xToken && token !== 'invalid_token') {
    const remember: string | null = localStorage.getItem('remember');
    if (remember === 'true') {
      localStorage.setItem('token', token);
      localStorage.setItem('xToken', xToken);
      localStorage.setItem('tokenStamp', moment().unix().toString());
    } else {
      sessionStorage.setItem('token', token);
      sessionStorage.setItem('xToken', xToken);
      sessionStorage.setItem('tokenStamp', moment().unix().toString())
    }
  }
  return response;
};

//请求配置
export const request: RequestConfig = {
  timeOut: 10000,
  requestInterceptors: [requestInterceptor],
  responseInterceptors: [responseInterceptor]
};
