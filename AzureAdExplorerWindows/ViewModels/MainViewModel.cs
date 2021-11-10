using AzureAdExplorerWindows.Services;
using Microsoft.Identity.Client;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureAdExplorerWindows.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly IAuthenticationService _authenticationService;

        string loginText;
        string resultText;
        int selectedAuthMode;
        List<string> authModeList;

        public IRelayCommand LoginCommand { get; }

        public MainViewModel(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;

            authModeList = new List<string>() { "System browser", "Integrated", "WAM" };
            SelectedAuthMode = "System browser";

            UpdateSignInState();

            LoginCommand = new RelayCommand(OnLoginClicked);
        }

        public string LoginText
        {
            get => loginText;
            set => SetProperty(ref loginText, value);
        }

        public string ResultText
        {
            get => resultText;
            set => SetProperty(ref resultText, value);
        }

        public string SelectedAuthMode
        {
            get => authModeList[selectedAuthMode];
            set => SetProperty(ref selectedAuthMode, authModeList.IndexOf(value));
        }

        public List<string> AuthModeList
        {
            get => authModeList;
        }

        private async void OnLoginClicked()
        {
            try
            {
                if (!this._authenticationService.UserContext.IsLoggedOn)
                {
                    if (selectedAuthMode == 2)
                        this._authenticationService.UseBroker = true;
                    else
                        this._authenticationService.UseBroker = false;

                    if (selectedAuthMode == 1)
                        await this._authenticationService.SignInAsync(useIwa: true);
                    else
                        await this._authenticationService.SignInAsync();

                    UpdateSignInState();
                }
                else
                {
                    await this._authenticationService.SignOutAsync();
                    UpdateSignInState();
                }
            }
            catch (Exception ex)
            {
                // Alert if any exception excluding user canceling sign-in dialog
                if (((ex as MsalException)?.ErrorCode != "authentication_canceled"))
                {
                    this.ResultText = $"Exception:\r\n{ex.Message}";
                    Debug.WriteLine($"Exception: {ex.Message}");
                }
            }
        }

        void UpdateSignInState()
        {
            LoginText = this._authenticationService.UserContext.IsLoggedOn ? "Logout" : "Login";

            var user = this._authenticationService.UserContext;
            this.ResultText = $"Name: \t{user.Name}\r\nId: \t{user.UserIdentifier}\r\nUPN: \t{user.UserPrincipalName}\r\nEmail:{user.EmailAddress}\r\n\r\nAccess token:\r\n{user.AccessToken}";

        }
    }
}
