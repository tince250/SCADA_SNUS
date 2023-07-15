using System.Net.WebSockets;

namespace snus_back.WebSockets
{
    public class UpdateAlarmHandler : WebSocketHandler
    {
        public UpdateAlarmHandler(WebSocketConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {
        }

        public async Task SendDataToClient(string clientId, object message)
        {
            var webSocket = WebSocketConnectionManager.GetSocketById(clientId);
            if (webSocket != null && webSocket.State == WebSocketState.Open)
            {
                await SendMessageAsync(webSocket, message);
            }
        }

        public override async Task HandleWebSocket(WebSocket webSocket, string name)
        {
            WebSocketConnectionManager.AddSocket(webSocket, name);

            try
            {
                var buffer = new byte[1024 * 4];
                WebSocketReceiveResult result;

                while (webSocket.State == WebSocketState.Open)
                {
                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    // Handle received WebSocket messages, if needed
                }
            }
            finally
            {
                WebSocketConnectionManager.RemoveSocket(WebSocketConnectionManager.GetId(webSocket));
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed", CancellationToken.None);
            }
        }
    }
}
