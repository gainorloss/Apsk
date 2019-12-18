namespace Apsk.AspNetCore
{
    public class RestResult
    {
        #region Ctor.
        private RestResult(bool success, string errorCode = "", string errorMsg = "", object result = null)
        {
            Success = success;
            ErrorCode = errorCode;
            ErrorMsg = errorMsg;

            if (result != null)
                Result = result;
        }
        private RestResult()
        { }
        #endregion

        #region Props.
        public bool Success { get; private set; }
        public string ErrorCode { get; private set; }
        public string ErrorMsg { get; private set; }
        public object Result { get; private set; }
        #endregion

        #region Static methods.
        public static RestResult Create(bool success, string errorCode = "", string errorMsg = "", object result = null)
        {
            return new RestResult(success, errorCode, errorMsg, result);
        }

        public static RestResult Ok(object result = null)
        {
            return Create(true, result: result);
        }

        public static RestResult Error(string errorCode = "", string errorMsg = "")
        {
            return Create(false, errorCode, errorMsg);
        }
        #endregion
    }
}
