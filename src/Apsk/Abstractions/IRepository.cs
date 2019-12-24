/******************************************************************
 * 
 *   仓储接口
 *   
 *   Creator: gainorloss
 * CreatedAt:【2019-12-11 14:10:05】
 */

namespace Apsk.Abstractions
{
    public interface IRepository<T, ID>
        where T : Entity<ID>
    {
        /// <summary>
        /// generate id
        /// </summary>
        /// <returns></returns>
        ID GenerateId();

        /// <summary>
        /// load
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T LoadByIdAsync(ID id);
    }
}
