using Auboreal.Core.DataPipeline;

namespace Auboreal.Unity.DataPipeline.UnitTests
{
    public class SuperTestData : IData, ReadOnlySuperTestData
    {
        public int SuperValue { get; set;}

        public void Clear()
        {
            SuperValue = 0;
        }
    }

    public class TestData : SuperTestData, ReadOnlyTestData
    {
        public int Value { get; set;}
        public string Msg { get; set;}

        public new void Clear()
        {
            base.Clear();
            Value = 0;
            Msg = "";
        }
    }

    public interface ReadOnlyTestData : ReadOnlySuperTestData
    {
        int Value { get; }
        string Msg { get; }
    }

    public interface ReadOnlySuperTestData : IReadOnlyData
    {
        int SuperValue { get; }
    }
}