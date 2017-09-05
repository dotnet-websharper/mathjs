#load "tools/includes.fsx"
open IntelliFactory.Build

let bt =
    BuildTool().PackageId("WebSharper.MathJS")
        .VersionFrom("WebSharper")
        .WithFramework(fun f -> f.Net40)

let main =
    bt.WebSharper4.Extension("WebSharper.MathJS")
        .SourcesFromProject()

let extens =
    bt.WebSharper4.Library("WebSharper.MathJS.Extensions")
        .SourcesFromProject()
        .References(fun r -> 
            [
                r.Project(main)
            ])

let tests =
    bt.WithFramework(fun f -> f.Net45).WebSharper4.SiteletWebsite("WebSharper.MathJS.Tests")
        .SourcesFromProject()
        .Embed([])
        .References(fun r ->
            [
                r.Project(main)
                r.Project(extens)
                r.NuGet("WebSharper.Testing").Latest(true).Reference()
                r.NuGet("WebSharper.UI.Next").Latest(true).Reference()
            ])

bt.Solution [    
    main
    extens
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
