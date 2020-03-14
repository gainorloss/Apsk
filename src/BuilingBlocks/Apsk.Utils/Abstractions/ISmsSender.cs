// <copyright file="ISmsSender.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Utils.Abstractions
{
    using Apsk.Utils.Models;

    public interface ISmsSender
    {
        SendSmsResponse SendCaptcha(string mobile, string code);
    }
}
