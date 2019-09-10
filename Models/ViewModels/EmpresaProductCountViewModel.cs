using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UGOCPBackEnd2019.Entities;

namespace UGOCPBackEnd2019.Models.ViewModels
{
    public class EmpresaProductCountViewModel
    {
        public EmpresaProductCountViewModel()
        {
               
        }
        public Guid IdCompany { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public int IdLocalidad { get; set; }
        public string localidad { get; set; }
        //public List<Product> LstProduct { get; set; }
        public int ProductCount { get; set; }
    }
}
