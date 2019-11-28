/************************************************************************************************
 * 
 * 事件处理器标注默认注册为IEventHandler
 * 
 * Creator:【gainorloss】
 * CreatedAt:【2019年11月26日11:32:53】
 * 
 * **********************************************************************************************/
using Infrastructure.Annotations;
using Infrastructure.Bus.Abstractions;

namespace Infrastructure.Bus.Annotations
{
    public class EventHandlerAttribute
        :ComponentAttribute
    {
        public EventHandlerAttribute()
        {
            ServiceType = typeof(IEventHandler);
        }
    }
}
