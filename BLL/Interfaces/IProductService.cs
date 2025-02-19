
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProductService : ICrud<Product>
    {
        public Task InstantCheckout(string customerId, Product product, int quantity);
        public Task<List<Product>> GetByCategoryAsync(string category);
    }
}
