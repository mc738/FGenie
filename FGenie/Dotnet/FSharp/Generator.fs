namespace FGenie.Dotnet.FSharp

open System
open FGenie.Domain
open FGenie.Dotnet.Common
open FGenie.Dotnet.Common.Shared
open FsToolbox.Extensions.Strings

module Generator =
    
    type GeneratorSettings =
        {
            Indent: IndentSettings
        }
    
    let generateRecord (settings: GeneratorSettings) (indent: int) (record: RecordType) =
        let baseIndent = createIndent settings.Indent indent

        let fields =
            record.Fields
            |> List.mapi (fun i f ->
                let name = f.Name.ToPascalCase()
                    
                
                let fieldTypeName =
                    match f.Value with
                    | Entity entityType -> failwith "todo"
                    | Value valueType -> valueType.GetDotNetTypeName()
               
                if i = 0 then
                    $"{{ {f.Name.ToPascalCase()}: {fieldTypeName}"
                elif i = record.Fields.Length - 1 then
                    $"  {f.Name.ToPascalCase()}: {fieldTypeName}"
                else
                    $"  {f.Name.ToPascalCase()}: {fieldTypeName} }}")
        
    
        [
            
            yield! fields
        ]
        |> String.concat Environment.NewLine

