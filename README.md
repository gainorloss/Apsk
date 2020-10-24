## Apsk (Application Shared Kernel)

We should enter in this in the entry point.
``` csharp
  var configuration=new ConfigurationBuiler()
                     .Build();//initialize configuration.

  var sp=new ServiceCollection()
        .AddApskComponents(configuration)//di
        .AddApskBus()//event bus
        .AddApskRestControllers()//dynamic api
        .AddApskJwtBearer(configuration)//jwt bearer authentication
        .BuildServiceProvider();
```
[![Travis Status](https://secure.travis-ci.org/gainorloss/Apsk.png)](https://travis-ci.org/gainorloss/Apsk)
[![Coverage Status](https://coveralls.io/repos/github/gainorloss/Apsk/badge.svg?branch=master)](https://coveralls.io/github/gainorloss/Apsk?branch=master)
![](https://github.com/gainorloss/Apsk/workflows/build/badge.svg)
![](https://github.com/gainorloss/Apsk/workflows/publish/badge.svg)
![](https://github.com/gainorloss/Apsk/workflows/test/badge.svg)
   
