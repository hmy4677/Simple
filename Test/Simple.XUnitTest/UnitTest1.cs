using Microsoft.AspNetCore.Mvc.Testing;
using Simple.Data.Entity;
using Newtonsoft.Json;

namespace Simple.XUnitTest;

public class UnitTest1 : IClassFixture<WebApplicationFactory<Web.FakeStartup>>
{
    private readonly WebApplicationFactory<Web.FakeStartup> _factory;
    public UnitTest1(WebApplicationFactory<Web.FakeStartup> factory)
    {
        _factory = factory;
    }
    [Theory]
    [InlineData("api/system/User/323473024460037")]
    public async Task TestGetUserInfo(string url)
    {
        using var client = _factory.CreateClient();
        using var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var resultStr = await response.Content.ReadAsStringAsync();
        var resultData =JsonConvert.DeserializeObject<WebTestResult<UserEntity>>(resultStr);
        Assert.Equal(200, resultData?.statusCode);
        Assert.True(resultData?.succeeded);
        Assert.NotNull(resultData?.data);
    }
}
