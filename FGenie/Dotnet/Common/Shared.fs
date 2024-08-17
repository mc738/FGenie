namespace FGenie.Dotnet.Common

open System

module Shared =

    type IndentSettings = { Character: char; Count: int }

    let createIndent (settings: IndentSettings) (count: int) =
        String(settings.Character, count * settings.Count)
