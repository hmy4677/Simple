import { withRouter, history } from 'umi';
import ProLayout from '@ant-design/pro-layout';
import moment from 'moment';
import type { MenuDataItem } from '@ant-design/pro-layout';
import {
    ThunderboltOutlined,
    BarChartOutlined
} from '@ant-design/icons';
import { RouteComponentProps } from 'react-router';
import { ReactComponent as Logo } from '@/assets/svg/logo-red.svg'


interface LayoutProp extends RouteComponentProps<any> {
    children: any;
}
export default withRouter((props: LayoutProp) => {
    //前端硬写菜单
    const navMenu: MenuDataItem[] = [
        { name: 'Misson', path: '/mission', icon: <ThunderboltOutlined /> },
        { name: 'Analysis', path: '/analysis', icon: <BarChartOutlined /> },
    ];

    //从数据库获取菜单并渲染
    // const IconMap = {
    //     settingIcon: <SettingOutlined />,
    //     orderIcon: <MoneyCollectOutlined />,
    //     terminalIcon: <RobotOutlined />,
    //     monitorIcon: <StarOutlined />,
    //     pieIcon: <PieChartOutlined />,
    // }
    // const loopMenuItem = (menus: MenuDataItem[]): MenuDataItem[] =>
    //     menus.map(({ icon, children, ...item }) => ({
    //         ...item,
    //         icon: icon && IconMap[icon as string],
    //         children: children && loopMenuItem(children),
    //     }));
    // const getNavMenuItems = async (): Promise<API.NavMenuItem[] | []> => {
    //     const navmenuResult = await getNavMenu(initialState?.currentUser?.id);
    //     if (navmenuResult.status === 200) {
    //         return navmenuResult.data;
    //     }
    //     else {
    //         console.error(navmenuResult.msg);
    //         return [];
    //     }
    // }


    return (
        <div style={{ height: '100vh', }}>
            <ProLayout title={websiteTitle}
                logo={<Logo fill='currentColor' width={35} height={35} />}
                navTheme='light'
                primaryColor='#1890ff'
                layout='mix'
                headerTheme='light'
                contentWidth='Fluid'
                fixSiderbar={true}
                menu={{
                    request: async () => {
                        return navMenu;
                    },
                    autoClose: false,
                }}
                menuItemRender={(item, dom) => (
                    <a
                        onClick={() => {
                            history.push(item.path || '/');
                        }}
                    >
                        {dom}
                    </a>
                )}
                footerRender={() => {
                    return (
                        <div style={{ textAlign: 'center' }}>
                            <span style={{ color: 'gray' }}>Copyright © {moment().format('YYYY')} {websiteTitle}</span>
                        </div>
                    );
                }}
            >
                {props.children}
            </ProLayout>
        </div >
    );
})
