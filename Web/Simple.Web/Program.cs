using Simple.Web;

var builder = WebApplication.CreateBuilder(args).Inject();

// Add services to the container.
builder.Services.AddJwt<JwtHandler>(enableGlobalAuthorize: true);
builder.Services.AddControllersWithViews().AddInjectWithUnifyResult<RESTfulResultProvider>();
builder.Services.AddRemoteRequest();
builder.Services.AddDBConnection();

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
