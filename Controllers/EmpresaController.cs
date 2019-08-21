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
        public EmpresaController(UgocpDbContext ctx)
        {
            _contextUGOCP = ctx;
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
                Company company = new Company();
                company.IdCompany = Guid.NewGuid();
                company.Name = empresaModel.Name;
                company.PhoneNumber = empresaModel.PhoneNumber;
                company.Address = empresaModel.Address;
                company.IdLocalidad = empresaModel.IdLocalidad;
                if(user.LstCompany == null)
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
           
            var user = _contextUGOCP.Users
                           .Include(u => u.LstCompany)
                           .Where(u => u.Id == IdUsuario).FirstOrDefault(); ;

            if (user == null)
            {
                return this.BadResponse("No se encontro al usuario.");
            }


            return this.OkResponse(user.LstCompany.ToList());
        }
    }
}