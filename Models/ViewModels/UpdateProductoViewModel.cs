﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UGOCPBackEnd2019.Models.ViewModels
{
    public class UpdateProductoViewModel
    {
        public Guid IdUsuario { get; set; }
        public Guid IdEmpresa { get; set; }
        public Guid IdProducto { get; set; }
        public int ClaveProductoServicio { get; set; }
        public string Name { get; set; }
        public string Calidad { get; set; }
        //public DateTime StartOfHarvest { get; set; }
        //public DateTime EndOfHarvest { get; set; }
        public int CuantityInKG { get; set; }
        public string DescripcionProductoServicio { get; set; }
    }
}
