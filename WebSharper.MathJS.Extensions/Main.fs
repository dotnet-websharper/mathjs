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