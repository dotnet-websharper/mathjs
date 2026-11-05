namespace WebSharper.MathJS.Sample

open WebSharper
open WebSharper.JavaScript
open WebSharper.MathJS
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Templating

[<JavaScript>]
module Client =

    type IndexTemplate = Template<"index.html", ClientLoad.FromDocument>

    [<SPAEntryPoint>]
    let Main () =
        // BigNumber values are MathNumber, so arithmetic goes through the
        // math.js functions (Math.Add / Math.Multiply / ...) rather than F#'s
        // operators -- which on the old `bigint` typing silently concatenated
        // strings. See dotnet-websharper/mathjs#8.
        let a = Math.Bignumber "0.1"
        let b = Math.Bignumber "0.2"
        let sum = Math.Add(a, b)
        let product = Math.Multiply(a, b)

        // Format() (from WebSharper.MathJS.Extensions) is routed through
        // math.js formatting, so vectors come out bracketed ("[8, 6, 4, 6]")
        // and can be fed back to Math.Parse.
        let vector = MathNumber [| 8.; 6.; 4.; 6. |]

        IndexTemplate()
            .Sum("0.1 + 0.2 = " + sum.Format())
            .Product("0.1 * 0.2 = " + product.Format())
            .Vector("vector.Format() = " + vector.Format())
            .Bind()
