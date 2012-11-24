#region license

//Copyright 2009 Zack Owens

//Licensed under the Microsoft Public License (Ms-PL) (the "License"); 
//you may not use this file except in compliance with the License. 
//You may obtain a copy of the License at 

//http://clubstarterkit.codeplex.com/license

//Unless required by applicable law or agreed to in writing, software 
//distributed under the License is distributed on an "AS IS" BASIS, 
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//See the License for the specific language governing permissions and 
//limitations under the License. 

#endregion

using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using ClubStarterKit.Infrastructure;

namespace ClubStarterKit.Web.Infrastructure.Membership
{
    public class PasswordCryptographyProvider : IPasswordCryptographyProvider
    {
        private const int SaltCharsMin = 10;
        private const int SaltCharsMax = 10;

        public string GenerateSalt()
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            int size = random.Next(SaltCharsMin, SaltCharsMax);

            for (int i = 0; i < size; i++)
            {
                char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public string GeneratePassword(string plainText, string salt)
        {
            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();

            byte[] keyArray = md5Provider.ComputeHash(UTF8Encoding.UTF8.GetBytes(salt));
            byte[] passwordArray = UTF8Encoding.UTF8.GetBytes(plainText);
            md5Provider.Clear();

            TripleDESCryptoServiceProvider cryptProvider = new TripleDESCryptoServiceProvider();
            cryptProvider.Key = keyArray;
            cryptProvider.Mode = CipherMode.ECB;
            cryptProvider.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = cryptProvider.CreateEncryptor();
            byte[] result = cTransform.TransformFinalBlock(passwordArray, 0, passwordArray.Length);
            cryptProvider.Clear();

            return Convert.ToBase64String(result, 0, result.Length);
        }

        public bool ValidatePassword(string saltedPassword, string salt, string plainText)
        {
            return saltedPassword.Equals(GeneratePassword(plainText, salt), StringComparison.Ordinal);
        }

        public MembershipPasswordFormat Format
        {
            get { return MembershipPasswordFormat.Hashed; }
        }
    }
}