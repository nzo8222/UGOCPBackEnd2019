using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UGOCPBackEnd2019.Data;
using UGOCPBackEnd2019.Extensions;


namespace UGOCPBackEnd2019.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        public ProductsController()
        {

        }

        [HttpGet("{filter}")]
        public IActionResult GetProductosByFilter([FromRoute] string filter)
        {
            var claveProdServ = new ClaveProdServ();
            var lstClaves = claveProdServ.GetList();

            var productos = lstClaves
                .Where(p => p.Descripcion.Contains(filter))
                .Take(15)
                .ToArray();

            return this.OkResponse(productos);

        }
    }
}
