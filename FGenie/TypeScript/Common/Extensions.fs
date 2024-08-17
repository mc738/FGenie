namespace FGenie.TypeScript.Common

[<AutoOpen>]
module Extensions =

    open FGenie.Domain

    type ValueType with

        member vt.GetTypeScriptTypeName() =
            match vt with
            | Boolean -> "bool"
            | Byte -> "byte"
            | UByte -> "unit8"
            | Char -> "char"
            | Decimal -> "decimal"
            | Single -> "single"
            | Double -> "double"
            | Int -> "int"
            | UInt -> "uint"
            | Short -> "int16"
            | UShort -> "uint16"
            | Long -> "int64"
            | ULong -> "uint64"
            | String -> "string"
            | DateTime -> "DateTime"
            | Guid -> "Guid"
            | Option valueType -> $"{valueType} option"
