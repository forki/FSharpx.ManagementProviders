﻿module FSharp.Management.Tests.CommonFoldersTests

open FSharp.Management
open NUnit.Framework
open FsUnitTyped

// User Directory tests
[<Test>]
let ``User Roaming AppData path should match Environment.GetFolderPath``() =
    let env = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData)
    CommonFolders.GetUser UserPath.RoamingApplicationData |> shouldEqual env

[<Test>]
let ``User Local AppData path should match Environment.GetFolderPath``() =
    let env = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData)
    CommonFolders.GetUser UserPath.LocalApplicationData |> shouldEqual env

[<Test>]
let ``User desktop folder should match Environment.GetFolderPath``() =
    let env = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop)
    CommonFolders.GetUser UserPath.Desktop |> shouldEqual env

// Shared User  Tests
[<Test>]
let ``Shared AppData path should match Environment.GetFolderPath``() =
    let env = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData)
    CommonFolders.GetSharedUser SharedPath.ApplicationData |> shouldEqual env

[<Test>]
let ``Shared music folder should match Environment.GetFolderPath``() =
    let env = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonMusic)
    CommonFolders.GetSharedUser SharedPath.Music |> shouldEqual env

// System tests
[<Test>]
let ``System temp folder should match Path class``() =
    let pathFolder = System.IO.Path.GetTempPath()
    CommonFolders.GetSystem SystemPath.Temp |> shouldEqual pathFolder

[<Test>]
let ``Windows system folder should contain kernel32.dll``() =
    let sysFolder = CommonFolders.GetSystem SystemPath.System
    let file = System.IO.Path.Combine(sysFolder, "kernel32.dll")
    System.IO.File.Exists file |> shouldEqual true

// Application  Tests
[<Test>]
let ``FSharpManangement path should contain tests\\FSharp.Manangement.Tests\\bin``() =
    let path = CommonFolders.GetApplication ApplicationPath.FSharpManagementLocation
    let tempFolder = System.IO.Path.GetTempPath()
    path |> shouldContainText "tests\\FSharp.Management.Tests\\bin"

[<Test>]
let ``FSharpManangement shadow copied path should contain FSharp.Manangement.Tests``() =
    let path = CommonFolders.GetApplication ApplicationPath.FSharpManagementShadowCopiedLocation
    path |> shouldContainText "FSharp.Management.Tests"

// Unfortunatley, most test runners fail to load the entry point assembly properly.  This may be VS, or nunit, etc, but requires
// special care to test properly
//[<Test>]
//let ``Entry location should contain nunit.exe``() =
//    let path = CommonFolders.GetApplication ApplicationPath.EntryPointLocation
//    let file = System.IO.Path.Combine(path, "nunit.ee")
//    System.IO.File.Exists file |> should equal true
