using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace snus_back.WebSockets
{
    public class WebSocketConnectionManager
    {
        private ConcurrentDictionary<string, WebSocket> _webSockets = new ConcurrentDictionary<string, WebSocket>();

        public WebSocket GetSocketById(string id)
        {
            return _webSockets.TryGetValue(id, out var socket) ? socket : null;
        }

        public ConcurrentDictionary<string, WebSocket> GetAll()
        {
            return _webSockets;
        }

        public string GetId(WebSocket socket)
        {
            foreach (var item in _webSockets)
            {
                if (item.Value == socket)
                {
                    return item.Key;
                }
            }
            return null;
        }

        public void AddSocket(WebSocket socket, string name)
        {
            var id = Guid.NewGuid().ToString();
            _webSockets.TryAdd(name, socket);
        }

        public bool RemoveSocket(string id)
        {
            return _webSockets.TryRemove(id, out _);
        }
    }
}
