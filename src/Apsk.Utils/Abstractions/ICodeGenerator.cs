namespace Apsk.Utils.Abstractions
{
    public interface ICodeGenerator
    {
        /// <summary>
        /// generate orderNo.
        /// </summary>
        /// <returns></returns>
        string GenerateOrderNo();

        /// <summary>
        /// generate distributed id.
        /// </summary>
        /// <returns></returns>
        long GenerateDid();
    }
}
