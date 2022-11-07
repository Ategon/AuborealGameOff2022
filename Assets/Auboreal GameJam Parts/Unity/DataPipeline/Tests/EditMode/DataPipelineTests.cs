using NUnit.Framework;
using Auboreal.Core.DataPipeline;

namespace Auboreal.Unity.DataPipeline.UnitTests
{
    public class DataPipelineTests
    {
        [Test]
        public void WriteTest()
        {
            int value0 = 1;
            string msg0 = "Test";

            int value1 = 2;
            string msg1 = "Test 1";

            TestData data = new TestData
            {
                Value = 0,
                Msg = ""
            };

            DataBuilder<TestData> pipeline = new DataBuilder<TestData>();
            TestDataGenerator gen0 = new TestDataGenerator
            {
                value = value0,
                msg = msg0,
                writeValue = true,
                writeMsg = true
            };
            TestDataGenerator gen1 = new TestDataGenerator
            {
                value = value1,
                msg = msg1,
                writeValue = true,
                writeMsg = true
            };

            pipeline.Generators.Add(gen0);
            pipeline.Generators.Add(gen1);
            pipeline.Build(data);

            Assert.AreEqual(data.Value, value1);
            Assert.AreEqual(data.Msg, msg1);
        }

        [Test]
        public void ContravarianceTest()
        {
            int value0 = 1;
            string msg0 = "Test";

            int value1 = 2;
            string msg1 = "Test 1";

            TestData data = new TestData
            {
                Value = 0,
                Msg = ""
            };

            DataBuilder<TestData> pipeline = new DataBuilder<TestData>();
            TestDataGenerator gen0 = new TestDataGenerator
            {
                value = value0,
                msg = msg0,
                writeValue = true,
                writeMsg = true
            };
            SuperTestDataGenerator gen1 = new SuperTestDataGenerator
            {
                superValue = value1,
                writeSuperValue = true,
            };

            pipeline.Generators.Add(gen0);
            pipeline.Generators.Add(gen1);
            pipeline.Build(data);

            Assert.AreEqual(data.SuperValue, value1);
            Assert.AreEqual(data.Value, value0);
            Assert.AreEqual(data.Msg, msg0);
        }
    }
}
