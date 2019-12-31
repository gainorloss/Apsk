// <copyright file="StringExtensions.cs" company="gainorloss">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Utils.Extensions
{
    using System;

    /// <summary>
    /// Extensions for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Truncate.
        /// </summary>
        /// <param name="oldStr"></param>
        /// <param name="maxLength"></param>
        /// <param name="endWith"></param>
        /// <returns><see cref="string"/>.</returns>
        public static string Truncate(string oldStr, int maxLength=6, string endWith="...")
        {
            // 判断原字符串是否为空
            if (string.IsNullOrEmpty(oldStr))
                return oldStr + endWith;

            // 返回字符串的长度必须大于1
            if (maxLength < 1)
                throw new ArgumentException("返回的字符串长度必须大于[0] ");

            // 判断原字符串是否大于最大长度
            if (oldStr.Length > maxLength)
            {
                // 截取原字符串
                string strTmp = oldStr.Substring(0, maxLength);

                // 判断后缀是否为空
                if (string.IsNullOrEmpty(endWith))
                    return strTmp;
                else
                    return strTmp + endWith;
            }

            return oldStr;
        }
    }
}
