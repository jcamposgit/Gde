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

using ClubStarterKit.Web.Infrastructure.Membership;
using Xunit;

namespace ClubStarterKit.Tests.Web.Infrastructure.MembershipProvider
{
    public class PasswordCrypotographyTests
    {
        [Fact]
        public void PasswordCryptography_GenerateSalt_ReturnsStringWithLengthGreaterThanFive()
        {
            Assert.InRange(new PasswordCryptographyProvider().GenerateSalt().Length, 5, int.MaxValue);
        }

        [Fact]
        public void PasswordCryptography_GeneratePassword_DoesntReturnThePlainTextPassword()
        {
            var prov = new PasswordCryptographyProvider();
            string salt = prov.GenerateSalt();
            string plainTextPassword = "plain_text_password";

            string salted = prov.GeneratePassword(plainTextPassword, salt);

            Assert.NotEqual(plainTextPassword, salted);
        }

        [Fact]
        public void PasswordCryptography_VerifyPassword_TrueWhenSaltedPasswordsEqual()
        {
            var prov = new PasswordCryptographyProvider();
            string salt = prov.GenerateSalt();
            string plainTextPassword = "plain_text_password";

            string salted = prov.GeneratePassword(plainTextPassword, salt);

            Assert.True(prov.ValidatePassword(salted, salt, plainTextPassword));
        }
    }
}