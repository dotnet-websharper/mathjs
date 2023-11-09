# WebSharper.MathJs

~~Now the MathJS Extension is a part of the [WebSharper core repository](https://github.com/dotnet-websharper/core).~~

Update: As of Nov 9, 2023, this repo is un-archived and will soon contain the updated, npm-based MathJs bindings for WebSharper.

The rationale for moving the MathJs bindings out of the [core](https://github.com/dotnet-websharper/core) repository and back into its own, is that WebSharper 7 is now able to extend type-driven translation via modular, ESM-based bindings. This means that for the MathJs use cases, high-precision values (`decimal`, etc.) can be used in RPCs and other server-client data exchanges by adding `WebSharper.MathJs` as a dependency, and otherwise the compiler will warn on usage, as expected.
