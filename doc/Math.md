# Math in WebSharper

Higher level math is supported in WebSharper via the [MathJS](http://mathjs.org/) and [MathJax](https://www.mathjax.org/) JavaScript libraries. The usage of these libraries are the same as in JavaScript with some small changes. JavaScript doesn't mind letting any type in any function, but in WebSharper to use more than one type in a function we have to use the MathNumber wrapper.

A little example:

In JavaScript:
```javascript
math.add("5", 1.7, true);
```

In WebSharper:
```fsharp
Math.Add(MathNumber("5"), MathNumber(1.7), MathNumber(true))
```

Exception is when you only use either only floats, ints, or Math.Units
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

F# already supports BigInteger (System.Numerics.BigInteger or bigint), but JavaScript does not by default. With WebSharper the usage of these types and their operators are just as easy as working with integers.

Constructing BigInteger:

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

<div style="width:100%;min-height:500px;position:relative"><iframe style="position:absolute;border:none;width:100%;height:100%;overflow:hidden" src="http://test2.try.websharper.com/embed/setr/0000DE"></iframe><div>

---

## Complex number

Just like BigInteger, Complex is a member of System.Numerics too, but JavaScript does not support them. To use the Complex type in our program we could construct it as we're used to it from .Net, but now we're able to do it with Math.Complex() too which is able to construct a Complex number by taking a string with the complex value.

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

<div style="width:100%;min-height:500px;position:relative"><iframe style="position:absolute;border:none;width:100%;height:100%" src="http://test2.try.websharper.com/embed/setr/0000DH"></iframe><div>

---

## Fraction

The original float type in JavaScript has limitations with its precision, but it's solved with the Fraction type which has a much higher precision with its operations. To use this new Fraction, we have to call the Math.Fraction() constructor.

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

<div style="width:100%;min-height:500px;position:relative"><iframe style="position:absolute;border:none;width:100%;height:100%" src="http://test2.try.websharper.com/embed/setr/0000DJ"></iframe><div>

---

## Vectors

For vector operations in WebSharper we have to use the MathJS.Math functions and in those functions we have to use the MathNumber wrapper for the vectors.

```fsharp
open WebSharper.MathJS

let myVector = [| 1.; 2.; 3. |]

let addVector = Math.Add(MathNumber(myVector), MathNumber(myVector))
```

<div style="width:100%;min-height:500px;position:relative"><iframe style="position:absolute;border:none;width:100%;height:100%" src="http://test2.try.websharper.com/embed/setr/0000Di"></iframe><div>

---

## Matrices

The same as for vectors, WebSharper grants a huge variety of Matrix operations and functions, but to use those, we need to wrap the matrices in MathNumber.

```fsharp
open WebSharper.MathJS

let myMatrix = [| [| 1.; 2. |]; [| 3.; 4. |] |]

let addMatrix = Math.Add(MathNumber(myMatrix), MathNumber(myMatrix))
```

<div style="width:100%;min-height:450px;position:relative"><iframe style="position:absolute;border:none;width:100%;height:100%" src="http://test2.try.websharper.com/embed/setr/0000DM"></iframe><div>

---

## Units

WebSharper allows you to calculate with units too. Most of the functions from Math accept Units to work with. 

```fsharp
open WebSharper.MathJS

//With a value and a unit
let myUnit = Math.Unit(5, "cm")

//Or simply by a string
let myUnitFromString = Math.Unit("5 cm")
```

<div style="width:100%;min-height:500px;position:relative"><iframe style="position:absolute;border:none;width:100%;height:100%" src="http://test2.try.websharper.com/embed/setr/0000DO"></iframe><div>

---

## An example for expressions

Calculation of expressions are not hard either. In this example we use the Math.Derivative with a TeX rendered output.

<div style="width:100%;min-height:400px;position:relative"><iframe style="position:absolute;border:none;width:100%;height:100%" src="http://test2.try.websharper.com/embed/setr/0000Cy"></iframe><div>

---

## Rendering expresions

There are many ways to render your expression on the screen with WebSharper. To do that we have to use MathJax as the following examples show:

### TeX

<div style="width:100%;min-height:300px;position:relative"><iframe style="position:absolute;border:none;width:100%;height:100%" src="http://test2.try.websharper.com/embed/setr/0000DN"></iframe><div>

### MathML

<div style="width:100%;min-height:450px;position:relative"><iframe style="position:absolute;border:none;width:100%;height:100%" src="http://test2.try.websharper.com/embed/setr/0000Dy"></iframe><div>

### Ascii Math

<div style="width:100%;min-height:300px;position:relative"><iframe style="position:absolute;border:none;width:100%;height:100%" src="http://test2.try.websharper.com/embed/setr/0000E1"></iframe><div>
