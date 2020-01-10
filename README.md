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
![](https://github.com/apsk/Apsk/workflows/build/badge.svg)
![](https://github.com/apsk/Apsk/workflows/publish/badge.svg)
![](https://github.com/apsk/Apsk/workflows/test/badge.svg)
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