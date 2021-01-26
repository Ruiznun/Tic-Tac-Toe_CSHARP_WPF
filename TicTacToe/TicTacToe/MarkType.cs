using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    /// <summary>
    /// The type of value a cell in the game is currently set to
    /// </summary>
    public enum MarkType
    {
        /// <summary>
        /// The cell hasn't been clicked yet
        /// </summary>
        Free,
        /// <summary>
        /// A cell has a O in it
        /// </summary>
        O,
        /// <summary>
        /// A cell ha a X in it
        /// </summary>
        X

    }
}
