using Bogus;
using Orion.Manager.Core.Common.ValueObjects;
using Orion.Manager.Core.Tests.Common.Constants;
using Xunit;

namespace Orion.Manager.Core.Tests.Tests.ValueObjects
{
    public class PhoneTest
    {
        private readonly string _phoneNumber = new Faker(LocaleConstants.Locale)
            .Phone.PhoneNumber("419########");
        
        [Fact]
        public void Should_Not_Be_Null()
        {
            var result = Phone.Create(null);
            
            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void Should_Not_Be_Empty()
        {
            var result = Phone.Create(string.Empty);
            
            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void Should_Not_Have_More_Than_11_Characters()
        {
            var result = Phone.Create($"{_phoneNumber}1");

            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }
        
        [Fact]
        public void Should_Not_Have_Less_Than_11_Characters()
        {
            var result = Phone.Create($"{_phoneNumber}".Remove(_phoneNumber.Length - 1));

            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }
        
        [Fact]
        public void Should_Have_Only_Numbers()
        {
            var result = Phone.Create($"{_phoneNumber}A");
            
            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }
        
        [Fact]
        public void Should_Not_Have_Special_Characters()
        {
            var result = Phone.Create($"{_phoneNumber}*");
            
            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }
        
        [Fact]
        public void Valid()
        {
            var result = Phone.Create(_phoneNumber);

            Assert.True(result.Success);
            Assert.NotNull(result.Value);
        }
    }
}