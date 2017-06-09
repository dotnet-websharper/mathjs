namespace WebSharper.MathJS.Extension

open WebSharper
open WebSharper.InterfaceGenerator

module Definition =
    open System.Numerics

    let Vector<'T> = Type.ArrayOf T<'T>

    let Matrix<'T> = Type.ArrayOf Vector<'T>

    let Unit
        = T<float> * T<string>
        + T<string>

    let AllValues<'T> = 
        [|
            T<float>
            Vector<'T>
            Matrix<'T>
            T<bigint>
            T<Complex>
            Unit
            T<bool>
        |]
        
    let WithTypes values f =
        List.reduce (fun l r -> l + r) <| List.map f values

    let Scope
        = Type.ArrayOf (T<string> * (Value + T<JavaScript.Function>))

    let Expression
        = T<string>

    let Config =
        Pattern.Config "config" {
            Required = []
            Optional =
                [
                    "epsilon", T<float>
                    "matrix", T<string>
                    "number", T<string>
                    "precision", T<float>
                    "predictable", T<bool>
                    "randomSeed", T<string>
                ]
        }

    let Options =
        Pattern.Config "options" {
            Required = []
            Optional =
                [
                    "override", T<bool>
                    "silent", T<bool>
                    "wrap", T<bool>
                ]
        }

    let Chain =
        Class "math.type.Chain"

    let Parser =
        Class "Parser"
        |+> Instance [
            "clear" => T<unit> ^-> T<unit>
            "eval" => T<string> ^-> Value
            "get" => T<string> ^-> Value + T<JavaScript.Function>
            "getAll" => T<unit> ^-> T<obj>
            "remove" => T<string> ^-> T<unit>
            "set" => T<string> * Value ^-> T<unit>
        ]

    let AccessorNode            = Class "math.expression.node.accessornode"
    let ArrayNode               = Class "math.expression.node.arraynode"
    let AssignmentNode          = Class "math.expression.node.assignmentnode"
    let BlockNode               = Class "math.expression.node.blocknode"
    let ConditionalNode         = Class "math.expression.node.conditionalnode"
    let ConstantNode            = Class "math.expression.node.constantnode"
    let FunctionAssignmentNode  = Class "math.expression.node.functionassignmentnode"
    let FunctionNode            = Class "math.expression.node.functionnode"
    let IndexNode               = Class "math.expression.node.indexnode"
    let ObjectNode              = Class "math.expression.node.objectnode"
    let OperatorNode            = Class "math.expression.node.operatornode"
    let ParenthesisNode         = Class "math.expression.node.parenthesisnode"
    let RangeNode               = Class "math.expression.node.rangenode"
    let SymbolNode              = Class "math.expression.node.symbolnode"
    let UpdateNode              = Class "math.expression.node.updatenode"

    let Node =
        Class "Node"
        |+> Instance [
            "clone" => T<unit> ^-> TSelf

            "cloneDeep" => T<unit> ^-> TSelf

            "compile" => T<unit> ^-> T<obj>

            "eval" => !? Scope ^-> T<obj>

            "equals" => TSelf ^-> T<bool>

            "filter" => T<JavaScript.Function> ^-> Type.ArrayOf TSelf

            "forEach" => T<JavaScript.Function> ^-> Type.ArrayOf TSelf

            "map" => T<JavaScript.Function> ^-> Type.ArrayOf TSelf

            "toString" => T<unit> ^-> T<string>

            "toTex" => T<unit> ^-> T<string>

            "transform" => T<JavaScript.Function> ^-> TSelf

            "traverse" => T<JavaScript.Function> ^-> T<unit>

            "comment" =? T<string>

            "isNode" =? T<bool>

            "type" =? T<string>
        ]

    AccessorNode
        |=> Implements [Node]
        |+> Instance [
            Constructor (Node * IndexNode)

            "object" =? Node
            "index" =? IndexNode
            "name" =? T<string>
        ]
        |> ignore

    ArrayNode
        |=> Implements [Node]
        |+> Instance [
            Constructor !| Node

            "items" =? !| Node
        ]
        |> ignore

    AssignmentNode
        |=> Implements [Node]
        |+> Instance [
            Constructor ((SymbolNode + AccessorNode) * IndexNode * Node)
            Constructor (SymbolNode * Node)

            "object" =? (SymbolNode + AccessorNode)

            "value" =? Node

            "index" =? IndexNode

            "name" =? T<string>
        ]
        |> ignore

    BlockNode
        |=> Implements [Node]
        |+> Instance [
            Constructor (!| (Node + (Node * T<bool>)))

            "blocks" =? !| (Node * T<bool>)
        ]
        |> ignore

    ConditionalNode
        |=> Implements [Node]
        |+> Instance [
            Constructor (Node * Node * Node)

            "condition" =? Node

            "trueExpr" =? Node

            "falseExpr" =? Node
        ]
        |> ignore

    ConstantNode
        |=> Implements [Node]
        |+> Instance [
            Constructor (Value * !? T<string>)

            "value" =? Value
            "valueType" =? T<string>
        ]
        |> ignore

    FunctionAssignmentNode
        |=> Implements [Node]
        |+> Instance [
            Constructor (T<string> * !| T<string> * Node)

            "name" =? T<string>
            "params" =? !| T<string>
            "expr" =? Node
        ]
        |> ignore

    FunctionNode
        |=> Implements [Node]
        |+> Instance [
            Constructor ((Node + T<string>) * !| Node)

            "fn" =? Node + T<string>
            "args" =? !| Node
        ]
        |> ignore

    IndexNode
        |=> Implements [Node]
        |+> Instance [
            Constructor (!| Node * !? T<bool>)

            "dimension" =? !| Node
            "dotNotation" =? T<bool>
        ]
        |> ignore

    ObjectNode
        |=> Implements [Node]
        |+> Instance [
            Constructor (T<string> * Node)

            "properties" =? T<string> * Node
        ]
        |> ignore

    OperatorNode
        |=> Implements [Node]
        |+> Instance [
            Constructor (T<string> * T<string> * !| Node)

            "op" =? T<string>
            "fn" =? T<string>
            "args" =? !| Node
        ]
        |> ignore

    ParenthesisNode
        |=> Implements [Node]
        |+> Instance [
            Constructor Node

            "content" =? Node
        ]
        |> ignore

    RangeNode
        |=> Implements [Node]
        |+> Instance [
            Constructor (Node * Node * !? Node)

            "start" =? Node
            "end" =? Node
            "step" =? Node
        ]
        |> ignore

    SymbolNode
        |=> Implements [Node]
        |+> Instance [
            Constructor T<string>

            "name" =? T<string>
        ]
        |> ignore

    UpdateNode
        |=> Implements [Node]
        |+> Instance [
            // not documented
        ]
        |> ignore


    let MathClass =
        Class "math"
        |+> Static [
            "config" => Config.Type ^-> T<unit>
            |> WithComment "Configure the math engine."

            "create" => !? Config.Type ^-> TSelf
            |> WithComment "Create and configure a math engine."

            "chain" => !? Value ^-> Chain.Type
            |> WithComment "Chain functions."

            "import" => (Type.ArrayOf T<string * JavaScript.Function> + Type.ArrayOf T<JavaScript.Function>) * !? Options.Type
            |> WithComment "Import custom function and values."

            "typed" => !? T<string> * T<obj> ^-> T<JavaScript.Function>
            |> WithComment "Creates a typed function."

            //constuction
            "bignumber" => Value ^-> T<bigint>

            "boolean" => Value ^-> T<bool>

            "complex" => (T<unit> + (T<float> * T<string>) + T<float> + T<Complex> + T<string> + Vector) ^-> T<Complex>

            "createUnit" => T<string> * (T<string> * Unit) * T<obj> ^-> Unit

            "fraction" => T<float> * T<float> ^-> T<float>

            "fraction" => (Vector ^-> Vector) + (Matrix ^-> Matrix)

            "index" => Vector ^-> Vector

            "matrix" => !? Matrix * !? T<string> * !? T<string> ^-> Matrix

            "number" => Value * !? T<string> ^-> Value

            "sparse" => !? Value * !? T<string> ^-> Matrix

            "splitUnit" => Unit * Type.ArrayOf T<string> ^-> Vector

            "string" => Value ^-> T<string>

            "unit" => !? T<float> * T<string> ^-> Value

            //expression
            "compile" => T<string> ^-> T<obj>

            "compile" => !| T<string> ^-> !| T<obj>

            "eval" => (T<string> * !? Scope) + (Type.ArrayOf T<string> * !? Scope) ^-> Value

            "help" => (T<JavaScript.Function> + T<string> + T<obj>) ^-> T<obj>

            "parse" =>(T<string> * !? Scope) + (Type.ArrayOf T<string> * !? Scope) ^-> T<obj>

            "parser" => T<unit> ^-> Parser.Type

            //algebra
            "derivate" => (Node + T<string>) * (SymbolNode + T<string>) * !? T<bool> ^-> Node

            "lsolve" => (Matrix + Vector) * (Matrix + Vector) ^-> (Matrix + Vector)

            "lup" => (Matrix + Vector) ^-> (Vector * Vector * Vector)

            "lusolve" => (Matrix + Vector + T<obj>) * (Matrix + Vector) ^-> (Matrix + Vector)

            "qr" => (Matrix + Vector) ^-> (Matrix * Matrix) + (Vector * Vector)

            "simplify" => (Node + T<string>) * !? (T<string * string> + T<string> + T<JavaScript.Function>) ^-> Node

            "slu" => Matrix * T<float> * T<float> ^-> T<obj>

            "usolve" => (Matrix + Vector) * (Matrix + Vector) ^-> (Matrix + Vector)

            //arithmetic
            "abs" => Value ^-> Value

            "add" => (Value * Value *+ Value) ^-> Value

            "cbrt" => Value * !? T<bool> ^-> Value

            "ceil" => Value ^-> Value

            "cube" => Value ^-> Value

            "divide" => Value * Value ^-> Value

            "dotDivide" => Value * Value ^-> Value

            "dotMultiply" => Value * Value ^-> Value

            "dotPow" => Value * Value ^-> Value

            "exp" => Value ^-> Value

            "fix" => Value ^-> Value

            "floor" => Value ^-> Value

            "gcd" => Value * Value ^-> Value

            "hypot" => !| Value ^-> Value

            "lcm" => !| Value ^-> Value

            "log" => Value * !? Value ^-> Value

            "log10" => Value ^-> Value

            "mod" => Value * Value ^-> Value

            "multiply" => !| Value ^-> Value

            "norm" => Value * !? Value ^-> Value

            "nthRoot" => Value * !? Value ^-> Value

            "pow" => Value * Value ^-> Value

            "round" => Value * Value ^-> Value

            "sign" => Value ^-> Value

            "sqrt" => Value ^-> Value

            "square" => Value ^-> Value

            "subtract" => Value * Value ^-> Value

            "unaryMinus" => Value ^-> Value

            "unaryPlus" => Value ^-> Value

            "xgcd" => Value * Value ^-> !| Value

            //bitwise
            "bitAnd" => Value * Value ^-> Value

            "bitNot" => Value ^-> Value

            "bitOr" => Value * Value ^-> Value

            "bitXor" => Value * Value ^-> Value

            "leftShift" => Value * Value ^-> Value

            "rightArithShift" => Value * Value ^-> Value

            "rightLogShift" => Value * Value ^-> Value

            //combinatorics
            "bellNumbers" => Value ^-> Value

            "catalan" => Value ^-> Value

            "composition" => Value * Value ^-> Value

            "stirlingS2" => Value * Value ^-> Value

            //complex
            "arg" => Value ^-> Value

            "conj" => Value ^-> Value

            "im" => Value ^-> Value

            "re" => Value ^-> Value

            //geometry
            "distance" => (Matrix + Vector) * (Matrix + Vector) ^-> Value

            "intersect" => (Matrix + Vector) * (Matrix + Vector) * (Matrix + Vector) * (Matrix + Vector) ^-> Vector

            //logic
            "and" => Value * Value ^-> Value

            "not" => Value ^-> Value

            "or" => Value * Value ^-> Value

            "xor" => Value * Value ^-> Value

            //matrix
            "concat" => !| (Matrix + Vector) ^-> (Matrix + Vector)

            "cross" => (Matrix + Vector) * (Matrix + Vector) ^-> (Matrix + Vector)

            "det" => (Matrix + Vector) ^-> Value

            "diag" => (Matrix + Vector) * !? Value * !? T<string> ^-> (Matrix + Vector)

            "dot" => (Matrix + Vector) * (Matrix + Vector) ^-> T<float>

            "eye" => T<int> * T<int> ^-> (Matrix + Vector + T<int>)

            "filter" => (Matrix + Vector) * (T<JavaScript.Function> + T<string>) ^-> (Matrix + Vector)

            "flatten" => (Matrix + Vector) ^-> (Matrix + Vector)

            "forEach" => (Matrix + Vector) * T<JavaScript.Function> ^-> T<unit>

            "inv" => Value ^-> Value

            "kron" => (Matrix + Vector) * (Matrix + Vector) ^-> (Matrix + Vector)

            "map" => (Matrix + Vector) * T<JavaScript.Function> ^-> (Matrix + Vector)

            "ones" => !| T<int> ^-> (Matrix + Vector + T<int>)

            "partitionSelect" => (Matrix + Vector) * T<int> * (T<string> + T<JavaScript.Function>) ^-> Value

            "range" => T<string> * !? (T<int> + T<bigint>) * !? (T<int> + T<bigint>) * !? (T<int> + T<bigint>) * !? T<bool> ^-> (Matrix * Vector)

            "reshape" => (Matrix + Vector) * !| T<int> ^-> (Matrix + Vector)

            "resize" => (Matrix + Vector) * (Matrix + Vector) * !? (T<int> + T<string>) ^-> (Matrix + Vector)

            "size" => Value ^-> (Matrix + Vector)

            "sort" => (Matrix + Vector) ^-> (T<string> + T<JavaScript.Function>) ^-> (Matrix + Vector)

            "squeeze" => (Matrix + Vector) ^-> Matrix

            "subset" => (Matrix + Vector + T<string>) * T<int> * !? (Matrix + Vector + T<int>) * !? Value ^-> (Matrix + Vector + T<string>)

            "trace" => (Matrix + Vector) ^-> T<float>

            "transpose" => (Matrix + Vector) ^-> (Matrix + Vector)

            "zeros" => !| T<int> ^-> (Matrix + Vector)

            //probability
            "combination" => (T<int> + T<bigint>) * (T<int> + T<bigint>) ^-> (T<int> + T<bigint>)

            "factorial" => Value ^-> Value

            "gamma" => (Matrix + Vector + T<int>) ^-> (Matrix + Vector + T<int>)

            "kldivergence" => (Matrix + Vector) * (Matrix + Vector)  ^-> T<int>

            "multinomial" => !| (T<int> + T<bigint>) ^-> (T<int> + T<bigint>)

            "permutations" => (T<int> + T<bigint>) * !? (T<int> + T<bigint>) ^-> (T<int> + T<bigint>)

            "pickRandom" => Vector * !? T<int> * !? Vector ^-> Vector

            "random" => !?(Matrix + Vector) * !? T<int> * !? T<int> ^-> (T<int> + Matrix + Vector)

            "randomInt" => T<int> * !? T<int> ^-> (T<int> + Matrix + Vector)

            "randomInt" => (Matrix + Vector) * !? T<int> * !? T<int> ^-> (T<int> + Matrix + Vector)

            //relational
            "compare" => Value * Value ^-> Value

            "deepEqual" => Value * Value ^-> Value

            "equal" => Value * Value ^-> Value

            "larger" => Value * Value ^-> Value

            "largerEq" => Value * Value ^-> Value

            "smaller" => Value * Value ^-> Value

            "smallerEq" => Value * Value ^-> Value

            "unequal" => Value * Value ^-> Value

            //special
            "erf" => (T<int> + Matrix + Vector) ^-> (T<int> + Matrix + Vector)
            
            //statistics
            "mad" => (Matrix + Vector) ^-> Value

            "max" => (Matrix + Vector) ^-> Value
            
            "mean" => (Matrix + Vector) ^-> Value 

            "median" => (Matrix + Vector) ^-> Value 

            "min" => (Matrix + Vector) ^-> Value 

            "mode" => (Matrix + Vector) ^-> Value 

            "prod" => (Matrix + Vector) ^-> Value 

            "quantileSeq" => (Matrix + Vector) * !? !|(T<int> + T<bigint> + Vector) * !? T<bool> ^-> Value

            "std" => (Matrix + Vector) * !? T<string> ^-> Value
            
            "sum" => (Matrix + Vector) ^-> Value 

            "var" => (Matrix + Vector) ^-> Value 

            //string
            "format" => Value * !? T<int> * !? T<JavaScript.Function> ^-> T<string>

            "print" => T<string> * !| Value * !? T<int> ^-> T<string>

            //trigonometry
            "acos" => Value ^-> Value
            
            "acosh" => Value ^-> Value
            
            "acot" => Value ^-> Value
            
            "acoth" => Value ^-> Value
            
            "acsc" => Value ^-> Value
            
            "acsch" => Value ^-> Value
            
            "asec" => Value ^-> Value
            
            "asech" => Value ^-> Value
            
            "asin" => Value ^-> Value
            
            "asinh" => Value ^-> Value
            
            "atan" => Value ^-> Value
            
            "atan2" => Value * Value ^-> Value
            
            "atanh" => Value ^-> Value
            
            "cos" => Value ^-> Value
            
            "cosh" => Value ^-> Value
            
            "cot" => Value ^-> Value
            
            "coth" => Value ^-> Value
            
            "csc" => Value ^-> Value
            
            "csch" => Value ^-> Value
            
            "sec" => Value ^-> Value
            
            "sech" => Value ^-> Value
            
            "sin" => Value ^-> Value
            
            "sinh" => Value ^-> Value
            
            "tan" => Value ^-> Value
            
            "tanh" => Value ^-> Value

            //unit
            "to" => (Unit + Vector + Matrix) * (Unit + Vector + Matrix) ^-> (Unit + Vector + Matrix)

            //utils
            "clone" => Value ^-> Value

            "isInteger" => Value ^-> T<bool>
            
            "isNaN" => Value ^-> T<bool>
            
            "isNegative" => Value ^-> T<bool>
            
            "isNumeric" => Value ^-> T<bool>
            
            "isPositive" => Value ^-> T<bool>
            
            "isPrime" => Value ^-> T<bool>
            
            "isZero" => Value ^-> T<bool>
            
            "typeof" => T<obj> ^-> T<string>

            "E" =? T<float>

            "i" =? T<Complex>

            "Infinity" =? T<float>
            
            "LN2" =? T<float>
            
            "LN10" =? T<float>
            
            "LOG2E" =? T<float>
            
            "LOG10E" =? T<float>
            
            "phi" =? T<float>
            
            "pi" =? T<float>

            "PI" =? T<float>
            
            "SQRT1_2" =? T<float>

            "SQRT2" =? T<float>

            "tau" =? T<float>

            "unitialized" =? T<obj>

            "version" =? T<string>   
        ]

    Chain 
        |=> Implements [MathClass]
        |> ignore

    let Assembly =
        Assembly [
            Namespace "WebSharper.MathJS.Resources" [
                (Resource "Js" "https://cdnjs.cloudflare.com/ajax/libs/mathjs/3.13.3/math.js")
                |> (fun r -> r.AssemblyWide())
            ]
            Namespace "WebSharper.MathJS" [
                MathClass
                Chain
                Node
                AccessorNode          
                ArrayNode             
                AssignmentNode        
                BlockNode             
                ConditionalNode       
                ConstantNode          
                FunctionAssignmentNode
                FunctionNode          
                IndexNode             
                ObjectNode            
                OperatorNode          
                ParenthesisNode       
                RangeNode             
                SymbolNode            
                UpdateNode
                Config
                Options
                Parser
            ]
        ]


[<Sealed>]
type Extension() =
    interface IExtension with
        member x.Assembly = Definition.Assembly

[<assembly: Extension(typeof<Extension>)>]
do ()
