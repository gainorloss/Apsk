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
[![Build Status](https://secure.travis-ci.org/gainorloss/Apsk.png)](https://travis-ci.org/gainorloss/Apsk)
![](https://github.com/gainorloss/Apsk/workflows/build/badge.svg)
![](https://github.com/gainorloss/Apsk/workflows/publish/badge.svg)
![](https://github.com/gainorloss/Apsk/workflows/test/badge.svg)
###  Components
   * [x] Dependency Injection
   * [x] AOP
   * [x] Logger
   * [x] Configuration
   * [x] Schedule

### API(restful api)
   * [x] Global rest response
   * [x] Filters
     * [x] Gloabal exception filter
   * [x] Jwt authentication
   * [x] Open api document
   * [ ] Validation
   * [x] Dynamic API

### MQ (rabbit mq)
   * EventBus

### Utils 
   * [x] ICaptchaGenerator:generate captcha
   * [x] ICodeGenerator:generate snowflake id & orderNo.
   * [x] ISmsSender:aliyun send sms.
   * [x] IXlsAppService:excel
   
