using MasterclassApiTest.Entities;
using MasterclassApiTest.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Net;

namespace MasterclassApiTest.Filters
{
    // Tutorial: https://nwb.one/blog/exception-filter-attribute-dotnet
    public class NotFoundFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            string errorMessage = string.Empty;
            // Log the exception
            if (context.Exception is KlantNotFoundException)
            {
                var ex = (KlantNotFoundException)context.Exception;
                var id = ex.Id;
                errorMessage = $"De klant met de gegeven ID ({id}) kon niet gevonden worden";
                Log.Error("Exception thrown when searching for a Klant with id (" + id + "): {@ex}", context.Exception);
            } else if (context.Exception is RekeningNotFoundException)
            {
                var ex = (RekeningNotFoundException)context.Exception;
                var klantId = ex.KlantId;
                var rekeningId = ex.RekeningId;
                errorMessage = $"De rekening met de gegeven ID ({rekeningId}) voor klant ({klantId}) kon niet gevonden worden";
                Log.Error("Exception thrown when searching for Rekening ("+rekeningId+") with Klant ID ("+klantId+"): {@ex}", context.Exception);
            }

            var result = new ObjectResult(new
            {
                errorMessage,
                context.Exception.Source,
                ExceptionType = context.Exception.GetType().FullName,
            })
            {
                StatusCode = (int)HttpStatusCode.NotFound
            };

            // Set the result
            context.Result = result;
        }
    }
}
