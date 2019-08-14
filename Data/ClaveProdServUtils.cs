using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UGOCPBackEnd2019.Data
{
    public class ClaveProdServ
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }

        public ClaveProdServ()
        {

        }
        public ClaveProdServ(string codigo, string descripcion)
        {
            Codigo = codigo;
            Descripcion = descripcion;
        }

        public List<ClaveProdServ> GetList()
        {
            
            string claveProdServ;
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "UGOCPBackEnd2019.Data.ClaveProdServ.dat";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                claveProdServ = reader.ReadToEnd();
            }

            //var text = System.IO.File.ReadAllText($@"C:\Users\Gonzo\Documents\UGOCPBackEnd\UGOCPBackEnd2019\UGOCPBackEnd2019\Data\ClaveProdServ.dat");


            var _lst = claveProdServ
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(a =>
                {
                    var separador = a.Split(Convert.ToChar("|"));
                    return new ClaveProdServ(separador[0], separador[1]);
                }).ToList();
            return _lst;
        }
    }
}
