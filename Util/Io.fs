﻿module Io

open Process1
open System
open System.IO

let rec readName() = 
    printf "Enter name: "
    match Console.ReadLine().Trim() with
    | "" -> readName()
    | s -> s

let rec readAge() = 
    printf "Enter age: "
    match Int32.TryParse(Console.ReadLine()) with
    | true, n when n > 0 -> n
    | _ -> readAge()

let readValues() = 
    let rec readValues' vs = 
        printf "Enter a value, or press enter to end: "
        match Console.ReadLine() with
        | "" -> List.rev vs
        | s -> 
            match Double.TryParse(s) with
            | true, v -> v :: vs
            | _ -> vs
            |> readValues'
    readValues' []

let rec readFilePath() = 
    printf "Enter file path: "
    let filePath = Console.ReadLine()
    if File.Exists(filePath) then filePath
    else readFilePath()

let rec print xs = 
    match xs with
    | [] -> ()
    | x :: tail -> 
        printfn "%A" x
        print tail

let rec reversePrint xs = 
    match xs with
    | [] -> ()
    | x :: tail -> 
        reversePrint tail
        printfn "%A" x

let deleteDirectory path = 
    // Avoid DirectoryNotFoundException
    if Directory.Exists(path) then Directory.Delete(path, true)

let deleteFile path = 
    // Avoid ArgumentNullException by File.Delete("")
    // Avoid ArgumentException by File.Delete(null)
    // Avoid DirectoryNotFoundException by File.Delete(@"C:\NonExistingDirectory\Foo.txt")
    if File.Exists(path) then File.Delete(path)

let copyDirectory sourceDirectory destinationDirectory = 
    deleteDirectory destinationDirectory
    startProcess "xcopy" (sprintf "%A %A /e /i /y" sourceDirectory destinationDirectory)

let rec walk dir pattern = 
    seq { 
        yield! Directory.EnumerateFiles(dir, pattern)
        for d in Directory.EnumerateDirectories(dir) do
            yield! walk d pattern
    }