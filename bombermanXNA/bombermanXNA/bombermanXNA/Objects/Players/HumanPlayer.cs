using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using bomberman.Main;
using bomberman.Moves;
using bomberman;
using bombermanXNA;

using bomberman.Objects.Elements.Materials;
using bomberman.Objects.Elements.Bonuses;

namespace bomberman.Objects.Players
{
    class HumanPlayer : Player
    {
        public bool leave;
        public int bombx;
        public int bomby;
        public HumanPlayer(game Tg) : base(Tg, 1000,1000, 4)
        {
            // TODO sprawdzić czy faktycznie player porusza się
            // z prędkością 4 pola na sek
            leave = false;
            PMove = new HumanMove(this);
        }

        public bool PerformMove(KBState kb)
        {

            long NowTicks = DateTime.Now.Ticks;
            long Difference = NowTicks - g.LastTicks;
            if (Difference < 0)
                throw new Exception("WTF?");


            float ms = Difference / 10000.0f;
            ms = Math.Min(ms, 50.0f);
            ms = Math.Max(ms, 0.0f);

            g.LastTicks = NowTicks;

            float oldXf = LocXf;
            float oldYf = LocYf;

            int homex = LocX;
            int homey = LocY;

            if (leave)
            {
                if (!LocatedXY(bombx, bomby))
                    leave = false;
            }

            if (kb.space == KeyState.PRESSED)
            {
                if (!(g.b.GetField(homex, homey) is Bomba))
                {
                    // TODO poprawić, że na szczególnych przypadkach z bomby wchodzi na bombę
                    g.b.SetField(homex, homey, new Bomba(g));
                    bombx = homex;
                    bomby = homey;
                    leave = true;
                }
            }

            if (kb.right == KeyState.PRESSED)
            {
                LocXf += ms * 0.1f;
                LocXf = Math.Min(45 * g.b.Width - 45, LocXf);
                MovePlayer(oldXf, oldYf, true);
            }

            if (kb.left == KeyState.PRESSED)
            {
                LocXf -= ms * 0.1f;
                LocXf = Math.Max(0.0f, LocXf);
                MovePlayer(oldXf, oldYf, true);
            }

            if (kb.up == KeyState.PRESSED)
            {
                LocYf -= ms * 0.1f;
                LocYf = Math.Max(0.0f, LocYf);
                MovePlayer(oldXf, oldYf, false);
            }

            if (kb.down == KeyState.PRESSED)
            {
                LocYf += ms * 0.1f;
                LocYf = Math.Min(45 * g.b.Height - 45, LocYf);
                MovePlayer(oldXf, oldYf, false);
            }

            int newX = LocX;
            int newY = LocY;

            Material matOld = g.b.GetField(homex, homey);
            Material matNew = g.b.GetField(newX, newY);

            Bonus bonOld = g.b.GetBonus(homex, homey);
            Bonus bonNew = g.b.GetBonus(newX, newY);

            if (matOld != null)
                matOld.triggerOutHandler();
            if (bonOld != null)
                bonOld.triggerOutHandler();

            if (matNew != null)
                matNew.triggerInHandler();
            if (bonNew != null)
                bonNew.triggerInHandler();

            return true;

        }

        protected override bool RestoreDefaults()
        {
            throw new NotImplementedException();
        }
    }
}
