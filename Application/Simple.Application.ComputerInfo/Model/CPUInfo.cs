namespace Simple.Application.ComputerInfo.Model;

/// <summary>
/// cpu信息
/// </summary>
public class CPUInfo
{
    /// <summary>
    /// cpu名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 核心数
    /// </summary>
    public string Cores { get; set; }

    /// <summary>
    /// 逻辑核心数
    /// </summary>
    public string LogicCores { get; set; }

    /// <summary>
    /// 占用率
    /// </summary>
    public string UsedRate { get; set; }
}