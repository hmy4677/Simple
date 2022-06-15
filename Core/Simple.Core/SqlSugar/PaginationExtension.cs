using System;
using SqlSugar;

namespace Simple.Core.SqlSugar
{
  public static class PaginationExtension
  {
    /// <summary>
    /// 分页器
    /// </summary>
    /// <typeparam name="T">输出类型</typeparam>
    /// <param name="data">需要分页数据</param>
    /// <param name="page">页码</param>
    /// <param name="pageSize">每页条数</param>
    /// <returns>分页结果</returns>
    public static async Task<DataPagination<T>> ToPaginationAsync<T>(this ISugarQueryable<T> data, int page, int pageSize)
    {
      RefAsync<int> total = 0;
      var list = await data.ToPageListAsync(page, pageSize, total);
      var result = new DataPagination<T>
      {
        List = list,
        Pagination = new Pagination
        {
          Page = page,
          PageSize = pageSize,
          Total = total
        }
      };
      return result;
    }
  }
}

