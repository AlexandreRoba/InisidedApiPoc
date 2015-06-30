using System;
using System.Security.Cryptography;
using System.Text;

namespace Insided.Api
{
    public class SignatureHasher
    {
        private byte[] _key;
        private HMACSHA256 _hmac;
        public SignatureHasher(string secretKey)
        {
            _key = Encoding.UTF8.GetBytes(secretKey);
            _hmac = new HMACSHA256 { Key = _key };
        }

        public string Hash(string input)
        {
            var inputInBytes = Encoding.UTF8.GetBytes(input);
            var encoded = _hmac.ComputeHash(inputInBytes);

            return Convert.ToBase64String(encoded);
        }
    }
}