namespace FGenie.Dotnet.FSharp

open System
open Microsoft.FSharp.Reflection

module Mapping =

    let getUnion (t: Type) =

        Ok()

    let getRecord (t: Type) =
        t.GetProperties()
        |> Array.iteri (fun i pi ->

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
