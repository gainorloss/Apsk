## Apsk (Application Shared Kernel)

We should enter in this in the entry point.
``` csharp
  var sp=new ServiceCollection()
        .AddComponents()//di
        .AddBus()//event bus
        .AddRestControllers()//dynamic api
        .BuildServiceProvider();
```
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
   * [ ] Filters
     * [x] Gloabal exception filter
     * [ ] Gloabal authentication filter
   * [ ] Jwt authentication
   * [ ] Validation

   * [x] Dynamic API

### MQ (rabbit mq)
   * EventBus