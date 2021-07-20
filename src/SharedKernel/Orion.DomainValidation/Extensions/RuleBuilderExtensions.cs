using System;
using FluentValidation;
using Orion.Core.Domain;
using Orion.OperationResult.Implementations;

namespace Orion.DomainValidation.Extensions
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, string> MustBeValueObject<T, TValueObject>(
            this IRuleBuilder<T, string> ruleBuilder,
            Func<string, ValueObjectResult<TValueObject>> factoryMethod)
            where TValueObject : ValueObject
        {
            return (IRuleBuilderOptions<T, string>)ruleBuilder.Custom((value, context) =>
            {
                var result = factoryMethod(value);

                if (result.Failure)
                    result.Errors.ForEach(context.AddFailure);
            });
        }
    }
}