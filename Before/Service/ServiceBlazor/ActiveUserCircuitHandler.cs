using Microsoft.AspNetCore.Components.Server.Circuits;

namespace Before.Service.ServiceBlazor
{
    public class ActiveUserCircuitHandler : CircuitHandler
    {
        private readonly ActiveUserCount _activeUserCount;

        public ActiveUserCircuitHandler(ActiveUserCount activeUserCount)
        {
            _activeUserCount = activeUserCount;
        }

        public override Task OnConnectionUpAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _activeUserCount.Increment();
            return base.OnConnectionUpAsync(circuit, cancellationToken);
        }

        public override Task OnConnectionDownAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _activeUserCount.Decrement();
            return base.OnConnectionDownAsync(circuit, cancellationToken);
        }
    }
}
