using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MoveYourNumbers.WpfApp.Models
{
    class MainWindowModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int?[] gameNumbers;

        public int?[] GameNumbers
        {
            get { return gameNumbers; }
            set { gameNumbers = value; OnPropertyChanged(nameof(GameNumbers)); }
        }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
