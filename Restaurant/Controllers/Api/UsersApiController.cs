using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Data;

namespace Restaurant.MVC.Controllers.Api
{
    //[Produces("application/json")]
    [Route("api/UsersApi")]
    public class UsersApiController : ControllerBase
    {
        private ApplicationDbContext _db;

        public UsersApiController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get(string type, string query)
        {
            if (query != null && type == "email")
            {
                var userList = _db.Users.Where(u => u.Email.Contains(query)).ToList();

                return Ok(userList);
            }

            return NotFound();

        }
    }
}