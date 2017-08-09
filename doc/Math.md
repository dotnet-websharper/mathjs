# Math in WebSharper

Higher level math is supported in WebSharper via the [MathJS](http://mathjs.org/) and [MathJax](https://www.mathjax.org/) JavaScript libraries. The usage of these libraries are the same as in `JavaScript` with some small changes. `JavaScript` would let you use any type in any function, but in WebSharper to use more than one type in a function we have to use the `MathNumber` wrapper.

A little example:

In `JavaScript`:
```javascript
math.add("5", 1.7, true);
```

In `WebSharper`:
```fsharp
Math.Add(MathNumber("5"), MathNumber(1.7), MathNumber(true))
```

Exception is when you use either only `floats`, `ints`, or `Math.Units`
```fsharp
//Only floats
Math.Add(1., 2., 3.)

//Only ints
Math.Add(1, 2, 3)

//Only Units
Math.Add(Math.Unit("1 cm"), Math.Unit("2 cm"), Math.Unit("3 cm"))
```

---

## Bignumbers

F# already supports `BigInteger` (`System.Numerics.BigInteger` or `bigint`), but `JavaScript` does not by default. With WebSharper the usage of these types and their operators are just as easy as working with integers.

Constructing `BigInteger`:

```fsharp
open System.Numerics
open WebSharper.MathJS

//The .Net way
let myBignum = bigint 100

//A new way
let myBignumFromString = Math.Bignumber("100")
```

Operations with these numbers are possible with the .Net way:
```fsharp
let addBignum = myBignum + myBignum //Math.Add(MathNumber(myBigint), MathNumber(myBigint))
```

<div style="width:100%;min-height:500px;position:relative"><iframe style="position:absolute;border:none;width:100%;height:100%;overflow:hidden" src="https://try.websharper.com/embed/setr/0000E3"></iframe></div>

---

## Complex number

Just like `BigInteger`, `Complex` is a member of `System.Numerics` too, but `JavaScript` does not support them. To use the `Complex` type in our program we could construct it as we're used to it from .Net, but now we're able to do it with `Math.Complex()` too which is able to construct a `Complex` number by taking a string with the complex value.

```fsharp
open System.Numerics
open WebSharper.MathJS

//The .Net way
let myComplex = Complex(1., 1.)

//A new way
let myComplexFromString = Math.Complex("1 + 1i")
```

After constructing the numbers, we can use them as we're used to it:
```fsharp
let addComplex = myComplex + myComplex //Math.Add(MathNumber(myComplex), MathNumber(myComplex))
```

<div style="width:100%;min-height:500px;position:relative"><iframe style="position:absolute;border:none;width:100%;height:100%" src="https://try.websharper.com/embed/setr/0000Ds"></iframe></div>

---

## Fraction

The original `float` type in `JavaScript` has limitations with its precision, but it's solved with the `Math.Fraction` type which has a much higher precision with its operations. To use this new `Math.Fraction`, we have to call the `Math.Fraction()` constructor.

We have many ways to create a Fraction, for example:

```fsharp
open WebSharper.MathJS

//From string
let fraction1 = Math.Fraction("1/2")

//By giving the numerator and denominator
let fraction2 = Math.Fraction(1, 2)

//From float
let fraction3 = Math.Fraction(0.5)
```

After 

<div style="width:100%;min-height:500px;position:relative"><iframe style="position:absolute;border:none;width:100%;height:100%" src="https://try.websharper.com/embed/setr/0000Dt"></iframe></div>

---

## Vectors

For vector operations in WebSharper we have to use the `MathJS.Math` functions and in those functions we have to use the `MathNumber` wrapper for the vectors. There are few exceptions when we don't have to wrap these vectors. If the function only accepts vectors or matrices, then the wrapper isn't needed (but can be used). (Note that if you wrap these in `MathNumber`, you might get a `MathNumber` return value.)

```fsharp
open WebSharper.MathJS

let myVector = [| 1.; 2.; 3. |]

let addVector = Math.Add(MathNumber(myVector), MathNumber(myVector))
```

<div style="width:100%;min-height:500px;position:relative"><iframe style="position:absolute;border:none;width:100%;height:100%" src="https://try.websharper.com/embed/setr/0000Dz"></iframe></div>

---

## Matrices

The same as for vectors, WebSharper grants a huge variety of Matrix operations and functions, but to use those, we need to wrap the matrices in MathNumber. As we have seen at the vectors, there are some functions where matrices can be used without the `MathNumber` wrapper. (Note that if you wrap these in `MathNumber`, you might get a `MathNumber` return value.)

```fsharp
open WebSharper.MathJS

let myMatrix = [| [| 1.; 2. |]; [| 3.; 4. |] |]

let addMatrix = Math.Add(MathNumber(myMatrix), MathNumber(myMatrix))
```

<div style="width:100%;min-height:450px;position:relative"><iframe style="position:absolute;border:none;width:100%;height:100%" src="https://try.websharper.com/embed/setr/0000Dy"></iframe></div>

---

## Units

WebSharper allows you to calculate with units too. Most of the functions from Math accept `Math.Unit`s to work with. Units are a special kind of types. They have a value and a measurement. Values with different kind of measurements can be used in operations and it will calulate with the given measurements. Units can be freed from their measurements (for example) by dividing.  

```fsharp
open WebSharper.MathJS

//With a value and a unit
let myUnit = Math.Unit(5, "cm")

//Or simply by a string
let myUnitFromString = Math.Unit("5 cm")
```

<div style="width:100%;min-height:500px;position:relative"><iframe style="position:absolute;border:none;width:100%;height:100%" src="https://try.websharper.com/embed/setr/0000Dr"></iframe></div>

---

## An example for expressions

There are many functions in `Math.` that calculates an expression, solves a problem. In this example we'll use the `Math.Derivative` function to get a `Node` with the result in it. A `Node` then can be converted to a `String`, but with the [MathJax extension](https://github.com/intellifactory/websharper.mathjax/blob/master/doc/doc.md) we can render the result. To do that we have to set up `MathJax` to parse and render `TeX` formulas then by using the `Node`'s `ToTex()` function we convert the result into a `String` with the formula in `TeX` formatting.

(Most of the functions don't result a `Node`, but they can be converted to `Node` by `Math.Parse()` or by other means. ([MathJax documentation](https://mathjax.org))

<div style="width:100%;min-height:400px;position:relative"><iframe style="position:absolute;border:none;width:100%;height:100%" src="https://try.websharper.com/embed/setr/0000E2"></iframe></div>