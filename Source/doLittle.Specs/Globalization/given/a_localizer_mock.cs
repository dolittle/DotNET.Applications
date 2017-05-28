using doLittle.Globalization;
using Machine.Specifications;
using Moq;

namespace doLittle.Specs.Globalization.given
{
    public class a_localizer_mock
    {
        protected static Mock<ILocalizer> localizer_mock;

        Establish context = () =>
                                {
                                    localizer_mock = new Mock<ILocalizer>();
                                    localizer_mock.Setup(l => l.BeginScope()).Returns(LocalizationScope.FromCurrentThread);
                                };
    }
}
