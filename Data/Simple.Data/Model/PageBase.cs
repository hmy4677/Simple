namespace Simple.Data.Model;

/// <summary>
/// 分页
/// </summary>
public class PageBase
{
    /// <summary>
    /// 页码
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// 每页行数
    /// </summary>
    public int PageSize { get; set; } = 20;
}