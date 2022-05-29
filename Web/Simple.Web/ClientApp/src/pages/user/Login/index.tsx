import DynamicBG from '@/components/DynamicBG';
import { Form, Input, Checkbox, Button, message } from 'antd';
import { UserOutlined, LockOutlined } from '@ant-design/icons';
import style from './style.less';
import { login } from './service';
import { history, useModel } from 'umi';
import { ILogin } from './typings';
import { ReactComponent as Logo } from '@/assets/svg/logo-red.svg';
import { useState } from 'react';

/**
 * 登录页面
 * @returns 
 */
const LoginPage: React.FC = () => {
    const { initialState, setInitialState } = useModel('@@initialState');
    const [loading, setLoading] = useState(false);

    return (
        <div>
            <DynamicBG />
            <Form
                name="normal_login"
                className={style.loginform}
                initialValues={{ remember: true }}
                onFinish={async (values: ILogin) => {
                    setLoading(true);
                    const userinfo = await login(values);
                    if (userinfo) {
                        setInitialState({ ...initialState, currentUser: userinfo });
                    }
                    setLoading(false);
                }}
            >
                <div className={style.titlediv}>
                    <span className={style.logoword} >{websiteTitle}</span>
                    <Logo fill='#1DA57A' width={35} height={35} />
                </div>
                <Form.Item
                    name="account"
                    rules={[{ required: true, message: 'Please input your Username!' }]}
                >
                    <Input prefix={<UserOutlined className="site-form-item-icon" />}
                        placeholder="Username"
                    />
                </Form.Item>
                <Form.Item
                    name="password"
                    rules={[{ required: true, message: 'Please input your Password!' }]}
                >
                    <Input
                        prefix={<LockOutlined className="site-form-item-icon" />}
                        type="password"
                        placeholder="Password"
                    />
                </Form.Item>
                <Form.Item>
                    <Form.Item name="remember" valuePropName="checked" noStyle>
                        <Checkbox>Remember me</Checkbox>
                    </Form.Item>
                    <a className="login-form-forgot" href=""> Forgot password</a>&emsp;
                    <a href="">Register now</a>
                </Form.Item>

                <Form.Item>
                    <Button type="primary"
                        htmlType="submit"
                        className="login-form-button"
                        loading={loading}
                        style={{ width: '100%' }}
                    >
                        Log in
                    </Button>

                </Form.Item>
            </Form>
        </div>
    );
}

export default LoginPage;