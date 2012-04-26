using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using bomberman.Moves;
using bomberman.Main;

namespace bomberman.Objects.Players
{
    class Duch : Player
    {

        public Duch(game Tg)
            : base(Tg, 1000, 1000, 2)
        {
            // TODO lol ;p
            PMove = new GhostRandomMove(this);
        }

        protected override bool RestoreDefaults()
        {
            throw new NotImplementedException();
        }

    }
}
