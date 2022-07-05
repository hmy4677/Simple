namespace Simple.Core.Redis;

public interface ISimpleRedis
{
    Task<int> SetArray(string key, string[] array);

    Task<string?[]?> GetArray(string key, long startIndex, long endIndex);
}