namespace AutoHub.API.Conventions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

public class ControllerModelConvention : IControllerModelConvention

{
    public void Apply(ControllerModel controller)
    {
        controller.Filters.Add(new ProducesAttribute("application/json"));
    }
}