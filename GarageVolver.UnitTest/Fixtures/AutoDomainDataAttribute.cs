using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace GarageVolver.UnitTest.Fixtures
{
    /// <summary>
    /// Helper class designed to fill test object with bogus entries.
    /// </summary>
    public class AutoDomainDataAttribute : AutoDataAttribute
    {
        public AutoDomainDataAttribute()
          : base(() => new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}
