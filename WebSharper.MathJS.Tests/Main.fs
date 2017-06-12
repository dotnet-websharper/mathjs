namespace WebSharper.MathJS.Tests

open WebSharper
open WebSharper.Sitelets
open WebSharper.UI.Next

[<JavaScript>]
module Client =
    open WebSharper.JavaScript
    open WebSharper.Testing
    open WebSharper.MathJS
    open System.Numerics

    let Tests =

        TestCategory "General" {

            Test "Sanity check" {
                equalMsg (1+2+3) 6 "1 + 2 + 3 = 6"
            }

            Test "MathJS add (float)" {
                equalMsg (MathJS.Math.Add(1., 2., 3.)) 6. "MathJS.Math.Add(1., 2., 3.) = 6"
            }

            Test "MathJS add (complex)" {
                let a = MathJS.Math.Complex("1.0 + 1.0i")
                let b = MathJS.Math.Complex("1.0 + 1.0i")
                let c = MathJS.Math.Complex("2.0 + 2.0i")
                equalMsg (MathJS.Math.Add(a, b)) c "MathJS.Math.Add(Complex(1., 1.), Complex(1., 1.)) = Complex(2., 2.)"
            }

            Test "MathJS Complex" {
                isTrueMsg ((MathJS.Math.Complex("2.0 + 6.0i")).JS.Equals(MathJS.Math.Complex(2., "6."))) "Complex(\"2.0 + 6.0i\") = Complex(2., \"6.\")"
            }

            Test "MathJS Simplify" {
                equalMsg (MathJS.Math.Simplify("3 + 2 / 4").ToString()) "7 / 2" "Simplify(3 + 2 / 4) = 7 / 2"
            }

            Test "MathJS Simplify with x and y" {
                equalMsg (MathJS.Math.Simplify("x * y * -x / (x ^ 2)").ToString()) "-y" "Simplify(x * y * -x / (x ^ 2)) = -y"
            }

            Test "MathJS Derivative" {
                equalMsg (MathJS.Math.Derivative("2x^2 + 3x + 4", "x").ToString()) "4 * x + 3" "Derivative(2x^2 + 3x + 4 with x) = 4 * x + 3"
            }

            //Test "MathJS Chaining" {
            //    let chain = MathJS.Math.Chain()
            //    equalMsg (MathJS.Math.Chain().Add(4).Multiply(2).Done()) 14 ""
            //}

        }

#if ZAFIR
    let RunTests() =
        Runner.RunTests [
            Tests
        ]
#endif

module Site =
    open WebSharper.UI.Next.Server
    open WebSharper.UI.Next.Html

    [<Website>]
    let Main =
        Application.SinglePage (fun ctx ->
            Content.Page(
                Title = "WebSharper.MathJS Tests",
                Body = [
#if ZAFIR
                    client <@ Client.RunTests() @>
#else
                    WebSharper.Testing.Runner.Run [
                        System.Reflection.Assembly.GetExecutingAssembly()
                    ]
                    |> Doc.WebControl
#endif
                ]
            )
        )
