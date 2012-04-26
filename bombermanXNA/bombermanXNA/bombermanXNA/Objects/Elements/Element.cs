using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using bomberman.Main;
using bomberman.Objects.Elements.Materials;
using bomberman.Objects.Elements.Bonuses;

namespace bomberman.Objects
{
    namespace Elements
    {
        abstract class Element : Object
        {
            //public event EventHandler TriggerIn;
            //public event EventHandler TriggerOut;
            //public event EventHandler TriggerOver;

            public void triggerInHandler()
            {

            }
            public void triggerOverHandler()
            {

            }
            public void triggerOutHandler()
            {

            }

            override public void Destroy()
            {
                // TODO
                for (int i = 0; i < g.b.Height; i++)
                    for (int j = 0; j < g.b.Width; j++)
                    {
                        if (this is Material)
                            g.b.SetField(j, i, null);
                        if (this is Bonus)
                            g.b.SetBonus(j, i, null);
                    }
            }

            public Element(game Tg)
                : base(Tg)
            {

            }

        }
    }
}
