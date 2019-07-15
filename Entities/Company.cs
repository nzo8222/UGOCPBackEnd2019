using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UGOCPBackEnd2019.Entities
{
    public class Company
    {
        public Guid IdCompany { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public List<Product> LstProduct { get; set; }
    }
}
