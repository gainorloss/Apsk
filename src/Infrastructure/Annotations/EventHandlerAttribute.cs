/************************************************************************************************
 * 
 * 事件处理器标注默认注册为EventHandler
 * 
 * Creator:【gainorloss】
 * CreatedAt:【2019年11月26日11:32:53】
 * 
 * **********************************************************************************************/
using Infrastructure.Annotations;

namespace Infrastructure.Annotations
{
    public class EventHandlerAttribute
        : ComponentAttribute
    {
        public EventHandlerAttribute()
        { }
    }
}
