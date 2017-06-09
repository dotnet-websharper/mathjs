#load "tools/includes.fsx"
open IntelliFactory.Build

let bt =
    BuildTool().PackageId("Zafir.MathJS")
        .VersionFrom("Zafir")
        //.WithFSharpVersion(FSharpVersion.FSharp41)
        //.WithFramework(fun f -> f.Net45)

let main =
    bt.Zafir.Extension("WebSharper.MathJS")
        .SourcesFromProject()
        .Embed([])
        .References(fun r -> [])

let tests =
    bt.Zafir.SiteletWebsite("WebSharper.MathJS.Tests")
        .SourcesFromProject()
        .Embed([])
        .References(fun r ->
            [
                r.Project(main)
                r.NuGet("Zafir.Testing").Latest(true).Reference()
                r.NuGet("Zafir.UI.Next").Latest(true).Reference()
            ])

bt.Solution [
    main
    tests

    bt.NuGet.CreatePackage()
        .Configure(fun c ->
            { c with
                Title = Some "WebSharper.MathJS"
                LicenseUrl = Some "http://websharper.com/licensing"
                ProjectUrl = Some "https://github.com/intellifactory/https://github.com/intellifactory/websharper.mathjs"
                Description = "WebSharper Extension for math.js 3.13.3"
                RequiresLicenseAcceptance = true })
        .Add(main)
]
|> bt.Dispatch
