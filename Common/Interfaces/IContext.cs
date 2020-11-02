using System.Threading;

namespace Common.Interfaces
{
    public interface IContext
    {
        void Send(SendOrPostCallback callback);
    }
}