using CSRedis;
using Infrastructure.Annotations;
using Infrastructure.Bus.Abstractions;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Infrastructure.Bus.Redis
{
    [Component(ComponentLifeTimeScope.Singleton)]
    public class RedisEventBus
          : IEventBus
    {
        static RedisEventBus()
        {
            var csredis = new CSRedisClient(null, "r-8vbegt4nw2n5k8rvo0pd.redis.zhangbei.rds.aliyuncs.com:6379,password=FhXhDs85588100,defaultDatabase=13,prefix=et.erp:");
            RedisHelper.Initialization(csredis);
        }

        private readonly IEventHandlerExecutionContext _ctx;
        public RedisEventBus(IEventHandlerExecutionContext ctx)
        {
            _ctx = ctx;
        }
        public void Dispose()
        { }

        public void Publish<T>(T @event) where T : IEvent
        {
            var eventKey = _ctx.GetEventKey<T>();
            var jsonStr = JsonConvert.SerializeObject(@event);
            RedisHelper.Publish(eventKey, jsonStr);
        }

        public async Task PublishAsync<T>(T @event) where T : IEvent
        {
            var eventKey = _ctx.GetEventKey<T>();
            var jsonStr = JsonConvert.SerializeObject(@event);
            await RedisHelper.PublishAsync(eventKey, jsonStr);
        }

        public void Subscribe<T, TH>()
            where T : IEvent
            where TH : IEventHandler<T>
        {
            _ctx.Register(typeof(T), typeof(TH));

            var eventKey = _ctx.GetEventKey<T>();
            RedisHelper.Subscribe((eventKey, async args =>
             {
                 var body = args.Body;
                 var @event = JsonConvert.DeserializeObject<T>(body);
                 await _ctx.HandleAsync(@event);
             }
            ));
        }

        public void Subscribe()
        {
            _ctx.Register(eventType =>
            {
                var eventKey = _ctx.GetEventKey(eventType);

                RedisHelper.Subscribe((eventKey, async args =>
                {
                    var body = args.Body;
                    var @event = JsonConvert.DeserializeObject(body, eventType) as IEvent;
                    await _ctx.HandleAsync(@event);
                }
                ));
            });
        }
    }
}
