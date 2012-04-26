using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using bomberman.Main;

namespace bomberman.Objects
{
    abstract class Object
    {
        public game g;
        public Object(game Tg) 
        {
            g = Tg;
        }

        abstract public void Destroy();


    }
}
