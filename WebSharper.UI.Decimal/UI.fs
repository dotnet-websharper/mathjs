// $begin{copyright}
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

namespace WebSharper.UI.Client

open System
open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client.BindVar

[<JavaScript>]
module internal String =
    let isBlank s =
        String.forall Char.IsWhiteSpace s

[<JavaScript>]
module internal Helpers =

    module BindVar =
        
        [<JavaScript; Inline "$e.checkValidity?$e.checkValidity():true">]
        let CheckValidity (e: Dom.Element) = X<bool>

        let ApplyValue (get: Get<'a>) (set: Set<'a>) : Apply<'a> = fun (var: Var<'a>) ->
            let mutable expectedValue = None
            let init (el: Dom.Element) =
                let onChange () =
                    var.UpdateMaybe(fun v ->
                        expectedValue <- get el
                        match expectedValue with
                        | Some x as o when x <> v -> o
                        | _ -> None)
                el.AddEventListener("change", onChange)
                el.AddEventListener("input", onChange)
                el.AddEventListener("keypress", onChange)
            let map v =
                match expectedValue with
                | Some x when x = v -> None
                | _ -> Some v
            init, Option.iter << set, var.View.Map map

        let DecimalSetUnchecked : Set<decimal> = fun el i ->
            el?value <- string i
        let DecimalGetUnchecked : Get<decimal> = fun el ->
            let s = el?value
            if String.isBlank s then Some 0.0m else
            match System.Decimal.TryParse(s) with
            | true, v -> Some v
            | false, _ -> None
        let DecimalApplyUnchecked : Apply<decimal> =
            ApplyValue DecimalGetUnchecked DecimalSetUnchecked

        let DecimalSetChecked : Set<CheckedInput<decimal>> = fun el i ->
            let i = i.Input
            if el?value <> i then el?value <- i
        let DecimalGetChecked : Get<CheckedInput<decimal>> = fun el ->
            let s = el?value
            if String.isBlank s then
                if CheckValidity el then Blank s else Invalid s
            else
                match System.Decimal.TryParse(s) with
                | true, v -> Valid (v, s)
                | false, _ -> Invalid s
            |> Some
        let DecimalApplyChecked : Apply<CheckedInput<decimal>> =
            ApplyValue DecimalGetChecked DecimalSetChecked

    let private ValueWith (bind: BindVar.Apply<'a>) (var: Var<'a>) =
        let init, set, view = bind var
        Attr.Append (on.afterRender init) (Attr.DynamicCustom set view)

    let DecimalValueUnchecked (var: Var<decimal>) =
        ValueWith BindVar.DecimalApplyUnchecked var

    let DecimalValue (var: Var<CheckedInput<decimal>>) =
        ValueWith BindVar.DecimalApplyChecked var

[<JavaScript>]
module internal Base =
    let DecimalInputUnchecked attr (var: Var<decimal>) (step: decimal) =
        Elt.Element
            "input" 
            (Seq.append attr [
                (if var.Get() = 0.m then Attr.Create "value" "0" else Attr.Empty)
                Helpers.DecimalValueUnchecked var
                Attr.Create "type" "number"
                Attr.Create "step" $"{step}"
            ])
            []

    let DecimalInput attr (var: Var<CheckedInput<decimal>>) (step: decimal)=
        Elt.Element
            "input"
            (Seq.append attr [
                Helpers.DecimalValue var
                Attr.Create "type" "number"
                Attr.Create "step" $"{step}"
            ])
            []

[<JavaScript>]
module Doc =
    module InputType =

        [<Inline>]
        let DecimalInput attr var step: Doc =
            As (Base.DecimalInput attr var step)

        [<Macro(typeof<Macros.InputV>, "DecimalInput")>]
        let DecimalInputV (attr: seq<Attr>) (var: CheckedInput<decimal>) (step: decimal)= X<Doc>

        [<Inline>]
        let DecimalInputUnchecked attr var step : Doc =
            As (Base.DecimalInputUnchecked attr var step)

        [<Macro(typeof<Macros.InputV>, "DecimalInputUnchecked")>]
        let DecimalInputUncheckedV (attr: seq<Attr>) (var: decimal) (step: decimal)= X<Doc>

    module Html =

        /// Input box with type="number".
        /// If the input box is blank, the value is set to 0.
        /// If the input is not parseable as a decimal, the value is unchanged from its last valid value.
        /// It is advised to use DecimalInput instead for better user experience.
        [<Inline; CompiledName "input">]
        let DecimalInputCSharp(var, [<ParamArray>] attrs: Attr[]) =
            InputType.DecimalInputUnchecked attrs var

[<JavaScript>]
module Elt =

    module InputType =

        [<Inline>]
        let DecimalInput attr var step: Elt =
            As (Base.DecimalInput attr var step)

        [<Macro(typeof<Macros.InputV>, "DecimalInput")>]
        let DecimalInputV (attr: seq<Attr>) (var: CheckedInput<decimal>) (step: decimal)= X<Elt>

        [<Inline>]
        let DecimalInputUnchecked attr var step : Elt =
            As (Base.DecimalInputUnchecked attr var step)

        [<Macro(typeof<Macros.InputV>, "DecimalInputUnchecked")>]
        let DecimalInputUncheckedV (attr: seq<Attr>) (var: decimal) (step: decimal)= X<Elt>


