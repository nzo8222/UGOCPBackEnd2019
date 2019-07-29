using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UGOCPBackEnd2019.Extensions;
using UGOCPBackEnd2019.Models;

namespace UGOCPBackEnd2019.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LocalidadController : ControllerBase
    {
        private readonly cat_localidadContext _contextLocalidad;

        public LocalidadController(cat_localidadContext ctx)
        {
            _contextLocalidad = ctx;
        }

        [HttpGet]
        [Route("GetEstados")]
        public async Task<IActionResult> GetEstados()
        {
            try
            {
                var estados = _contextLocalidad.Estados.ToList();
                return this.OkResponse(estados);
            }
            catch(Exception ex)
            {
                return this.BadResponse(ex.ToString());
            }
        }
        [HttpPost]
        [Route("GetMunicipios")]
        public async Task<IActionResult> GetMunicipios([FromBody]MunicipioViewModel idEstadoFromFachada)
        {
            try
            {
                var municipios =  _contextLocalidad.Municipios.Where(m => m.EstadoId == idEstadoFromFachada.EstadoId);
                return this.OkResponse(municipios);
            }
            catch(Exception ex)
            {
                return this.BadResponse(ex.ToString());
            }
        }

    }
}