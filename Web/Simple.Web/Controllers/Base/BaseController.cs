using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using Simple.Services.DbInitial.Interface;

namespace Simple.Web.Controllers.Base
{
  [Route("api/[controller]")]
  public class BaseController : Controller
  {
    //private readonly IDbInitial dbInitial;

    //public BaseController(IDbInitial dbInitial)
    //{
    //  this.dbInitial = dbInitial;
    //}


    //// GET api/values/5
    //[HttpGet,AllowAnonymous]
    //public void InitDB()
    //{
    //  dbInitial.CreateDbAndTable();
    //}

    // POST api/values
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}

