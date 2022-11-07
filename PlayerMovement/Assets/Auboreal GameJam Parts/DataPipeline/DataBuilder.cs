using System.Collections.Generic;

namespace Auboreal.Core.DataPipeline
{
    public class DataBuilder<T> where T : IData
    {
        public List<IDataGenerator<T>> Generators { get; private set; }

        public DataBuilder()
        {
            Generators = new List<IDataGenerator<T>>();
        }

        public void Build(T data)
        {
            int writeCount = 0;

            List<IDataGenerator<T>> writing = new List<IDataGenerator<T>>();
            List<IDataGenerator<T>> writingNext = new List<IDataGenerator<T>>();

            writing.AddRange(Generators);
            writing.ForEach(x => x.Start());

            while (writing.Count > 0)
            {
                writing.ForEach(x => x.StartRound());

                foreach (IDataGenerator<T> generator in writing)
                {
                    generator.Write(data);
                    writeCount++;

                    if (generator.IsNotDoneWriting())
                    {
                        //add better code to handle infinite Write loops (Icecubegame)
                        if (writeCount < 9999)
                        {
                            writingNext.Add(generator);
                        }
                    }
                }

                writing.Clear();
                List<IDataGenerator<T>> temp = writing;
                writing = writingNext;
                writingNext = temp;
            }
        }
    }
}