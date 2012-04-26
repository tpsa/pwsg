using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using bomberman.Moves;
using bomberman.Main;

namespace bomberman.Objects.Players
{
    class Kropla : Player
    {

        public Kropla(game Tg) : base(Tg, 1000,1000, 4)
        {
            // TODO
            PMove = new AttackNearbyMove(this);
        }

        override protected bool RestoreDefaults()
        {
            throw new NotImplementedException();
        }

    }
}
