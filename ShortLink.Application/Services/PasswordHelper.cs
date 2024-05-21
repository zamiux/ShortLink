using ShortLink.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShortLink.Application.Services
{
    public class PasswordHelper : IPasswordHelper
    {
        public string EncodePasswordMD5(string pass)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 mD5;

            mD5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(pass);
            encodedBytes = mD5.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes);

        }
    }
}
