namespace FGenie.Dotnet.FSharp

open FGenie.Domain


module Mapping =

    open System
    open FGenie
    open FGenie.Domain
    open FGenie.Dotnet.Common
    open FGenie.Dotnet.Common.TypeHelper
    open Microsoft.FSharp.Reflection
    open FsToolbox.Extensions.Strings

    [<RequireQualifiedAccess>]
    type CategorizedType =
        | Base of BaseType
        | Record
        | Union
        | UnhandledType of Type

        static member TryCreate(t: Type) =
            match BaseType.TryFromType t with
            | Ok bt -> CategorizedType.Base bt
            | Error _ ->
                if FSharpType.IsRecord t then CategorizedType.Record
                elif FSharpType.IsUnion t then CategorizedType.Union
                else CategorizedType.UnhandledType t

    let getUnion (t: Type) : Result<UnionType, string> = failwith "TODO"

    let getRecord (t: Type) =
        let (fields, errors) =
            t.GetProperties()
            |> Array.mapi (fun i pi ->

                // Check if the property type is either a base type of record/union.

                match CategorizedType.TryCreate pi.PropertyType with
                | CategorizedType.Base baseType ->
                    ({ Name = pi.Name.ToSnakeCase() |> NameType.Normalized
                       Value = FieldValue.Value baseType.ValueType
                       Metadata = Map.empty }
                    : Field)
                    |> Ok
                | CategorizedType.Record -> Error "TODO - map record fields"
                | CategorizedType.Union -> Error "TODO - map record fields"
                | CategorizedType.UnhandledType ``type`` -> Error "Unhandled type")
            |> sortResults

        ({ Name = t.Name.ToSnakeCase() |> NameType.Normalized
           Fields = fields
           Metadata = Map.empty }
        : RecordType)
        |> Ok


    let getFromType (t: Type) =
        if FSharpType.IsRecord t then
            getRecord t |> Result.map EntityType.Record
        elif FSharpType.IsUnion t then
            getUnion t |> Result.map EntityType.Union
        elif isCollectionType t then
            failwith "TODO handle collections"
        else
            Error $"Type `{t.FullName}` is neither a FSharp record or union."


    let get<'T> () =

        let t = typeof<'T>

        getFromType t
