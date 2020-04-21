using System;
using System.Collections.Generic;
using System.Text;

namespace MoveYourNumbers.Logic
{
    public static class Factory
    {
        public static IController Create()
        {
            return new Controllers.Controller();
        }
    }
}
