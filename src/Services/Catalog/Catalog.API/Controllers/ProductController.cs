using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Database;
using Catalog.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductDatabase productDatabase;
        private readonly ILogger logger;

        public ProductController(IProductDatabase productDatabase, ILogger<ProductController> logger)
        {
            this.productDatabase = productDatabase;
            this.logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var brand = await productDatabase.GetSingleAsync(it => it.Id == id);

            if (brand == null)
                return NotFound();

            return Ok(brand);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetListAll()
        {
            var brand = await productDatabase.GetListAsync(it => true);

            if (brand == null)
                return NotFound();

            return Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProductModel productModel)
        {
            if (productModel == null)
                return BadRequest();

            var id = await productDatabase.CreateAsync(productModel);

            return Ok(id);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]JObject body)
        {
            var json = body.ToString();

            if (string.IsNullOrWhiteSpace(json))
            {
                return BadRequest();
            }

            await productDatabase.UpdateAsync(it => it.Id == id, json);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            await productDatabase.DeleteAsync(it => it.Id == id);

            return Ok();
        }
    }
}
