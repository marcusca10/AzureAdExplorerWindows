using AzureAdExplorerWindows.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureAdExplorerWindows.Services
{
    public interface IAuthenticationService
    {
        UserContext UserContext { get; set; }
        bool UseBroker { get; set; }
        Task SignInAsync(bool useWebView = true, bool useIwa = false);
        Task SignOutAsync();
    }
}
