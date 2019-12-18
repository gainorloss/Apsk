/******************************************************************
 * 
 *   工作单元
 *   
 *   Creator: gainorloss
 * CreatedAt:【2019-12-11 13:54:21】
 */
using System.Threading.Tasks;

namespace Apsk.Abstractions
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
    }
}
