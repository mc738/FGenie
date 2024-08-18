namespace FGenie.OpenApi.Common

open Microsoft.OpenApi.Models

[<AutoOpen>]
module Shared =
    
    type MappedOpenApiSchema = {
        Name: string
        Schema: OpenApiSchema
    }
    
    
    let nullSchema =
        let s = OpenApiSchema()
        s.Type <- "null"
        s
        

