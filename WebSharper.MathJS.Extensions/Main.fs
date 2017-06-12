namespace WebSharper.MathJS.Extensions

open WebSharper
open WebSharper.JavaScript
open System.Numerics

module Extensions =
    
    [<Proxy(typeof<System.Numerics.Complex>)>]
    type private Complex =

        [<Inline "math.complex($r, $i)">]
        new (r : float, i : float) = {}

        member this.Imaginary
            with [<Inline "$this.im">] get () = X<float>

        member this.Magnitude
            with [<Inline "math.abs($this)">] get () = X<float>

        member this.Phase
            with [<Inline "math.atan2($this.im, $this.re)">] get () = X<float>

        member this.Real
            with [<Inline "$this.re">] get () = X<float>

        [<Inline "math.abs($c)">]
        static member Abs(c : Complex) = X<float>

        [<Inline "math.acos($c)">]
        static member Acos(c : Complex) = X<float>

        [<Inline "math.add($c1, $c2)">]
        static member Add(c1 : Complex, c2 : Complex) = X<Complex>

        [<Inline "math.asin($c)">]
        static member Asin(c : Complex) = X<float>

        [<Inline "math.atan($c)">]
        static member Atan(c : Complex) = X<float>

        [<Inline "math.conj($c)">]
        static member Conjugate(c : Complex) = X<Complex>

        [<Inline "math.cos($c)">]
        static member Cos(c : Complex) = X<float>

        [<Inline "math.cosh($c)">]
        static member Cosh(c : Complex) = X<float>

        [<Inline "math.divide($c1, $c2)">]
        static member Divide(c1 : Complex, c2 : Complex) = X<Complex>

        [<Inline "$this.equals($c)">]
        static member Equals(c : Complex) = X<bool>

        [<Inline "math.exp(c)">]
        static member Exp(c : Complex) = X<float>

        [<Inline "math.complex.fromPolar($r, $i)">]
        static member FromPolarCoordinates(r : float, i : float) = X<Complex>

        [<Inline "math.log($c)">]
        static member Log(c : Complex) = X<float>

        [<Inline "math.log($c, $b)">]
        static member Log(c : Complex, b : int) = X<float>

        [<Inline "math.log10($c)">]
        static member Log10(c : Complex) = X<float>

        [<Inline "math.multiply($c1, $c2)">]
        static member Multiply(c1 : Complex, c2 : Complex) = X<Complex>

        [<Inline "math.unaryMinus($c)">]
        static member Negate(c : Complex) = X<Complex>

        [<Inline "math.pow($c1, $c2)">]
        static member Pow(c1 : Complex, c2 : Complex) = X<Complex>

        [<Inline "math.pow($c, $d)">]
        static member Pow(c : Complex, d : float) = X<Complex>

        [<Inline "math.sin($c)">]
        static member Sin(c : Complex) = X<Complex>

        [<Inline "math.sinh($c)">]
        static member Sinh(c : Complex) = X<Complex>

        [<Inline "math.sqrt($c)">]
        static member Sqrt(c : Complex) = X<Complex>

        [<Inline "math.subtract($c1, $c2)">]
        static member Subtract(c1 : Complex, c2 : Complex) = X<Complex>

        [<Inline "math.tan($c)">]
        static member Tan(c : Complex) = X<Complex>

        [<Inline "math.tanh($c)">]
        static member Tanh(c : Complex) = X<Complex>

        [<Inline "$this.toString()">]
        override x.ToString() = X<string>

        [<Inline "$this.format($n)">]
        member x.ToString(n : string) = X<string>

        [<Inline "math.complex(0, 1)">]
        static member ImaginaryOne = X<Complex>

        [<Inline "math.complex(1, 0)">]
        static member One = X<Complex>

        [<Inline "math.complex(0, 0)">]
        static member Zero = X<Complex>

        [<Inline "math.add($c1, $c2)">]
        static member Addition(c1 : Complex, c2 : Complex) = X<Complex>

        [<Inline "math.divide($c1, $c2">]
        static member Division(c1 : Complex, c2 : Complex) = X<Complex>

        [<Inline "$c1.equals($c2)">]
        static member Equality(c1 : Complex, c2 : Complex) = X<bool>

        [<Inline "math.complex($i)">]
        static member Explicit(i : bigint) = X<Complex>

        [<Inline "!$c1.equals($c2)">]
        static member Inequality(c1 : Complex, c2 : Complex) = X<bool>

        [<Inline "math.multiply($c1, $c2)">]
        static member Mulitply(c1 : Complex, c2 : Complex) = X<Complex>

        [<Inline "math.unaryMinus($c)">]
        static member UnaryNegation(c : Complex) = X<Complex>

