using System;
namespace Simple.Core.SqlSugar
{
  public class DataPagination<T>
  {
    public Pagination Pagination { get; set; }
    public List<T> List { get; set; }
  }

  public class Pagination
  {
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
  }
}

