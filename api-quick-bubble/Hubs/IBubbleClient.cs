using Domain;

namespace api_quick_bubble.Hubs
{
    public interface IBubbleClient
    {
        Task NewBubble(Bubble bubble);
        Task ConnectionUpdate(int count);
    }
}
