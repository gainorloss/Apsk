## Apsk (Application Shared Kernel)

``` csharp
  var sp=new ServicsCollection()
        .AddComponents()//di
        .AddBus()//event bus
        .AddRestController()//dynamic api
        .Build()
```

###  Components
   * [x] Dependency Injection
   * [x] AOP
   * [x] Logger
   * [x] Configuration
   * [x] Schedule

### API(restful api)
   * [x] Dynamic API
   * [ ] Filters
     * [x] Gloabal exception filter
     * [ ] Gloabal authentication filter
   * [x] Global Rest Response
   * [ ] Jwt Authentication
   * [ ] Validation

### MQ (rabbit mq)
   * EventBus