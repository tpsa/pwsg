using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using bomberman.Moves;
using bomberman.Main;

namespace bomberman.Objects.Players
{
    class Balon : Player
    {

        public Balon(game Tg) : base(Tg, 1000, 1000, 3)
            
        {
            // TODO lol
            PMove =  new RandomMove(this);
            //PMove = new AlwaysAttackMove(this);
        }

        public void perform()
        {
            this.PMove.perform();
        }


        override protected bool RestoreDefaults()
        {
            throw new NotImplementedException();
        }
    }

}
