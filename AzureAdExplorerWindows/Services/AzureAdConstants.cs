using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureAdExplorerWindows.Services
{
    public static class AzureAdConstants
    {
        // Azure AD Coordinates
        public static string Tenant = "Enter_the_Tenant_Id_Here";
        public static string ClientID = "Enter_the_Application_Id_Here";
    
        public static string[] Scopes = { "User.Read" };

        public static string Authority = $"https://login.microsoftonline.com/{Tenant}/";
        public static string EmbeddedRedirectUri = "https://login.microsoftonline.com/common/oauth2/nativeclient";
        public static string SystemRedirectUri = "http://localhost";
    }
}
