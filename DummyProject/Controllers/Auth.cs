using DummyProject.BAL.IAuthServices;
using DummyProject.BAL.AuthServices;
using DummyProjectModel.Enitity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;

namespace DummyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {

        private readonly IAuthService _authService;
        public Auth(IAuthService authService)

        {

            _authService = authService ?? throw new ArgumentNullException(nameof(authService));

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _authService.GetAllUsers();
            return Ok();

        }


        [HttpPost]
        [Route("UserRegistration")]
        public async Task <IActionResult> UserRegistration(Registration registration)
        {
            ResponseModel resp = new ResponseModel();
            if (ModelState.IsValid) {
                try
                {
                    var result = _authService.UserRegistration(registration); 
                    if(result != null)
                    {
                        resp.IsSuccess = true;
                        resp.Message = "Sussessfully added.";
                        return Ok(resp);
                    }
                    else
                    {
                        resp.IsSuccess = false;
                        resp.Message = "Unable to added user.";
                        return BadRequest(resp);
                    }
                    
                }
                catch (Exception ex)
                {

                    resp.IsSuccess = false;
                    resp.Message = ex.Message;
                    return StatusCode(500, resp);
                }
               
            }
            return Ok(resp);
        }


        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Loging(string email, string password)
        {
            ResponseModel resp = new ResponseModel();
            try
            {
                var user = await _authService.UserLogin(email, password);
                if (user != null)
                {
                    resp.IsSuccess = true;
                    resp.Message = "Succesfully Login.";
                    return Ok(user);
                }
                else
                {
                    resp.IsSuccess = false;
                    resp.Message = "failed to login.";
                    return BadRequest(resp);
                }
            }
            catch (Exception ex)
            {
                resp.IsSuccess = false;
                resp.Message = ex.Message;
                return StatusCode(500, resp);
            }
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUser()
        {
            var user = await _authService.GetAllUsers();
            if (user != null || user.Any())
                return NotFound(new { Message = "No users found" });
            return Ok(user);
        }
        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid userId)
        {
            var result =await _authService.GetUserById(userId);
            return Ok(result);
        }
    }
}
