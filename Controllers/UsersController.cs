﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UGOCPBackEnd2019.Data;
using UGOCPBackEnd2019.Extensions;
using UGOCPBackEnd2019.Helpers;
using UGOCPBackEnd2019.Models;
using UGOCPBackEnd2019.Services;
using UGOCPBackEnd2019.Services.Models;
using Microsoft.EntityFrameworkCore;
using UGOCPBackEnd2019.Models.ViewModels;

namespace UGOCPBackEnd2019.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _usrMngr;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly UgocpDbContext _context;
        private readonly cat_localidadContext _ctxLocalidad;

        public UsersController(UserManager<User> manager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions, UgocpDbContext ctx, cat_localidadContext _localidadContext)
        {
            _context = ctx;
            _usrMngr = manager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _ctxLocalidad = _localidadContext;
        }

        [HttpGet("{IdUsuario}")]
        public IActionResult GetUserData([FromRoute] Guid IdUsuario)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == IdUsuario);

                if (user == null)
                {
                    return this.BadResponse("No se encontro al usuario.");
                }

                var localidad = _ctxLocalidad.Localidades.FirstOrDefault(l => l.Id == user.IdLocalidad);
                var municipio = _ctxLocalidad.Municipios.FirstOrDefault(m => m.Id == localidad.MunicipioId);
                var estado = _ctxLocalidad.Estados.FirstOrDefault(e => e.Id == municipio.EstadoId);

                DatosUsuarioViewModel DatosUsuarioVM = new DatosUsuarioViewModel();
                DatosUsuarioVM.Address = user.Address;
                DatosUsuarioVM.CellPhone = user.CellPhone;
                DatosUsuarioVM.Charge = user.Charge;
                DatosUsuarioVM.CivilStatus = user.CivilStatus;
                DatosUsuarioVM.ClaveDeElector = user.ClaveDeElector;
                DatosUsuarioVM.CURP = user.CURP;
                DatosUsuarioVM.DateOfBirth = user.DateOfBirth;
                DatosUsuarioVM.Estado = estado.Nombre;
                DatosUsuarioVM.FullName = user.FullName;
                DatosUsuarioVM.Gender = user.Gender;
                DatosUsuarioVM.Localidad = localidad.Nombre;
                DatosUsuarioVM.Municipio = municipio.Nombre;
                DatosUsuarioVM.NumberINECredential = user.NumberINECredential;
                DatosUsuarioVM.Ocupation = user.Ocupation;
                DatosUsuarioVM.PhoneNumber = user.PhoneNumber;

                  return this.OkResponse(DatosUsuarioVM);
            }
            catch(Exception ex)
            {
                  return this.BadResponse(ex.ToString());
            }
        }


        [HttpPost]
        [Route("Registro")]
        public async Task<IActionResult> RegistroUsuario([FromBody]RegisterUserViewModel usuario)
        {
            var user = new User
            {
                
                Id = Guid.NewGuid(),
                UserName = usuario.Usuario,
                Email = usuario.Email
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

            var identity = await GetClaimsIdentity(logInViewModel.Usuario, logInViewModel.Password);


            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, logInViewModel.Usuario, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return this.OkResponse(new SessionModel
            {
                UserId = user.Id.ToString(),
                Email = user.Email,
                ExpiresIn = jwt.ExpiresIn,
                JwtToken = jwt.Auth_Token,
                Role = user.Role
            });

        }

        [HttpPost]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] ModeloUpdateUsuario modelo)
        {
            try
            {
                // Busca al usuario.
                var user = _context.Users.FirstOrDefault(u => u.Id == modelo.Id);

                if (user == null)
                {
                    return this.BadResponse("No se encontro al usuario.");
                }

                user.FullName = modelo.FullName;
                user.Address = modelo.Address;
                user.IdLocalidad = modelo.IdLocalidad;
                user.CellPhone = modelo.CellPhone;
                user.PhoneNumber = modelo.PhoneNumber;
                user.DateOfBirth = modelo.DateOfBirth;
                user.Gender = modelo.Gender;
                user.CivilStatus = modelo.CivilStatus;
                user.Ocupation = modelo.Ocupation;
                user.Charge = modelo.Charge;
                user.CURP = modelo.CURP;
                user.ClaveDeElector = modelo.ClaveDeElector;
                user.NumberINECredential = modelo.NumberINECredential;

                await _context.SaveChangesAsync();

                return this.OkResponse("Se actualizaron los cambios correctamente.");
            }
            catch(Exception ex)
            {
                return this.BadResponse(ex.ToString());
            }
        }

        // Config JWT.
        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _usrMngr.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _usrMngr.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }

        
    }
}
