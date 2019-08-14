using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UGOCPBackEnd2019.Data;
using UGOCPBackEnd2019.Entities;
using UGOCPBackEnd2019.Extensions;
using UGOCPBackEnd2019.Models;
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