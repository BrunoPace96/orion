using Bogus;
using Orion.Manager.Core.Common.ValueObjects;
using Orion.Manager.Core.Tests.Common.Constants;
using Xunit;

namespace Orion.Manager.Core.Tests.Tests.ValueObjects
{
    public class EmailTest
    {
        private readonly Faker _faker = new(LocaleConstants.Locale);
        
        [Fact]
        public void Should_Not_Be_Null()
        {
            var result = Email.Create(null);
            
            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void Should_Not_Be_Empty()
        {
            var result = Email.Create(string.Empty);
            
            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void Should_Not_Have_Less_Than_10_Characters()
        {
            var result = Email.Create("di@di.com");

            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }
        
        [Fact]
        public void Should_Not_Have_More_Than_150_Characters()
        {
            var email = $"{_faker.Lorem.Letter(141)}@gmail.com";
            var result = Email.Create(email);

            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }
        
        [Fact]
        public void Should_Contains_An_At_Sign()
        {
            var result = Email.Create("didi.gmail.com");
            
            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }
        
        [Fact]
        public void Should_Contains_Characters_The_Left_Of_The_At_Sign()
        {
            var result = Email.Create("@gmail.com.br");
            
            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }
        
        [Fact]
        public void Should_Contains_Characters_The_Right_Of_The_At_Sign()
        {
            var result = Email.Create("didi.developer@");
            
            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }
        
        [Fact]
        public void Valid()
        {
            var result = Email.Create(_faker.Person.Email);
            
            Assert.True(result.Success);
            Assert.NotNull(result.Value);
        }
    }
}