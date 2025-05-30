﻿// $begin{copyright}
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

namespace WebSharper.MathJS.Tests.Client

open WebSharper
open WebSharper.JavaScript
open WebSharper.Testing
open WebSharper.MathJS
open System.Numerics

[<JavaScript>]
module Main =

    let private addOne (c: IControlBody) =
        let e = JS.Document.CreateElement("div")
        JS.Document.QuerySelector("#main").AppendChild(e) |> ignore
        c.ReplaceInDom(e)

    let RunTests runServerSide autoStart =
        if not autoStart then
            JavaScript.JS.Inline "QUnit.config.autostart = false";
        
        let tests = 
            [|
                BigInt.Tests
                Decimal.Tests runServerSide
            |]

        Runner.RunTests tests |> addOne

    [<SPAEntryPoint>]
    let Main =
        RunTests true true