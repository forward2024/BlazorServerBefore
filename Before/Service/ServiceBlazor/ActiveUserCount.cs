using Microsoft.AspNetCore.Components.Server.Circuits;

namespace Before.Service.ServiceBlazor
{
    public class ActiveUserCount
    {
        private int _activeUsers;
        public int ActiveUsers => _activeUsers;

        public event Func<Task> ActiveUsersChanged;

        public async Task Increment()
        {
            Interlocked.Increment(ref _activeUsers);
            if (ActiveUsersChanged != null)
            {
                await ActiveUsersChanged.Invoke();
            }
        }

        public async Task Decrement()
        {
            Interlocked.Decrement(ref _activeUsers);
            if (ActiveUsersChanged != null)
            {
                await ActiveUsersChanged.Invoke();
            }
        }
    }
}
