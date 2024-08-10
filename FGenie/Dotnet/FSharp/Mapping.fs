namespace FGenie.Dotnet.FSharp

open System
open FGenie.Dotnet.Common
open FGenie.Dotnet.Common.TypeHelper
open Microsoft.FSharp.Reflection

module Mapping =
    
    
    [<RequireQualifiedAccess>]
    type CategorizedType =
        | Base of BaseType
        | Record
        | Union
        | Option of CategorizedType
        
        static member TryCreate() =
            if FSharpType.IsRecord t then
                CategorizedType.Record
            elif FSharpType.IsUnion t then
                CategorizedType.Union
            elif
                BaseType. 
    let getUnion (t: Type) =

        Ok()

    let getRecord (t: Type) =
        t.GetProperties()
        |> Array.iteri (fun i pi ->

            // Check if the property type is either a base type of record/union.
            
            
            pi.pro


            ())

        Ok()

    
    let get<'T> () =

        let t = typeof<'T>

        if FSharpType.IsRecord t then
            getRecord t
        elif FSharpType.IsUnion t then
            getUnion t
        else
            Error $"Type `{t.FullName}` is neither a FSharp record or union."
