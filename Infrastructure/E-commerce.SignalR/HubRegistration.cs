using E_commerce.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.SignalR
{
    public static class HubRegistration
    {
        public static void MapHubs(this WebApplication application)
        {
            application.MapHub<ProductHub>("/product/hub");

            //butun hublari program.cs-de app.maphub yazmamaq ucun istifade olunan extensiondur.
        }
    }
}
