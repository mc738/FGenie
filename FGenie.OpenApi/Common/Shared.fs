namespace FGenie.OpenApi.Common

open Microsoft.OpenApi.Models

[<AutoOpen>]
module Shared =
    
    let nullSchema =
        let s = OpenApiSchema()
        s.Type <- "null"
        s
        

