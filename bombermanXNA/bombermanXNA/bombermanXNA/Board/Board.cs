using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;


using bomberman.Main;
using bomberman.Objects.Elements.Materials;
using bomberman.Objects.Elements.Bonuses;
using bomberman.Objects.Players;

namespace bomberman.Board
{

    class Bpoint
    {
        public int x { get; set; }
        public int y { get; set; }

        static public bool equals(Bpoint a, Bpoint b)
        {
            if (a.x != b.x)
                return false;
            if (a.y != b.y)
                return false;
            return true;
        }
    };

    class Board
    {
        game g;
        private Material[,] Fields;
        private Bonus[,] Bonuses;
        internal List<Player> opponents;
        internal HumanPlayer player;

        public event EventHandler DeathNotifyEvent;
        public event EventHandler DestructionNotifyEvent;
        public Random r;

        public int HeuristicEstimate(Bpoint from, Bpoint to)
        {
            double diffx = Math.Abs(from.x - to.x);
            double diffy = Math.Abs(from.y - to.y);
            return (int)(diffx + diffy); // metryka taksówkowa
            //return (int)Math.Sqrt(diffx * diffx + diffy * diffy);
        }

        public List<Bpoint> ReconstructPath(Bpoint[,] come_from, Bpoint goal)
        {
            List<Bpoint> path = new List<Bpoint>();
            Bpoint precedence = come_from[goal.y, goal.x];
            if (precedence != null)
                path.AddRange(ReconstructPath(come_from, precedence));
            path.Add(goal);
            return path;
        }

        public void GenerateOpponents()
        {
            int ile = opponents.Count;
            if (ile > 4) return;
            int ile_wypro = Math.Max(4 - ile, 0);

            for (int i = 0; ile_wypro >= 0 && i < Height; i++)
                for (int j = 0; ile_wypro >= 0 && j < Width; j++)
                {
                    if (IsFree(j, i))
                    {
                        int los = r.Next() % 100;
                        if (los < 5)
                        {
                            Balon b = new Balon(g);
                            b.LocX = j;
                            b.LocY = i;
                            b.gotox = j;
                            b.gotoy = i;
                            --ile_wypro;
                            opponents.Add(b);
                            continue;
                        }
                        if (los < 10)
                        {
                            Gabka d = new Gabka(g);
                            d.LocX = j;
                            d.LocY = i;
                            d.gotox = j;
                            d.gotoy = i;
                            --ile_wypro;
                            opponents.Add(d);
                            continue;
                        }
                        if (los < 15)
                        {
                            Duch gab = new Duch(g);
                            gab.LocX = j;
                            gab.LocY = i;
                            gab.gotox = j;
                            gab.gotoy = i;
                            --ile_wypro;
                            continue;
                        }
                        if (los < 20)
                        {
                            Kropla k = new Kropla(g);
                            k.LocX = j;
                            k.LocY = i;
                            k.gotox = j;
                            k.gotoy = i;
                            --ile_wypro;
                            continue;
                        }
                        if (los < 50)
                        {
                            Usmiech u = new Usmiech(g);
                            u.LocX = j;
                            u.LocY = i;
                            u.gotox = j;
                            u.gotoy = i;
                            --ile_wypro;
                            continue;
                        }
                    }
                }

        }

