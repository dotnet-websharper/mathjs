(function()
{
 "use strict";
 var Global,WebSharper,Testing,Random,Sample,SC$1,Pervasives,TestCategoryBuilder,Runner,SubtestBuilder,TestBuilder,SC$2,Runner$1,RunnerControlBody,TestCategory,IntelliFactory,Runtime,List,Seq,Arrays,Operators,Math,String,Concurrency,Unchecked,QUnit,Enumerator;
 Global=window;
 WebSharper=Global.WebSharper=Global.WebSharper||{};
 Testing=WebSharper.Testing=WebSharper.Testing||{};
 Random=Testing.Random=Testing.Random||{};
 Sample=Random.Sample=Random.Sample||{};
 SC$1=Global.StartupCode$WebSharper_Testing$Random=Global.StartupCode$WebSharper_Testing$Random||{};
 Pervasives=Testing.Pervasives=Testing.Pervasives||{};
 TestCategoryBuilder=Pervasives.TestCategoryBuilder=Pervasives.TestCategoryBuilder||{};
 Runner=Pervasives.Runner=Pervasives.Runner||{};
 SubtestBuilder=Pervasives.SubtestBuilder=Pervasives.SubtestBuilder||{};
 TestBuilder=Pervasives.TestBuilder=Pervasives.TestBuilder||{};
 SC$2=Global.StartupCode$WebSharper_Testing$Pervasives=Global.StartupCode$WebSharper_Testing$Pervasives||{};
 Runner$1=Testing.Runner=Testing.Runner||{};
 RunnerControlBody=Runner$1.RunnerControlBody=Runner$1.RunnerControlBody||{};
 TestCategory=Testing.TestCategory=Testing.TestCategory||{};
 IntelliFactory=Global.IntelliFactory;
 Runtime=IntelliFactory&&IntelliFactory.Runtime;
 List=WebSharper&&WebSharper.List;
 Seq=WebSharper&&WebSharper.Seq;
 Arrays=WebSharper&&WebSharper.Arrays;
 Operators=WebSharper&&WebSharper.Operators;
 Math=Global.Math;
 String=Global.String;
 Concurrency=WebSharper&&WebSharper.Concurrency;
 Unchecked=WebSharper&&WebSharper.Unchecked;
 QUnit=Global.QUnit;
 Enumerator=WebSharper&&WebSharper.Enumerator;
 Sample=Random.Sample=Runtime.Class({
  get_Data:function()
  {
   return this.data;
  }
 },WebSharper.Obj,Sample);
 Sample.Make=function(generator,count)
 {
  return new Sample.New$1(generator,count);
 };
 Sample.New=Runtime.Ctor(function(generator)
 {
  Sample.New$1.call(this,generator,100);
 },Sample);
 Sample.New$1=Runtime.Ctor(function(generator,count)
 {
  Sample.New$2.call(this,List.ofSeq(Seq.delay(function()
  {
   return Seq.append(Seq.map(function(i)
   {
    return Arrays.get(generator.Base,i);
   },Operators.range(0,Arrays.length(generator.Base)-1)),Seq.delay(function()
   {
    return Seq.map(function()
    {
     return generator.Next();
    },Operators.range(1,count));
   }));
  })));
 },Sample);
 Sample.New$2=Runtime.Ctor(function(data)
 {
  this.data=data;
 },Sample);
 Random.Imply=function(a,b)
 {
  return Random.Implies(a,b);
 };
 Random.Choose=function(gens,f)
 {
  var gengen;
  gengen=Random.Map(function(i)
  {
   return Arrays.get(gens,i);
  },Random.Within(0,Arrays.length(gens)-1));
  return{
   Base:[],
   Next:function()
   {
    return f(gengen.Next()).Next();
   }
  };
 };
 Random.Anything=function()
 {
  SC$1.$cctor();
  return SC$1.Anything;
 };
 Random.allTypes=function()
 {
  SC$1.$cctor();
  return SC$1.allTypes;
 };
 Random.OptionOf=function(generator)
 {
  return Random.Mix(Random.Const(null),Random.Map(function(a)
  {
   return{
    $:1,
    $0:a
   };
  },generator));
 };
 Random.Const=function(x)
 {
  return{
   Base:[x],
   Next:function()
   {
    return x;
   }
  };
 };
 Random.MixManyWithoutBases=function(gs)
 {
  var i;
  return{
   Base:[],
   Next:(i=[0],function()
   {
    i[0]=(i[0]+1)%Arrays.length(gs);
    return Arrays.get(gs,i[0]).Next();
   })
  };
 };
 Random.MixMany=function(gs)
 {
  var i;
  return{
   Base:Arrays.concat(Arrays.ofSeq(Seq.delay(function()
   {
    return Seq.map(function(g)
    {
     return g.Base;
    },gs);
   }))),
   Next:(i=[0],function()
   {
    i[0]=(i[0]+1)%Arrays.length(gs);
    return Arrays.get(gs,i[0]).Next();
   })
  };
 };
 Random.Mix=function(a,b)
 {
  var left;
  return{
   Base:a.Base.concat(b.Base),
   Next:(left=[false],function()
   {
    left[0]=!left[0];
    return left[0]?a.Next():b.Next();
   })
  };
 };
 Random.OneOf=function(seeds)
 {
  var index;
  return{
   Base:seeds,
   Next:(index=Random.Within(1,Arrays.length(seeds)),function()
   {
    return Arrays.get(seeds,index.Next()-1);
   })
  };
 };
 Random.Tuple3Of=function(a,b,c)
 {
  return{
   Base:Arrays.ofSeq(Seq.delay(function()
   {
    return Seq.collect(function(x)
    {
     return Seq.collect(function(y)
     {
      return Seq.map(function(z)
      {
       return[x,y,z];
      },c.Base);
     },b.Base);
    },a.Base);
   })),
   Next:function()
   {
    return[a.Next(),b.Next(),c.Next()];
   }
  };
 };
 Random.Tuple2Of=function(a,b)
 {
  return{
   Base:Arrays.ofSeq(Seq.delay(function()
   {
    return Seq.collect(function(x)
    {
     return Seq.map(function(y)
     {
      return[x,y];
     },b.Base);
    },a.Base);
   })),
   Next:function()
   {
    return[a.Next(),b.Next()];
   }
  };
 };
 Random.StringExhaustive=function()
 {
  SC$1.$cctor();
  return SC$1.StringExhaustive;
 };
 Random.String=function()
 {
  SC$1.$cctor();
  return SC$1.String;
 };
 Random.ListOf=function(generator)
 {
  return Random.Map(List.ofArray,Random.ArrayOf(generator));
 };
 Random.ArrayOf=function(generator)
 {
  return{
   Base:[[]],
   Next:function()
   {
    return Arrays.init(Random.Natural().Next()%100,function()
    {
     return generator.Next();
    });
   }
  };
 };
 Random.FloatWithin=function(low,hi)
 {
  return{
   Base:[low,hi],
   Next:function()
   {
    return low+(hi-low)*Math.random();
   }
  };
 };
 Random.Within=function(low,hi)
 {
  return{
   Base:[low,hi],
   Next:function()
   {
    return Random.Natural().Next()%(hi-low)+low;
   }
  };
 };
 Random.Natural=function()
 {
  SC$1.$cctor();
  return SC$1.Natural;
 };
 Random.Int=function()
 {
  SC$1.$cctor();
  return SC$1.Int;
 };
 Random.FloatExhaustive=function()
 {
  SC$1.$cctor();
  return SC$1.FloatExhaustive;
 };
 Random.Float=function()
 {
  SC$1.$cctor();
  return SC$1.Float;
 };
 Random.Boolean=function()
 {
  SC$1.$cctor();
  return SC$1.Boolean;
 };
 Random.Exponential=function(lambda)
 {
  return{
   Base:[],
   Next:function()
   {
    return-Math.log(1-Random.StandardUniform().Next())/lambda;
   }
  };
 };
 Random.StandardUniform=function()
 {
  SC$1.$cctor();
  return SC$1.StandardUniform;
 };
 Random.Implies=function(a,b)
 {
  return!a||b;
 };
 Random.Map=function(f,gen)
 {
  var f$1;
  return{
   Base:Arrays.map(f,gen.Base),
   Next:(f$1=gen.Next,function(x)
   {
    return f(f$1(x));
   })
  };
 };
 SC$1.$cctor=Runtime.Cctor(function()
 {
  var g,bases;
  function f(v)
  {
   return Math.abs(v);
  }
  SC$1.StandardUniform={
   Base:[],
   Next:function()
   {
    return Math.random();
   }
  };
  SC$1.Boolean={
   Base:[true,false],
   Next:function()
   {
    return Random.StandardUniform().Next()>0.5;
   }
  };
  SC$1.Float={
   Base:[0],
   Next:function()
   {
    return(Random.Boolean().Next()?1:-1)*Random.Exponential(0.1).Next();
   }
  };
  SC$1.FloatExhaustive={
   Base:[0,Global.NaN,Global.Infinity,-Global.Infinity],
   Next:function()
   {
    return Random.Float().Next();
   }
  };
  SC$1.Int={
   Base:[0,1,-1],
   Next:function()
   {
    return Math.round(Random.Float().Next())>>0;
   }
  };
  SC$1.Natural={
   Base:[0,1],
   Next:(g=Random.Int().Next,function(x)
   {
    return f(g(x));
   })
  };
  SC$1.String={
   Base:[""],
   Next:function()
   {
    return Arrays.init(Random.Natural().Next()%100,function()
    {
     return String.fromCharCode(Random.Int().Next()%256);
    }).join("");
   }
  };
  SC$1.StringExhaustive={
   Base:[null,""],
   Next:Random.String().Next
  };
  SC$1.allTypes=(bases=[Random.Int(),Random.Float(),Random.Boolean(),Random.String()],bases.concat(Arrays.ofSeq(Seq.delay(function()
  {
   return Seq.collect(function(g$1)
   {
    return Seq.append(Seq.collect(function(h)
    {
     return Seq.append([Random.Tuple2Of(g$1,h)],Seq.delay(function()
     {
      return Seq.map(function(i)
      {
       return Random.Tuple3Of(g$1,h,i);
      },bases);
     }));
    },bases),Seq.delay(function()
    {
     return Seq.append([Random.ListOf(g$1)],Seq.delay(function()
     {
      return[Random.ArrayOf(g$1)];
     }));
    }));
   },bases);
  }))));
  SC$1.Anything=Random.MixManyWithoutBases(Random.allTypes());
  SC$1.$cctor=Global.ignore;
 });
 TestCategoryBuilder=Pervasives.TestCategoryBuilder=Runtime.Class({},WebSharper.Obj,TestCategoryBuilder);
 TestCategoryBuilder.New=Runtime.Ctor(function(name)
 {
  this.name=name;
 },TestCategoryBuilder);
 Runner.WithTimeout=function(timeOut,a)
 {
  return a;
 };
 Runner.AddTestAsync=function(t,r,asserter)
 {
  return Runner.MapAsync(function(args)
  {
   var b;
   b=null;
   return Concurrency.Delay(function()
   {
    return Concurrency.Bind(t(asserter,args),function()
    {
     return Concurrency.Return(args);
    });
   });
  },r(asserter));
 };
 Runner.AddTest=function(t,r,asserter)
 {
  return Runner.Map(function(args)
  {
   t(asserter,args);
   return args;
  },r(asserter));
 };
 Runner.MapAsync=function(f,x)
 {
  var args,b;
  return x.$==1?(args=x.$0,{
   $:1,
   $0:(b=null,Concurrency.Delay(function()
   {
    return Concurrency.Bind(args,function(a)
    {
     return f(a);
    });
   }))
  }):{
   $:1,
   $0:f(x.$0)
  };
 };
 Runner.Bind=function(f,x)
 {
  var args,b;
  return x.$==1?(args=x.$0,{
   $:1,
   $0:(b=null,Concurrency.Delay(function()
   {
    return Concurrency.Bind(args,function(a)
    {
     return Runner.ToAsync(f(a));
    });
   }))
  }):f(x.$0);
 };
 Runner.Map=function(f,x)
 {
  var args,b;
  return x.$==1?(args=x.$0,{
   $:1,
   $0:(b=null,Concurrency.Delay(function()
   {
    return Concurrency.Bind(args,function(a)
    {
     return Concurrency.Return(f(a));
    });
   }))
  }):{
   $:0,
   $0:f(x.$0)
  };
 };
 Runner.ToAsync=function(x)
 {
  var args,b;
  return x.$==1?x.$0:(args=x.$0,(b=null,Concurrency.Delay(function()
  {
   return Concurrency.Return(args);
  })));
 };
 SubtestBuilder=Pervasives.SubtestBuilder=Runtime.Class({
  For:function(r,y)
  {
   return function(asserter)
   {
    var m,a,b;
    m=r(asserter);
    return m.$==1?(a=m.$0,{
     $:1,
     $0:(b=null,Concurrency.Delay(function()
     {
      return Concurrency.Bind(a,function(a$1)
      {
       var m$1;
       m$1=(y(a$1))(asserter);
       return m$1.$==1?m$1.$0:Concurrency.Return(m$1.$0);
      });
     }))
    }):(y(m.$0))(asserter);
   };
  },
  Zero:function()
  {
   return function()
   {
    return{
     $:0,
     $0:void 0
    };
   };
  },
  Return:function(x)
  {
   return function()
   {
    return{
     $:0,
     $0:x
    };
   };
  },
  Yield:function(x)
  {
   return function()
   {
    return{
     $:0,
     $0:x
    };
   };
  },
  Bind:function(a,f)
  {
   return function(asserter)
   {
    var b;
    return{
     $:1,
     $0:(b=null,Concurrency.Delay(function()
     {
      return Concurrency.Bind(a,function(a$1)
      {
       var m;
       m=(f(a$1))(asserter);
       return m.$==1?m.$0:Concurrency.Return(m.$0);
      });
     }))
    };
   };
  },
  RunSubtest:function(r,subtest)
  {
   return function(asserter)
   {
    return Runner.Bind(function(a)
    {
     return(subtest(a))(asserter);
    },r(asserter));
   };
  },
  RaisesMsgAsync:function(r,value,message)
  {
   function t(asserter,args)
   {
    var value$1,b;
    value$1=value(args);
    b=null;
    return Concurrency.Delay(function()
    {
     return Concurrency.TryWith(Concurrency.Delay(function()
     {
      return Concurrency.Bind(value$1,function()
      {
       asserter.ok(false,message);
       return Concurrency.Return(null);
      });
     }),function()
     {
      asserter.ok(true,message);
      return Concurrency.Return(null);
     });
    });
   }
   return function(a)
   {
    return Runner.AddTestAsync(t,r,a);
   };
  },
  RaisesAsync:function(r,value)
  {
   function t(asserter,args)
   {
    var value$1,b;
    value$1=value(args);
    b=null;
    return Concurrency.Delay(function()
    {
     return Concurrency.TryWith(Concurrency.Delay(function()
     {
      return Concurrency.Bind(value$1,function()
      {
       asserter.ok(false,"Expected raised exception");
       return Concurrency.Return(null);
      });
     }),function()
     {
      asserter.ok(true);
      return Concurrency.Return(null);
     });
    });
   }
   return function(a)
   {
    return Runner.AddTestAsync(t,r,a);
   };
  },
  RaisesMsg:function(r,value,message)
  {
   function t(asserter,args)
   {
    try
    {
     value(args);
     return asserter.ok(false,message);
    }
    catch(m)
    {
     return asserter.ok(true,message);
    }
   }
   return function(a)
   {
    return Runner.AddTest(t,r,a);
   };
  },
  Raises:function(r,value)
  {
   function t(asserter,args)
   {
    try
    {
     value(args);
     return asserter.ok(false,"Expected raised exception");
    }
    catch(m)
    {
     return asserter.ok(true);
    }
   }
   return function(a)
   {
    return Runner.AddTest(t,r,a);
   };
  },
  For$1:function(gen,f)
  {
   return this.For$2(new Sample.New(gen),f);
  },
  For$2:function(sample,f)
  {
   return function(asserter)
   {
    var acc,src,l,e,r;
    acc={
     $:0,
     $0:void 0
    };
    src=sample.get_Data();
    while(true)
     if(src.$==1)
      {
       l=src.$1;
       e=src.$0;
       r=f(e);
       acc=(function(f$1)
       {
        return function(x)
        {
         return Runner.Bind(f$1,x);
        };
       }(function(r$1)
       {
        return function()
        {
         return r$1(asserter);
        };
       }(r)))(acc);
       src=l;
      }
     else
      return acc;
   };
  },
  PropertyWithSample:function(r,sample,attempt)
  {
   return function(asserter)
   {
    function loop(attempt$1,acc,src)
    {
     var l,e,r$1;
     while(true)
      if(src.$==1)
       {
        l=src.$1;
        e=src.$0;
        r$1=attempt$1(e);
        acc=(function(f)
        {
         return function(x)
         {
          return Runner.Bind(f,x);
         };
        }(function(r$2)
        {
         return function(args)
         {
          return Runner.Map(function()
          {
           return args;
          },r$2(asserter));
         };
        }(r$1)))(acc);
        src=l;
       }
      else
       return acc;
    }
    return Runner.Bind(function(args)
    {
     var sample$1;
     sample$1=sample(args);
     return loop(attempt(args),{
      $:0,
      $0:args
     },sample$1.get_Data());
    },r(asserter));
   };
  },
  ForEach:function(r,src,attempt)
  {
   return function(asserter)
   {
    function loop(attempt$1,acc,src$1)
    {
     var l,e,r$1;
     while(true)
      if(src$1.$==1)
       {
        l=src$1.$1;
        e=src$1.$0;
        r$1=attempt$1(e);
        acc=(function(f)
        {
         return function(x)
         {
          return Runner.Bind(f,x);
         };
        }(function(r$2)
        {
         return function(args)
         {
          return Runner.Map(function()
          {
           return args;
          },r$2(asserter));
         };
        }(r$1)))(acc);
        src$1=l;
       }
      else
       return acc;
    }
    return Runner.Bind(function(args)
    {
     return loop(attempt(args),{
      $:0,
      $0:args
     },List.ofSeq(src(args)));
    },r(asserter));
   };
  },
  IsFalseAsync:function(r,value,message)
  {
   function t(asserter,args)
   {
    var b;
    b=null;
    return Concurrency.Delay(function()
    {
     return Concurrency.Bind(value(args),function(a)
     {
      asserter.ok(!a,message);
      return Concurrency.Return(null);
     });
    });
   }
   return function(a)
   {
    return Runner.AddTestAsync(t,r,a);
   };
  },
  IsFalseAsync$1:function(r,value)
  {
   function t(asserter,args)
   {
    var b;
    b=null;
    return Concurrency.Delay(function()
    {
     return Concurrency.Bind(value(args),function(a)
     {
      asserter.ok(!a);
      return Concurrency.Return(null);
     });
    });
   }
   return function(a)
   {
    return Runner.AddTestAsync(t,r,a);
   };
  },
  IsFalseMsg:function(r,value,message)
  {
   function t(asserter,args)
   {
    return asserter.ok(!value(args),message);
   }
   return function(a)
   {
    return Runner.AddTest(t,r,a);
   };
  },
  IsFalse:function(r,value)
  {
   function t(asserter,args)
   {
    return asserter.ok(!value(args));
   }
   return function(a)
   {
    return Runner.AddTest(t,r,a);
   };
  },
  IsTrueMsgAsync:function(r,value,message)
  {
   function t(asserter,args)
   {
    var b;
    b=null;
    return Concurrency.Delay(function()
    {
     return Concurrency.Bind(value(args),function(a)
     {
      asserter.ok(a,message);
      return Concurrency.Return(null);
     });
    });
   }
   return function(a)
   {
    return Runner.AddTestAsync(t,r,a);
   };
  },
  IsTrueAsync:function(r,value)
  {
   function t(asserter,args)
   {
    var b;
    b=null;
    return Concurrency.Delay(function()
    {
     return Concurrency.Bind(value(args),function(a)
     {
      asserter.ok(a);
      return Concurrency.Return(null);
     });
    });
   }
   return function(a)
   {
    return Runner.AddTestAsync(t,r,a);
   };
  },
  IsTrueMsg:function(r,value,message)
  {
   function t(asserter,args)
   {
    return asserter.ok(value(args),message);
   }
   return function(a)
   {
    return Runner.AddTest(t,r,a);
   };
  },
  IsTrue:function(r,value)
  {
   function t(asserter,args)
   {
    return asserter.ok(value(args));
   }
   return function(a)
   {
    return Runner.AddTest(t,r,a);
   };
  },
  NotApproxEqualMsgAsync:function(r,actual,expected,message)
  {
   function t(asserter,args)
   {
    var b;
    b=null;
    return Concurrency.Delay(function()
    {
     var expected$1;
     expected$1=expected(args);
     return Concurrency.Bind(actual(args),function(a)
     {
      asserter.push(Math.abs(a-expected$1)>0.0001,a,expected$1,message);
      return Concurrency.Return(null);
     });
    });
   }
   return function(a)
   {
    return Runner.AddTestAsync(t,r,a);
   };
  },
  NotApproxEqualAsync:function(r,actual,expected)
  {
   function t(asserter,args)
   {
    var b;
    b=null;
    return Concurrency.Delay(function()
    {
     var expected$1;
     expected$1=expected(args);
     return Concurrency.Bind(actual(args),function(a)
     {
      asserter.push(Math.abs(a-expected$1)>0.0001,a,expected$1);
      return Concurrency.Return(null);
     });
    });
   }
   return function(a)
   {
    return Runner.AddTestAsync(t,r,a);
   };
  },
  NotApproxEqualMsg:function(r,actual,expected,message)
  {
   function t(asserter,args)
   {
    var actual$1,expected$1;
    actual$1=actual(args);
    expected$1=expected(args);
    return asserter.push(Math.abs(actual$1-expected$1)>0.0001,actual$1,expected$1,message);
   }
   return function(a)
   {
    return Runner.AddTest(t,r,a);
   };
  },
  NotApproxEqual:function(r,actual,expected)
  {
   function t(asserter,args)
   {
    var actual$1,expected$1;
    actual$1=actual(args);
    expected$1=expected(args);
    return asserter.push(Math.abs(actual$1-expected$1)>0.0001,actual$1,expected$1);
   }
   return function(a)
   {
    return Runner.AddTest(t,r,a);
   };
  },
  ApproxEqualMsgAsync:function(r,actual,expected,message)
  {
   function t(asserter,args)
   {
    var b;
    b=null;
    return Concurrency.Delay(function()
    {
     var expected$1;
     expected$1=expected(args);
     return Concurrency.Bind(actual(args),function(a)
     {
      asserter.push(Math.abs(a-expected$1)<0.0001,a,expected$1,message);
      return Concurrency.Return(null);
     });
    });
   }
   return function(a)
   {
    return Runner.AddTestAsync(t,r,a);
   };
  },
  ApproxEqualAsync:function(r,actual,expected)
  {
   function t(asserter,args)
   {
    var b;
    b=null;
    return Concurrency.Delay(function()
    {
     var expected$1;
     expected$1=expected(args);
     return Concurrency.Bind(actual(args),function(a)
     {
      asserter.push(Math.abs(a-expected$1)<0.0001,a,expected$1);
      return Concurrency.Return(null);
     });
    });
   }
   return function(a)
   {
    return Runner.AddTestAsync(t,r,a);
   };
  },
  ApproxEqualMsg:function(r,actual,expected,message)
  {
   function t(asserter,args)
   {
    var actual$1,expected$1;
    actual$1=actual(args);
    expected$1=expected(args);
    return asserter.push(Math.abs(actual$1-expected$1)<0.0001,actual$1,expected$1,message);
   }
   return function(a)
   {
    return Runner.AddTest(t,r,a);
   };
  },
  ApproxEqual:function(r,actual,expected)
  {
   function t(asserter,args)
   {
    var actual$1,expected$1;
    actual$1=actual(args);
    expected$1=expected(args);
    return asserter.push(Math.abs(actual$1-expected$1)<0.0001,actual$1,expected$1);
   }
   return function(a)
   {
    return Runner.AddTest(t,r,a);
   };
  },
  DeepEqualMsgAsync:function(r,actual,expected,message)
  {
   function t(asserter,args)
   {
    var b;
    b=null;
    return Concurrency.Delay(function()
    {
     var expected$1;
     expected$1=expected(args);
     return Concurrency.Bind(actual(args),function(a)
     {
      asserter.deepEqual(a,expected$1,message);
      return Concurrency.Return(null);
     });
    });
   }
   return function(a)
   {
    return Runner.AddTestAsync(t,r,a);
   };
  },
  DeepEqualAsync:function(r,actual,expected)
  {
   function t(asserter,args)
   {
    var b;
    b=null;
    return Concurrency.Delay(function()
    {
     var expected$1;
     expected$1=expected(args);
     return Concurrency.Bind(actual(args),function(a)
     {
      asserter.deepEqual(a,expected$1);
      return Concurrency.Return(null);
     });
    });
   }
   return function(a)
   {
    return Runner.AddTestAsync(t,r,a);
   };
  },
  DeepEqualMsg:function(r,actual,expected,message)
  {
   function t(asserter,args)
   {
    return asserter.deepEqual(actual(args),expected(args),message);
   }
   return function(a)
   {
    return Runner.AddTest(t,r,a);
   };
  },
  DeepEqual:function(r,actual,expected)
  {
   function t(asserter,args)
   {
    return asserter.deepEqual(actual(args),expected(args));
   }
   return function(a)
   {
    return Runner.AddTest(t,r,a);
   };
  },
  JsEqualMsgAsync:function(r,actual,expected,message)
  {
   function t(asserter,args)
   {
    var b;
    b=null;
    return Concurrency.Delay(function()
    {
     var expected$1;
     expected$1=expected(args);
     return Concurrency.Bind(actual(args),function(a)
     {
      asserter.equal(a,expected$1,message);
      return Concurrency.Return(null);
     });
    });
   }
   return function(a)
   {
    return Runner.AddTestAsync(t,r,a);
   };
  },
  JsEqualAsync:function(r,actual,expected)
  {
   function t(asserter,args)
   {
    var b;
    b=null;
    return Concurrency.Delay(function()
    {
     var expected$1;
     expected$1=expected(args);
     return Concurrency.Bind(actual(args),function(a)
     {
      asserter.equal(a,expected$1);
      return Concurrency.Return(null);
     });
    });
   }
   return function(a)
   {
    return Runner.AddTestAsync(t,r,a);
   };
  },
  JsEqualMsg:function(r,actual,expected,message)
  {
   function t(asserter,args)
   {
    return asserter.equal(actual(args),expected(args),message);
   }
   return function(a)
   {
    return Runner.AddTest(t,r,a);
   };
  },
  JsEqual:function(r,actual,expected)
  {
   function t(asserter,args)
   {
    return asserter.equal(actual(args),expected(args));
   }
   return function(a)
   {
    return Runner.AddTest(t,r,a);
   };
  },
  NotEqualMsgAsync:function(r,actual,expected,message)
  {
   function t(asserter,args)
   {
    var b;
    b=null;
    return Concurrency.Delay(function()
    {
     var expected$1;
     expected$1=expected(args);
     return Concurrency.Bind(actual(args),function(a)
     {
      asserter.push(!Unchecked.Equals(a,expected$1),a,expected$1,message);
      return Concurrency.Return(null);
     });
    });
   }
   return function(a)
   {
    return Runner.AddTestAsync(t,r,a);
   };
  },
  NotEqualAsync:function(r,actual,expected)
  {
   function t(asserter,args)
   {
    var b;
    b=null;
    return Concurrency.Delay(function()
    {
     var expected$1;
     expected$1=expected(args);
     return Concurrency.Bind(actual(args),function(a)
     {
      asserter.push(!Unchecked.Equals(a,expected$1),a,expected$1);
      return Concurrency.Return(null);
     });
    });
   }
   return function(a)
   {
    return Runner.AddTestAsync(t,r,a);
   };
  },
  NotEqualMsg:function(r,actual,expected,message)
  {
   function t(asserter,args)
   {
    var actual$1,expected$1;
    actual$1=actual(args);
    expected$1=expected(args);
    return asserter.push(!Unchecked.Equals(actual$1,expected$1),actual$1,expected$1,message);
   }
   return function(a)
   {
    return Runner.AddTest(t,r,a);
   };
  },
  NotEqual:function(r,actual,expected)
  {
   function t(asserter,args)
   {
    var actual$1,expected$1;
    actual$1=actual(args);
    expected$1=expected(args);
    return asserter.push(!Unchecked.Equals(actual$1,expected$1),actual$1,expected$1);
   }
   return function(a)
   {
    return Runner.AddTest(t,r,a);
   };
  },
  EqualMsgAsync:function(r,actual,expected,message)
  {
   function t(asserter,args)
   {
    var b;
    b=null;
    return Concurrency.Delay(function()
    {
     var expected$1;
     expected$1=expected(args);
     return Concurrency.Bind(actual(args),function(a)
     {
      asserter.push(Unchecked.Equals(a,expected$1),a,expected$1,message);
      return Concurrency.Return(null);
     });
    });
   }
   return function(a)
   {
    return Runner.AddTestAsync(t,r,a);
   };
  },
  EqualAsync:function(r,actual,expected)
  {
   function t(asserter,args)
   {
    var b;
    b=null;
    return Concurrency.Delay(function()
    {
     var expected$1;
     expected$1=expected(args);
     return Concurrency.Bind(actual(args),function(a)
     {
      asserter.push(Unchecked.Equals(a,expected$1),a,expected$1);
      return Concurrency.Return(null);
     });
    });
   }
   return function(a)
   {
    return Runner.AddTestAsync(t,r,a);
   };
  },
  EqualMsg:function(r,actual,expected,message)
  {
   function t(asserter,args)
   {
    var actual$1,expected$1;
    actual$1=actual(args);
    expected$1=expected(args);
    return asserter.push(Unchecked.Equals(actual$1,expected$1),actual$1,expected$1,message);
   }
   return function(a)
   {
    return Runner.AddTest(t,r,a);
   };
  },
  Equal:function(r,actual,expected)
  {
   function t(asserter,args)
   {
    var actual$1,expected$1;
    actual$1=actual(args);
    expected$1=expected(args);
    return asserter.push(Unchecked.Equals(actual$1,expected$1),actual$1,expected$1);
   }
   return function(a)
   {
    return Runner.AddTest(t,r,a);
   };
  },
  Expect:function(r,assertionCount)
  {
   function t(asserter,args)
   {
    return asserter.expect(assertionCount(args));
   }
   return function(a)
   {
    return Runner.AddTest(t,r,a);
   };
  }
 },WebSharper.Obj,SubtestBuilder);
 SubtestBuilder.New=Runtime.Ctor(function()
 {
 },SubtestBuilder);
 TestBuilder=Pervasives.TestBuilder=Runtime.Class({
  Run:function(e)
  {
   QUnit.test(this.name,function(asserter)
   {
    var m,asy,done,b;
    try
    {
     m=e(asserter);
     m.$==1?(asy=m.$0,(done=asserter.async(),Concurrency.Start((b=null,Concurrency.Delay(function()
     {
      return Concurrency.TryFinally(Concurrency.Delay(function()
      {
       return Concurrency.TryWith(Concurrency.Delay(function()
       {
        return Concurrency.Bind(Runner.WithTimeout(1000,asy),function()
        {
         return Concurrency.Return(null);
        });
       }),function(a)
       {
        asserter.equal(a,null,"Test threw an unexpected asynchronous exception");
        return Concurrency.Return(null);
       });
      }),function()
      {
       done();
      });
     })),null))):null;
    }
    catch(e$1)
    {
     asserter.equal(e$1,null,"Test threw an unexpected synchronous exception");
    }
   });
  }
 },SubtestBuilder,TestBuilder);
 TestBuilder.New=Runtime.Ctor(function(name)
 {
  SubtestBuilder.New.call(this);
  this.name=name;
 },TestBuilder);
 Pervasives.PropertyWithSample=function(name,set,f)
 {
  var b;
  b=Pervasives.Test(name);
  b.Run(b.PropertyWithSample(b.Yield(),function()
  {
   return set;
  },function()
  {
   return f;
  }));
 };
 Pervasives.PropertyWith=function(name,gen,f)
 {
  var b;
  b=Pervasives.Test(name);
  b.Run(b.PropertyWithSample(b.Yield(),function($1)
  {
   return Sample.Make(gen,100);
  },function()
  {
   return f;
  }));
 };
 Pervasives.Do=function()
 {
  SC$2.$cctor();
  return SC$2.Do;
 };
 Pervasives.Test=function(name)
 {
  return new TestBuilder.New(name);
 };
 Pervasives.TestCategory=function(name)
 {
  return new TestCategoryBuilder.New(name);
 };
 SC$2.$cctor=Runtime.Cctor(function()
 {
  SC$2.Do=new SubtestBuilder.New();
  SC$2.$cctor=Global.ignore;
 });
 RunnerControlBody=Runner$1.RunnerControlBody=Runtime.Class({
  ReplaceInDom:function(e)
  {
   var fixture,qunit,parent;
   Unchecked.Equals(Global.document.querySelector("#qunit"),null)?(fixture=Global.document.createElement("div"),fixture.setAttribute("id","qunit-fixture"),qunit=Global.document.createElement("div"),qunit.setAttribute("id","qunit"),parent=e.parentNode,parent.replaceChild(fixture,e),parent.insertBefore(qunit,fixture)):void 0;
   this.run();
  }
 },WebSharper.Obj,RunnerControlBody);
 RunnerControlBody.New=Runtime.Ctor(function(run)
 {
  this.run=run;
 },RunnerControlBody);
 Runner$1.RunTests=function(tests)
 {
  return new RunnerControlBody.New(function()
  {
   var e,t;
   e=Enumerator.Get(tests);
   try
   {
    while(e.MoveNext())
     {
      t=e.Current();
      QUnit.module(t.name);
      t.run();
     }
   }
   finally
   {
    if("Dispose"in e)
     e.Dispose();
   }
  });
 };
 TestCategory=Testing.TestCategory=Runtime.Class({
  Run:function(name,f)
  {
   var $this;
   $this=this;
   QUnit.test(name,function(a)
   {
    var done,t;
    $this.asserter=a;
    done=$this.asserter.async();
    t=f();
    t.ContinueWith$2(function()
    {
     return done();
    });
    Unchecked.Equals(t.get_Status(),0)?t.Start():void 0;
   });
  },
  Run$1:function(name,f)
  {
   var $this;
   $this=this;
   QUnit.test(name,function(a)
   {
    $this.asserter=a;
    f();
   });
  },
  Expect:function(n)
  {
   this.asserter.expect(n);
  },
  Raises:function(expr,message)
  {
   try
   {
    expr();
    this.asserter.ok(false,message);
   }
   catch(m)
   {
    this.asserter.ok(true,message);
   }
  },
  Raises$1:function(expr)
  {
   try
   {
    expr();
    this.asserter.ok(false,"Expected raised exception");
   }
   catch(m)
   {
    this.asserter.ok(true);
   }
  },
  Raises$2:function(expr,message)
  {
   try
   {
    expr();
    this.asserter.ok(false,message);
   }
   catch(m)
   {
    this.asserter.ok(true,message);
   }
  },
  Raises$3:function(expr)
  {
   try
   {
    expr();
    this.asserter.ok(false,"Expected raised exception");
   }
   catch(m)
   {
    this.asserter.ok(true);
   }
  },
  IsFalse:function(value,message)
  {
   this.asserter.ok(!value);
  },
  IsFalse$1:function(value)
  {
   this.asserter.ok(!value);
  },
  IsTrue:function(value,message)
  {
   this.asserter.ok(value,message);
  },
  IsTrue$1:function(value)
  {
   this.asserter.ok(value);
  },
  NotApproxEqual:function(x,y,epsilon,message)
  {
   this.asserter.push(Math.abs(x-y)>epsilon,x,y,message);
  },
  NotApproxEqual$1:function(x,y,message)
  {
   this.asserter.push(Math.abs(x-y)>0.0001,x,y,message);
  },
  NotApproxEqual$2:function(x,y,epsilon)
  {
   this.asserter.push(Math.abs(x-y)>epsilon,x,y);
  },
  NotApproxEqual$3:function(x,y)
  {
   this.asserter.push(Math.abs(x-y)>0.0001,x,y);
  },
  ApproxEqual:function(x,y,epsilon,message)
  {
   this.asserter.push(Math.abs(x-y)<epsilon,x,y,message);
  },
  ApproxEqual$1:function(x,y,message)
  {
   this.asserter.push(Math.abs(x-y)<0.0001,x,y,message);
  },
  ApproxEqual$2:function(x,y,epsilon)
  {
   this.asserter.push(Math.abs(x-y)<epsilon,x,y);
  },
  ApproxEqual$3:function(x,y)
  {
   this.asserter.push(Math.abs(x-y)<0.0001,x,y);
  },
  DeepEqual:function(x,y,message)
  {
   this.asserter.deepEqual(x,y,message);
  },
  DeepEqual$1:function(x,y)
  {
   this.asserter.deepEqual(x,y);
  },
  JsEqual:function(x,y,message)
  {
   this.asserter.equal(x,y,message);
  },
  JsEqual$1:function(x,y)
  {
   this.asserter.equal(x,y);
  },
  NotEqual:function(x,y,message)
  {
   this.asserter.push(!Unchecked.Equals(x,y),x,y,message);
  },
  NotEqual$1:function(x,y)
  {
   this.asserter.push(!Unchecked.Equals(x,y),x,y);
  },
  Equal:function(x,y,message)
  {
   this.asserter.push(Unchecked.Equals(x,y),x,y,message);
  },
  Equal$1:function(x,y)
  {
   this.asserter.push(Unchecked.Equals(x,y),x,y);
  }
 },WebSharper.Obj,TestCategory);
 TestCategory.New=Runtime.Ctor(function()
 {
  this.asserter=null;
 },TestCategory);
}());
