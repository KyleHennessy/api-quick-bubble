namespace Application.Services.Interfaces
{
    public interface IConnectionCounter
    {
        int GetConnectionCount();
        int IncrementConnectionCount();
        int DecrementConnectionCount();
    }
}
