using System;
using System.Collections.Generic;

namespace UGOCPBackEnd2019
{
    public partial class Estados
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Abrev { get; set; }
        public short Activo { get; set; }
    }
}
