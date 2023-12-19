using System.Net;
using Libraries.Web.Common.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Libraries.Web.Common.ControllerModelConventions;

public class GlobalControllerModelConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        controller.Filters.Add(new ProducesResponseTypeAttribute(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest));
        controller.Filters.Add(new ProducesResponseTypeAttribute(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError));
        controller.Filters.Add(new ProducesAttribute("application/json"));
    }
}