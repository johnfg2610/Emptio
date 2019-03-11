using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Models
{
    public class BrandModel
    {
        public Guid? Id { get; set; } = null;

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
