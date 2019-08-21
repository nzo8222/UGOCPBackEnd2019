using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UGOCPBackEnd2019.Models.ViewModels
{
    public class UpdateEmpresaViewModel
    {
        public Guid IdUsuario { get; set; }
        public Guid IdEmpresa { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public int IdLocalidad { get; set; }
    }
}
