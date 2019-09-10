using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UGOCPBackEnd2019.Models.ViewModels;

namespace UGOCPBackEnd2019.Helpers
{
    public class LocationHelper
    {
        private readonly cat_localidadContext _contextLocalidad;

        public LocationHelper(cat_localidadContext ctx)
        {
            _contextLocalidad = ctx;
        }
        public DatosLocalidadViewModel GetLocationDataByLocalityID(int idLocalidad)
        {
            
            DatosLocalidadViewModel Datos = new DatosLocalidadViewModel();
            var localidad = _contextLocalidad.Localidades.FirstOrDefault(l => l.Id == Convert.ToInt32(idLocalidad));
            var municipio = _contextLocalidad.Municipios.FirstOrDefault(m => m.Id == localidad.MunicipioId);
            var estado = _contextLocalidad.Estados.FirstOrDefault(e => e.Id == municipio.EstadoId);
            Datos.Estado = estado.Nombre;
            Datos.Municipio = municipio.Nombre;
            Datos.Localidad = localidad.Nombre;

            return Datos;
        }
    }
}
