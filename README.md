## Apsk (Application Shared Kernel)

``` csharp
  var sp=new ServicsCollection()
        .AddComponents()//di
        .AddBus()//event bus
        .AddRestController()//dynamic api
        .Build()
```

1. Components
   * [x] Dependency Injection
   * [x] AOP
   * [x] Logger
   * [x] Configuration

2. API(restful api)
   * [x] Dynamic API
   * [x] Global Rest Response
   * [ ] Jwt Authentication
   * [ ] Validation

3. [x] Schedule

4. MQ (rabbit mq)
   * EventBus