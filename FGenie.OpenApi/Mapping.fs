namespace FGenie.OpenApi

open FGenie.Domain
open Microsoft.OpenApi.Models
open FGenie.OpenApi.Common

module Mapping =
    
    let mapField (field: Field) =
        //field.Name.ToCamelCase(),
        
        match field.Value with
        | Entity entityType -> failwith "todo"
        | Value valueType -> valueType.ToOpenApiSchema()
        
        
        ()
        
    
    let mapRecord (recordType: RecordType) =
        let schema = OpenApiSchema()
        
        schema.Type <- "object"
        
        schema.Properties
        
        
        schema

