import { request } from 'umi';
import { ILogin, ILoginResponse } from './typings';
import { message } from 'antd';
import { history } from 'umi';

/**
 * 登录
 * @param loginInfo 登录信息
 * @returns 用户信息
 */
export const login = async (loginInfo: ILogin): Promise<API.ICurrentUser | undefined> => {
  const result = await request<ILoginResponse>('/api/user/login', { data: loginInfo, method: 'POST' });
  if (result.succeeded) {
    saveUserInfo(loginInfo.remember, result.data);
    history.replace('/');
    return result.data;
  } else {
    message.error('Login Failed:' + result.errors);
  }
}
/**
 * 存储用户信息
 * @param remember 记住登录状态
 * @param userInfo 用户信息
 */
const saveUserInfo = (remember: boolean, userInfo: API.ICurrentUser): void => {
  localStorage.setItem('remember', remember ? 'true' : 'false');
  if (remember) {
    localStorage.setItem('user', JSON.stringify(userInfo));
  } else {
    sessionStorage.setItem('user', JSON.stringify(userInfo));
  }
}
