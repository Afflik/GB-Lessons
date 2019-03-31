using System;
using System.Collections.Generic;
using System.Drawing;

namespace Lession1
{
    class CustomException : Exception
    {
        public CustomException(string message) : base(message) { }
    }
}
