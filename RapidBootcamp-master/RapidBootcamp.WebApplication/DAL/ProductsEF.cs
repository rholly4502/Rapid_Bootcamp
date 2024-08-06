using Microsoft.EntityFrameworkCore;
using RapidBootcamp.WebApplication.Models;

namespace RapidBootcamp.WebApplication.DAL
{
    public class ProductsEF : IProduct
    {
        private readonly AppDbContext _appDbContext;
        public ProductsEF(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Product Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            var results = _appDbContext.Products.Include(p => p.Category);

            return results.ToList();
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProductsByName(string productName)
        {
            throw new NotImplementedException();
        }

        public Product Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
