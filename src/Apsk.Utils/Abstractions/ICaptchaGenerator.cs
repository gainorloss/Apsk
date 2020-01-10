// <copyright file="ICaptchaGenerator.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Utils.Abstractions
{
    using System.DrawingCore;

    public interface ICaptchaGenerator
    {
         string CreateVerifyCode(CaptchaType type);

        /// <summary>
        /// 验证码图片 => <see cref="Bitmap"/>.
        /// </summary>
        /// <param name="verifyCode">验证码</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <returns>Bitmap</returns>
         Bitmap CreateBitmapByImgVerifyCode(string verifyCode, int width, int height);

        /// <summary>
        /// 验证码图片 => byte[].
        /// </summary>
        /// <param name="verifyCode">验证码</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <returns>byte[].</returns>
         byte[] CreateByteByImgVerifyCode(string verifyCode, int width, int height);
    }
}
