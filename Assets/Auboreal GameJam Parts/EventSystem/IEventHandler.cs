namespace Auboreal.Core.EventSystem
{
    public interface IEventHandler
    {
        void HandleEvent(IEvent e);
    }
}