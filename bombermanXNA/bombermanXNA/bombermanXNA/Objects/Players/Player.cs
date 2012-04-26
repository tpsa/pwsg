using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bomberman.Moves;
using bomberman.Main;
using bomberman.Board;

using bomberman.Objects.Elements.Materials;
using bomberman.Objects.Elements.Bonuses;

namespace bomberman.Objects
{
    namespace Players
    {
        abstract class Player : Object
        {
            const float FieldWidth = 45;
            const float FieldHeight = 45;
            protected uint CurrentHealth;
            protected uint MaxHealth;
            protected Move PMove;
            protected double Speed;
            public event EventHandler AttackEvent;
            protected float X;
            protected float Y;
            public int gotox;
            public int gotoy;
            public List<Bpoint> trasa;
            long refresh;

            public void MovePlayer(float oldXf, float oldYf, bool horizontal)
            {
                //Material mat = b.GetField(player.LocX, player.LocY);

                int x = (int)(LocXf / 45);
                int y = (int)(LocYf / 45);
                int x2 = Math.Min(x + 1, g.b.Width - 1);
                int y2 = Math.Min(y + 1, g.b.Height - 1);
                int homex = LocX;
                int homey = LocY;

                // poprawić jeżeli ktoś chce iść po przekątnej z lewa góry do prawa w dól a w dole jest bomba
                // to gość ma iść w prawo ;p a nie zatrzymywać się

                // prawie dobrze ale zachodzi zbyt głęboko z bomby (ja rzuca)

                for (int i = y; i <= y2; i++)
                    for (int j = x; j <= x2; j++)
                    {
                        Material mat = g.b.GetField(j, i);
                        if (LocatedXY(j, i) && !(mat is Korytarz))
                        {
                            if (horizontal)
                            {
                                if (this is HumanPlayer)
                                {
                                    HumanPlayer p = (HumanPlayer)this;
                                    if (!p.leave)
                                    {
                                        LocXf = oldXf;
                                        return;
                                    }

                                    Material m = p.g.b.GetField(p.LocX, p.LocY);
                                    if (!(m is Korytarz) && (!(m is Bomba)))
                                    {
                                        LocXf = oldXf;
                                        return;
                                    }

                                    if (m is Bomba && (p.LocX != p.bombx || p.LocY != p.bomby))
                                    {
                                        LocXf = oldXf;
                                        return;
                                    }

                                }
                            }
                            else
                            {
                                if (this is HumanPlayer)
                                {
                                    HumanPlayer p = (HumanPlayer)this;
                                    if (!p.leave)
                                    {
                                        LocYf = oldYf;
                                        return;
                                    }

                                    Material m = p.g.b.GetField(p.LocX, p.LocY);
                                    if (!(m is Korytarz) && (!(m is Bomba)))
                                    {
                                        LocYf = oldYf;
                                        return;
                                    }

                                    if (m is Bomba && (p.LocX != p.bombx || p.LocY != p.bomby))
                                    {
                                        LocYf = oldYf;
                                        return;
                                    }
                                }
                            }
                            return;
                        }
                    }

            }

            override public void Destroy()
            {
                if (this == g.b.player)
                    g.end();
                else
                {
                    g.b.opponents.Remove(this);
                }
            }

            public bool InMove()
            {
                if (gotox*45 == LocXf && gotoy*45 == LocYf)
                    return false;
                else
                    return true;
            }

            public float LocXf
            {
                get
                {
                    return X;
                }
                set
                {
                    if (X >= g.b.Width * 45)
                        throw new IndexOutOfRangeException();
                    X = value;
                }
            }

            public float LocYf
            {
                get
                {
                    return Y;
                }
                set
                {
                    if (Y >= g.b.Height * 45)
                        throw new IndexOutOfRangeException();
                    Y = value;
                }
            }
            
            public int LocX
            {
                get
                {
                    int field = 0;
                    int calk = (int)(X / FieldWidth);
                    if (X - calk * FieldWidth < 45 / 2)
                        field = calk;
                    else
                        field = calk + 1;
                    if (field >= g.b.Width)
                        field = g.b.Width - 1;
                    return field;
                }
                set
                {
                    X = value * FieldWidth;
                }
            }

            public bool LocatedXY(int x, int y)
            {
                float left = x * 45;
                float top = y * 45;

                if (X - left >= -40 && X - left <= 40 && Y - top >= -40 && Y - top <= 40)
                    return true;
                else
                    return false;

            }

            public int LocY
            {
                get
                {
                    int field;
                    int calk = (int)(Y / FieldHeight);
                    if (Y - calk * FieldHeight < 45 / 2)
                        field =  calk;
                    else
                        field =  calk + 1;
                    if (field >= g.b.Height)
                        field = g.b.Height - 1;
                    return field;
                }
                set
                {
                    Y = value * FieldHeight;
                }
            }
                

            abstract protected bool RestoreDefaults();

            public Player(game Tg, uint CurrentHealth, uint MaxHealth, double Speed) : base(Tg)
            {
                this.CurrentHealth = CurrentHealth;
                this.MaxHealth = MaxHealth;
                AttackEvent += AttackEventHandler;
            }

            public bool PerformMove()
            {
                if (InMove())
                {
                    float diffx = gotox*45 - LocXf;
                    float diffy = gotoy*45 - LocYf;

                    if (diffx != 0)
                    {
                        if (diffx > 0)
                            LocXf += 1;
                        else
                            LocXf -= 1;
                    }

                    if (diffy != 0)
                    {
                        if (diffy > 0)
                            LocYf += 1;
                        else
                            LocYf -= 1;
                    }

                }
                else
                {

                    if (trasa == null || trasa.Count == 0 || DateTime.Now.Ticks - refresh > 10000000)
                    {
                        refresh = DateTime.Now.Ticks;
                        bool result = PMove.perform();
                    }
                    else
                    {
                        Bpoint paszli = trasa.First();
                        trasa.Remove(paszli);
                        gotox = paszli.x;
                        gotoy = paszli.y;
                    }
                }

                /// zmiana jego lokalizacji

                if (LocX == g.b.player.LocX && LocY == g.b.player.LocY)
                    g.end();

                return true;
            }

            private void AttackEventHandler (object sender, EventArgs e)
            {
                // TODO
            }


        }
    }
}
