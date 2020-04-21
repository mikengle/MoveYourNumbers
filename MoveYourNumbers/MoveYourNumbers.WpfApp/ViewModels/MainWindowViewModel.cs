using MoveYourNumbers.Logic;
using System.ComponentModel;

namespace MoveYourNumbers.WpfApp.ViewModels
{
    internal class MainWindowViewModel:INotifyPropertyChanged
    {
        private IController controller;
        public event PropertyChangedEventHandler PropertyChanged;

        private int?[] gameNumbers;
        private bool[] isEmptyNumberField;
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

        public bool[] IsEmptyNumberField
        {
            get { return isEmptyNumberField; }
            private set { isEmptyNumberField = value; OnPropertyChanged(nameof(IsEmptyNumberField)); }
        }

        public MainWindowViewModel()
        {
            controller = Factory.Create();
            GameNumbers = new int?[16];
            //GameNumbers[0] = 1;
            //GameNumbers[15] = 15;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
