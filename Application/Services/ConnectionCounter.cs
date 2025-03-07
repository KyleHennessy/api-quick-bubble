using Application.Services.Interfaces;

namespace Application.Services
{
    public class ConnectionCounter : IConnectionCounter
    {
        private int _counter = 0;
        public int GetConnectionCount()
        {
            return _counter;
        }

        public int IncrementConnectionCount()
        {
            _counter++;
            return _counter;
        }

        public int DecrementConnectionCount()
        {
            if (_counter > 0)
            {
                _counter--;
            }

            return _counter;
        }

    }
}
