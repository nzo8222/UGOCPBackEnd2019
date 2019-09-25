using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UGOCPBackEnd2019.Data;

namespace UGOCPBackEnd2019.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly cat_localidadContext _context;
        public ValuesController(cat_localidadContext ctx)
        {
            _context = ctx;
        }
        // GET api/values
        //public async Task<IActionResult> RegistroUsuario([FromBody]RegisterUserViewModel usuario)
        [HttpGet]
        public IActionResult Get()
        {
            var localidades = _context.Localidades.FirstOrDefault(l => l.Nombre == "Ciudad Obregón");
            return new OkObjectResult(localidades);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
