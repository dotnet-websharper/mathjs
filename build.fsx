#if INTERACTIVE
#r "nuget: FAKE.Core"
#r "nuget: Fake.Core.Target"
#r "nuget: Fake.IO.FileSystem"
#r "nuget: Fake.Tools.Git"
#r "nuget: Fake.DotNet.Cli"
#r "nuget: Fake.DotNet.AssemblyInfoFile"
#r "nuget: Fake.DotNet.Paket"
#r "nuget: Paket.Core"
#else
#r "paket:
nuget FSharp.Core 5.0.0
nuget FAKE.Core
nuget Fake.Core.Target
nuget Fake.IO.FileSystem
nuget Fake.Tools.Git
nuget Fake.DotNet.Cli
nuget Fake.DotNet.AssemblyInfoFile
nuget Fake.DotNet.Paket
nuget Paket.Core prerelease //"
#endif

#load "paket-files/wsbuild/github.com/dotnet-websharper/build-script/WebSharper.Fake.fsx"
open System.IO
open System.Diagnostics
open System.Threading
open Fake.Core
open Fake.Core.TargetOperators
open Fake.DotNet
open Fake.IO
open Fake.IO.FileSystemOperators
// open Fake.JavaScript
open WebSharper.Fake

let WithProjects projects args =
    { args with BuildAction = Projects projects }

let viteRun directory =
    let npxPath = ProcessUtils.tryFindFileOnPath "npx"
    match npxPath with
    | None -> failwith "Could not find npx"
    | Some npxPath ->
        CreateProcess.fromRawCommandLine npxPath "vite"
        |> CreateProcess.withWorkingDirectory directory
        |> CreateProcess.ensureExitCode



let onStdout (line: string) =
    printfn "%s" line

let onStderr (line: string) =
    printfn "%s" line

let run createProcess =
    createProcess
    |> CreateProcess.redirectOutputIfNotRedirected
    |> CreateProcess.withOutputEvents (onStdout) (onStderr)


Target.create "RunMainTestsRelease" <| fun _ ->
    ()
    // if Environment.environVarAsBoolOrDefault "SKIP_CORE_TESTING" false || not <| System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform System.Runtime.InteropServices.OSPlatform.Windows then
    //     Trace.log "Chutzpah testing skipped"
    // else

    // Trace.log "Starting Web test project"
    // let mutable startedOk = false
    // let started = new EventWaitHandle(false, EventResetMode.ManualReset)

    // use webTestsProc = new Process()
    // webTestsProc.StartInfo.FileName <- @"WebSharper.MathJS.Tests.Server\bin\Release\net6.0\WebSharper.MathJS.Tests.Server.exe"
    // webTestsProc.StartInfo.WorkingDirectory <- @"WebSharper.MathJS.Tests.Server"
    // webTestsProc.StartInfo.UseShellExecute <- false
    // webTestsProc.StartInfo.RedirectStandardOutput <- true
    
    // webTestsProc.OutputDataReceived.Add(fun d -> 
    //     if not (isNull d) then
    //         if not startedOk then            
    //             Trace.log d.Data
    //         if d.Data.Contains("Application started.") then
    //             startedOk <- true   
    //             started.Set() |> ignore
    // )
    // webTestsProc.Exited.Add(fun _ -> 
    //     if not startedOk then
    //         failwith "Starting Web test project failed."    
    // )

    // webTestsProc.Start()
    // webTestsProc.BeginOutputReadLine()
    // started.WaitOne()
    // Thread.Sleep(8000)


    // use vite = new Process()
    // vite.StartInfo.FileName <- "cmd"
    // vite.StartInfo.UseShellExecute <- false
    // vite.StartInfo.RedirectStandardOutput <- true
    // vite.StartInfo.Arguments <- "/c npx vite"
    // vite.StartInfo.WorkingDirectory <- "./WebSharper.MathJS.Tests.Client/"

    // vite.Start()
    // vite.BeginOutputReadLine()
    // printfn "VITE started"
    // Thread.Sleep(8000)

    // let res =
    //     Shell.Exec(
    //         "packages/test/Chutzpah/tools/chutzpah.console.exe", 
    //         "http://localhost:5174 /engine Chrome /parallelism 1 /silent /failOnError /showFailureReport"
    //     )
    // webTestsProc.Kill()
    // vite.Kill()
    // if res <> 0 then
    //     failwith "Chutzpah test run failed"

let t = 
    LazyVersionFrom "WebSharper" |> WSTargets.Default
    |> fun args ->
        { args with
            Attributes = [
                AssemblyInfo.Company "IntelliFactory"
                AssemblyInfo.Copyright "(c) IntelliFactory 2023"
                AssemblyInfo.Title "https://github.com/dotnet-websharper/ui"
                AssemblyInfo.Product "WebSharper UI"
            ]
        }
    |> MakeTargets

"WS-BuildRelease"
    ==> "RunMainTestsRelease"

"RunMainTestsRelease"
    ==> "CI-Release"

t |> RunTargets