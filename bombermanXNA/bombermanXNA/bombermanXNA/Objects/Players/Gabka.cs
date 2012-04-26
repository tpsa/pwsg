using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using bomberman.Moves;
using bomberman.Main;

namespace bomberman.Objects.Players
{
    class Gabka : Player
    {

        public Gabka(game Tg)
            : base(Tg, 1000, 1000, 4)
        {
            // TODO pieprzyć C#
            PMove = new AttackNearbyMove(this);
        }

        protected override bool RestoreDefaults()
        {
            throw new NotImplementedException();
        }

    }
}
