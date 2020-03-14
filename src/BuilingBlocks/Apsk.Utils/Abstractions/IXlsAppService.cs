using System.Collections.Generic;

namespace Apsk.Utils.Abstractions
{
    public interface IXlsAppService
    {
        /// <summary>
        /// 提取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        IEnumerable<T> Extract<T>(string fileName)
             where T : class, new();

        /// <summary>
        /// 保存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        void Save<T>(string fileName, IEnumerable<T> data);
    }
}
