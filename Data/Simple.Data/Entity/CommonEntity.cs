using SqlSugar;

namespace Simple.Data.Entity;

public class CommonEntity
{
    [SugarColumn(IsPrimaryKey = true)]
    public long Id { get; set; }

    public DateTime CreateTime { get; set; }

    [SugarColumn(IsNullable = true)]
    public DateTime? UpdateTime { get; set; }

    public int Status { get; set; }
}