using System;
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
        public int ClaveProductoServicio { get; set; }
        public string Name { get; set; }
        public string Calidad { get; set; }
        public DateTime StartOfHarvest { get; set; }
        public DateTime EndOfHarvest { get; set; }
        public int CuantityInKG { get; set; }
        //[ForeignKey("CompanyIdCompany")]
        //public Company Compañia { get; set; }
    }
}
