using AspectCore.DynamicProxy;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace Apsk.AOP
{
    /// <summary>
    /// SQL事务AOP拦截器
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class TransactionalAttribute
        : AbstractInterceptorAttribute
    {
        /// <summary>
        /// 超时秒数
        /// </summary>
        public int Timeout { get; private set; } = 1;

        /// <summary>
        /// 事务处理级别
        /// </summary>
        public IsolationLevel Isolation { get; private set; }

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {

#if DEBUG
            System.Console.WriteLine("Transaction start......");
#endif

            var transactionOption = new TransactionOptions()
            {
                IsolationLevel = Isolation,
                Timeout = TimeSpan.FromSeconds(Timeout)
            };

            using (var transcation = new TransactionScope(new TransactionScopeOption(), transactionOption, TransactionScopeAsyncFlowOption.Enabled))
            {
                await next(context);
                transcation.Complete();
            }
#if DEBUG
            System.Console.WriteLine("Transaction end......");
#endif
        }
    }
}
