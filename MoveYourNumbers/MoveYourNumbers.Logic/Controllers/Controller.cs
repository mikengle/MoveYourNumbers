using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoveYourNumbers.Logic.Controllers
{
    internal class Controller : IController
    {

        public async Task<int?[]> ResetGameNumbers(int edgeLength)
        {
            Random rnd = new Random();
            int fields = edgeLength * edgeLength;
            if (fields != 9 && fields != 16 && fields != 25)
                throw new ArgumentException($"{nameof(edgeLength)} - Not accepted edge length");
            int[] initNumbers = new int[fields];
            int?[] gameNumbers = new int?[fields];

            for (int i = 0; i < initNumbers.Length-1; )
            {
                int candidate = rnd.Next(1, fields);
                if ((IsInArray(initNumbers, candidate)) == false)
                {
                    initNumbers[i] = candidate;
                    i++;
                }
            }

            return await AssignGameNumbers(initNumbers);
        }

        public async Task<bool[]> GetEnabledNumberFields(int?[] gameNumbers, int edgeLength)
        {
            if (gameNumbers == null)
                throw new ArgumentNullException(nameof(gameNumbers));

            if (gameNumbers.Length != edgeLength * edgeLength)
                throw new ArgumentException($"{nameof(edgeLength)} - didn´t match to {nameof(gameNumbers)}");

            bool[] enabledFields = new bool[gameNumbers.Length];
            int emptyFieldPosition = await FindEmptyFieldIndex(gameNumbers);

            enabledFields[emptyFieldPosition] = true;
            if (((emptyFieldPosition + 1) % edgeLength) != 0) //right field is possible
            {
                enabledFields[emptyFieldPosition + 1] = true;
            }
            if (emptyFieldPosition != 0 && emptyFieldPosition % edgeLength != 0) // left field is possible
            {
                enabledFields[emptyFieldPosition - 1] = true;
            }
            if ((emptyFieldPosition + edgeLength) < gameNumbers.Length) // down field is possible
            {
                enabledFields[emptyFieldPosition + edgeLength] = true;
            }
            if ((emptyFieldPosition - edgeLength) >= 0) // up field is possible
            {
                enabledFields[emptyFieldPosition - edgeLength] = true;
            }

            return enabledFields;
        }

        private Task<int> FindEmptyFieldIndex(int?[] gameNumbers)
        {
            return Task.Run(() =>
            {
                for (int i = 0; i < gameNumbers.Length; i++)
                {
                    if (gameNumbers[i] == null)
                        return i;
                }

                throw new ApplicationException($"{nameof(gameNumbers)} - Can´t find empty field!");
            });
        }

        public Task<int?[]> Move(int?[] actualGameNumbers, bool[] enabledNumberFields, int newPosition)
        {
            throw new NotImplementedException();
        }

        Task<bool> IController.IsFinished(int?[] gameNumbers)
        {
            return Task.Run(async () =>
            {
                if (gameNumbers == null)
                    throw new ArgumentNullException(nameof(gameNumbers));

                List<int> numbers = new List<int>();
                int emptyFieldPosition = await FindEmptyFieldIndex(gameNumbers);

                for (int i = 0; i < gameNumbers.Length; i++)
                {
                    if (i != emptyFieldPosition)
                    {
                        numbers.Add((int)gameNumbers[i]);
                    }
                }

                for (int i = 1; i < numbers.Count; i++)
                {
                    if (numbers[i - 1] + 1 != numbers[i])
                    {
                        return false;
                    }
                }

                return true;
            });
        }

        #region PrivateMethods

        private Task<int?[]> AssignGameNumbers(int[] numbers)
        {
            return Task.Run(() =>
            {
                int?[] gameNumbers = new int?[numbers.Length];
                for (int i = 0; i < numbers.Length; i++)
                {
                    if (numbers[i] == 0)
                    {
                        gameNumbers[i] = null;
                    }
                    else
                    {
                        gameNumbers[i] = numbers[i];
                    }
                }
                return gameNumbers;
            });
        }


        private bool IsInArray(int[] numbers, int candidate)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == candidate)
                {
                    return true;
                }
            }
            return false;
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
