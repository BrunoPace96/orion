using Bogus;
using Orion.Manager.Core.Common.ValueObjects;
using Orion.Manager.Core.Tests.Common.Constants;
using Xunit;

namespace Orion.Manager.Core.Tests.Tests.ValueObjects
{
    public class NameTest
    {
        private readonly Faker _faker = new(LocaleConstants.Locale);
        
        [Fact]
        public void Should_Not_Be_Null()
        {
            var result = Name.Create(null);
            
            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void Should_Not_Be_Empty()
        {
            var name = string.Empty;
            
            var result = Name.Create(name);
            
            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void Should_Not_Have_More_Than_150_Characters()
        {
            var name = _faker.Lorem.Sentence(151);
            
            var result = Name.Create(name);

            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }
        
        [Fact]
        public void Should_Not_Have_Less_Than_10_Characters()
        {
            var name = _faker.Lorem.Sentence(9);
            
            var result = Name.Create(name);

            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }
        
        [Fact]
        public void Should_Not_Have_Numbers()
        {
            var name = _faker.Name.FullName() + 1;
            
            var result = Name.Create(name);
            
            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }
        
        [Fact]
        public void Should_Not_Have_Special_Characters()
        {
            var name = _faker.Name.FullName() + "*";
            
            var result = Name.Create(name);
            
            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }
        
        [Fact]
        public void Valid()
        {
            var name = _faker.Name.FullName();
            var result = Name.Create(name);

            Assert.True(result.Success);
            Assert.NotNull(result.Value);
        }
    }
}