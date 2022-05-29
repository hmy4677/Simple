declare namespace API {
  interface IResponse {
    statusCode: number;
    succeeded: boolean;
    timestamp: number;
    data?: any;
    errors?: string;
    extras?: any;
  }
  interface ICurrentUser {
    id?: string;
    account?: string;
    name?: string;
    avatarUrl?: string;
    token?: string;
    status?: number;
  }
}
declare const initArr: any[];
declare const dateStr: string;
declare const dateTimeStr: string;
declare const timeStr: string;
declare interface IPagination {
  total: number;
  page: number;
  pageSize: number;
}
declare const initPagination: IPagination;

declare const websiteTitle: string;