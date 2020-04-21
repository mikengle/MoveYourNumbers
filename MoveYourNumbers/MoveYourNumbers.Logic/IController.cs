using System;
using System.Threading.Tasks;

namespace MoveYourNumbers.Logic
{
    public interface IController:IDisposable
    {
        int?[] GameNumbers { get; }
        bool[] NumberFieldsEnabled { get; }
        bool[] IsEmptyNumberField { get; }
        int Moves { get; }
        bool IsFinished { get; }
        Task Reset();
        Task Move(int oldPosition, int newPosition);
    }
}
