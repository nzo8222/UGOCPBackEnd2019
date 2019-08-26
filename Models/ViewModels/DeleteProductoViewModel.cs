using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UGOCPBackEnd2019.Models.ViewModels
{
    public class DeleteProductoViewModel
    {
        public Guid IdUsuario { get; set; }
        public Guid IdEmpresa { get; set; }
        public Guid IdProducto { get; set; }
    }
}
