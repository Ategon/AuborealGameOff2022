using Auboreal.Core.DataPipeline;

namespace Auboreal.Unity.DataPipeline.UnitTests
{
    public class SuperTestDataGenerator : IDataGenerator<SuperTestData>
    {
        public bool writeSuperValue = false;

        public int superValue;

        public void Write(SuperTestData data)
        {
            if (writeSuperValue) data.SuperValue = superValue;
        }
    }

    public class TestDataGenerator : IDataGenerator<TestData>
    {
        public bool writeValue;
        public bool writeMsg;

        public int value;
        public string msg;

        public void Write(TestData data)
        {
            if (writeValue) data.Value = value;
            if (writeMsg) data.Msg = msg;
        }
    }
}