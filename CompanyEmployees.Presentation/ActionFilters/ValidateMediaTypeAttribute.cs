﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.Presentation.ActionFilters
{
    public class ValidateMediaTypeAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context) 
        {
            var acceptedHeaderPresent = context.HttpContext.Request
                .Headers.ContainsKey("Accept");
            if (!acceptedHeaderPresent)
            {
                context.Result = new BadRequestObjectResult($"Accept header is missing");

                return;
            }

            var mediaType = context.HttpContext.Request.Headers["Accept"].FirstOrDefault();

            if(!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue?
                outMediaType))
            {
                context.Result = new BadRequestObjectResult($"Media type not preesent. " +
                    $"Please add Accept header with requireed media type.");
                return;
            }

            context.HttpContext.Items.Add("AcceotHeaderMediaType", outMediaType);

        } 

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
