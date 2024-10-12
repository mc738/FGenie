
open System
open Microsoft.FSharp.Reflection

module POC =

    module Iter1 =

        type Foo = { Id: int; Name: string }

        and Bar = { SubId: int; SubName: string }

        let ``parse fsharp record with nested record`` =

            ()



// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"
