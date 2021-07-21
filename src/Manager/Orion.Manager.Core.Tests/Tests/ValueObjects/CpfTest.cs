using Bogus;
using Bogus.Extensions.Brazil;
using Orion.Manager.Core.Common.ValueObjects;
using Orion.Manager.Core.Tests.Common.Constants;
using Xunit;

namespace Orion.Manager.Core.Tests.Tests.ValueObjects
{
    public class CpfTest
    {
        private readonly Faker _faker = new(LocaleConstants.Locale);
        
        [Fact]
        public void Should_Not_Be_Null()
        {
            var result = Cpf.Create(null);
            
            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void Should_Not_Be_Empty()
        {
            var cpf = string.Empty;
            
            var result = Cpf.Create(cpf);
            
            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void Should_Not_Have_More_Than_11_Digits()
        {
            var cpf = _faker.Person.Cpf(false) + 0;
            
            var result = Cpf.Create(cpf);

            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }
        
        [Fact]
        public void Should_Not_Have_Less_Than_11_Digits()
        {
            var cpf = _faker.Person.Cpf(false).Remove(11 - 1);
            
            var result = Cpf.Create(cpf);

            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }
        
        [Fact]
        public void Should_Not_Repeat_All_Digits()
        {
            var result = Cpf.Create("000.000.000-00");
            
            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }
        
        [Fact]
        public void Should_Be_Valid()
        {
            var result = Cpf.Create("843.658.120-24");
            
            Assert.True(result.Failure);
            Assert.NotEmpty(result.Errors);
        }
        
        [Fact]
        public void Should_Accept_Values_With_Mask()
        {
            var result = Cpf.Create("843.658.120-25");
            
            Assert.True(result.Success);
            Assert.NotNull(result.Value);
        }
        
        [Fact]
        public void Valid()
        {
            var result = Cpf.Create("84365812025");

            Assert.True(result.Success);
            Assert.NotNull(result.Value);
        }
    }
}