using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using bomberman.Objects.Players;

namespace bomberman.Moves
{
    class GhostRandomMove : RandomMove
    {

        public GhostRandomMove(Player p)
            : base(p)
        {

        }

        protected internal override bool perform()
        {
            return base.perform();
        }
    }
}
