using System;
using System.Net;
using MicroPack.WebApi.Exceptions;

namespace Pacco.APIGateway.Ocelot.Infrastructure
{
    internal sealed class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        public ExceptionResponse Map(Exception exception)
            => exception switch
            {
                _ => new ExceptionResponse(new {code = "error", reason = "There was an error."},
                    HttpStatusCode.BadRequest)
            };
    }
}