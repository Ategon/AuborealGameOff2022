namespace Auboreal.Core.DataPipeline
{
    public interface IDataGenerator<in T> where T : IData
    {
        void Start() { }
        void StartRound() { }
        void Write(T data);
        bool IsNotDoneWriting() => false;
    }
}
