open System
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open WebSharper.AspNetCore

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    
    // Add services to the container.
    builder.Services.AddWebSharper()
        .AddWebSharperRemoting<WebSharper.MathJS.Tests.Client.Shared.API>(Site.implementedApi)
        .AddAuthentication("WebSharper")
    |> ignore

    let app = builder.Build()

    // Configure the HTTP request pipeline.
    if not (app.Environment.IsDevelopment()) then
        app.UseExceptionHandler("/Error")
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            .UseHsts()
        |> ignore

    WebSharper.Web.Remoting.DisableCsrfProtection ()
    WebSharper.Web.Remoting.AddAllowedOrigin "*"

    app.UseAuthentication()
        .UseStaticFiles()
        .UseWebSharper(fun ws -> ws.UseRemoting(true) |> ignore)
    |> ignore
    
    app.Run()

    0 // Exit code
