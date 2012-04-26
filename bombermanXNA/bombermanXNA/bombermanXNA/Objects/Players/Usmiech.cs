using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using bomberman.Main;
using bomberman.Moves;

namespace bomberman.Objects.Players
{
    class Usmiech : Player
    {
        public Usmiech(game Tg) : base(Tg, 1000,1000, 6)
        {
            // TODO
            PMove = new AlwaysAttackMove(this);
        }

        override protected bool RestoreDefaults()
        {
            throw new NotImplementedException();
        }
        
    }
}
