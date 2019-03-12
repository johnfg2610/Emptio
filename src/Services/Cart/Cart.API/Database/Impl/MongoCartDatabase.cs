using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cart.API.Models;
using MongoDB.Driver;
using Common;

namespace Cart.API.Database.Impl
{
    public class MongoCartDatabase : ICartDatabase
    {
        private readonly IMongoCollection<CartModel> _cartCollection;
        private readonly IdGenerator idGenerator;

        public MongoCartDatabase(IMongoCollection<CartModel> brandCollection, IdGenerator idGenerator) {
            this._cartCollection = brandCollection;
            this.idGenerator = idGenerator;
        }
        public async Task CreateAsync(CartModel cartModel)
        {
            await _cartCollection.InsertOneAsync(cartModel);
        }

        public Task DeleteAsync(Expression<Func<CartModel, bool>> predicate)
        {
            return _cartCollection.DeleteOneAsync(predicate);
        }

        public async Task<List<CartModel>> GetListAsync(Expression<Func<CartModel, bool>> predicate)
        {
            return await(await _cartCollection.FindAsync(predicate)).ToListAsync();
        }

        public async Task<CartModel> GetSingleAsync(Expression<Func<CartModel, bool>> predicate)
        {
            return await(await _cartCollection.FindAsync(predicate)).FirstOrDefaultAsync();
        }

        public Task UpdateAsync(Expression<Func<CartModel, bool>> predicate, string updateJson)
        {
            return _cartCollection.FindOneAndUpdateAsync(predicate, new JsonUpdateDefinition<CartModel>(updateJson));
        }

        public Task UpdateAsync(Expression<Func<CartModel, bool>> predicate, object updateObject)
        {
            return _cartCollection.FindOneAndUpdateAsync(predicate, new ObjectUpdateDefinition<CartModel>(updateObject));
        }
    }
}
