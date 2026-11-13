// $begin{copyright}
//
// This file is part of WebSharper
//
// Copyright (c) 2008-2026 IntelliFactory
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

open WebSharper
open WebSharper.MathJS

/// Provides a round-trippable string form for math.js values.
[<JavaScript; AutoOpen>]
module MathNumberExtensions =

    type MathNumber with

        /// The math.js string representation of this value, as produced by
        /// `math.format`. Unlike the default `ToString()` (which maps to the
        /// wrapped JavaScript value's `toString()`), this is round-trippable:
        /// vectors are bracketed, e.g. `"[8, 6, 4, 6]"`, and can be fed back to
        /// `Math.Parse`. See dotnet-websharper/mathjs#8.
        member this.Format() : string =
            MathJS.Math.Format(this)
