using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace bombermanXNA.Menus
{
    class MainMenu
    {
        Game1 g;
        protected SpriteBatch sb;
        int position = 0;
        long LastTicks;

        public MainMenu(Game1 g, SpriteBatch sb)
        {
            this.g = g;
            this.sb = sb;
            position = 0;
            LastTicks = DateTime.Now.Ticks;
        }

        public void Update(KBState kb)
        {

            if (DateTime.Now.Ticks - LastTicks < 2500000)
                return;

            LastTicks = DateTime.Now.Ticks;

            if (kb.down == KeyState.PRESSED)
                position = (position + 1) % 2;

            if (kb.up == KeyState.PRESSED)
            {
                if (position > 0)
                    --position;
                else position = 1;
            }

            if (kb.enter == KeyState.PRESSED)
            {
                // uruchom daną pozycję
                switch (position)
                {
                    case 0:
                        g.MenuActive = false;
                        break;
                    case 1:
                        g.Exit();
                        break;
                    default:
                        throw new Exception();
                }
            }

        }

        public void Draw()
        {
            Vector2 v = new Vector2(50,20);
            Vector2 bigger = new Vector2(1.2f, 1.2f);
            Vector2 vscale;
            Vector2 normal = new Vector2(1.0f, 1.0f);
            Rectangle okno = new Rectangle(0, 0,
                g.Window.ClientBounds.Width, g.Window.ClientBounds.Height);
            sb.Draw(g.penguins, okno, Color.Azure);

            if (position == 0)
                vscale = bigger;
            else
                vscale = normal;

            sb.DrawString(g.FontMenu,
                "Start gry", new Vector2(400, 400), Color.AliceBlue, 0, new Vector2(0, 0), vscale, SpriteEffects.None, 0);

            if (position == 1)
                vscale = bigger;
            else
                vscale = normal;

            sb.DrawString(g.FontMenu, 
                "Koniec gry", new Vector2(400,440), Color.AliceBlue, 0, new Vector2(0,0), vscale, SpriteEffects.None, 0);
        }
    }
}
