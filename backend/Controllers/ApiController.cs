using Microsoft.AspNetCore.Mvc;
using backend.DTO;

namespace backend.API
{
    [ApiController]
    [Route("/[controller]/login")]
    public class APIController
    {
        [HttpPost]
        public string Login([FromBody] LoginDto loginDto)
        {
            return "Login Successful!";
        }
    }
}