        public List<Bpoint> GetShortestPath(Bpoint from, Bpoint to)
        {
            List<Bpoint> path = new List<Bpoint>();
            List<Bpoint> closedset = new List<Bpoint>();
            List<Bpoint> openset = new List<Bpoint>();
            
            int[,] g_score = new int[Height, Width];
            int[,] h_score = new int[Height, Width];
            int[,] f_score = new int[Height, Width];
            Bpoint[,] come_from = new Bpoint[Height,Width];
            Bpoint[,] nodes = new Bpoint[Height, Width];


            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                {
                    nodes[i, j] = new Bpoint { x = j, y = i };
                }

            openset.Add(nodes[from.y, from.x]);

            g_score[from.y, from.x] = 0;
            h_score[from.y, from.x] = HeuristicEstimate(from, to);
            f_score[from.y,from.x] = g_score[from.y, from.x] + h_score[from.y, from.x];

            while (openset.Count != 0)
            {
                int koszt_minimalny = int.MaxValue;
                Bpoint current = null;
                // znajdź element z najmniejszym kosztem
                for (int i = 0; i < Height; i++)
                    for (int j = 0; j < Width; j++)
                    {
                        if (openset.Contains(nodes[i,j]))
                        {
                            if (koszt_minimalny > f_score[i, j])
                            {
                                koszt_minimalny = f_score[i, j];
                                current = nodes[i, j];
                            }
                        }
                    }
                // mamy current
                if (current == nodes[to.y, to.x])
                {
                    // zrekonstruuj path
                    path = ReconstructPath(come_from, nodes[to.y, to.x]);
                    return path;
                }

                bool tentative_is_better = false;

                openset.Remove(current);
                closedset.Add(current);

                for (int i = Math.Max(current.y - 1, 0); i <= Math.Min(current.y + 1, Height - 1); i++)
                    for (int j = Math.Max(current.x - 1, 0); j <= Math.Min(current.x + 1, Width - 1); j++)
                    {
                        if (i == current.y && j == current.x)
                            continue;

                        if (i == current.y || j == current.x)
                        {
                            // łapią się wszyscy sąsiedzi
                            if (IsFree(j, i))
                            {
                                Bpoint neighbour = nodes[i, j];
                                if (closedset.Contains(neighbour))
                                    continue;

                                int tentative_g_score = g_score[current.y, current.x] + 1;

                                if (!openset.Contains(neighbour))
                                {
                                    openset.Add(neighbour);
                                    h_score[neighbour.y, neighbour.x] = HeuristicEstimate(neighbour, to);
                                    tentative_is_better = true;
                                }
                                else
                                {
                                    if (tentative_g_score < g_score[neighbour.y, neighbour.x])
                                        tentative_is_better = true;
                                    else
                                        tentative_is_better = false;
                                }

                                if (tentative_is_better)
                                {
                                    come_from[neighbour.y, neighbour.x] = current;
                                    g_score[neighbour.y, neighbour.x] = tentative_g_score;
                                    f_score[neighbour.y, neighbour.x] = g_score[neighbour.y, neighbour.x]
                                        + h_score[neighbour.y, neighbour.x];
                                }

                            }
                        }

                    }

            }

            return null;
        }

        private void DeathNotifyEventHandler(object sender, EventArgs e)
        {
            Player p = sender as Player;
            g.b.opponents.Remove(p);
        }

        private void DestructionNotifyEventHandler(object sender, EventArgs e)
        {
            if (sender is Bonus)
            {
                Bonus b = sender as Bonus;
                for (int i = 0; i < Bonuses.GetLength(0); i++)
                    for (int j = 0; j < Bonuses.GetLength(1); j++)
                    {
                        if (Bonuses[i, j] == b)
                        {
                            Bonuses[i, j] = null;
                            return;
                        }
                    }
            }
            else
            {
                Material m = sender as Material;
                for (int i = 0; i < Fields.GetLength(0); i++)
                    for (int j = 0; j < Fields.GetLength(1); j++)
                    {
                        if (Fields[i, j] == m)
                        {
                            Fields[i, j] = null;
                            return;
                        }
                    }
            }
        }

        public int Width
        {
            get
            {
                return Fields.GetLength(0);
            }
        }

        public int Height
        {
            get
            {
                return Fields.GetLength(1);
            }
        }


        public Board(game Tg, string FileName)
        {
            g = Tg;
            r = new Random((int)DateTime.Now.Ticks);
            LoadFromFile(FileName);
            GenerateRandomBoard(DateTime.Now.Millisecond); // TODO zamienić 666 na coś innego (seed)
            Bonuses[2, 3] = new Moneta(g);
            Fields[2, 4] = new Bomba(g);
            if (Fields.GetLength(0) != Bonuses.GetLength(0))
                throw new Exception ("Wymiary szerokości plansz nie zgadzają się");
            if (Fields.GetLength(1) != Bonuses.GetLength(1))
                throw new Exception ("Wymiary wysokości plansz nie zgadzają się");
        }

