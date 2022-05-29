/**发出windows通知 */
export const windowsNotice = (title:string,content:string) => {
    Notification.requestPermission().then(permission => {
        if (permission === 'granted') {
            let notification = new Notification(title, {//通知标题
                lang: 'zh',
                // icon: logo,//图标
                body: content,//通知文字内容
                silent: true,///是否静音
                renotify: false,//是否通知用户-新通知替换旧通知后
                requireInteraction: false//是否需要手动关闭
            });
            //回调事件
            //显示时
            notification.onshow = () => {
            }
            //点击时
            notification.onclick = () => {
            }
            //错误时
            notification.onerror = () => {
            }
            //关闭时
            notification.onclose = () => {
            }
        }
    });
}