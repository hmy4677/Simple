import copy from 'copy-to-clipboard';

/**
 * 复制到粘贴板
 * @param text 文本
 */
export const copyTo = (text: string) => {
    copy(text);
}