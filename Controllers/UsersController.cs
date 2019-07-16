using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UGOCPBackEnd2019.Extensions;
using UGOCPBackEnd2019.Models;

namespace UGOCPBackEnd2019.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _usrMngr;

        public UsersController(UserManager<User> manager)
        {
            _usrMngr = manager;
        }

        [HttpPost]
        [Route("Registro")]
        public async Task<IActionResult> RegistroUsuario([FromBody]RegisterUserViewModel usuario)
        {
            var user = new User
            {
                
                Id = Guid.NewGuid(),
                UserName = usuario.Usuario,
                Email = usuario.Email,
                PhoneNumber = usuario.PhoneNumber,
                Address = usuario.Address,
                Zone = usuario.Zone,
                State = usuario.State,
                Municipality = usuario.Municipality,
                Town = usuario.Town,
                CellPhone = usuario.CellPhone,
                Age = usuario.Age,
                Gender = usuario.Gender,
                CivilStatus = usuario.CivilStatus,
                Ocupation = usuario.Ocupation,
                Charge = usuario.Charge,
                CURP = usuario.CURP,
                ClaveDeElector = usuario.ClaveDeElector,
                NumberINECredential = usuario.NumberINECredential
            };

            var result = await _usrMngr.CreateAsync(user, usuario.Password);

            if (!result.Succeeded) return this.BadResponse("No se pudo crear el usuario.");

            return this.OkResponse("Ok");
        }

        [HttpPost]
        [Route("LogIn")]
        public async Task<IActionResult> LogIn([FromBody] LoginUsuarioViewModel logInViewModel)
        {
            // Valida los datos de acceso.

            if(string.IsNullOrEmpty(logInViewModel.Usuario) || string.IsNullOrEmpty(logInViewModel.Password))
            {
                return this.BadResponse("Es necesario llenar todos los campos");
            }

            var user = await _usrMngr.FindByNameAsync(logInViewModel.Usuario);

            if(user == null)
            {
                return this.BadResponse("No se encontro al usuario");
            }
            var passwordCheck = await _usrMngr.CheckPasswordAsync(user, logInViewModel.Password);

            if(!passwordCheck)
            {
                return this.BadResponse("La contraseña no coincide.");
            }

            return this.OkResponse("");
        }
    }
}
