using System;
using System.Collections.Generic;
using System.Text;

namespace TaMTests
{
    abstract class AbstractSquare
    {
        AbstractSquare(bool top, bool left, bool bottom, bool right, bool hasMinotuar,
                bool hasTheseus, bool isExit)
                => throw new NotImplementedException("must reimplemnt this as constructor in square");

        bool HasMinotaur { get; }

        bool HasTheseus { get; }

        bool IsExit { get; }
    }
}
