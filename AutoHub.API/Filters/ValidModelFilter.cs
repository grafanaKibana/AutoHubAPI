using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AutoHub.API.Filters
{
    public class ValidModelFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            /*var actionArgument = context.ActionDescriptor.Parameters.Last().ParameterType;

            using var sr = new StreamReader(context.HttpContext.Request.Body);
            var jsonBody = sr.ReadToEndAsync();
            var jsonObject = JObject.Parse(await jsonBody);
            var schemaGenerator = new JsonSchemaGenerator();
            var schema = schemaGenerator.Generate(actionArgument);
            
            if (!jsonObject.IsValid(schema))
            {
                context.HttpContext.Response.StatusCode = 400;
                context.Result = new ContentResult
                {
                    Content = "Body object is not allowed type"
                };
            }*/
            if (!context.ModelState.IsValid)
            {
                context.HttpContext.Response.StatusCode = 400;
                context.Result = new ContentResult
                {
                    Content = "Body object is not allowed type"
                };
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}