using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UGOCPBackEnd2019.Extensions;
using UGOCPBackEnd2019.Models;
using UGOCPBackEnd2019.Models.ViewModels;

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
        [HttpGet("{IdLocalidad}")]
        [Route("GetLocationData/{IdLocalidad}")]
        public IActionResult GetDataLocacion([FromRoute] int IdLocalidad)
        {
            try
            {
                DatosLocalidadViewModel Datos = new DatosLocalidadViewModel();
                var localidad = _contextLocalidad.Localidades.FirstOrDefault(l => l.Id == Convert.ToInt32(IdLocalidad));
                var municipio = _contextLocalidad.Municipios.FirstOrDefault(m => m.Id == localidad.MunicipioId);
                var estado = _contextLocalidad.Estados.FirstOrDefault(e => e.Id == municipio.EstadoId);
                Datos.Estado = estado.Nombre;
                Datos.Municipio = municipio.Nombre;
                Datos.Localidad = localidad.Nombre;
                return this.OkResponse(Datos);
            }
            catch(Exception ex)
            {
                return this.BadResponse(ex.ToString());
            }
            
        }

        [HttpGet]
        [Route("GetEstados")]
        public IActionResult GetEstados()
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
        public IActionResult GetMunicipios([FromBody]MunicipioViewModel idEstadoFromFachada)
        {
            try
            {
                var municipios =  _contextLocalidad.Municipios.Where(m => m.EstadoId == idEstadoFromFachada.Id);
                return this.OkResponse(municipios);
            }
            catch(Exception ex)
            {
                return this.BadResponse(ex.ToString());
            }
        }
        [HttpPost]
        [Route("GetLocalidades")]
        public IActionResult GetLocalidad([FromBody]LocalidadViewModel idMunicipioFromFachada)
        {
            try
            {
                var localidades = _contextLocalidad.Localidades.Where(l => l.MunicipioId == idMunicipioFromFachada.Id);
                return this.OkResponse(localidades);
            }
            catch(Exception ex)
            {
                return this.BadResponse(ex.ToString());
            }
        }

    }
}