using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UGOCPBackEnd2019.Data;
using UGOCPBackEnd2019.Extensions;
using UGOCPBackEnd2019.Models.ViewModels;
using UGOCPBackEnd2019.Entities;

namespace UGOCPBackEnd2019.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly UgocpDbContext _contextUGOCP;
        public ProductsController(UgocpDbContext ctx)
        {
            _contextUGOCP = ctx;
        }
        [HttpPost]
        [Route("PostProduct")]
        public IActionResult PostProducto([FromBody] PostProductoViewModel model)
        {
            try
            {
                var user = _contextUGOCP.Users
                            .Include(u => u.LstCompany)
                            .Where(u => u.Id == model.IdUsuario).FirstOrDefault();
                if (user == null)
                {
                    return this.BadResponse("No se encontro al usuario.");
                }
                var empresa = user.LstCompany.FirstOrDefault(lc => lc.IdCompany == model.IdEmpresa);
                Product product = new Product();
                product.IdProduct = Guid.NewGuid();
                product.Name = model.Name;
                product.ClaveProductoServicio = model.ClaveProductoServicio;
                product.Calidad = model.Calidad;
                product.CuantityInKG = model.CuantityInKG;
                product.StartOfHarvest = model.StartOfHarvest;
                product.EndOfHarvest = model.EndOfHarvest;
                if(empresa.LstProduct == null)
                {
                    empresa.LstProduct = new List<Product>();
                }
                empresa.LstProduct.Add(product);
                _contextUGOCP.SaveChanges();
                return this.OkResponse("Se agrego el producto correctamente.");
            }
            catch(Exception ex)
            {
               return this.BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("GetProducts")]
        public IActionResult GetProductosByCompany([FromBody] UpdateEmpresaViewModel model)
        {
            try
            {
                var user = _contextUGOCP.Users
                           .Include(u => u.LstCompany)
                           .ThenInclude(p => p.LstProduct)
                           .Where(u => u.Id == model.IdUsuario).FirstOrDefault();
                
                if (user == null)
                {
                    return this.BadResponse("No se encontro al usuario.");
                }

                var empresa = user.LstCompany.FirstOrDefault(lc => lc.IdCompany == model.IdEmpresa).LstProduct;
                // user.LstCompany.FirstOrDefault(lc => lc.IdCompany == model.IdEmpresa);
                var lstProductos = empresa;
                return this.OkResponse(lstProductos);
            }
            catch(Exception ex)
            {
                return this.BadRequest(ex.ToString());
            }
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
