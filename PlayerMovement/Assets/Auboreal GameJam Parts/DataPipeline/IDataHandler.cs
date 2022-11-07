namespace Auboreal.Core.DataPipeline
{
    public interface IDataHandler<in T> where T : IReadOnlyData
    {
        void Handle(T data);
    }
}