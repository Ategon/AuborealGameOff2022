using System.Collections.Generic;

namespace Auboreal.Core.DataPipeline
{
    public class DataConsumer<T> where T : IReadOnlyData
    {
        public List<IDataHandler<T>> Handlers { get; private set; }

        public DataConsumer()
        {
            Handlers = new List<IDataHandler<T>>();
        }

        public void Consume(T data)
        {
            foreach (IDataHandler<T> handler in Handlers)
            {
                handler.Handle(data);
            }
        }
    }
}