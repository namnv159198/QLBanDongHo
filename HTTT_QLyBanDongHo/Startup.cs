using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HTTT_QLyBanDongHo.Startup))]
namespace HTTT_QLyBanDongHo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
