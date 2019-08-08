using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UGOCPBackEnd2019.Models
{
    public class ModeloUpdateUsuario
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public int IdLocalidad { get; set; }
        public string CellPhone { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
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
