using Simple.Application.Extension.Model;
using System.ComponentModel;

namespace Simple.Application.Extension;

public static class Extension
{
    /// <summary>
    /// 获取两个对象同名属性的差异值列表
    /// </summary>
    /// <param name="source">原对象</param>
    /// <param name="target">目标对象</param>
    /// <returns>差异列表</returns>
    public static List<ObjectDifference> Diffrence(this object source, object target)
    {
        var diff = new List<ObjectDifference>();
        if (source == null || target == null)
        {
            return diff;
        }
        var srcProps = source.GetType().GetProperties();
        var tarProps = target.GetType().GetProperties();

        diff = srcProps.Join(tarProps, s => s.Name, t => t.Name,
            (s, t) => new ObjectDifference
            {
                Key = s.Name,
                SrcValue = s.GetValue(source, null).ToString(),
                TarValue = t.GetValue(target, null).ToString()
            }).Where(p => p.SrcValue != p.TarValue).ToList();

        return diff;
    }

    /// <summary>
    /// 获取枚举描述
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="tField"></param>
    /// <returns></returns>
    public static string GetDescription<T>(this T tField) where T : System.Enum
    {
        Type enumType = typeof(T);
        var name = System.Enum.GetName(enumType, tField);
        if (name == null)
            return string.Empty;
        object[] objs = enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (objs == null || objs.Length == 0)
            return string.Empty;
        DescriptionAttribute attr = objs[0] as DescriptionAttribute;
        return attr.Description;
    }
}