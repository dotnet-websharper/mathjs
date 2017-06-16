namespace WebSharper.MathJS.Extensions

open WebSharper
open WebSharper.JavaScript
open WebSharper.MathJS
open System.Numerics

[<Require(typeof<WebSharper.MathJS.Resources.Js>)>]    
module internal Extensions =
    
    [<Proxy(typeof<System.Numerics.Complex>)>]
    type ComplexProxy =

        [<Inline "math.complex($r, $i)">]
        new (r : float, i : float) = {}

        member this.Imaginary
            with [<Inline "$this.im">] get () = X<float>

        //member this.Magnitude
        //    with [<Inline>] get () = Math.Abs(this)

        member this.Magnitude
            with [<Inline "math.abs($this)">] get () = X<float>

        member this.Phase
            with [<Inline "math.atan2($this.im, $this.re)">] get () = X<float>

        member this.Real
            with [<Inline "$this.re">] get () = X<float>

        [<Inline "math.abs($c)">]
        static member Abs(c : Complex) = X<float>

        [<Inline "math.acos($c)">]
        static member Acos(c : Complex) = X<Complex>

        [<Inline "math.add($c1, $c2)">]
        static member Add(c1 : Complex, c2 : Complex) = X<Complex>

        [<Inline "math.asin($c)">]
        static member Asin(c : Complex) = X<Complex>

        [<Inline "math.atan($c)">]
        static member Atan(c : Complex) = X<Complex>

        [<Inline "math.conj($c)">]
        static member Conjugate(c : Complex) = X<Complex>

        [<Inline "math.cos($c)">]
        static member Cos(c : Complex) = X<Complex>

        [<Inline "math.cosh($c)">]
        static member Cosh(c : Complex) = X<Complex>

        [<Inline "math.divide($c1, $c2)">]
        static member Divide(c1 : Complex, c2 : Complex) = X<Complex>

        [<Inline "$this.equals($c)">]
        static member Equals(c : Complex) = X<bool>

        [<Inline "math.exp(c)">]
        static member Exp(c : Complex) = X<Complex>

        [<Inline "math.complex.fromPolar($r, $i)">]
        static member FromPolarCoordinates(r : float, i : float) = X<Complex>

        [<Inline "math.log($c)">]
        static member Log(c : Complex) = X<Complex>

        [<Inline "math.log($c, $b)">]
        static member Log(c : Complex, b : float) = X<Complex>

        [<Inline "math.log10($c)">]
        static member Log10(c : Complex) = X<Complex>

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
        static member ToString() = X<string>

        [<Inline "$this.format($n)">]
        member x.ToString(n : string) = X<string>

        [<Inline "math.complex(0, 1)">]
        static member ImaginaryOne = X<Complex>

        [<Inline "math.complex(1, 0)">]
        static member One = X<Complex>

        [<Inline "math.complex(0, 0)">]
        static member Zero = X<Complex>

        [<Inline "math.add($c1, $c2)">]
        static member op_Addition(c1 : Complex, c2 : Complex) = X<Complex>

        [<Inline "math.divide($c1, $c2)">]
        static member op_Division(c1 : Complex, c2 : Complex) = X<Complex>

        [<Inline "$c1.equals($c2)">]
        static member op_Equality(c1 : Complex, c2 : Complex) = X<bool>

        [<Inline "math.complex($i)">]
        static member op_Explicit(i : bigint) = X<Complex>

        [<Inline "!$c1.equals($c2)">]
        static member op_Inequality(c1 : Complex, c2 : Complex) = X<bool>

        [<Inline "math.multiply($c1, $c2)">]
        static member op_Multiply(c1 : Complex, c2 : Complex) = X<Complex>

        [<Inline "math.subtract($c1, $c2)">]
        static member op_Subtraction(c1 : Complex, c2 : Complex) = X<Complex>

        [<Inline "math.unaryMinus($c)">]
        static member op_UnaryNegation(c : Complex) = X<Complex>

    [<Proxy(typeof<System.Numerics.BigInteger>)>]
    type BigIntegerProxy =

        [<Inline "math.bignumber($v)">]
        new (v : byte[]) = {}

        [<Inline "math.bignumber($v)">]
        new (v : decimal) = {}

        [<Inline "math.bignumber($v)">]
        new (v : double) = {}

        [<Inline "math.bignumber($v)">]
        new (v : int32) = {}

        [<Inline "math.bignumber($v)">]
        new (v : int64) = {}

        [<Inline "math.bignumber($v)">]
        new (v : single) = {}

        [<Inline "math.bignumber($v)">]
        new (v : uint32) = {}

        [<Inline "math.bignumber($v)">]
        new (v : uint64) = {}

        [<Inline "math.abs($n)">]
        static member Abs(n : bigint) = X<bigint>

        [<Inline "math.add($n1, $n2)">]
        static member Add(n1 : bigint, n2 : bigint) = X<bigint>

        [<Inline "math.compare($n1, $n2)">]
        static member Compare(n1 : bigint, n2 : bigint) = X<int>

        [<Inline "math.compare($this, $n)">]
        static member CompareTo(n : bigint) = X<int>

        [<Inline "math.compare($this, $n)">]
        static member CompareTo(n : int64) = X<int>

        [<Inline "math.compare($this, $n)">]
        static member CompareTo(n : obj) = X<int>

        [<Inline "math.compare($this, $n)">]
        static member CompareTo(n : uint64) = X<int>

        [<Inline "math.divide($n1, $n2)">]
        static member Divide(n1 : bigint, n2 : bigint) = X<bigint>

        [<Inline "math.equal($this, $n)">]
        static member Equals(n : bigint) = X<bool>
        
        [<Inline "math.equal($this, $n)">]
        static member Equals(n : int64) = X<bool>
        
        [<Inline "math.equal($this, $n)">]
        static member Equals(n : obj) = X<bool>
        
        [<Inline "math.equal($this, $n)">]
        static member Equals(n : uint64) = X<bool>

        [<Inline "math.gcd($n1, $n2)">]
        static member GreatestCommonDivisor(n1 : bigint, n2 : bigint) = X<bigint>

        [<Inline "math.log($n)">]
        static member Log(n : bigint) = X<float>

        [<Inline "math.log($n, $b)">]
        static member Log(n : bigint, b : float) = X<float>

        [<Inline "math.log10($n)">]
        static member Log10(n : bigint) = X<float>

        [<Inline "math.max($n1, $n2)">]
        static member Max(n1 : bigint, n2 : bigint) = X<bigint>

        [<Inline "math.min($n1, $n2)">]
        static member Min(n1 : bigint, n2 : bigint) = X<bigint>

        [<Inline "math.mod(math.pow($v, $e), $m)">]
        static member ModPow(v : bigint, e : bigint, m : bigint) = X<bigint>

        [<Inline "math.multiply($n1, $n2)">]
        static member Mulitply(n1 : bigint, n2 : bigint) = X<bigint>

        [<Inline "math.bignumber($s)">]
        static member Parse(s : string) = X<bigint>

        [<Inline "math.pow($n1, $n2)">]
        static member Pow(n1 : bigint, n2 : int32) = X<bigint>

        [<Inline "math.mod($n1, $n2)">]
        static member Remainder(n1 : bigint, n2 : bigint) = X<bigint>

        [<Inline "math.subtract($n1, $n2)">]
        static member Subtract(n1 : bigint, n2 : bigint) = X<bigint>

        [<Inline "math.format($this)">]
        static member ToString() = X<string>
        
        [<Inline "math.add($n1, $n2)">]
        static member op_Addition(n1 : bigint, n2 : bigint) = X<bigint>

        [<Inline "math.bitAnd($n1, $n2)">]
        static member op_BitwiseAnd(n1 : bigint, n2 : bigint) = X<bigint>

        [<Inline "math.bitOr($n1, $n2)">]
        static member op_BitwiseOr(n1 : bigint, n2 : bigint) = X<bigint>

        [<Inline "math.divide($n1, $n2)">]
        static member op_Division(n1 : bigint, n2 : bigint) = X<bigint>

        [<Inline "math.equal($n1, $n2)">]
        static member op_Equality(n1 : bigint, n2 : bigint) = X<bool>

        [<Inline "math.equal($n1, $n2)">]
        static member op_Equality(n1 : bigint, n2 : int64) = X<bool>

        [<Inline "math.equal($n1, $n2)">]
        static member op_Equality(n1 : bigint, n2 : uint64) = X<bool>

        [<Inline "math.equal($n1, $n2)">]
        static member op_Equality(n1 : int64, n2 : bigint) = X<bool>

        [<Inline "math.equal($n1, $n2)">]
        static member op_Equality(n1 : uint64, n2 : bigint) = X<bool>

        [<Inline "math.bitxor($n1, $n2)">]
        static member op_ExclusiveOr(n1 : bigint, n2 : bigint) = X<bigint>

        [<Inline "!math.equal($n1, $n2)">]
        static member op_Inequality(n1 : bigint, n2 : bigint) = X<bool>

        [<Inline "!math.equal($n1, $n2)">]
        static member op_Inequality(n1 : bigint, n2 : int64) = X<bool>

        [<Inline "!math.equal($n1, $n2)">]
        static member op_Inequality(n1 : bigint, n2 : uint64) = X<bool>

        [<Inline "!math.equal($n1, $n2)">]
        static member op_Inequality(n1 : int64, n2 : bigint) = X<bool>

        [<Inline "!math.equal($n1, $n2)">]
        static member op_Inequality(n1 : uint64, n2 : bigint) = X<bool>

        [<Inline "math.mod($n1, $n2)">]
        static member op_Modulus(n1 : bigint, n2 : bigint) = X<bigint>

        [<Inline "math.multiply($n1, $n2)">]
        static member op_Multiply(n1 : bigint, n2 : bigint) = X<bigint>

        [<Inline "math.subtract($n1, $n2)">]
        static member op_Subtraction(n1 : bigint, n2 : bigint) = X<bigint>

        [<Inline "math.unaryMinus($n)">]
        static member op_UnaryNegation(n : bigint) = X<bigint>

        [<Inline "math.unaryPlus($n)">]
        static member op_UnaryPlus(n : bigint) = X<bigint>