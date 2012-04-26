using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using bomberman.Objects.Players;

namespace bomberman.Moves
{
    abstract class Move
    {
        protected Player p;

        public Move(Player p)
        {
            this.p = p;
        }

        abstract internal protected bool perform();
    }
}
