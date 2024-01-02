using CRUDWebAPI.Data;
using CRUDWebAPI.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CRUDWebAPI.Repositories {
    public class ProductService : IProductService
    {
        private readonly DbContextClass _dbContext;

        public ProductService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddProductAsync(Product product)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@ProductId", product.ProductId));
            parameter.Add(new SqlParameter("@ProductName", product.ProductName));
            parameter.Add(new SqlParameter("@ProductDescription", product.ProductDescription));
            parameter.Add(new SqlParameter("@ProductPrice", product.ProductPrice));
            parameter.Add(new SqlParameter("@ProductStock", product.ProductStock));

            var result = await Task.Run(() =>  _dbContext.Database
           .ExecuteSqlRawAsync(@"exec AddNewProduct @ProductId, @ProductName, @ProductDescription, @ProductPrice, @ProductStock", parameter.ToArray()));

            return result;
        }

        public async Task<int> DeleteProductAsync(int ProductId)
        {
            return await Task.Run(() => _dbContext.Database.ExecuteSqlInterpolatedAsync($"DeletePrductByID {ProductId}"));
        }

        public async Task<IEnumerable<Product>> GetProductByIdAsync(int ProductId)
        {
            var param = new SqlParameter("@ProductId", ProductId);

            var productDetails = await Task.Run(() => _dbContext.Products
                            .FromSqlRaw(@"exec GetPrductByID @ProductId", param).ToListAsync());

            return productDetails;
        }

        public async Task<List<Product>> GetProductListAsync()
        {
            return await _dbContext.Products
                .FromSqlRaw<Product>("GetPrductList")
                .ToListAsync();
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@ProductId", product.ProductId));
            parameter.Add(new SqlParameter("@ProductName", product.ProductName));
            parameter.Add(new SqlParameter("@ProductDescription", product.ProductDescription));
            parameter.Add(new SqlParameter("@ProductPrice", product.ProductPrice));
            parameter.Add(new SqlParameter("@ProductStock", product.ProductStock));

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec UpdateProduct @ProductId, @ProductName, @ProductDescription, @ProductPrice, @ProductStock", parameter.ToArray()));
            return result;
        }
    }
}


