namespace FGenie

module Domain =
     
    [<RequireQualifiedAccess>]
    type Language =
        | FSharp
        | CSharp
        | TypeScript
        | JavaScript
        | JSONSchema
   
    [<RequireQualifiedAccess>]
    type ValueType =
        | String
        | Number
        | Integer
        | Object
        | Array
        | Boolean
        | Null
    
        static member TryDeserialize(value: string) =
            match value.ToLower() with
            | "string" -> Some ValueType.String
            | "number" -> Some ValueType.Number
            | "integer" -> Some ValueType.Integer
            | "object" -> Some ValueType.Object
            | "array" -> Some ValueType.Array
            | "boolean" -> Some ValueType.Boolean
            | "null" -> Some ValueType.Null
            | _ -> None
        
    type SupportedType =
        | Boolean
        | Byte
        | UByte
        | Char
        | Decimal
        | Double
        | Float
        | Int
        | UInt
        | Short
        | UShort
        | Long
        | ULong
        | String
        | DateTime
        | Guid
        | Option of SupportedType
    
    
    
    type Schema =
        { Uri: string
          Type: string
          Required: string list
          Properties: Property list
        }

    and Property =
        { Name: string
          Type: string
          Description: string
          MarkdownDescription: string }

    and AllOf = {
        If: If
        Then: Then
    }
    
    and If = {
        Properties: string
        Required: string list
    }
    
    and Then = {
        Ref: string
    }
    
    
    
    