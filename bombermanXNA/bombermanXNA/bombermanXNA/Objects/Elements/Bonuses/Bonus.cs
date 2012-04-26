using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using bomberman.Main;

namespace bomberman.Objects.Elements.Bonuses
{
    abstract class Bonus : Element
    {
        int lifetime;
        public Bonus(game Tg)
            : base(Tg)
        {
            lifetime = 8;
            g.sec.seconderTrigger += SecHandler;
        }

        void SecHandler(object sender, EventArgs e)
        {
            lifetime -= 1;
            if (lifetime < 0)
                Destroy();
        }

        new public void Destroy()
        {
            for (int i = 0; i < g.b.Height; i++)
                for (int j = 0; j < g.b.Width; j++)
                {
                    if (this == g.b.GetBonus(j, i))
                    {
                        g.b.SetBonus(j, i, null);
                        return;
                    }
                }
        }
        new public void triggerInHandler()
        {
            Destroy();
            g.AddPoints(100);
        }

    }
}
