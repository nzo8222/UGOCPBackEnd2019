using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UGOCPBackEnd2019.Models
{
    public class ModeloUpdateUsuario
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string Zone { get; set; }
        public string State { get; set; }
        public string Municipality { get; set; }
        public string Town { get; set; }
        public string CellPhone { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string CivilStatus { get; set; }
        public string Ocupation { get; set; }
        public string Charge { get; set; }
        public string CURP { get; set; }
        public string ClaveDeElector { get; set; }
        public string NumberINECredential { get; set; }
        //public List<Company> LstCompany { get; set; }
    }
}
