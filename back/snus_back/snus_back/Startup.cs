using snus_back.Services;

namespace snus_back
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ScanService scanService)
        {
            RunOnApplicationStart();
            scanService.Run();
        }

        private void RunOnApplicationStart()
        {
        }   
    }

}
