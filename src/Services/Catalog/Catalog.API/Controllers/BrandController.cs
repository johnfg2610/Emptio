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
    public class BrandController : Controller
    {
        private readonly IBrandDatabase brandDatabase;

        public BrandController(IBrandDatabase brandDatabase)
        {
            this.brandDatabase = brandDatabase;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var brand = await brandDatabase.GetSingleAsync(it => it.Id == id);

            if (brand == null)
                return NotFound();

            return Ok(brand);
        }

        [HttpGet("{id}/list")]
        public async Task<IActionResult> GetList([FromRoute]Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var brand = await brandDatabase.GetListAsync(it => it.Id == id);

            if (brand == null)
                return NotFound();

            return Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BrandModel brandModel)
        {
            if (brandModel == null)
                return BadRequest();

            var id = await brandDatabase.CreateAsync(brandModel);

            return Ok(id);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]BrandModel brandModel)
        {
            if (brandModel == null)
                return BadRequest();

            await brandDatabase.UpdateAsync(id, brandModel);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            await brandDatabase.DeleteAsync(it => it.Id == id);

            return Ok();
        }
    }
}
