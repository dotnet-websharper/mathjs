// $begin{copyright}
//
// This file is part of WebSharper
//
// Copyright (c) 2008-2018 IntelliFactory
//
// Licensed under the Apache License, Version 2.0 (the "License"); you
// may not use this file except in compliance with the License.  You may
// obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
// implied.  See the License for the specific language governing
// permissions and limitations under the License.
//
// $end{copyright}

namespace WebSharper

open System.Numerics
open WebSharper
open WebSharper.JavaScript

type M = WebSharper.MathJS.Math

[<Proxy(typeof<Complex>)>]
[<Type "$import(Complex, mathjs)">]
type internal ComplexProxy =

    [<Inline>]
    static member CtorProxy (r: float, i: float) = WebSharper.MathJS.Math.Complex(r, string i)

    member this.Imaginary
        with [<Inline "$this.im">] get () = X<float>

    member this.Real
        with [<Inline "$this.re">] get () = X<float>

    member this.Magnitude
        with [<Inline>] get () = M.Abs (this |> As<Complex>) |> As<float>

    member this.Phase
        with [<Inline>] get () = M.Atan2(this.Imaginary, this.Real)


    [<Inline>]
    static member Abs(c : Complex) = M.Abs (c |> As<WebSharper.MathJS.Complex>) |> As<float>

    [<Inline>]
    static member Acos(c : Complex) = M.Acos (c |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline>]
    static member Add(c1 : Complex, c2 : Complex) =
        M.Add(c1 |> As<WebSharper.MathJS.Complex>, c2 |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline>]
    static member Asin(c : Complex) =
        M.Asin(c |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline>]
    static member Atan(c : Complex) =
        M.Atan(c |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline>]
    static member Conjugate(c : Complex) =
        M.Conj(c |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline>]
    static member Cos(c : Complex) =
        M.Cos(c |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline>]
    static member Cosh(c : Complex) =
        M.Cosh(c |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline>]
    static member Divide(c1 : Complex, c2 : Complex) =
        M.Divide(c1 |> As<WebSharper.MathJS.Complex>, c2 |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline "$this.equals($c)">]
    member this.Equals(c : Complex) = X<bool>

    [<Inline>]
    static member Exp(c : Complex) = M.Exp(c |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline "math.complex.fromPolar($r, $i)">]
    static member FromPolarCoordinates(r : float, i : float) = X<Complex> // TODO

    [<Inline>]
    static member Log(c : Complex) = M.Log(c |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline>]
    static member Log(c : Complex, b : float) = M.Log(c |> As<WebSharper.MathJS.Complex>, b |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline>]
    static member Log10(c : Complex) = M.Log10(c |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline>]
    static member Multiply(c1 : Complex, c2 : Complex) = M.Multiply(c1 |> As<WebSharper.MathJS.Complex>, c2 |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline>]
    static member Negate(c : Complex) = M.UnaryMinus(c |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline>]
    static member Pow(c1 : Complex, c2 : Complex) = M.Pow(c1 |> As<WebSharper.MathJS.Complex>, c2 |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline>]
    static member Pow(c : Complex, d : float) = M.Pow(c |> As<WebSharper.MathJS.Complex>, d |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline>]
    static member Sin(c : Complex) = M.Sin(c |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline>]
    static member Sinh(c : Complex) = M.Sinh(c |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline>]
    static member Sqrt(c : Complex) = M.Sqrt(c |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline>]
    static member Subtract(c1 : Complex, c2 : Complex) = M.Subtract(c1 |> As<WebSharper.MathJS.Complex>, c2 |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline>]
    static member Tan(c : Complex) = M.Tan(c |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline>]
    static member Tanh(c : Complex) = M.Tanh(c |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline "$this.toString()">]
    override this.ToString() = X<string>

    [<Inline "$this.format($n)">]
    member x.ToString(n : string) = X<string>

    [<Inline>]
    static member ImaginaryOne = M.Complex(0, "1")

    [<Inline>]
    static member One = M.Complex(1, "0")

    [<Inline>]
    static member Zero = M.Complex(0, "0")

    [<Inline>]
    static member op_Addition(c1 : Complex, c2 : Complex) = M.Add(c1 |> As<WebSharper.MathJS.Complex>, c2 |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline>]
    static member op_Division(c1 : Complex, c2 : Complex) = M.Divide(c1 |> As<WebSharper.MathJS.Complex>, c2 |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline "$c1.equals($c2)">]
    static member op_Equality(c1 : Complex, c2 : Complex) = X<bool>

    [<Inline>]
    static member op_Explicit(i : bigint) = M.Complex(string i)

    [<Inline "!$c1.equals($c2)">]
    static member op_Inequality(c1 : Complex, c2 : Complex) = X<bool>

    [<Inline>]
    static member op_Multiply(c1 : Complex, c2 : Complex) = M.Multiply(c1 |> As<WebSharper.MathJS.Complex>, c2 |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline>]
    static member op_Subtraction(c1 : Complex, c2 : Complex) = M.Subtract(c1 |> As<WebSharper.MathJS.Complex>, c2 |> As<WebSharper.MathJS.Complex>) |> As<Complex>

    [<Inline>]
    static member op_UnaryNegation(c : Complex) = M.UnaryMinus(c |> As<WebSharper.MathJS.Complex>) |> As<Complex>

