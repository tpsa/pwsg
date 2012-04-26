using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using bomberman.Objects.Players;
using bomberman.Board;
using bomberman;
using bomberman.Objects.Elements.Bonuses;
using bomberman.Objects.Elements.Materials;
using bombermanXNA;

namespace bomberman.Main
{
    class game
    {
        internal event EventHandler GameStarted;
        internal event EventHandler GamePaused;
        internal event EventHandler GameFinished;
        internal event EventHandler GameSecond;
        internal event EventHandler GameDraw;
        internal event EventHandler GameOver;
        int counter;

        public Seconder sec;

        Texture2D pacman;

        internal int RefreshRate;
        internal Board.Board b;
        CSettings settings;
        Music music;
        public long LastTicks;


        private int points;
        private int lifes;
        private int MaxTime;
        private int TimeLeft;

        public int GPoints
        {
            get
            {
                return points;
            }

        }

        public void AddPoints(int value)
        {
            if (value < 0)
                throw new Exception("nie ma cheatowania");
            points += value;
        }

        public int GLifes
        {
            get
            {
                return lifes;
            }
        }

        public int GTimeLeft
        {
            get
            {
                return TimeLeft;
            }
        }

        protected Game1 g;

        public game(Game1 g)
        {
            sec = new Seconder();
            this.g = g;
            settings = new CSettings();
            RefreshRate = 20;
            music = new Music();
            points = 0;
            lifes = 3;
            MaxTime = 10;
            TimeLeft = MaxTime;
            sec.seconderTrigger += SecHandler;
            GameFinished += GameFinishedHandler;
            GameStarted += GameStartedHandler;
            GameDraw += GameDrawHandler;
            GamePaused += GamePausedHandler;
            GameOver += GameOverHandler;
            b = new Board.Board(this, "map.xml");
            b.player = new HumanPlayer(this);

            Player op = new Usmiech(this);
            op.LocX = 9;
            op.LocY = 9;
            op.gotox = op.LocX;
            op.gotoy = op.LocY;
            b.opponents = new List<Player>();
            b.opponents.Add(op);

        }

        private void SecHandler(object sender, EventArgs e)
        {
            counter = (counter + 1) % 5;
            g.Window.Title = counter.ToString();
            if (counter == 0)
            {
                b.GenerateRandomBoard((double)DateTime.Now.Millisecond);
                b.GenerateOpponents();
            }
        }

        public int Update (KBState kb, long LastTicks)
        {
            // zrób coś ;>
            if (kb.escape == KeyState.PRESSED)
            {
                kb.enter = KeyState.FREE;
                g.MenuActive = true;
                return 0;
            }

            sec.Run();

            foreach (Player p in b.opponents)
                p.PerformMove();

            b.player.PerformMove(kb);
            
            if (b.player == null)
                return -1;
            else
                return 0;
        }

        public void Draw(SpriteBatch sb)
        {
            int x = (int)b.player.LocXf;
            int y = (int)b.player.LocYf;
            


            for (int i = 0; i < b.Height; i++)
                for (int j = 0; j < b.Width; j++)
                {
                    
                    Bonus Bon = b.GetBonus(j, i);
                    Material Mat = b.GetField(j, i);
                    if (Mat is Korytarz)
                        sb.Draw(g.korytarz, new Rectangle(45 + j * 45, 45 + i * 45, 45, 45), Color.White);
                    if (Mat is BlokTrwaly)
                        sb.Draw(g.mur, new Rectangle(45 + j * 45, 45 + i * 45, 45, 45), Color.White);
                    if (Mat is BlokNietrwaly)
                        sb.Draw(g.drzwi, new Rectangle(45 + j * 45, 45 + i * 45, 45, 45), Color.White);
                    if (Bon is Serce)
                        sb.Draw(g.heart, new Rectangle(45 + j * 45, 45 + i * 45, 45, 45), Color.White);
                    if (Bon is Moneta)
                        sb.Draw(g.moneta, new Rectangle(45 + j * 45, 45 + i * 45, 45, 45), Color.White);
                    if (Bon is Ogien)
                        sb.Draw(g.ogien, new Rectangle(45 + j * 45, 45 + i * 45, 45, 45), Color.White);
                    if (Bon is Wrotki)
                        sb.Draw(g.wrotki, new Rectangle(45 + j * 45, 45 + i * 45, 45, 45), Color.White);
                    if (Bon is Zegar)
                        sb.Draw(g.zegar, new Rectangle(45 + j * 45, 45 + i * 45, 45, 45), Color.White);
                    if (Mat is Bomba)
                    {
                        sb.Draw(g.korytarz, new Rectangle(45 + j * 45, 45 + i * 45, 45, 45), Color.White);
                        sb.Draw(g.bomba, new Rectangle(45 + j * 45, 45 + i * 45, 45, 45), Color.White);
                    }
                }

            for (int i = 0; i <= b.Height + 1; i++)
            {
                sb.Draw(g.mur, new Rectangle(i * 45, 0, 45, 45), Color.White);
                sb.Draw(g.mur, new Rectangle(0, i * 45, 45, 45), Color.White);
                sb.Draw(g.mur, new Rectangle(i * 45, (1+b.Height) * 45, 45, 45), Color.White);
                sb.Draw(g.mur, new Rectangle((b.Height+1) * 45, i * 45,  45, 45), Color.White);
            }

            sb.Draw(g.point, new Rectangle(45 + b.player.LocX*45, 45 + b.player.LocY*45, 45, 45), Color.White);            
            sb.Draw(g.pacman, new Rectangle(45 + x, 45 + y, 45, 45), Color.White);
            foreach (Player p in b.opponents)
            {
                Texture2D texopp = g.balon;
                if (p is Balon)
                    texopp = g.balon;
                if (p is Gabka)
                    texopp = g.gabka;
                if (p is Duch)
                    texopp = g.duch;
                if (p is Kropla)
                    texopp = g.kropla;
                if (p is Usmiech)
                    texopp = g.usmiech;
                sb.Draw(texopp, new Rectangle(45 + (int)p.LocXf, 45 + (int)p.LocYf, 45, 45), Color.White);
            }
        }


        public bool Finished
        {
            get
            {
                if(lifes < 0 | TimeLeft <= 0)
                    return true;
                else return false;
            }
        }

        public void start()
        {
            //Seconder.Start();
            //Logic.Start();
            GameStarted(this, new EventArgs());
        }

        public void pause()
        {
            GamePaused(this, new EventArgs());
        }

        public void end()
        {
            GameFinished(this, new EventArgs());
        }

        public void GameStartedHandler(object sender, EventArgs e)
        {
        }

        public void GamePausedHandler(object sender, EventArgs e)
        {
        }

        public void GameOverHandler(object sender, EventArgs e)
        {
            GameFinished(this, new EventArgs());
        }

        public void GameFinishedHandler(object sender, EventArgs e)
        {
            
        }



        private void SeconderTickHandler(object sender, EventArgs e)
        {
            GameSecond(this, new EventArgs());
            --TimeLeft;
            if (TimeLeft < 0)
            {
                GameOver(this, new EventArgs());
            }
        }

        private void LogicHandler(object sender, EventArgs e)
        {
            GameDraw(this, new EventArgs());
        }

        private void GameDrawHandler(object sender, EventArgs e)
        {

        }


    }
}
