namespace Auboreal.Core.DataPipeline
{
    public interface IData
    {
        void Clear();
    }

    public interface IReadOnlyData
    {
        //Currently, there is nothing that forces this to be readonly
        //In other words, this interface only serves to tell other devs
        //that any derived type should be a readOnly interface of the 
        //derived type of IData.
    }
}