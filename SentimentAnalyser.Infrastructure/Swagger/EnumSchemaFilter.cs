using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SentimentAnalyser.Infrastructure.Swagger
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                var names = new OpenApiArray();
                names.AddRange(Enum.GetNames(context.Type).Select(x => new OpenApiString(x)));
                schema.Extensions.Add(new KeyValuePair<string, IOpenApiExtension>("x-enum-varnames", names));
            }
        }
    }
}