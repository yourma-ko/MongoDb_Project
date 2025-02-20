using BLL.Interfaces;
using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IOrderService orderService;
        public UserController(IUserService userService, IOrderService orderService)
        {
            this.userService = userService;
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers([FromServices] IUserService userService)
        {
            var users = await userService.GetAllAsync();
            return Ok(users);
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUser([FromServices] IUserService userService, User user)
        {
            await userService.RegisterAsync(user);
            return Ok(user);
        }
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromServices] IUserService userService, string email, string password)
        {
            var loggedInUser = await userService.LoginAsync(email, password);
            return Ok(loggedInUser);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Order>>> GetUserOrders([FromServices] IUserService userService, string id)
        {
            var orders = await userService.GetByIdAsync(id);
            return Ok(orders);
        }
    }
}
