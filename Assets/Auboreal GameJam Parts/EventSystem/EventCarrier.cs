using System.Collections.Generic;
using System.Linq;

namespace Auboreal.Core.EventSystem
{
    public class ForwardsPreOrderEventCarrier : PreOrderEventCarrier
    {
        public override IEnumerable<IEventDirectory> GetNextDirectory(IEventDirectory dir)
        {
            return dir.GetNext();
        }
    }

    public class BackwardsPreOrderEventCarrier : PreOrderEventCarrier
    {
        public override IEnumerable<IEventDirectory> GetNextDirectory(IEventDirectory dir)
        {
            return dir.GetPrevious();
        }
    }

    public abstract class PreOrderEventCarrier : IEventCarrier
    {
        public void DeliverEvent(IEventDirectory start, IEvent e)
        {
            foreach (IEventDirectory dir in Traverse(start).Skip(1))
            {
                SubmitEvent(dir, e);
            }
        }

        public void SubmitEvent(IEventDirectory dir, IEvent e)
        {
            foreach (IEventHandler handler in dir.GetHandlers())
            {
                ((dynamic)handler).HandleEvent(e as dynamic);
            }
        }

        public IEnumerable<IEventDirectory> Traverse(IEventDirectory start)
        {
            Queue<IEventDirectory> queue = new Queue<IEventDirectory>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                IEventDirectory current = queue.Dequeue();
                foreach (IEventDirectory dir in GetNextDirectory(current))
                {
                    queue.Enqueue(dir);
                }
                yield return current;
            }
        }

        public abstract IEnumerable<IEventDirectory> GetNextDirectory(IEventDirectory dir);
    }

    public interface IEventCarrier
    {
        void DeliverEvent(IEventDirectory start, IEvent e);
    }
}