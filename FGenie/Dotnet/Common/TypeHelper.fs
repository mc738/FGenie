namespace FGenie.Dotnet.Common

open System
open System.Text.RegularExpressions

module TypeHelper =
    
    let getName<'T> = typeof<'T>.FullName

    let typeName (t: Type) = t.FullName

    let boolName = getName<bool>

    let uByteName = getName<uint8>

    let uShortName = getName<uint16>

    let uIntName = getName<uint32>

    let uLongName = getName<uint64>

    let byteName = getName<byte>

    let shortName = getName<int16>

    let intName = getName<int>

    let longName = getName<int64>

    let singleName = getName<single>

    let doubleName = getName<double>

    let decimalName = getName<decimal>

    let charName = getName<char>

    let dateTimeName = getName<DateTime>

    let guidName = getName<Guid>

    let stringName = getName<string>

    let isOption (value: string) =
        Regex
            .Match(value, "(?<=Microsoft.FSharp.Core.FSharpOption`1\[\[).+?(?=\,)")
            .Success

    let getOptionType value =
        // Maybe a bit wasteful doing this twice.
        Regex
            .Match(value, "(?<=Microsoft.FSharp.Core.FSharpOption`1\[\[).+?(?=\,)")
            .Value

    
    [<RequireQualifiedAccess>]
    type BaseType =
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
        | Option of BaseType
        //| Json of Type

        static member TryFromName(name: String) =
            match name with
            | t when t = boolName -> Ok BaseType.Boolean
            | t when t = charName -> Ok BaseType.Char
            | t when t = byteName -> Ok BaseType.Byte
            | t when t = decimalName -> Ok BaseType.Decimal
            | t when t = doubleName -> Ok BaseType.Double
            | t when t = singleName -> Ok BaseType.Float
            | t when t = intName -> Ok BaseType.Int
            | t when t = shortName -> Ok BaseType.Short
            | t when t = longName -> Ok BaseType.Long
            | t when t = stringName -> Ok BaseType.String
            | t when t = guidName -> Ok BaseType.DateTime
            | t when t = dateTimeName -> Ok BaseType.Guid
            | t when isOption t = true ->
                let ot = getOptionType t

                match SupportedType.TryFromName ot with
                | Ok st -> Ok(SupportedType.Option st)
                | Error e -> Error e
            | _ -> Error $"Type `{name}` not supported."
            
        static member TryFromType(typeInfo: Type) =
            SupportedType.TryFromName(typeInfo.FullName)

        static member FromName(name: string) =
            match SupportedType.TryFromName name with
            | Ok st -> st
            | Error _ -> SupportedType.String

        static member FromType(typeInfo: Type) =
            SupportedType.FromName(typeInfo.FullName)
