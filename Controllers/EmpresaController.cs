using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UGOCPBackEnd2019.Data;
using UGOCPBackEnd2019.Entities;
using UGOCPBackEnd2019.Extensions;
using UGOCPBackEnd2019.Models.ViewModels;

namespace UGOCPBackEnd2019.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly UgocpDbContext _contextUGOCP;
        private readonly cat_localidadContext _contextLocalidad;
        public EmpresaController(UgocpDbContext ctx, cat_localidadContext ctxLocalidad)
        {
            _contextUGOCP = ctx;
            _contextLocalidad = ctxLocalidad;
        }
        [HttpPost]
        [Route("DeleteEmpresa")]
        public IActionResult DeleteEmpresa([FromBody] DeleteEmpresaViewModel empresaModel)
        {
            try
            {
                var user = _contextUGOCP.Users
                           .Include(u => u.LstCompany)
                           .Where(u => u.Id == empresaModel.IdUsuario).FirstOrDefault();
                if (user == null)
                {
                    return this.BadResponse("No se encontro al usuario.");
                }
                var empresa = user.LstCompany.FirstOrDefault(lc => lc.IdCompany == empresaModel.IdEmpresa);
                _contextUGOCP.Remove(empresa);
                _contextUGOCP.SaveChanges();
                return this.OkResponse("Se borro la empresa correctamente.");
            }
            catch(Exception ex)
            {
                return this.BadResponse(ex.ToString());
            }
        }
        [HttpPost]
        [Route("UpdateEmpresa")]
        public IActionResult UpdateEmpresa([FromBody] UpdateEmpresaViewModel empresaModel)
        {
            try
            {
                var user = _contextUGOCP.Users
                           .Include(u => u.LstCompany)
                           .Where(u => u.Id == empresaModel.IdUsuario).FirstOrDefault();
                if (user == null)
                {
                    return this.BadResponse("No se encontro al usuario.");
                }
                var empresa = user.LstCompany.FirstOrDefault(lc => lc.IdCompany == empresaModel.IdEmpresa);
                empresa.Name = empresaModel.Name;
                empresa.PhoneNumber = empresaModel.PhoneNumber;
                empresa.Address = empresaModel.Address;
                empresa.IdLocalidad = empresaModel.IdLocalidad;
                var localidad = _contextLocalidad.Localidades.FirstOrDefault(l => l.Id == empresaModel.IdLocalidad);
                empresa.localidad = localidad.Nombre;
                _contextUGOCP.Update(empresa);
                _contextUGOCP.SaveChanges();
                return this.OkResponse("Cambios Guardados correctamente.");

            }
            catch(Exception ex)
            {
                return this.BadResponse(ex.ToString());
            }
        }
        [HttpPost]
        [Route("PostEmpresa")]
        public IActionResult PostEmpresa([FromBody] EmpresaViewModel empresaModel)
        {
            try
            {
                
                var user = _contextUGOCP.Users
                           .Include(u => u.LstCompany)
                           .Where(u => u.Id == empresaModel.IdUsuario).FirstOrDefault();

                if (user == null)
                {
                    return this.BadResponse("No se encontro al usuario.");
                }
                var companies = user.LstCompany.ToList();
                foreach(var comp in companies)
                {
                    if(empresaModel.Name == comp.Name)
                    {
                        return this.BadResponse("Ya existe una compañia con ese nombre");
                    }
                }
                Company company = new Company();
                company.IdCompany = Guid.NewGuid();
                company.Name = empresaModel.Name;
                company.PhoneNumber = empresaModel.PhoneNumber;
                company.Address = empresaModel.Address;
                company.IdLocalidad = empresaModel.IdLocalidad;
                var localidad = _contextLocalidad.Localidades.FirstOrDefault(l => l.Id == empresaModel.IdLocalidad);
                company.localidad = localidad.Nombre;
                if (user.LstCompany == null)
                {
                    user.LstCompany = new List<Company>();
                }
                
                user.LstCompany.Add(company);
                _contextUGOCP.SaveChanges();
                return this.OkResponse("Se guardo la lista correctamente.");
            }
            catch(Exception ex)
            {
                return this.BadRequest(ex.ToString());
            }
            
        }

        [HttpGet("{IdUsuario}")]
        public IActionResult GetEmpresas([FromRoute] Guid IdUsuario)
       {
            try
            {
                var user = _contextUGOCP.Users
                          .Include(u => u.LstCompany)
                          .Where(u => u.Id == IdUsuario).FirstOrDefault(); ;

                if (user == null)
                {
                    return this.BadResponse("No se encontro al usuario.");
                }

                if(user.LstCompany.Count() == 0)
                {
                    return this.BadResponse("Este usuario no tiene empresas registradas.");
                }
                List<EmpresaProductCountViewModel> listaEmpresasConProductos = new List<EmpresaProductCountViewModel>();
                EmpresaProductCountViewModel empresaConProductos = new EmpresaProductCountViewModel();
                var listaCompany = user.LstCompany.ToList();

                foreach(var company in listaCompany)
                {
                    empresaConProductos.Address = company.Address;
                    empresaConProductos.IdCompany = company.IdCompany;
                    empresaConProductos.IdLocalidad = company.IdLocalidad;
                    empresaConProductos.localidad = company.localidad;
                    empresaConProductos.Name = company.Name;
                    empresaConProductos.PhoneNumber = company.PhoneNumber;
                    if(company.LstProduct == null)
                    {
                        empresaConProductos.ProductCount = 0;
                    }
                    else
                    {
                        empresaConProductos.ProductCount = company.LstProduct.Count();
                    }
                    
                    listaEmpresasConProductos.Add(empresaConProductos);
                }
                return this.OkResponse(listaEmpresasConProductos);
            }
            catch(Exception ex)
            {
                return this.BadRequest(ex.ToString());
            }
        }


    }
}