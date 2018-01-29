(function()
{
 "use strict";
 var Global,System,Decimal,WebSharper,MathJS,Extensions,Extensions$1,SC$1,IntelliFactory,Runtime,Arrays,math;
 Global=window;
 System=Global.System=Global.System||{};
 Decimal=System.Decimal=System.Decimal||{};
 WebSharper=Global.WebSharper=Global.WebSharper||{};
 MathJS=WebSharper.MathJS=WebSharper.MathJS||{};
 Extensions=MathJS.Extensions=MathJS.Extensions||{};
 Extensions$1=Extensions.Extensions=Extensions.Extensions||{};
 SC$1=Global.StartupCode$WebSharper_MathJS_Extensions$Main=Global.StartupCode$WebSharper_MathJS_Extensions$Main||{};
 IntelliFactory=Global.IntelliFactory;
 Runtime=IntelliFactory&&IntelliFactory.Runtime;
 Arrays=WebSharper&&WebSharper.Arrays;
 math=Global.math;
 Decimal=System.Decimal=Runtime.Class({},null,Decimal);
 Decimal.New=Runtime.Ctor(function(parts)
 {
  var sign,scale;
  sign=(Arrays.get(parts,3)&-2147483648)!==0;
  scale=Arrays.get(parts,3)>>16&127;
  Extensions$1.CreateDecimal(Arrays.get(parts,0),Arrays.get(parts,1),Arrays.get(parts,2),sign,scale);
 },Decimal);
 Extensions$1.CreateDecimal=function(lo,mid,hi,isNegative,scale)
 {
  var m,uint_sup,_this,_this$1,quotient,value,_this$2,_this$3,_this$4,_this$5,a,a$1,_this$6,_this$7;
  function n(x)
  {
   return Extensions$1.WSDecimalMath().number(x);
  }
  function reinterpret(x)
  {
   var _this$8;
   return x>=0?n(x):(_this$8=m.chain(uint_sup),_this$8.add.apply(_this$8,[x])).done();
  }
  m=Extensions$1.WSDecimalMath();
  uint_sup=(_this=(_this$1=m.chain(429496729),_this$1.multiply.apply(_this$1,[10])),_this.add.apply(_this,[6])).done();
  quotient=m.chain(10).pow(-scale).done();
  value=(_this$2=(_this$3=(_this$4=(_this$5=m.chain(reinterpret(hi)),_this$5.multiply.apply(_this$5,[uint_sup])),(a=reinterpret(mid),_this$4.add.apply(_this$4,[a]))),_this$3.multiply.apply(_this$3,[uint_sup])),(a$1=reinterpret(lo),_this$2.add.apply(_this$2,[a$1]))).done();
  return(_this$6=(_this$7=m.chain(isNegative?-1:1),_this$7.multiply.apply(_this$7,[value])),_this$6.multiply.apply(_this$6,[quotient])).done();
 };
 Extensions$1.WSDecimalMath=function()
 {
  SC$1.$cctor();
  return SC$1.WSDecimalMath;
 };
 SC$1.$cctor=Runtime.Cctor(function()
 {
  var r;
  SC$1.WSDecimalMath=math.create((r={},r.number="BigNumber",r.precision=29,r));
  SC$1.$cctor=Global.ignore;
 });
}());
