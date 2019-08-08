using System;
using System.Collections.Generic;

namespace UGOCPBackEnd2019.ModelsSatProductos
{
    public partial class F4CClaveprodserv
    {
        public int Id { get; set; }
        public string CClaveProdServ { get; set; }
        public string Descripción { get; set; }
        public string IncluirIvaTrasladado { get; set; }
        public string IncluirIepsTrasladado { get; set; }
        public string ComplementoQueDebeIncluir { get; set; }
        public string FechaInicioVigencia { get; set; }
        public string FechaFinVigencia { get; set; }
        public string PalabrasSimilares { get; set; }
    }
}
