# MathJS

[MathJS](http://mathjs.org/) is a powerful math JavaScript library. It allows the usage of big numbers, complex numbers, units, fractions with high precision, vectors or matrices. In WebSharper using these types and functions are the same with a few exceptions.

## MathNumber

MathJS allows you to use many different types in a single function, but in F# it's harder with the types. To solve this problem we have introduced the `MathNumber` type. `MathNumber` has a constructor with every useable type from MathJS and serves as a wrapper for these functions.
Note that if you use `MathNumber` as parameters, the return type will be wrapped in `MathNumber` as well.

### Supported types in MathNumber()

* float[]
* float[][]
* float
* float32
* System.Numerics.BigInteger
* System.Numerics.Complex
* string
* bool
* int8
* int16
* int32
* int64
* uint8
* uint16
* uint32
* uint64

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

There are exceptions when we don't have to use the `MathNumber` wrapper:

* If every parameter is a `float` or `int` (most commonly used types in math)
* If every parameter is a `Math.Unit` type.
* Some functions where only Vectors or Matrices are accepted

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

The same goes for `bigint` and the other types.

## Chaining

Chaining works the same way as before. The same rules applies with the `MathNumber`, if the chained value is an `int`, `float` or `Math.Unit` then the wrapping isn't needed. `Chain` has every function from the `Math` class.