using System;
using System.Collections.Generic;

namespace UGOCPBackEnd2019
{
    public partial class Municipios
    {
        public int Id { get; set; }
        public int EstadoId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public short Activo { get; set; }
    }
}
