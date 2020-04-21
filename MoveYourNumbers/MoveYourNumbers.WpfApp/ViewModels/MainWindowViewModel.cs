using MoveYourNumbers.Logic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MoveYourNumbers.WpfApp.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private const int EDGE_LENGTH = 4;
        public event PropertyChangedEventHandler PropertyChanged;

        private int?[] gameNumbers;
        private bool[] numberFieldsEnabled;

        public int?[] GameNumbers
        {
            get { return gameNumbers; }
            private set { gameNumbers = value; OnPropertyChanged(nameof(GameNumbers)); }
        }

        public bool[] NumberFieldsEnabled
        {
            get { return numberFieldsEnabled; }
            private set { numberFieldsEnabled = value; OnPropertyChanged(nameof(NumberFieldsEnabled)); }
        }

        public MainWindowViewModel()
        {
            using var controller = Factory.Create();
            var task = Task.Run(async () => 
            { 
                GameNumbers = await controller.ResetGameNumbers(EDGE_LENGTH);
                NumberFieldsEnabled = await controller.GetEnabledNumberFields(GameNumbers, EDGE_LENGTH);
            });
            task.Wait();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
