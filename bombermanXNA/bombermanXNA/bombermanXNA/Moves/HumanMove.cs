using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using bomberman.Objects.Players;

namespace bomberman.Moves
{
    class HumanMove : Move
    {

        public HumanMove(Player p)
            : base(p)
        {

        }

        protected internal override bool perform()
        {
            return true;
            //throw new NotImplementedException();
        }
    }
}
