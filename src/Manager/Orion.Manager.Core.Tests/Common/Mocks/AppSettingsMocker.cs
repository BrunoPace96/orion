using Bogus;
using Orion.Manager.SharedKernel.Settings;

namespace Orion.Manager.Core.Tests.Common.Mocks
{
    public class AppSettingsMocker
    {
        public static AppSettings Mock() =>
            new Faker<AppSettings>()
                .Rules((_, _) => {});
    }
}