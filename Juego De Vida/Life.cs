using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Juego_De_Vida
{
    class LifeBlock
    {
        
        bool isAlive;// { get; set; }
        bool isZombie;// { get; set; }
        int weight;// { get; set; }

        public LifeBlock() {}

        public void setStatus(bool b) { isAlive = b; }
        public bool getStatus() { return isAlive; }
        public void setZStatus(bool b) { isZombie = b; }
        public bool getZStatus(){ return isZombie; }
        public void setWeight(int i) { if (i > 0 && i < 4) weight = i; }
        public int getWeight() { return weight; }

        public LifeBlock getMe() { return (LifeBlock)this.MemberwiseClone(); }
    }


    public class Life
    {
        private int blocksx;
        private int blocksy;

        private int ae = 3;
        private int ae3 = 3;
        private int ae2 = 2;

        ThreadSafeRandom rnd1 = new ThreadSafeRandom();
        ThreadSafeRandom rnd2 = new ThreadSafeRandom();

        LifeBlock[,] blocks     = new LifeBlock[0, 0];
        LifeBlock[,] blocksnext = new LifeBlock[0, 0];

        private Color alivecolor;
        private Color a2;
        private Color a3;
        private Color deadcolor;
        private Color d2;
        private Color d3;
        private Color zombiecolor;

        private bool special = true;
        public bool Special { set { special = value; } }

        private int density = 40;
        public int Density { set { density = value; } }
        


        public Life(int x, int y, Color al, Color de, Color zo, bool sp, int dsy)
        {
            special = sp;
            density = dsy;
            blocksx = x;
            blocksy = y;
            alivecolor  = al;
            deadcolor   = de;
            zombiecolor = zo;
            a2 = ControlPaint.Dark(al, (float)0.05);
            a3 = ControlPaint.Dark(al, (float)0.10);
            d2 = ControlPaint.Dark(de, (float)0.05);
            d3 = ControlPaint.Dark(de, (float)0.10);

            blocks = new LifeBlock[blocksx, blocksy];
            blocksnext = new LifeBlock[blocksx, blocksy];

            Action<int> action = yy =>
            {
                for (int xx = 0; xx < blocksx; xx++)
                {
                    blocks[xx, yy] = new LifeBlock();
                    blocksnext[xx, yy] = new LifeBlock();

                    blocks[xx, yy].setZStatus(false);
                    blocksnext[xx, yy].setZStatus(false);

                    if (ThreadSafeRandom.Next(100) < density)
                        blocks[xx, yy].setStatus(true);
                    else
                        blocks[xx, yy].setStatus(false);

                    if (!special)
                    {
                        blocks[xx, yy].setWeight(1);
                    }
                    else
                    {
                        int r = ThreadSafeRandom.Next(33);
                        blocks[xx, yy].setWeight(1);

                        if ((r >= 0) && (r < 11))
                            blocks[xx, yy].setWeight(1);
                        else if ((r >= 11) && (r < 22))
                            blocks[xx, yy].setWeight(2);
                        else if (r >= 22)
                            blocks[xx, yy].setWeight(3);
                    }
                }
            };
            Parallel.For(0, blocksy, action);
        }

        public void setmode(bool b)
        {
            if (!b)
            {
                ae = 3;
                ae3 = 3;
                ae2 = 2;
            }
            else
            {
                ae = 2;
                ae3 = 2;
                ae2 = 2;
            }
        }

        public void seedNew()
        {
            int T = blocksx * blocksy;
            int t = (int)Math.Ceiling(0.055f * T);

            Action<int> action = yy =>
            {
                for (int xx = 0; xx < blocksx; xx++)
                {
                    if(!blocks[xx, yy].getStatus() && !blocks[xx, yy].getZStatus())
                        if (ThreadSafeRandom.Next(T) < t){
                            blocks[xx, yy].setStatus(true);
                            blocksnext[xx, yy].setStatus(true);
                        }
                }
            };
            Parallel.For(0, blocksy, action);
        }
        public void seedZombie()
        {
            int T = blocksx * blocksy;
            int t = (int)Math.Ceiling(0.001f * T);

            Action<int> action = yy =>
            {
                for (int xx = 0; xx < blocksx; xx++)
                {
                    if (blocks[xx, yy].getStatus() && !blocks[xx, yy].getZStatus())
                        if (ThreadSafeRandom.Next(T) < t)
                        {
                            blocks[xx, yy].setZStatus(true);
                            blocksnext[xx, yy].setZStatus(true);
                        }
                }
            };
            Parallel.For(0, blocksy, action);
        }

        public void setColors(Color a, Color d, Color z)
        {
            alivecolor = a;
            deadcolor = d;
            zombiecolor = z;

            a2 = ControlPaint.Dark(a, (float)0.05);
            a3 = ControlPaint.Dark(a, (float)0.25);
            d2 = ControlPaint.Dark(d, (float)0.05);
            d3 = ControlPaint.Dark(d, (float)0.25);
        }
        public void Draw(float locX, float locY, bool Z, Bitmap bm)
        {
            float xS = (float)(bm.Width - 2) / blocksx;
            float yS = (float)(bm.Height - 2) / blocksy;
            
            int x = (int)Math.Floor(locX / xS);
            int y = (int)Math.Floor(locY / yS);

            if (!Z)
            {
                blocks[x, y].setStatus(!blocks[x, y].getStatus());
                blocksnext[x, y].setStatus(!blocks[x, y].getStatus());
                blocks[x, y].setZStatus(false);
                blocksnext[x, y].setZStatus(false);
            }
            else if (Z && blocks[x, y].getStatus())
            {
                blocks[x, y].setZStatus(!blocks[x, y].getZStatus());
                blocksnext[x, y].setZStatus(!blocks[x, y].getZStatus());
            }
        }

        public Bitmap getImage(Bitmap bm) 
        {
            Graphics gr = Graphics.FromImage(bm);

            float xS = (float)(bm.Width - 2) / blocksx;
            float yS = (float)(bm.Height-2) / blocksy;
            
            for (int y = 0; y < blocksy; y++)
            {
                for (int x = 0; x < blocksx; x++)
                {
                    if(blocks[x, y].getZStatus())
                        gr.FillRectangle(new SolidBrush(zombiecolor), (x * xS), (y * yS), xS, yS);
                    else if (blocks[x, y].getWeight() == 1)
                        gr.FillRectangle(new SolidBrush(blocks[x, y].getStatus() ? alivecolor : deadcolor), (x * xS), (y * yS), xS, yS);
                    else if (blocks[x, y].getWeight() == 2)
                        gr.FillRectangle(new SolidBrush(blocks[x, y].getStatus() ? a2 : d2), (x * xS), (y * yS), xS, yS);
                    else if (blocks[x, y].getWeight() == 3)
                        gr.FillRectangle(new SolidBrush(blocks[x, y].getStatus() ? a3 : d3), (x * xS), (y * yS), xS, yS);
                    else
                        gr.FillRectangle(new SolidBrush(Color.Lime), (x * xS), (y * yS), xS, yS);
                }
            }
            return bm;
        }

        public void getNextState(bool diag, bool virus, bool wrap, bool self)
        {
            Action<int> action = y =>
            {
                for (int x = 0; x < blocksx; x++)
                {
                    getNextStatus(x, y, diag, virus, wrap, self);
                }
            };
            Parallel.For(0, blocksy, action);
            //blocks = blocksnext;
            Action<int> actionTwo = y =>
            {
                for (int x = 0; x < blocksx; x++)
                {
                    blocks[x, y] = (LifeBlock)blocksnext[x, y].getMe();
                }
            };
            Parallel.For(0, blocksy, actionTwo);
        }

        private void getNextStatus(int x, int y, bool diag, bool virus, bool wrap, bool self)
        {
            int aliveneighboors = 0;
            blocksnext[x, y].setZStatus(false);

            bool Z = false;

            if (!((x - 1) <= 0))
            {
                if (blocks[x - 1, y].getStatus()) aliveneighboors++;
                if (blocks[x - 1, y].getZStatus()) Z = true;
            }
            if (!((x + 1) >= blocksx))
            {
                if (blocks[x + 1, y].getStatus()) aliveneighboors++;
                if (blocks[x + 1, y].getZStatus()) Z = true;
            }
            if (!((y - 1) <= 0))
            {
                if (blocks[x, y - 1].getStatus()) aliveneighboors++;
                if (blocks[x, y - 1].getZStatus()) Z = true;
            }
            if (!((y + 1) >= blocksy))
            {
                if (blocks[x, y + 1].getStatus()) aliveneighboors++;
                if (blocks[x, y + 1].getZStatus()) Z = true;
            }
            if (diag)
            {
                if (!((x - 1) <= 0) && !((y - 1) <= 0))
                {
                    if (blocks[x - 1, y - 1].getStatus()) aliveneighboors++;
                    if (blocks[x - 1, y - 1].getZStatus()) Z = true;
                }
                if (!((x - 1) <= 0) && !((y + 1) >= blocksy))
                {
                    if (blocks[x - 1, y + 1].getStatus()) aliveneighboors++;
                    if (blocks[x - 1, y + 1].getZStatus()) Z = true;
                }
                if (!((x + 1) >= blocksx) && !((y - 1) <= 0))
                {
                    if (blocks[x + 1, y - 1].getStatus()) aliveneighboors++;
                    if (blocks[x + 1, y - 1].getZStatus()) Z = true;
                }
                if (!((x + 1) >= blocksx) && !((y + 1) >= blocksy))
                {
                    if (blocks[x + 1, y + 1].getStatus()) aliveneighboors++;
                    if (blocks[x + 1, y + 1].getZStatus()) Z = true;
                }
            }
            if (self)
            {
                if (blocks[x, y].getStatus()) aliveneighboors++;
            }



            blocksnext[x, y].setWeight(1);
            if (special)
            {
                int r = ThreadSafeRandom.Next(33);

                if ((r >= 0) && (r < 11))
                    blocksnext[x, y].setWeight(1);
                else if ((r >= 11) && (r < 22))
                    blocksnext[x, y].setWeight(2);
                else if (r >= 22)
                    blocksnext[x, y].setWeight(3);
            }



            blocksnext[x, y].setZStatus(false);
            if (!blocks[x, y].getStatus())
            {
                blocksnext[x, y].setStatus(blocks[x, y].getStatus());
                if (aliveneighboors == ae)
                {
                    blocksnext[x, y].setStatus(true);
                    blocksnext[x, y].setZStatus(Z);
                }
            }
            else if (blocks[x, y].getStatus())
            {
                blocksnext[x, y].setStatus(blocks[x, y].getStatus());
                blocksnext[x, y].setZStatus(Z);
                if ((aliveneighboors > ae3) || (aliveneighboors < ae2) || blocks[x, y].getZStatus())
                {
                    blocksnext[x, y].setStatus(false);
                    blocksnext[x, y].setZStatus(false);
                }
            }
        }
    }
}
