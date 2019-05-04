using System;
using Prism.Commands;
using Prism.Navigation;
using TempConverter;


namespace WCFDemo.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string input;
        public string Input
        {
            get { return input; }
            set { SetProperty(ref input, value); }
        }

        private string result;
        public string Result
        {
            get { return result; }
            set { SetProperty(ref result, value); }
        }

        public DelegateCommand Calculate { get; set; }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "WCF Demo";

            Calculate = new DelegateCommand(CalculateExecute);
        }

        private async void CalculateExecute()
        {
            try {
                var client = new TempConvertSoapClient(TempConvertSoapClient.EndpointConfiguration.TempConvertSoap12);
                string celsius = await client.FahrenheitToCelsiusAsync(Input);

                Result = $"The temp in celsius is: {celsius}";
            } catch (Exception ex) {
                Result = $"Error: {ex.Message}";
            }
        }
    }
}
