﻿module Paket.ProjectFile.InterProjectDependencySpecs

open Paket
open NUnit.Framework
open FsUnit

[<Test>]
let ``should detect no dependencies in empty proj file``() =
    ProjectFile.Load("./ProjectFile/TestData/Empty.fsprojtest").Value.GetInterProjectDependencies()
    |> shouldBeEmpty

[<Test>]
let ``should detect Paket dependency in Project1 proj file``() =
    ProjectFile.Load("./ProjectFile/TestData/Project1.fsprojtest").Value.GetInterProjectDependencies()
    |> List.map (fun p -> p.Name)
    |> shouldEqual ["Paket"]

[<Test>]
let ``should detect Paket and Paket.Core dependency in Project2 proj file``() =
    ProjectFile.Load("./ProjectFile/TestData/Project2.fsprojtest").Value.GetInterProjectDependencies()
    |> List.map (fun p -> p.Name)
    |> shouldEqual ["Paket"; "Paket.Core"]

[<Test>]
let ``should detect path for dependencies in Project2 proj file``() =
    ProjectFile.Load("./ProjectFile/TestData/Project2.fsprojtest").Value.GetInterProjectDependencies()
    |> List.map (fun p -> p.Path)
    |> shouldEqual ["..\..\src\Paket\Paket.fsproj"; "..\Paket.Core\Paket.Core.fsproj"]

[<Test>]
let ``should detect Guids for dependencies in Project2 proj file``() =
    ProjectFile.Load("./ProjectFile/TestData/Project2.fsprojtest").Value.GetInterProjectDependencies()
    |> List.map (fun p -> p.GUID.ToString())
    |> shouldEqual ["09b32f18-0c20-4489-8c83-5106d5c04c93"; "7bab0ae2-089f-4761-b138-a717aa2f86c5"]