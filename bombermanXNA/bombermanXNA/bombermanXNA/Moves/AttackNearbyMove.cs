using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using bomberman.Objects.Players;
using bomberman.Board;

namespace bomberman.Moves
{
    class AttackNearbyMove : Move
    {
        public AttackNearbyMove(Player p) : base(p)
        {
            
        }

        protected internal override bool perform()
        {
            if (Math.Abs(p.LocX - p.g.b.player.LocX) < 5 && Math.Abs(p.LocY - p.g.b.player.LocY) < 5)
            {
                Bpoint from = new Bpoint{ x = p.LocX, y = p.LocY};
                Bpoint to = new Bpoint { x = p.g.b.player.LocX, y = p.g.b.player.LocY};

                p.trasa = p.g.b.GetShortestPath(from, to);
            }
            else
            {
            int x = p.LocX;
            int y = p.LocY;

            Board.Board b = p.g.b;

            if (p.InMove())
                return true;

                                    Random r = new Random();
            for (int i = Math.Max(0,y-1); i <= Math.Min(y+1, b.Height -1); i++)
                for (int j = Math.Max(x - 1, 0); j <= Math.Min(x + 1, b.Width - 1); j++)
                {
                    if ((j == x || i == y) && !(i == y && j == x))
                    {
                        if (!p.g.b.IsFree(j, i)) continue;
                        int los = r.Next() % 4;
                        if (los == 0)
                        {
                            p.gotox = j;
                            p.gotoy = i;
                            return true;
                        }
                    }
                }
            }

            return true;
        }
    }
}
