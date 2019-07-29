using System;
using System.Collections.Generic;

namespace UGOCPBackEnd2019
{
    public partial class Localidades
    {
        public int Id { get; set; }
        public int MunicipioId { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Altitud { get; set; }
        public string Carta { get; set; }
        public string Ambito { get; set; }
        public int Poblacion { get; set; }
        public int Masculino { get; set; }
        public int Femenino { get; set; }
        public int Viviendas { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public short Activo { get; set; }
    }
}
