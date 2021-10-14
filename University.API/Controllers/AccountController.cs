using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using University.BL.DTOs;


namespace University.API.Controllers
{
    public class AccountController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            bool isValid = (loginDTO.Password.Equals("123456"));
            if (isValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(loginDTO.Username);
                return Ok(token);
            }
            return Unauthorized();
        }
    }
}