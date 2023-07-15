using snus_back.Services;
using snus_back.WebSockets;

namespace snus_back
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ScanService scanService)
        {
            app.UseRouting();
            app.UseWebSockets();

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/ws/updateInput")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        var handler = app.ApplicationServices.GetService<UpdateInputHandler>();
                        await handler.HandleWebSocket(webSocket, "input");
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                else if (context.Request.Path == "/ws/updateAlarm")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        var handler = app.ApplicationServices.GetService<UpdateAlarmHandler>();
                        await handler.HandleWebSocket(webSocket, "alarm");
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                else
                {
                    await next();
                }
            });

        
            RunOnApplicationStart();
            scanService.Run();
        }

        private void RunOnApplicationStart()
        {
        }
    }

}
