namespace FGenie

[<AutoOpen>]
module Common =

    let sortResults (results: Result<'T, 'U> seq) =
        results
        |> Seq.fold
            (fun (success: 'T list, errors: 'U list) result ->
                match result with
                | Ok resultValue -> resultValue :: success, errors
                | Error errorValue -> success, errorValue :: errors)
            ([], [])
        |> fun (success, errors) -> success |> List.rev, errors |> List.rev
