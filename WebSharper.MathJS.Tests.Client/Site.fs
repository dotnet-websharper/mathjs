module Site

open WebSharper
open WebSharper.Sitelets


let implementedApi : WebSharper.MathJS.Tests.Client.Shared.API =
    {
        Add1ToDecimal =
            fun d -> 
                async {
                    return d + 1m
                }
    }
