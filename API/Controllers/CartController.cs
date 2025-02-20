using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCartByCustomerId(string customerId)
        {
            var cart = await cartService.getCartByCustomerIdAsync(customerId);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPost("{customerId}/items")]
        public async Task<IActionResult> AddItemToCart(string customerId, [FromBody] CartItem item)
        {
            await cartService.AddItemToCartAsync(customerId, item);
            return NoContent();
        }

        [HttpDelete("{customerId}/items")]
        public async Task<IActionResult> RemoveItemFromCart(string customerId, [FromBody] CartItem item)
        {
            await cartService.RemoveItemFromCartAsync(customerId, item);
            return NoContent();
        }

        [HttpPost("{customerId}/checkout")]
        public async Task<ActionResult<Order>> CheckoutFromCart(string customerId)
        {
            var order = await cartService.CheckoutFromCartAsync(customerId);
            return NoContent();
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> ClearCart(string customerId)
        {
            await cartService.ClearCartAsync(customerId);
            return NoContent();
        }

        [HttpPost("{customerId}/items/{productId}/check")]
        public async Task<IActionResult> CheckItem(string customerId, string productId, [FromBody] bool isChecked)
        {
            await cartService.CheckItemAsync(customerId, productId, isChecked);
            return NoContent();
        }

        [HttpPost("{customerId}/items/{productId}/quantity")]
        public async Task<IActionResult> ChangeQuantity(string customerId, string productId, [FromBody] int delta)
        {
            var item = new CartItem { ProductId = productId };
            await cartService.ChangeQuantityAsync(customerId, item, delta);
            return NoContent();
        }

        [HttpGet("{customerId}/total")]
        public async Task<IActionResult> CalculateTotal(string customerId)
        {
            var total = await cartService.CalculateTotal(customerId);
            return Ok(total);
        }
    }
}
