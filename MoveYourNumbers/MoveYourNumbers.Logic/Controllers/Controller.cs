using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoveYourNumbers.Logic.Controllers
{
    internal class Controller : IController
    {
        private const int MAX_FIELDS = 16;
        public int?[] GameNumbers { get; private set; }
        public int Moves { get; private set; }

        public bool IsFinished { get; private set; }

        public bool[] NumberFieldsEnabled { get; private set; }

        public bool[] IsEmptyNumberField { get; private set; }

        public Controller()
        {
            GameNumbers = new int?[MAX_FIELDS];
            NumberFieldsEnabled = new bool[MAX_FIELDS];
            IsEmptyNumberField = new bool[MAX_FIELDS];
        }
        public async Task Move(int oldPosition, int newPosition)
        {
            GameNumbers = await SwitchFieldPosition(oldPosition, newPosition);
            IsFinished = await EvaluateGameFinished(GameNumbers);
        }

        public async Task Reset()
        {
            IsFinished = false;
            GameNumbers = await InitGameField();
            IsEmptyNumberField = await GetEmptyFieldBitMap(GameNumbers);
            NumberFieldsEnabled = await GetNeighbourFields(IsEmptyNumberField);
        }

        #region PrivateMethods
        private Task<int?[]> SwitchFieldPosition(int oldPosition, int newPosition)
        {
            throw new NotImplementedException();
        }

        private Task<bool> EvaluateGameFinished(int?[] gameNumbers)
        {
            throw new NotImplementedException();
        }

        private Task<bool[]> GetNeighbourFields(bool[] isEmptyNumberField)
        {
            throw new NotImplementedException();
        }

        private Task<bool[]> GetEmptyFieldBitMap(int?[] gameNumbers)
        {
            throw new NotImplementedException();
        }

        private Task<int?[]> InitGameField()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Controller()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
