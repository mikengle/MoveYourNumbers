using System;
using System.Threading.Tasks;

namespace MoveYourNumbers.Logic
{
    public interface IController:IDisposable
    {
        /// <summary>
        /// Init game field with numbers
        /// Only 3x3=9 or 4x4=16 or 5x5=25 fields are possible
        /// </summary>
        /// <param name="edgeLength"></param>
        /// <returns>int?[] gameNumbers</returns>
        Task<int?[]> ResetGameNumbers(int edgeLength);

        /// <summary>
        /// return bit map of enabled fields for moving empty field
        /// to its neighbour fields
        /// </summary>
        /// <param name="gameNumbers"></param>
        /// <param name="edgeLength"></param>
        /// <returns>bool[] enabledNumberFields</returns>
        Task<bool[]> GetEnabledNumberFields(int?[] gameNumbers,int edgeLength);

        /// <summary>
        /// Move the empty numberField to a new Position
        /// </summary>
        /// <param name="actualGameNumbers"></param>
        /// <param name="enabledNumberFields"></param>
        /// <param name="newPosition"></param>
        /// <returns>int?[] newGameNumbers</returns>
        Task<int?[]> Move(int?[] actualGameNumbers, bool[] enabledNumberFields, int newPosition);

        /// <summary>
        /// check gameNumbers if it is in order (1 to 15)
        /// </summary>
        /// <param name="gameNumbers"></param>
        /// <returns></returns>
        Task<bool> IsFinished(int?[] gameNumbers);

        /// <summary>
        /// Returns index of field with null
        /// </summary>
        /// <param name="gameNumbers"></param>
        /// <returns>int emptyFieldIndex</returns>
        Task<int> GetEmptyFieldPosition(int?[] gameNumbers);
    }
}
