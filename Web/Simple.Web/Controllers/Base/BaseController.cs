using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simple.Services.DbInitial.Interface;

namespace Simple.Web.Controllers.Base;

[Route("api/[controller]")]
public class BaseController : Controller
{
    private readonly IDbInitial dbInitial;

    public BaseController(IDbInitial dbInitial)
    {
        this.dbInitial = dbInitial;
    }

    [HttpGet("init_db"), AllowAnonymous]
    public void InitDB()
    {
        dbInitial.CreateDbAndTable();
    }
}