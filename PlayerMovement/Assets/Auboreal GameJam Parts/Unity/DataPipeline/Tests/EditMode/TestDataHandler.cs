using Auboreal.Core.DataPipeline;

namespace Auboreal.Unity.DataPipeline.UnitTests
{
    public class SuperTestDataHandler : IDataHandler<ReadOnlySuperTestData>
    {
        public void Handle(ReadOnlySuperTestData data)
        {

        }
    }

    public class TestDataHandler : IDataHandler<ReadOnlyTestData>
    {
        public void Handle(ReadOnlyTestData data)
        {

        }
    }
}