using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureAdExplorerWindows.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        string loginText;
        string resultText;

        public IRelayCommand LoginCommand { get; }

        public MainViewModel()
        {
            LoginText = "Login";

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

        private void OnLoginClicked()
        {
            ResultText = "Clicked";
        }
    }
}