        public Material GetField(int x, int y)
        {
            if (y < 0 || y >= Fields.GetLength(0))
                throw new IndexOutOfRangeException();
            if (x < 0 || x >= Fields.GetLength(1))
                throw new IndexOutOfRangeException();
            return Fields[y, x];
        }

        public bool IsFree(int x, int y)
        {
            if (!(Fields[y, x] is Korytarz))
                return false;
            foreach (Player p in g.b.opponents)
                if (p.LocX == x && p.LocY == y)
                    return false;
            return true;
        }

        public Bonus GetBonus(int x, int y)
        {
            if (y < 0 || y >= Bonuses.GetLength(0))
                throw new IndexOutOfRangeException();
            if (x < 0 || x >= Bonuses.GetLength(1))
                throw new IndexOutOfRangeException();
            return Bonuses[y, x];
        }

        public void SetBonus(int x, int y, Bonus b)
        {
            if (y < 0 || y >= Bonuses.GetLength(0))
                throw new IndexOutOfRangeException();
            if (x < 0 || x >= Bonuses.GetLength(1))
                throw new IndexOutOfRangeException();
            Bonuses[y, x] = b;
        }

        public void SetField(int x, int y, Material m)
        {
            if (y < 0 || y >= Bonuses.GetLength(0))
                throw new IndexOutOfRangeException();
            if (x < 0 || x >= Bonuses.GetLength(1))
                throw new IndexOutOfRangeException();
            Fields[y, x] = m;
        }

        private bool LoadFromFile(string FileName)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(FileName);
            XmlElement el = xdoc.DocumentElement;

            XmlNode info = el.SelectSingleNode("/map/info/size");
            int width = 0;
            int height = 0;
            foreach (XmlAttribute attr in info.Attributes)
            {
                if (attr.Name == "width")
                    width = Int32.Parse(attr.Value);
                if (attr.Name == "height")
                    height = Int32.Parse(attr.Value);
            }
            

            XmlNodeList childs = el.ChildNodes;
            XmlNodeList list = el.SelectSingleNode("/map/elements").ChildNodes; // bloki
            Fields = new Material[height, width];
            Bonuses = new Bonus[height, width];

            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    Fields[i, j] = new Korytarz(g);

            foreach (XmlNode n in list)
            {
                int x = -1;
                int y = -1;
                bool BlokTrwaly = false;
                foreach (XmlAttribute attr in n.Attributes)
                {
                    if (attr.Name == "x")
                        x = Int32.Parse(attr.Value);
                    if (attr.Name == "y")
                        y = Int32.Parse(attr.Value);
                    if (attr.Name == "type")
                        if (attr.Value == "block")
                            BlokTrwaly = true;
                        else
                            BlokTrwaly = false;
                }
                if (BlokTrwaly)
                    Fields[y, x] = new BlokTrwaly(g);
                else
                    Fields[y, x] = new BlokNietrwaly(g);
            }

            return true;
        }

        public bool GenerateRandomBoard(double Seed)
        {

            // przelecieć się po polach wolnych i dla poszczególnych prawdopdobieństw
            // polosować bonusy
            for (int i = 0; i < Fields.GetLength(0); i++)
                for (int j = 0; j < Fields.GetLength(1); j++)
                {
                    if (Fields[i, j] is Korytarz && Bonuses[i,j] == null)
                    {
                        int los = r.Next() % 100;
                        if (los < 5)
                        {
                            // pojawia się nowy bonus
                            int los2 = r.Next() % 100;
                            if (los2 < 10)
                            {
                                Bonuses[i, j] = new Moneta(g);
                                continue;
                            }
                            if (los2 < 20)
                            {
                                Bonuses[i, j] = new Ogien(g);
                                continue;
                            }
                            if (los2 < 30)
                            {
                                Bonuses[i, j] = new Serce(g);
                                continue;
                            }
                            if (los2 < 40)
                            {
                                Bonuses[i, j] = new Wrotki(g);
                                continue;
                            }
                            if (los2 < 50)
                            {
                                Bonuses[i, j] = new Zegar(g);
                            }
                        }
                    }
                }

            return false;
            //throw new NotImplementedException();
        }

    }
}
