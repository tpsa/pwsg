using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bomberman.Objects.Players;
using bomberman.Objects.Elements.Bonuses;

using bomberman.Main;

namespace bomberman.Objects.Elements.Materials
{
    class Bomba : Material
    {
        const int TimeLife = 3;
        int TimeLeft;
        bool Exploded = false;
        public Bomba(game Tg) : base(Tg)
        {
            TimeLeft = 3;
            g.sec.seconderTrigger += SeconderHandler;
            //TriggerOver += Explode;
        }

        void Explode()
        {
            
            int x = 0;
            int y = 0;

            if (!Exploded)
                Exploded = true;
            else return;
            bool koniec = false;
            for (int i = 0; !koniec && i < g.b.Height; i++)
                for (int j = 0; !koniec && j < g.b.Width; j++)
                {
                    if (g.b.GetField(j, i) == this)
                    {
                        x = j;
                        y = i;
                        koniec = true;
                    }
                }

            g.b.SetField(x, y, new Korytarz(g));
            g.sec.seconderTrigger -= this.SeconderHandler;

            for (int i = Math.Max(0,y - 1); i <= Math.Min(y + 1, g.b.Height-1); i++)
                for (int j = Math.Max(0,x - 1); j <= Math.Min(x + 1, g.b.Width-1); j++)
                    Attack(j, i);
        }

        public void Attack(int x, int y)
        {
            if (x == g.b.player.LocX && y == g.b.player.LocY)
            {
                g.b.player.Destroy();
                g.end();
                return;
            }

            foreach (Player p in g.b.opponents)
            {
                if (x == p.LocX && y == p.LocY)
                {
                    p.Destroy();
                    break;
                }
            }

            Material m = g.b.GetField(x, y);
            Bonus b = g.b.GetBonus(x, y);
            if (m is Bomba)
            {
                //for (int i = Math.Max(0, y - 1); i <= Math.Min(y + 1, g.b.Height - 1); i++)
                //    for (int j = Math.Max(0, x - 1); j <= Math.Min(x + 1, g.b.Width - 1); j++)
                //        Attack(j, i);      
                Bomba bo = m as Bomba;
                bo.Explode();
            }

            if (!(m is Korytarz) && !(m is BlokTrwaly))
                g.b.SetField(x, y, new Korytarz(g));
            g.b.SetBonus(x,y,null);



        }

        public void SeconderHandler(object sender, EventArgs e)
        {
            --TimeLeft;
            if (TimeLeft < 0)
                Explode();
        }

    }
}
