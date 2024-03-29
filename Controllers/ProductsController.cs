﻿using Microsoft.AspNetCore.Mvc;
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
        [Route("DeleteProduct")]
        public IActionResult DeleteProducto([FromBody] DeleteProductoViewModel model)
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

                var lstProductos = user.LstCompany.FirstOrDefault(lc => lc.IdCompany == model.IdEmpresa).LstProduct;
                var producto = lstProductos.FirstOrDefault(p => p.IdProduct == model.IdProducto);
                _contextUGOCP.Remove(producto);
                _contextUGOCP.SaveChanges();
                return this.OkResponse("Se elimino el producto correctamente.");
            }
            catch(Exception ex)
            {
                return this.BadResponse(ex.ToString());
            }
        }




        [HttpPost]
        [Route("UpdateProduct")]
        public IActionResult UpdateProducto([FromBody] UpdateProductoViewModel model)
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

                var lstProductos = user.LstCompany.FirstOrDefault(lc => lc.IdCompany == model.IdEmpresa).LstProduct;
                var producto = lstProductos.FirstOrDefault(p => p.IdProduct == model.IdProducto);
                if(producto == null)
                {
                    return this.BadResponse("No se encontro el producto.");
                }
                producto.Name = model.Name;
                //producto.StartOfHarvest = model.StartOfHarvest;
                //producto.EndOfHarvest = model.EndOfHarvest;
                producto.Calidad = model.Calidad;
                producto.ClaveProductoServicio = model.ClaveProductoServicio.ToString();
                producto.CuantityInKG = model.CuantityInKG;
                _contextUGOCP.Update(producto);
                _contextUGOCP.SaveChanges();

                return this.OkResponse("Se guardaron los cambios correctamente.");
            }
            catch(Exception ex)
            {
                return this.BadResponse(ex.ToString());
            }
        }


        [HttpPost]
        [Route("PostProduct")]
        public IActionResult PostProducto([FromBody] PostProductoViewModel model)
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
                var empresa = user.LstCompany.FirstOrDefault(lc => lc.IdCompany == model.IdEmpresa);
                foreach (var prod in empresa.LstProduct)
                {
                    if (model.Name == prod.Name)
                    {
                        return this.BadResponse("Ya existe un producto con ese nombre.");
                    }
                }
                Product product = new Product();
                product.IdProduct = Guid.NewGuid();
                product.Name = model.Name;
                product.ClaveProductoServicio = model.ClaveProductoServicio.ToString();
                product.Calidad = model.Calidad;
                product.CuantityInKG = model.CuantityInKG;
                product.DescripcionProductoServicio = model.DescripcionProductoServicio;
                if(product.LstMonthsOfHarvest == null)
                {
                    product.LstMonthsOfHarvest = new List<Month>();
                }

                foreach(var mes in model.MonthsOfHarvest)
                {
                    Month month = new Month();
                    month.Id = Guid.NewGuid();
                    //month.Id = product.LstMonthsOfHarvest.Count();
                    month.Name = mes.ToString();
                    product.LstMonthsOfHarvest.Add(month);
                }
                
                //product.StartOfHarvest = model.StartOfHarvest;
                //product.EndOfHarvest = model.EndOfHarvest;
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
               return this.BadResponse(ex.ToString());
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
                           .ThenInclude(p => p.LstMonthsOfHarvest)
                           .Where(u => u.Id == model.IdUsuario).FirstOrDefault();
                
                if (user == null)
                {
                    return this.BadResponse("No se encontro al usuario.");
                }

                var lstProductos = user.LstCompany.FirstOrDefault(lc => lc.IdCompany == model.IdEmpresa).LstProduct;
                if (lstProductos.Count() == 0)
                {
                    return this.BadResponse("Esta empresa no tiene productos registrados.");
                }
                
                return this.OkResponse(lstProductos);
            }
            catch(Exception ex)
            {
                return this.BadResponse(ex.ToString());
            }
        }

        [HttpGet("{filter}")]
        [Route("filter/{filter}")]
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

        [HttpGet("{claveSat}")]
        [Route("GetCategoriaSat/{claveSat}")]
        public IActionResult GetClaveSat([FromRoute] string claveSat)
        {
            var claveProdServ = new ClaveProdServ();
            var lstClaves = claveProdServ.GetList();

            var productos = lstClaves
                .FirstOrDefault(p => p.Codigo == claveSat);
                //.Where(p => p.Descripcion.Contains(claveSat))
                //.Take(15)
                //.ToArray();

            return this.OkResponse(productos);

        }
    }
}
