namespace FGenie.OpenApi.Common

open FGenie.Domain
open Microsoft.OpenApi.Models

[<AutoOpen>]
module rec Extension =

    type ValueType with

        member vt.ToOpenApiSchema() =
            let typeSchema = OpenApiSchema()

            match vt with
            | Boolean -> typeSchema.Type <- "boolean"
            | Byte -> typeSchema.Type <- "integer"
            | UByte -> typeSchema.Type <- "integer"
            | Char -> typeSchema.Type <- "string"
            | Decimal -> typeSchema.Type <- "number"
            | Single ->
                typeSchema.Type <- "number"
                typeSchema.Format <- "float"
            | Double ->
                typeSchema.Type <- "number"
                typeSchema.Format <- "double"
            | Int ->
                typeSchema.Type <- "integer"
                typeSchema.Format <- "int32"
            | UInt -> typeSchema.Type <- "integer"
            | Short -> typeSchema.Type <- "integer"
            | UShort -> typeSchema.Type <- "integer"
            | Long ->
                typeSchema.Type <- "integer"
                typeSchema.Format <- "int64"
            | ULong -> typeSchema.Type <- "integer"
            | String -> typeSchema.Type <- "string"
            | DateTime ->
                typeSchema.Type <- "string"
                typeSchema.Format <- "date-time"
            | Guid -> typeSchema.Type <- "string"
            | Option valueType ->
                typeSchema.AnyOf.Add(valueType.ToOpenApiSchema())
                typeSchema.AnyOf.Add(nullSchema)
                
            typeSchema

    type Field with
        member ft.ToOpenApiSchema() =
            match ft.Value with
            | Entity entityType -> entityType.ToOpenApiSchema()
            | Value valueType -> valueType.ToOpenApiSchema()
            
    type FieldValue with
        member fv.ToOpenApiSchema() =
            match fv with
            | Entity entityType -> entityType.ToOpenApiSchema()
            | Value valueType -> valueType.ToOpenApiSchema()

    type EntityType with
        member et.ToOpenApiSchema() =
            match et with
            | EntityType.Record recordType -> recordType.ToOpenApiSchema()
            | EntityType.Union unionType -> failwith "todo"
    
    type RecordType with
         
         member rt.ToOpenApiSchema() =
             let schema = OpenApiSchema()
             
             schema.Type <- "object"
             
             rt.Fields
             |> List.iter (fun f -> schema.Properties.Add(f.Name.ToCamelCase(), f.ToOpenApiSchema()))
             
             schema