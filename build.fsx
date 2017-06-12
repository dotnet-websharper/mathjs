#load "tools/includes.fsx"
open IntelliFactory.Build

let bt =
    BuildTool().PackageId("WebSharper.MathJS")
        .VersionFrom("WebSharper")
        .WithFramework(fun f -> f.Net40)

let main =
    bt.WebSharper.Extension("WebSharper.MathJS")
        .SourcesFromProject()

let extens =
    bt.WebSharper.Library("WebSharper.MathJS.Extensions")
        .SourcesFromProject()
        .References(fun r -> 
            [
                r.Project(main)
            ])

let tests =
    bt.WithFramework(fun f -> f.Net45).WebSharper.SiteletWebsite("WebSharper.MathJS.Tests")
        .SourcesFromProject()
        .Embed([])
        .References(fun r ->
            [
                r.Project(main)
                r.NuGet("WebSharper.Testing").Reference()
                r.NuGet("WebSharper.UI.Next").Reference()
            ])

bt.Solution [
    extens
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
        .Add(extens)
]
|> bt.Dispatch
