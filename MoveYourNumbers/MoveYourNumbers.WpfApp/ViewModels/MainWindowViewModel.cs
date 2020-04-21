using MoveYourNumbers.Logic;
using MoveYourNumbers.WpfApp.Commands;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MoveYourNumbers.WpfApp.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private const int EDGE_LENGTH = 4;
        public event PropertyChangedEventHandler PropertyChanged;

        private int?[] gameNumbers;
        private bool[] numberFieldsEnabled;
        private ICommand cmdUp;
        private ICommand cmdDown;
        private ICommand cmdLeft;
        private ICommand cmdRight;
        private ICommand cmdReset;
        private string moves;
        private int movesCount;
        private ICommand cmdNumButton;

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

        public String Moves 
        {
            get { return moves; }
            private set { moves = value; OnPropertyChanged(nameof(Moves)); }
        }

        public ICommand CmdUp {
            get
            {
                if (cmdUp == null)
                {
                    cmdUp = new RelayCommand(o => {
                        MoveEmptyNumberFieldWithArrowButton(-EDGE_LENGTH);
                    }, (o => true)
                    );
                }

                return cmdUp;
            }
            set { cmdUp = value; OnPropertyChanged(nameof(CmdUp)); }
        }

        public ICommand CmdDown
        {
            get
            {
                if (cmdDown == null)
                {
                    cmdDown = new RelayCommand(o => {
                        MoveEmptyNumberFieldWithArrowButton(EDGE_LENGTH);
                    }, (o => true)
                    );
                }

                return cmdDown;
            }
            set { cmdDown = value; OnPropertyChanged(nameof(CmdDown)); }
        }

        public ICommand CmdLeft
        {
            get
            {
                if (cmdLeft == null)
                {
                    cmdLeft = new RelayCommand(o => {
                        MoveEmptyNumberFieldWithArrowButton(-1);
                    }, (o => true)
                    );
                }

                return cmdLeft;
            }
            set { cmdLeft = value; OnPropertyChanged(nameof(CmdLeft)); }
        }

        public ICommand CmdRight
        {
            get
            {
                if (cmdRight == null)
                {
                    cmdRight = new RelayCommand(o => {
                        MoveEmptyNumberFieldWithArrowButton(1);
                    }, (o => true)
                    );
                }

                return cmdRight;
            }
            set { cmdRight = value; OnPropertyChanged(nameof(CmdRight)); }
        }

        public ICommand CmdReset
        {
            get
            {
                if (cmdReset == null)
                {
                    cmdReset = new RelayCommand(o => {
                        ResetGame();
                    }, (o => true)
                    );
                }

                return cmdReset;
            }
            set { cmdReset = value; OnPropertyChanged(nameof(CmdReset)); }
        }

        public ICommand CmdNumButton
        {
            get
            {
                if (cmdNumButton == null)
                {
                    cmdNumButton = new RelayCommand(o => {
                        MoveEmptyNumberFieldWithNumButton(Convert.ToInt32(o));
                    }, (o => true)
                    );
                }

                return cmdNumButton;
            }
            set { cmdNumButton = value; OnPropertyChanged(nameof(CmdNumButton)); }
        }

        public MainWindowViewModel()
        {
            ResetGame();
        }

        private void MoveEmptyNumberFieldWithNumButton(int position)
        {
            MoveEmptyNumberField(position, false);
        }

        private void MoveEmptyNumberFieldWithArrowButton(int position)
        {
            MoveEmptyNumberField(position, true);
        }

        private void MoveEmptyNumberField(int position, bool needEmptyFieldPosition)
        {
            try
            {
                var task = Task.Run(async () =>
                {
                    using var ctrl = Factory.Create();
                    int newPositionIndex = -1;

                    if (needEmptyFieldPosition)
                    {
                        newPositionIndex = await ctrl.GetEmptyFieldPosition(GameNumbers) + position;
                    }
                    else
                    {
                        newPositionIndex = position;
                    }
                    GameNumbers = await ctrl.Move(GameNumbers, NumberFieldsEnabled, newPositionIndex);
                    NumberFieldsEnabled = await ctrl.GetEnabledNumberFields(GameNumbers, EDGE_LENGTH);
                    if (await ctrl.IsFinished(GameNumbers))
                    {
                        MessageBox.Show($"Das Spiel wurde mit {movesCount} Bewegungen gewonnen!");
                    }
                });
                task.Wait();
                movesCount++;
                Moves = $"Moves: {movesCount}";
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void ResetGame()
        {
            var task = Task.Run(async () =>
            {
                using var ctrl = Factory.Create();
                GameNumbers = await ctrl.ResetGameNumbers(EDGE_LENGTH);
                NumberFieldsEnabled = await ctrl.GetEnabledNumberFields(GameNumbers, EDGE_LENGTH);
                movesCount = 0;
                Moves = $"Moves: {movesCount}";
            });
            task.Wait();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
