namespace FGenie

module Domain =

    open FsToolbox.Extensions.Strings

    [<RequireQualifiedAccess>]
    type Language =
        | FSharp
        | CSharp
        | TypeScript
        | JavaScript
        | JSONSchema

    type NameType =
        | Normalized of string
        | Json of string
        | Unknown of string

        member nt.Normalize() =
            match nt with
            | Normalized s -> s
            | Json s -> s.ToSnakeCase()
            | Unknown s -> s.ToSnakeCase()

        member nt.ToPascalCase() =
            match nt with
            | Normalized s -> s.ToPascalCase()
            | Json s -> s.ToPascalCase()
            | Unknown s -> s.ToPascalCase()

        member nt.ToCamelCase() =
            match nt with
            | Normalized s -> s.ToCamelCase()
            | Json s -> s
            | Unknown s -> s.ToCamelCase()

    type ValueType =
        | Boolean
        | Byte
        | UByte
        | Char
        | Decimal
        | Single
        | Double
        | Int
        | UInt
        | Short
        | UShort
        | Long
        | ULong
        | String
        | DateTime
        | Guid
        | Option of ValueType

    [<RequireQualifiedAccess>]
    type EntityType =
        | Record of RecordType
        | Union of UnionType
        
        member et.TryGetTypeName() =
            match et with
            | Record recordType -> Some recordType.Name
            | Union unionType -> Some unionType.Name
        
    and RecordType =
        { Name: NameType
          Fields: Field list
          Metadata: Map<string, string> }

    and Field =
        { Name: NameType
          Value: FieldValue
          IsCollection: bool
          Metadata: Map<string, string> }

    and FieldValue =
        | Entity of EntityType
        | Value of ValueType

    and UnionType =
        { Name: NameType
          Entries: UnionEntry list
          Metadata: Map<string, string> }

    and UnionEntry =
        { Name: NameType
          Field: FieldValue
          Metadata: Map<string, string> }


    and ArrayType = { Type: FieldValue }
