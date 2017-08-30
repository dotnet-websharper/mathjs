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

    let Tests () =

        TestCategory "General" {

            Test "Sanity check" {
                equalMsg (1+2+3) 6 "1 + 2 + 3 = 6"
            }

            Test "MathJS add (float)" {
                equalMsg (MathJS.Math.Add(1., 2., 3.)) 6. "MathJS.Math.Add(1., 2., 3.) = 6"
            }

            
            Test "MathJS add (fraction)" {
                let a = MathJS.Math.Fraction(0.1)
                let b = MathJS.Math.Fraction(0.2)
                equalMsg (MathJS.Math.Fraction(a + b)) (MathJS.Math.Fraction(0.3)) "MathJS.Math.Add(.1, .2) = .3"
            }

            Test "MathJS add (int)" {
                equalMsg (MathJS.Math.Add(1, 2, 3)) 6 "MathJS.Math.Add(1, 2, 3) = 6"
            }

            Test "MathJS add (unit)" {
                let a = MathJS.Math.Unit("5 cm")
                let b = MathJS.Math.Unit("10 cm")
                let c = MathJS.Math.Unit("15 cm")
                approxEqualMsg (MathJS.Math.Add(a, b).ToNumeric("cm")) (c.ToNumeric("cm")) "MathJS.Add(5 cm + 10 cm) = 15 cm"
            }

            Test "MathJS add (complex)" {
                let a = Complex(1., 1.)
                let b = Complex(1., 1.)
                let c = MathNumber((a + b))
                equalMsg (MathJS.Math.Add(MathNumber(a), MathNumber(b))) c "MathJS.Math.Add(Complex(1., 1.), Complex(1., 1.)) = Complex(2., 2.)"
            }

            //Test "MathJS multiply (complex)" {
            //    let a = Complex(1., 1.)
            //    let b = Complex(1., 1.)
            //    let c = MathNumber(a*b)
            //    equalMsg (MathJS.Math.Multiply(MathNumber(a), MathNumber(b))) c "a*b"
            //}

            //Test "MathJS multiply (bigint)" {
            //    let a = BigInteger(100)
            //    let b = BigInteger(100)
            //    let c = MathNumber((a * b))
            //    equalMsg (MathJS.Math.Multiply(MathNumber(a), MathNumber(b))) c "a*b"
            //}

            Test "MathJS add (bignum)" {
                let a = BigInteger(100)
                let b = BigInteger(200)
                let c = MathNumber((a + b))
                equalMsg (MathJS.Math.Add(MathNumber(a), MathNumber(b))) c "MathJS.Math.Add(BigNumber(100), BigNumber(200)) = BigNumber(300)"
            }

            Test "MathJS Complex" {
                isTrueMsg ((MathNumber(MathJS.Math.Complex("2.0 + 6.0i"))).JS.Equals(MathNumber(MathJS.Math.Complex(2., "6.")))) "Complex(\"2.0 + 6.0i\") = Complex(2., \"6.\")"
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

            Test "MathJS Chaining" {
                let chain = MathJS.Math.Chain(4.).Add(5.).Multiply(10.).Done().ValueOf()
                equalMsg chain (90. :> obj) "Chain(4).Add(5).Mulitply(10) = 90"
            }

            Test "MathJS Expressions" {
                equalMsg (MathJS.Math.Eval("sqrt(3^2 + 4^2)").ToString()) "5" "Eval(sqrt(3^2 + 4^2)) = 5"
                equalMsg (MathJS.Math.Eval("2 inch to cm").ToString()) "5.08 cm" "Eval(2 inch to cm) = 5.08 cm"
            }

            Test "MathJS Det" {
                equalMsg (MathJS.Math.Det(MathNumber([| [| 2.; 1. |]; [| 1.; 2. |] |]))) 3. "MathJS.Math.Det([| [| 2.; 1. |]; [| 1.; 2. |] |]) = 3."
            }

            Test "MathJS Eval with Scope" {
                let scope = New ["a", 3. :> obj; "b", 4. :> obj]
                equalMsg (MathJS.Math.Eval("a * b", scope).ToString()) "12" "Eval(a * b where a = 3, b = 4) = 12"
            }

            Test "MathJS factorial" {
                equalMsg (MathJS.Math.Factorial(5.)) 120. "5! = 120"
            }

            Test "MathJS dot product" {
                let a = MathNumber([| 2.; 4.; 1. |])
                let b = MathNumber([| 2.; 2.; 3. |])
                equalMsg (MathJS.Math.Dot(a, b)) 15. "Dot([2,4,1], [2,2,3]) = 15"
                equalMsg (MathJS.Math.Multiply(a, b)) (MathNumber(15.)) "Multiply([2,4,1], [2,2,3]) = 15"
            }

            Test "MathJS cross procudt" {
                let a = MathNumber([| [| 1.; 2.; 3. |] |])
                let b = MathNumber([| [| 4. |]; [| 5. |]; [| 6. |] |])
                equalMsg (MathJS.Math.Cross(a, b)) (MathNumber([| [| -3.; 6.; -3. |] |])) "Cross([[1,2,3]],[[4],[5],[6]]) = [[-3,6,-3]]"
            }

            Test "MathJS insanity check" {
                equalMsg (MathJS.Math.Add(MathNumber("5"), MathNumber(1.2), MathNumber(true))) (MathNumber(7.2)) "Add(\"5\", 1.2, true) = 7.2"
            }

            Test "Decimal sanity check" {
                equal ((0.1m).ToString()) "0.1"
                equalMsg ((0.1m + 0.2m).ToString()) "0.3" "0.1m + 0.2m = 0.3m"
                equalMsg (1m * 1m) 1m "1m * 1m = 1m"
            }

            let createConstituentCtorDesc (low, mid, high, (isNeg: bool), (scale: byte)) (value: decimal) =
                sprintf "decimal(%i,%i,%i,%b,%i) = %s" low mid high isNeg (int scale) (value.ToString())

            Test "Decimal constituent constructor test" {
                equalMsg
                    (System.Decimal(0,0,0,false,0uy))
                    0m
                    (createConstituentCtorDesc (0,0,0,false,0uy) 0m)
                equalMsg
                    (System.Decimal(0,0,0,false,27uy))
                    0m
                    (createConstituentCtorDesc (0,0,0,false,27uy) 0m)
                equalMsg
                    (System.Decimal(0,0,0,true,0uy))
                    0m
                    (createConstituentCtorDesc (0,0,0,true,0uy) 0m)
                equalMsg
                    (System.Decimal(1000000000,0,0,false,0uy))
                    1000000000m
                    (createConstituentCtorDesc (1000000000,0,0,false,0uy) 1000000000m)
                equalMsg
                    (System.Decimal(0,1000000000,0,false,0uy))
                    4294967296000000000m
                    (createConstituentCtorDesc (0,1000000000,0,false,0uy) 4294967296000000000m)
                equalMsg
                    (System.Decimal(0,0,1000000000,false,0uy))
                    18446744073709551616000000000m
                    (createConstituentCtorDesc (0,0,1000000000,false,0uy) 0m)
                equalMsg
                    (System.Decimal(1000000000,1000000000,1000000000,false,0uy))
                    18446744078004518913000000000m
                    (createConstituentCtorDesc (1000000000,1000000000,1000000000,false,0uy) 18446744073709551616000000000m)
                equalMsg
                    (System.Decimal(-1,-1,-1,false,0uy))
                    79228162514264337593543950335m
                    (createConstituentCtorDesc (-1,-1,-1,false,0uy) 79228162514264337593543950335m)
                equalMsg
                    (System.Decimal(-1,-1,-1,true,0uy))
                    -79228162514264337593543950335m
                    (createConstituentCtorDesc (-1,-1,-1,true,0uy) -79228162514264337593543950335m)
                equalMsg
                    (System.Decimal(-1,-1,-1,false,15uy))
                    79228162514264.337593543950335m
                    (createConstituentCtorDesc (-1,-1,-1,false,15uy) 79228162514264.337593543950335m)
                equalMsg
                    (System.Decimal(-1,-1,-1,false,15uy).ToString())
                    "79228162514264.337593543950335"
                    ((createConstituentCtorDesc (-1,-1,-1,false,15uy) 79228162514264.337593543950335m) + " (as string)")
                equalMsg
                    (System.Decimal(-1,-1,-1,false,28uy))
                    7.9228162514264337593543950335m
                    (createConstituentCtorDesc (-1,-1,-1,false,28uy) 7.9228162514264337593543950335m)
                equalMsg
                    (System.Decimal(2147483647,0,0,false,18uy))
                    0.000000002147483647m
                    (createConstituentCtorDesc (2147483647,0,0,false,18uy) 0.000000002147483647m)
                equalMsg
                    (System.Decimal(2147483647,0,0,false,28uy))
                    0.0000000000000000002147483647m
                    (createConstituentCtorDesc (2147483647,0,0,false,28uy) 0.0000000000000000002147483647m)
                equalMsg
                    (System.Decimal(2147483647,0,0,true,28uy))
                    -0.0000000000000000002147483647m
                    (createConstituentCtorDesc (2147483647,0,0,true,28uy) -0.0000000000000000002147483647m)
            }

            let createInt32ArrayCtorDesc (bits: int32[]) (expected:decimal) =
                sprintf "decimal([|0x%X; 0x%X; 0x%X; 0x%X |]) = %s" (bits.[0]) (bits.[1]) (bits.[2]) (bits.[3]) (expected.ToString())

            Test "Decimal int32[] ctor test" {
                equalMsg
                    (System.Decimal([| 0x0; 0x0; 0x0; 0x0 |]))
                    0m
                    (createInt32ArrayCtorDesc [| 0x0; 0x0; 0x0; 0x0 |] 0m)
                equalMsg
                    (System.Decimal([| 0x3B9ACA00; 0x0; 0x0; 0x0 |]))
                    1000000000m
                    (createInt32ArrayCtorDesc [| 0x3B9ACA00; 0x0; 0x0; 0x0 |] 1000000000m)
                equalMsg
                    (System.Decimal([| 0x0; 0x3B9ACA00; 0x0; 0x0 |]))
                    4294967296000000000m
                    (createInt32ArrayCtorDesc [| 0x0; 0x3B9ACA00; 0x0; 0x0 |] 4294967296000000000m)
                equalMsg
                    (System.Decimal([| 0x0; 0x0; 0x3B9ACA00; 0x0 |]))
                    18446744073709551616000000000m
                    (createInt32ArrayCtorDesc [| 0x0; 0x0; 0x3B9ACA00; 0x0 |] 18446744073709551616000000000m)
                equalMsg
                    (System.Decimal([| 0xFFFFFFFF; 0xFFFFFFFF; 0xFFFFFFFF; 0x0 |]))
                    79228162514264337593543950335m
                    (createInt32ArrayCtorDesc [| 0xFFFFFFFF; 0xFFFFFFFF; 0xFFFFFFFF; 0x0 |] 79228162514264337593543950335m)
                equalMsg
                    (System.Decimal([| 0xFFFFFFFF; 0xFFFFFFFF; 0xFFFFFFFF; 0x80000000 |]))
                    -79228162514264337593543950335m
                    (createInt32ArrayCtorDesc [| 0xFFFFFFFF; 0xFFFFFFFF; 0xFFFFFFFF; 0x80000000 |] -79228162514264337593543950335m)
                equalMsg
                    (System.Decimal([| 0xFFFFFFFF; 0x0; 0x0; 0x100000 |]))
                    0.0000004294967295m
                    (createInt32ArrayCtorDesc [| 0xFFFFFFFF; 0x0; 0x0; 0x100000 |] 0.0000004294967295m)
                equalMsg 
                    (System.Decimal([| 0xFFFFFFFF; 0x0; 0x0; 0x1C0000 |]))
                    0.0000000000000000004294967295m
                    (createInt32ArrayCtorDesc [| 0xFFFFFFFF; 0x0; 0x0; 0x1C0000 |] 0.0000000000000000004294967295m)
                equalMsg
                    (System.Decimal([| 0xF0000; 0xF0000; 0xF0000; 0xF0000 |]))
                    18133887298.441562272235520m
                    (createInt32ArrayCtorDesc [| 0xF0000; 0xF0000; 0xF0000; 0xF0000 |] 18133887298.441562272235520m)
            }

        }

#if ZAFIR
    let RunTests() =
        Runner.RunTests [
            Tests ()
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
