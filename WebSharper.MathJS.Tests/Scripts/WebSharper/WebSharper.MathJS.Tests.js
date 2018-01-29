(function()
{
 "use strict";
 var Global,WebSharper,MathJS,Tests,Client,Testing,Runner,Pervasives,math,Unchecked,Extensions,Extensions$1,QUnit;
 Global=window;
 WebSharper=Global.WebSharper=Global.WebSharper||{};
 MathJS=WebSharper.MathJS=WebSharper.MathJS||{};
 Tests=MathJS.Tests=MathJS.Tests||{};
 Client=Tests.Client=Tests.Client||{};
 Testing=WebSharper&&WebSharper.Testing;
 Runner=Testing&&Testing.Runner;
 Pervasives=Testing&&Testing.Pervasives;
 math=Global.math;
 Unchecked=WebSharper&&WebSharper.Unchecked;
 Extensions=MathJS&&MathJS.Extensions;
 Extensions$1=Extensions&&Extensions.Extensions;
 QUnit=Global.QUnit;
 Client.RunTests=function()
 {
  return Runner.RunTests([Client.Tests()]);
 };
 Client.Tests=function()
 {
  var s;
  s={
   name:Pervasives.TestCategory("General").name,
   run:function()
   {
    var b,b$1,b$2,b$3,b$4,b$5,a,b$6,b$7,a$1,b$8,b$9,b$10,b$11,b$12,b$13,_this,_this$1,b$14,b$15,b$16,b$17,b$18,b$19,b$20,b$21;
    b=Pervasives.Test("Sanity check");
    b.Run(b.EqualMsg(b.Yield(),function()
    {
     return 1+2+3;
    },function()
    {
     return 6;
    },"1 + 2 + 3 = 6"));
    b$1=Pervasives.Test("MathJS add (float)");
    b$1.Run(b$1.EqualMsg(b$1.Yield(),function()
    {
     var _this$2;
     _this$2=Global.math;
     return _this$2.add.apply(_this$2,[1,2,3]);
    },function()
    {
     return 6;
    },"Math.Add(1., 2., 3.) = 6"));
    b$2=Pervasives.Test("MathJS add (fraction)");
    b$2.Run(b$2.EqualMsg(b$2.Yield([Global.math.fraction(0.1),Global.math.fraction(0.2)]),function(t)
    {
     return Global.math.fraction(t[0]+t[1]);
    },function()
    {
     return Global.math.fraction(0.3);
    },"Math.Add(.1, .2) = .3"));
    b$3=Pervasives.Test("MathJS add (int)");
    b$3.Run(b$3.EqualMsg(b$3.Yield(),function()
    {
     var _this$2;
     _this$2=Global.math;
     return _this$2.add.apply(_this$2,[1,2,3]);
    },function()
    {
     return 6;
    },"Math.Add(1, 2, 3) = 6"));
    b$4=Pervasives.Test("MathJS add (unit)");
    b$4.Run(b$4.ApproxEqualMsg(b$4.Yield([Global.math.unit("5 cm"),Global.math.unit("10 cm"),Global.math.unit("15 cm")]),function(t)
    {
     var _this$2;
     return(_this$2=Global.math,_this$2.add.apply(_this$2,[t[0],t[1]])).toNumeric("cm");
    },function(t)
    {
     return t[2].toNumeric("cm");
    },"MathJS.Add(5 cm + 10 cm) = 15 cm"));
    b$5=Pervasives.Test("MathJS add (complex)");
    b$5.Run(b$5.EqualMsg((a=math.complex(1,1),(b$6=math.complex(1,1),b$5.Yield([a,b$6,math.add(a,b$6)]))),function(t)
    {
     var _this$2;
     _this$2=math;
     return _this$2.add.apply(_this$2,[t[0],t[1]]);
    },Global.trd,"Math.Add(Complex(1., 1.), Complex(1., 1.)) = Complex(2., 2.)"));
    b$7=Pervasives.Test("MathJS add (bignum)");
    b$7.Run(b$7.EqualMsg((a$1=math.bignumber(100),(b$8=math.bignumber(200),b$7.Yield([a$1,b$8,math.add(a$1,b$8)]))),function(t)
    {
     var _this$2;
     _this$2=math;
     return _this$2.add.apply(_this$2,[t[0],t[1]]);
    },Global.trd,"Math.Add(BigNumber(100), BigNumber(200)) = BigNumber(300)"));
    b$9=Pervasives.Test("MathJS Complex");
    b$9.Run(b$9.IsTrueMsg(b$9.Yield(),function()
    {
     return Unchecked.Equals(math.complex("2.0 + 6.0i"),math.complex(2,"6."));
    },"Complex(\"2.0 + 6.0i\") = Complex(2., \"6.\")"));
    b$10=Pervasives.Test("MathJS Simplify");
    b$10.Run(b$10.EqualMsg(b$10.Yield(),function()
    {
     return math.simplify("3 + 2 / 4").toString();
    },function()
    {
     return"7 / 2";
    },"Simplify(3 + 2 / 4) = 7 / 2"));
    b$11=Pervasives.Test("MathJS Simplify with x and y");
    b$11.Run(b$11.EqualMsg(b$11.Yield(),function()
    {
     return math.simplify("x * y * -x / (x ^ 2)").toString();
    },function()
    {
     return"-y";
    },"Simplify(x * y * -x / (x ^ 2)) = -y"));
    b$12=Pervasives.Test("MathJS Derivative");
    b$12.Run(b$12.EqualMsg(b$12.Yield(),function()
    {
     return math.derivative("2x^2 + 3x + 4","x").toString();
    },function()
    {
     return"4 * x + 3";
    },"Derivative(2x^2 + 3x + 4 with x) = 4 * x + 3"));
    b$13=Pervasives.Test("MathJS Chaining");
    b$13.Run(b$13.EqualMsg(b$13.Yield((_this=(_this$1=math.chain(4),_this$1.add.apply(_this$1,[5])),_this.multiply.apply(_this,[10])).done().valueOf()),Global.id,function()
    {
     return 90;
    },"Chain(4).Add(5).Mulitply(10) = 90"));
    b$14=Pervasives.Test("MathJS Expressions");
    b$14.Run(b$14.EqualMsg(b$14.EqualMsg(b$14.Yield(),function()
    {
     return Global.String(math["eval"]("sqrt(3^2 + 4^2)"));
    },function()
    {
     return"5";
    },"Eval(sqrt(3^2 + 4^2)) = 5"),function()
    {
     return Global.String(math["eval"]("2 inch to cm"));
    },function()
    {
     return"5.08 cm";
    },"Eval(2 inch to cm) = 5.08 cm"));
    b$15=Pervasives.Test("MathJS Det");
    b$15.Run(b$15.EqualMsg(b$15.Yield(),function()
    {
     return math.det([[2,1],[1,2]]);
    },function()
    {
     return 3;
    },"Math.Det([| [| 2.; 1. |]; [| 1.; 2. |] |]) = 3."));
    b$16=Pervasives.Test("MathJS Eval with Scope");
    b$16.Run(b$16.EqualMsg(b$16.Yield({
     a:3,
     b:4
    }),function(scope)
    {
     return Global.String(math["eval"]("a * b",scope));
    },function()
    {
     return"12";
    },"Eval(a * b where a = 3, b = 4) = 12"));
    b$17=Pervasives.Test("MathJS factorial");
    b$17.Run(b$17.EqualMsg(b$17.Yield(),function()
    {
     return math.factorial(5);
    },function()
    {
     return 120;
    },"5! = 120"));
    b$18=Pervasives.Test("MathJS dot product");
    b$18.Run(b$18.EqualMsg(b$18.EqualMsg(b$18.Yield([[2,4,1],[2,2,3]]),function(t)
    {
     return math.dot(t[0],t[1]);
    },function()
    {
     return 15;
    },"Dot([2,4,1], [2,2,3]) = 15"),function(t)
    {
     var _this$2;
     _this$2=math;
     return _this$2.multiply.apply(_this$2,[t[0],t[1]]);
    },function()
    {
     return 15;
    },"Multiply([2,4,1], [2,2,3]) = 15"));
    b$19=Pervasives.Test("MathJS cross procudt");
    b$19.Run(b$19.EqualMsg(b$19.Yield([[[1,2,3]],[[4],[5],[6]]]),function(t)
    {
     return math.cross(t[0],t[1]);
    },function()
    {
     return[[-3,6,-3]];
    },"Cross([[1,2,3]],[[4],[5],[6]]) = [[-3,6,-3]]"));
    b$20=Pervasives.Test("MathJS insanity check");
    b$20.Run(b$20.EqualMsg(b$20.Yield(),function()
    {
     var _this$2;
     _this$2=math;
     return _this$2.add.apply(_this$2,["5",1.2,true]);
    },function()
    {
     return 7.2;
    },"Add(\"5\", 1.2, true) = 7.2"));
    b$21=Pervasives.Test("Decimal sanity check");
    b$21.Run(b$21.EqualMsg(b$21.EqualMsg(b$21.Equal(b$21.Yield(),function()
    {
     var c;
     c=Extensions$1.CreateDecimal(1,0,0,false,1);
     return Global.String(c);
    },function()
    {
     return"0.1";
    }),function()
    {
     var a$2,a$3,_this$2;
     a$2=Extensions$1.CreateDecimal(1,0,0,false,1);
     a$3=Extensions$1.CreateDecimal(2,0,0,false,1);
     _this$2=Extensions$1.WSDecimalMath();
     return _this$2.add.apply(_this$2,[a$2,a$3]);
    },function()
    {
     return Extensions$1.CreateDecimal(3,0,0,false,1);
    },"0.1m + 0.2m = 0.3m"),function()
    {
     var a$2,a$3,_this$2;
     a$2=Extensions$1.CreateDecimal(1,0,0,false,0);
     a$3=Extensions$1.CreateDecimal(1,0,0,false,0);
     _this$2=Extensions$1.WSDecimalMath();
     return _this$2.multiply.apply(_this$2,[a$2,a$3]);
    },function()
    {
     return Extensions$1.CreateDecimal(1,0,0,false,0);
    },"1m * 1m = 1m"));
   }
  };
  QUnit.module(s.name);
  return s;
 };
}());
