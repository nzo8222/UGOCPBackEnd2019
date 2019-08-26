using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UGOCPBackEnd2019.Extensions
{
    public static class ServerResponseExtensions
    {
        public static OkObjectResult OkResponse(this ControllerBase controller, object payload)
        {
            return new OkObjectResult(new RespuestaServidor
            {
                Exitoso = true,
                Payload = payload
            });
        }
        //BadRequestObjectResult
        public static OkObjectResult BadResponse(this ControllerBase controller, string errorMessage)
        {
            return new OkObjectResult(new RespuestaServidor
            {
                Exitoso = false,
                MensajeError = errorMessage
            });
        }

    }

    public class RespuestaServidor
    {
        public bool Exitoso { get; set; }
        public string MensajeError { get; set; }
        public object Payload { get; set; }
    }
}
