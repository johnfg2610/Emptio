using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Database;
using Catalog.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductDatabase productDatabase;

        public ProductController(IProductDatabase productDatabase)
        {
            this.productDatabase = productDatabase;
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var product = await productDatabase.GetSingleAsync(it => it.Id == id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("/{id}/list")]
        public async Task<IActionResult> GetList([FromRoute]Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var product = await productDatabase.GetListAsync(it => it.Id == id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProductModel productModel)
        {
            if (productModel == null)
                return BadRequest();

            var id = await productDatabase.CreateAsync(productModel);

            return Ok(id);
        }

        [HttpPatch("/{id}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]ProductModel productModel)
        {
            if (productModel == null)
                return BadRequest();

            await productDatabase.UpdateAsync(it => it.Id == id, productModel);

            return Ok();
        }
        
        [HttpDelete("/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            await productDatabase.DeleteAsync(it => it.Id == id);

            return Ok();
        }

    }
}
