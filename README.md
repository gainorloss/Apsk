## Apsk (Application Shared Kernel)

We should enter in this in the entry point.
``` csharp
  var sp=new ServicsCollection()
        .AddComponents()//di
        .AddBus()//event bus
        .AddRestController()//dynamic api
        .Build()
```
![.NET%20Core](https://github.com/gainorloss/Apsk/workflows/.NET%20Core/badge.svg)
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