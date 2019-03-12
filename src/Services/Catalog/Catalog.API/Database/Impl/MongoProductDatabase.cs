using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Catalog.API.Models;
using Common;
using MongoDB.Driver;

namespace Catalog.API.Database.Impl
{
    public class MongoProductDatabase : IProductDatabase
    {
        private readonly IMongoCollection<ProductModel> _productCollection;
        private readonly IdGenerator idGenerator;

        public MongoProductDatabase(IMongoCollection<ProductModel> productCollection, IdGenerator idGenerator)
        {
            _productCollection = productCollection;
            this.idGenerator = idGenerator;
        }

        public async Task<ProductModel> GetSingleAsync(Expression<Func<ProductModel, bool>> predicate)
        {
            return await(await _productCollection.FindAsync(predicate)).FirstOrDefaultAsync();
        }

        public async Task<List<ProductModel>> GetListAsync(Expression<Func<ProductModel, bool>> predicate)
        {
            return await (await _productCollection.FindAsync(predicate)).ToListAsync();
        }

        public async Task<Guid> CreateAsync(ProductModel productModel)
        {
            productModel.Id = idGenerator.GenerateID();
            await _productCollection.InsertOneAsync(productModel);
            return productModel.Id;
        }

        public Task UpdateAsync(Expression<Func<ProductModel, bool>> predicate, string updateJson)
        {
            return _productCollection.FindOneAndUpdateAsync(predicate, new JsonUpdateDefinition<ProductModel>(updateJson));
        }

        public Task UpdateAsync(Expression<Func<ProductModel, bool>> predicate, object updateObject)
        {
            return _productCollection.FindOneAndUpdateAsync(predicate, new ObjectUpdateDefinition<ProductModel>(updateObject));
        }

        public Task DeleteAsync(Expression<Func<ProductModel, bool>> predicate)
        {
            return _productCollection.DeleteOneAsync(predicate);
        }
    }
}
