/************************************************************************************************
 * 
 * 内存事件总线
 * 
 * Creator:【gainorloss】
 * CreatedAt:【2019年11月26日11:36:38】
 * 
 * **********************************************************************************************/
using Apsk.Abstractions;

namespace Apsk
{
    internal class EventProcessedArgs
    {
        public IEvent Event { get; private set; }
        public EventProcessedArgs(IEvent @event)
        {
            Event = @event;
        }
    }
}