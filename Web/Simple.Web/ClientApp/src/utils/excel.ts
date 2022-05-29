import XLSX from 'xlsx';
import moment from 'moment';

/**
 * 导出excel
 * @param thead 表头
 * @param data 数据
 * @returns 消息
 */
export function exportExcel(thead: string[], data: any[]) {
    if (data?.length === 0) {
        return '未获得导出数据,请先查询';
    }
    try {
        let dataList = [];
        dataList.push(thead);
        data?.map(item => {
            dataList.push(Object.values(item));
            return;
        });
        const sheet = XLSX.utils.aoa_to_sheet(dataList);
        const book = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(book, sheet);
        XLSX.writeFile(book, `${moment().format('YYYY-MM-DD')}数据导出.xlsx`);
        return '正在导出数据,请查看文件保存!';
    }
    catch (error) {
        console.error(error);
        return '数据导出失败!';
    }
}

/**读取excel */
export function readExcel(data: any): any[] {
    try {
        const workBook = XLSX.read(data, { type: 'binary' });
        const sheetNames = workBook.SheetNames;
        return XLSX.utils.sheet_to_json(workBook.Sheets[sheetNames[0]], { header: 1 });
    }
    catch (error) {
        return [{
            message: error
        }]
    }

}