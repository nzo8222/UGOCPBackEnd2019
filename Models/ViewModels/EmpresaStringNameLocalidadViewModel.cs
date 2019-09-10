using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UGOCPBackEnd2019.Entities;

namespace UGOCPBackEnd2019.Models.ViewModels
{
    public class EmpresaStringNameViewModel
    {
        public EmpresaStringNameViewModel()
        {
                
        }
        public Guid IdCompany  { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Localidad { get; set; }
        public List<Product> LstProduct { get; set; }
    }
}
