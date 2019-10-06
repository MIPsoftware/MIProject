using Microsoft.Owin;
using Owin;


[assembly: OwinStartup(typeof(MIPChat.Startup))]
namespace MIPChat
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}