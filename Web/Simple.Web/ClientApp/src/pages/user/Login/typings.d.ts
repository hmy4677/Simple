export interface ILogin{
    account:string;
    password:string;
    remember:boolean;
}
export interface ILoginResponse extends API.IResponse{
    data:API.ICurrentUser;
}