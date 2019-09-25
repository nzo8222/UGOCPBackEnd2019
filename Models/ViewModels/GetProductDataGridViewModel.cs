using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UGOCPBackEnd2019.Entities;

namespace UGOCPBackEnd2019.Models.ViewModels
{
    public class GetProductDataGridViewModel
    {
        public GetProductDataGridViewModel()
        {

        }
        public Guid IdProduct { get; set; }
        public string ClaveProductoServicio { get; set; }
        public string DescripcionProductoServicio { get; set; }
        public string Name { get; set; }
        public string Calidad { get; set; }
        public List<Month> LstMonthsOfHarvest { get; set; }
        public int CuantityInKG { get; set; }
    }
}
