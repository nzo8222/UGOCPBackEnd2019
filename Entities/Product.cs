﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UGOCPBackEnd2019.Entities
{
    public class Product
    {
        public Product()
        {

        }
        public Guid IdProduct { get; set; }
        public string ClaveProductoServicio { get; set; }
        public string DescripcionProductoServicio { get; set; }
        public string Name { get; set; }
        public string Calidad { get; set; }
        public List<Month> LstMonthsOfHarvest { get; set; }
        public int CuantityInKG { get; set; }
        //[ForeignKey("CompanyIdCompany")]
        //public Company Compañia { get; set; }
    }
}
