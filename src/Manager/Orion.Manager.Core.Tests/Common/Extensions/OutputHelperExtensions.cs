using Newtonsoft.Json;
using Xunit.Abstractions;

namespace Orion.Manager.Core.Tests.Common.Extensions
{
    public static class OutputHelperExtensions
    {
        public static void PrintResult<TValue>(this ITestOutputHelper output, TValue value)
        {
            output.WriteLine(JsonConvert.SerializeObject(value, Formatting.Indented));
        }
    }
}