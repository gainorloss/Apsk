// <copyright file="CodeGenerator.cs" company="apsk">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Apsk.Utils
{
    using System;
    using Apsk.Annotations;
    using Apsk.Utils.Abstractions;
    using IdGen;

    [Component(ComponentLifeTimeScope.Singleton)]
    public class CodeGenerator
         : ICodeGenerator
    {
        private static IdGenerator _generator;

        private static readonly object _lock = new object();

        /// <inheritdoc/>
        public long GenerateDid()
        {
            return CreateIdGenerator().CreateId();
        }

        /// <inheritdoc/>
        public string GenerateOrderNo()
        {
            string strDateTimeNumber = DateTime.Now.ToString("yyyyMMddHHmmssms");
            string strRandomResult = NextRandom(1000, 1).ToString();
            return strDateTimeNumber + strRandomResult;
        }

        /// <summary>
        /// 参考：msdn上的RNGCryptoServiceProvider例子.
        /// </summary>
        /// <param name="numSeeds"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private int NextRandom(int numSeeds, int length)
        {
            // Create a byte array to hold the random value.
            byte[] randomNumber = new byte[length];

            // Create a new instance of the RNGCryptoServiceProvider.
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();

            // Fill the array with a random value.
            rng.GetBytes(randomNumber);

            // Convert the byte to an uint value to make the modulus operation easier.
            uint randomResult = 0x0;
            for (int i = 0; i < length; i++)
            {
                randomResult |= (uint)randomNumber[i] << ((length - 1 - i) * 8);
            }

            return (int)(randomResult % numSeeds) + 1;
        }

        private static IdGenerator CreateIdGenerator()
        {
            if (_generator == null)
            {
                lock (_lock)
                {
                    if (_generator == null)
                    {
                        _generator = new IdGenerator(0);
                    }
                }
            }

            return _generator;
        }
    }
}
