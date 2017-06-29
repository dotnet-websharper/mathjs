# MathJS

[MathJS](http://mathjs.org/) is a powerful math JavaScript library. It allows the usage of big numbers, complex numbers, units, precise fractions, vectors or matrices. In WebSharper using these types and functions are the same with a few exceptions.

## MathNumber

MathJS allows you to use many different types in a single function, but in F# it's harder with the types. To solve this problem we have introduced the `MathNumber` type. `MathNumber` has a constructor with every useable type from MathJS and server as a wrapper for these functions.

### Using MathNumber

To use different types in a function, we have to wrap the parameters in `MathNumber`. For example:

JavaScript:
```js
math.add("5", 1.7, true);
```

WebSharper:
```fsharp
Math.Add(MathNumber("5"), MathNumber(1.7), MathNumber(true))
```

But when we use `MathNumber` as parameter, the return type will be `MathNumber` too.

There are expections when we don't have to use the `MathNumber` wrapper:
- If every parameter is a float or int (most commonly used types in math)
- If every parameter is a Math.Unit type.
- Some functions where only Vectors or Matrices are accepted

## Operations

With MathJS WebSharper allows you to use the `System.Numerics.Biginteger` (`bigint`) and the `System.Numerics.Complex` types' own .Net operations and functions. For example:

```fsharp
//When you'd write
Math.Add(MathNumber(Complex(5., 2.)), MathNumber(Complex(3., 1.)))

//You can just use the (+) operator
Complex(5., 2.) + Complex(3., 1.)

//Or we could use the Complex methods
Complex.Log10(Complex(5., 2.))
```

The same goes for `bigint`.