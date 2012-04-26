using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using bomberman.Objects.Players;

using bomberman.Board;

namespace bomberman.Moves
{
    class AlwaysAttackMove : Move
    {
        public AlwaysAttackMove(Player p)
            : base(p)
        {

        }

        protected internal override bool perform()
        {
            Bpoint from = new Bpoint{ x = p.LocX, y = p.LocY};
            Bpoint to = new Bpoint { x = p.g.b.player.LocX, y = p.g.b.player.LocY};

            p.trasa = p.g.b.GetShortestPath(from, to);
            return true;
        }

    }
}
