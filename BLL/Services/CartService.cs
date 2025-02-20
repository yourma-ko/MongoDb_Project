using BLL.Interfaces;
using BLL.Utilities;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository cartRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;
        private readonly IUserRepository userRepository;

        public CartService(ICartRepository cartRepository, IOrderRepository orderRepository, IProductRepository productRepository, IUserRepository userRepository)
        {
            this.cartRepository = cartRepository;
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.userRepository = userRepository;
        }
        public async Task AddItemToCartAsync(string customerId, CartItem item)
        {
            var cart = await cartRepository.GetCartByCustomerIdAsync(customerId);
            if (cart == null)
            {
                cart = new Cart
                {
                    CustomerId = customerId,
                    CartItems = new List<CartItem> { item }
                };
                await cartRepository.AddCartAsync(cart);
            }
            else
            {
                cart.CartItems.Add(item);
                await cartRepository.UpdateCartAsync(cart);
            }
        }

        public async Task<decimal> CalculateTotal(string customerId)
        {
            var cart = await cartRepository.GetCartByCustomerIdAsync(customerId);
            return cart.CartItems.Sum(item => item.Quantity * item.Price);
        }

        public async Task<Order> CheckoutFromCartAsync(string customerId)
        {
            var cart = await cartRepository.GetCartByCustomerIdAsync(customerId);
            var checkedItems = cart.CartItems.Where(ci => ci.Checked == true).ToList();
            var order = new Order
            {
                Id = ObjectId.GenerateNewId().ToString(),
                CustomerId = customerId,
                OrderDateTime = DateTime.Now,
                Status = OrderStatus.Pending,
                OrderItems = checkedItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId.ToString(),
                    Title = item.Title,
                    Quantity = item.Quantity,
                    Price = item.Price,
                }).ToList(),
                Total = checkedItems.Sum(item => item.Quantity * item.Price)
            };
            await orderRepository.AddAsync(order);
            foreach (var item in checkedItems)
            {
                var product = await productRepository.GetByIdAsync(item.ProductId.ToString());

                if (product == null)
                {
                    throw new InvalidOperationException($"Продукт с ID {item.ProductId} не найден.");
                }

                if (product.Quantity < item.Quantity)
                {
                    throw new InvalidOperationException($"Недостаточно товара '{product.Title}' на складе. Доступно: {product.Quantity}, требуется: {item.Quantity}.");
                }

                product.Quantity -= item.Quantity;
                await productRepository.UpdateAsync(product);
            }

            var uncheckedItems = cart.CartItems.Where(ci => ci.Checked == false).ToList();
            cart.CartItems = uncheckedItems;
            cart.LastUpdated = DateTime.Now;
            await cartRepository.UpdateCartAsync(cart);
            var user = await userRepository.GetByIdAsync(customerId);
            if (user == null)
            {
                throw new InvalidOperationException($"Пользователь с ID {customerId} не найден.");
            }

            if (user.Orders == null)
            {
                user.Orders = new List<Order>();
            }

            user.Orders.Add(order);
            await userRepository.UpdateAsync(user);
            return order;
        }

        public async Task ClearCartAsync(string customerId)
        {
            var cart = await cartRepository.GetCartByCustomerIdAsync(customerId);
            cart.CartItems.Clear();
            await cartRepository.UpdateCartAsync(cart);
        }

        public async Task<Cart> getCartByCustomerIdAsync(string customerId)
        {
            var cart = await cartRepository.GetCartByCustomerIdAsync(customerId);
            if (cart == null)
            {
                cart = new Cart
                {
                    CustomerId = customerId,
                    CartItems = new List<CartItem>()
                };
            }
            return cart;
        }

        public async Task RemoveItemFromCartAsync(string customerId, CartItem item)
        {
            var cart = await cartRepository.GetCartByCustomerIdAsync(customerId);

            var itemToRemove = cart.CartItems.FirstOrDefault(ci => ci.ProductId == item.ProductId);

            if (itemToRemove != null)
            {
                cart.CartItems.Remove(itemToRemove);
                await cartRepository.UpdateCartAsync(cart);
            }
        }
        public async Task ChangeQuantityAsync(string customerId, CartItem item, int delta)
        {
            var cart = await cartRepository.GetCartByCustomerIdAsync(customerId);
            var cartItem = cart.CartItems.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (cartItem != null)
            {
                cartItem.Quantity += delta;
                if (cartItem.Quantity <= 0)
                {
                    cart.CartItems.Remove(cartItem);
                }
                cart.LastUpdated = DateTime.UtcNow;
                await cartRepository.UpdateCartAsync(cart);
            }
        }

        public async Task CheckItemAsync(string customer, string productId, bool isChecked)
        {
            var cart = await cartRepository.GetCartByCustomerIdAsync(customer);
            var item = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
            item.Checked = isChecked;
            cart.LastUpdated = DateTime.UtcNow;
            await cartRepository.UpdateCartAsync(cart);
        }
    }
}