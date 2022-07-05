using SqlSugar;

namespace Simple.Data.Entity
{
    /// <summary>
    /// 系统-用户表
    /// </summary>
    [SugarTable("sys_users", IsDisabledUpdateAll = true)]
    public class UserEntity : CommonEntity
    {
        public string Account { get; set; }
        public string Name { get; set; }

        [SugarColumn(ColumnDataType = "varchar(128)")]
        public string Password { get; set; }

        [SugarColumn(IsNullable = true)]
        public string AvatarUrl { get; set; }
    }
}