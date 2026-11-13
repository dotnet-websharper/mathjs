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

namespace WebSharper.MathJS.Sample

open WebSharper
open WebSharper.JavaScript
open WebSharper.MathJS
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Templating

[<JavaScript>]
module Client =

    type IndexTemplate = Template<"index.html", ClientLoad.FromDocument>

    // ------------------------------------------------------------------
    // Theme (light by default, persisted; shared with index.html)
    // ------------------------------------------------------------------

    let themeStorageKey = "websharper-mathjs-theme"
    let theme = Var.Create "light"
    let themeLabel = Var.Create "🌙 Dark mode"

    let applyTheme () =
        JS.Document.DocumentElement.SetAttribute("data-theme", theme.Value)
        themeLabel.Value <- (if theme.Value = "dark" then "☀️ Light mode" else "🌙 Dark mode")

    let toggleTheme () =
        theme.Value <- (if theme.Value = "dark" then "light" else "dark")
        try JS.Window.LocalStorage.SetItem(themeStorageKey, theme.Value) with _ -> ()
        applyTheme ()

    // ------------------------------------------------------------------
    // Helpers
    // ------------------------------------------------------------------

    let private attempt (f: unit -> string) =
        try f () with e -> "error: " + e.Message

    let private fmt (x: float) =
        if System.Double.IsNaN x then "NaN"
        elif System.Double.IsPositiveInfinity x then "∞"
        elif System.Double.IsNegativeInfinity x then "-∞"
        else x.ToString()

    let private boolText (b: bool) = if b then "true" else "false"

    // ------------------------------------------------------------------
    // Interactive expression
    // ------------------------------------------------------------------

    let expr = Var.Create "sin(x) * cos(x)"

    // The expression is evaluated with x = 2 so that expressions in `x` can be
    // evaluated as well as differentiated.
    let private scope : obj = New [ ("x", 2. :> obj) ]
    let evalResult = Var.Create ""
    let parsed = Var.Create ""
    let simplified = Var.Create ""
    let derivative = Var.Create ""
    let chain = Var.Create ""

    let evaluateExpr () =
        evalResult.Value <-
            attempt (fun () -> "evaluate (x = 2) = " + Math.Evaluate(expr.Value, scope).ToString())
        parsed.Value <-
            attempt (fun () -> "parse().toString() = " + Math.Parse(expr.Value).ToString())
        simplified.Value <-
            attempt (fun () -> "simplify = " + Math.Simplify(expr.Value).ToString())
        derivative.Value <-
            attempt (fun () -> "derivative(…, \"x\") = " + Math.Derivative(expr.Value, "x").ToString())

    // ------------------------------------------------------------------
    // Numbers and fractions
    // ------------------------------------------------------------------

    let private a = Math.Bignumber "0.1"
    let private b = Math.Bignumber "0.2"
    let private sum = Math.Add(a, b)
    let private product = Math.Multiply(a, b)
    let private vector = MathNumber [| 8.; 6.; 4.; 6. |]

    let bignumberText =
        String.concat "   "
            [
                "0.1 + 0.2 = " + sum.Format()
                "0.1 * 0.2 = " + product.Format()
                "vector = " + vector.Format()
            ]

    let private f1 = Math.Fraction(1, 3)
    let private f2 = Math.Fraction(1, 6)
    let private fsum = Math.Add(f1, f2)

    let fractionText =
        String.concat "   "
            [
                "1/3 + 1/6 = " + fsum.ToString()
                "num = " + string (Math.Num(fsum))
                "den = " + string (Math.Den(fsum))
            ]

    let bernoulliText =
        "bernoulli(8) = " + string (Math.Bernoulli(8.))

    let nullishText =
        "nullish(null, 42) = " + string (Math.Nullish(null, 42.))

    // ------------------------------------------------------------------
    // Units and physical constants
    // ------------------------------------------------------------------

    let unitConvertText =
        "2 inch → " + Math.Unit("2 inch").To("cm").ToString()

    let unitBestText =
        "0.05 m → " + Math.Unit(0.05, "m").ToBest().ToString()

    let speedOfLightText =
        "speedOfLight = " + Math.SpeedOfLight.ToString()

    let planckConstantText =
        "planckConstant = " + Math.PlanckConstant.ToString()

    // ------------------------------------------------------------------
    // Matrices
    // ------------------------------------------------------------------

    let private dense = DenseMatrix([| [| 1.; 2. |]; [| 3.; 4. |] |])

    let matrixText =
        String.concat "   "
            [
                "storage = " + dense.Storage()
                "size = " + String.concat " × " (Array.map string (dense.Size()))
                "get([1, 1]) = " + string (dense.Get([| 1; 1 |]))
            ]

    let matrixContentsText =
        String.concat "   "
            [
                "diagonal = " + string (dense.Diagonal().ToArray())
                "swapRows(0, 1) = " + string ((dense.SwapRows(0, 1)).ToArray())
            ]

    let private m = MathNumber [| [| 1.; 2. |]; [| 3.; 4. |] |]

    let determinantText =
        String.concat "   "
            [
                "det = " + fmt (Math.Det(m))
                "trace = " + fmt (Math.Trace(m))
            ]

    let transposeText =
        "transpose = " + Math.Transpose(m).Format()

    // ------------------------------------------------------------------
    // Decimal and complex numbers (the Extensions assembly)
    // ------------------------------------------------------------------

    let extensionsText =
        String.concat "   "
            [
                "0.1m + 0.2m = " + string (0.1m + 0.2m)
                "Complex(1, 2) * Complex(3, 4) = "
                    + string (System.Numerics.Complex(1., 2.) * System.Numerics.Complex(3., 4.))
            ]

    // ------------------------------------------------------------------
    // Statistics, predicates and type checks
    // ------------------------------------------------------------------

    let private data = MathNumber [| 2.; 4.; 4.; 4.; 5.; 5.; 7.; 9. |]

    let statisticsText =
        String.concat "   "
            [
                "mean = " + fmt (Math.Mean(data))
                "median = " + fmt (Math.Median(data))
                "std = " + fmt (Math.Std(data))
                "min = " + fmt (Math.Min(data))
                "max = " + fmt (Math.Max(data))
            ]

    let predicatesText =
        String.concat "   "
            [
                "isFraction(1/3) = " + boolText (Math.IsFraction(f1))
                "isBigNumber(0.1) = " + boolText (Math.IsBigNumber(a))
                "isDenseMatrix(m) = " + boolText (Math.IsDenseMatrix(dense))
                "isCollection([|…|]) = " + boolText (Math.IsCollection([| 1.; 2. |]))
                "isFinite(∞) = " + boolText (Math.IsFinite(infinity))
                "isBounded([|1, 2|]) = " + boolText (Math.IsBounded([| 1.; 2. |]))
            ]

    let objectsText =
        String.concat "   "
            [
                "isHelp(help(\"sin\")) = " + boolText (Math.IsHelp(Math.Help("sin")))
                "isResultSet(evaluate(\"1; 2\")) = " + boolText (Math.IsResultSet(Math.Evaluate("1; 2")))
                "isChain(chain(1)) = " + boolText (Math.IsChain(Math.Chain(1.)))
                "isRange(Range(0, 10, 2)) = " + boolText (Math.IsRange(Range(0., 10., 2., false)))
            ]

    let typeOfText =
        String.concat "   "
            [
                "typeof(1/3) = " + Math.TypeOf(f1)
                "typeof(0.1) = " + Math.TypeOf(a)
                "typeof(vector) = " + Math.TypeOf(vector)
            ]

    // ------------------------------------------------------------------
    // Entry point
    // ------------------------------------------------------------------

    [<SPAEntryPoint>]
    let Main () =
        try
            let stored = JS.Window.LocalStorage.GetItem(themeStorageKey)
            if stored ===. "dark" || stored ===. "light" then
                theme.Value <- stored
        with _ -> ()

        applyTheme ()

        evaluateExpr ()
        chain.Value <- attempt (fun () -> "chain(4).add(5).multiply(10) = " + string (Math.Chain(4.).Add(5.).Multiply(10.).Done().ValueOf()))

        IndexTemplate()
            .version(Math.Version)
            .themeLabel(themeLabel.V)
            .ToggleTheme(fun _ -> toggleTheme ())
            .Expr(expr)
            .EvaluateExpr(fun _ -> evaluateExpr ())
            .evalResult(evalResult.V)
            .parsed(parsed.V)
            .simplified(simplified.V)
            .derivative(derivative.V)
            .chain(chain.V)
            .bignumber(bignumberText)
            .fraction(fractionText)
            .bernoulli(bernoulliText)
            .nullish(nullishText)
            .unitConvert(unitConvertText)
            .unitBest(unitBestText)
            .speedOfLight(speedOfLightText)
            .planckConstant(planckConstantText)
            .matrix(matrixText)
            .matrixContents(matrixContentsText)
            .determinant(determinantText)
            .transpose(transposeText)
            .extensions(extensionsText)
            .statistics(statisticsText)
            .predicates(predicatesText)
            .objects(objectsText)
            .typeOf(typeOfText)
            .Bind()