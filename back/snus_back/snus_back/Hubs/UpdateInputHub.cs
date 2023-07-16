using Microsoft.AspNetCore.SignalR;
using snus_back.Models;

namespace snus_back.Hubs
{
    public class UpdateInputHub : Hub
    {
        public UpdateInputHub() { }

        public async Task SendUpdateInput(TagRecord tagRecord)
        {
            await Clients.All.SendAsync("input", tagRecord);
        }
    }
}
