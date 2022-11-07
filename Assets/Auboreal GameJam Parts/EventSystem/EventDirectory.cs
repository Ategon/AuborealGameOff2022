using System.Collections.Generic;

namespace Auboreal.Core.EventSystem
{
    public class EventDirectory : IEventDirectory
    {
        public List<IEventDirectory> Next { get; private set; }
        public List<IEventDirectory> Previous { get; private set; }
        public List<IEventHandler> Handlers { get; private set; }

        public EventDirectory()
        {
            Next = new List<IEventDirectory>();
            Previous = new List<IEventDirectory>();
            Handlers = new List<IEventHandler>();
        }

        public IEnumerable<IEventDirectory> GetNext()
        {
            return Next;
        }

        public IEnumerable<IEventDirectory> GetPrevious()
        {
            return Previous;
        }

        public IEnumerable<IEventHandler> GetHandlers()
        {
            return Handlers;
        }

        public void ConnectBidirectional(EventDirectory to)
        {
            EventDirectory from = this;
            from.ConnectForwards(to);
            to.ConnectBackwards(from);
        }

        public void DisconnectBidirectional(EventDirectory to)
        {
            EventDirectory from = this;
            from.DisconnectForwards(to);
            to.DisconnectBackwards(from);
        }

        public void ConnectForwards(EventDirectory to)
        {
            this.Next.Add(to);
        }

        public void DisconnectForwards(EventDirectory to)
        {
            this.Next.Remove(to);
        }

        public void ConnectBackwards(EventDirectory from)
        {
            this.Previous.Add(from);
        }

        public void DisconnectBackwards(EventDirectory from)
        {
            this.Previous.Remove(from);
        }
    }

    public interface IEventDirectory
    {
        IEnumerable<IEventDirectory> GetNext();
        IEnumerable<IEventDirectory> GetPrevious();
        IEnumerable<IEventHandler> GetHandlers();
    }
}