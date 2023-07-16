using Microsoft.AspNetCore.SignalR;
using snus_back.Models;

namespace snus_back.Hubs
{
    public class UpdateAlarmHub : Hub
    {
        public UpdateAlarmHub() { }

        public async Task SendUpdateAlarm(Alarm alarm)
        {
            await Clients.All.SendAsync("alarm", alarm);
        }
    }
}
