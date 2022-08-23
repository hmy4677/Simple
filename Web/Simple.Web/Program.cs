using Furion.UnifyResult;
using Simple.Application.Payment.Option;
using Simple.Core.Redis;
using Simple.Core.Sugar;
using Simple.Web;
using Yitter.IdGenerator;

var builder = WebApplication.CreateBuilder(args).Inject();

// Add services to the container.
builder.Services.AddJwt<JwtHandler>(enableGlobalAuthorize: true);//JWT
builder.Services.AddControllersWithViews().AddInjectWithUnifyResult<RESTfulResultProvider>();//统一REST
builder.Services.AddRemoteRequest();//远程请求
builder.Services.AddDBConnection();//连数据库
builder.Services.AddRedisConnection();//连Redis
builder.Services.AddConfigurableOptions<WechatPayOptions>();//微信支付options
builder.Services.AddConfigurableOptions<AliPayOptions>();//支付宝options
var app = builder.Build();

app.UseInject("swagger");//http://localhost:5000/swagger
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

YitIdHelper.SetIdGenerator(new IdGeneratorOptions { WorkerId = 20 });

app.Run();