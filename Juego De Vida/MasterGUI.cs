using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Juego_De_Vida
{
    public partial class MasterGUI : Form
    {
        private int Gruns = 1;
        private int xblocks = 101;
        private int yblocks = 61;
        private Life life;
        private Bitmap btm;
        private CancellationTokenSource _cancellation;
        private Random rnd = new Random();


        public MasterGUI()
        {
            InitializeComponent();
            life = new Life(xblocks, yblocks, this.controlPanelEx1.aC1, this.controlPanelEx1.dC1, this.controlPanelEx1.zC, this.controlPanelEx1.spM, this.controlPanel1.Density);
            btm = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            life.getNextState(this.controlPanel1.Diagnal, false, false, this.controlPanel1.Self);
            btm = life.getImage(btm);
            pictureBox1.Image = btm;
        }

        private void UserControl_ButtonClick(object sender, EventArgs e)
        {
            Stopwatch swM = new Stopwatch();
            TimeSpan ats;
            List<TimeSpan> sourcelist = new List<TimeSpan>();

            if (_cancellation == null)
            {
                swM.Start();
                _cancellation = new CancellationTokenSource();
                var token = _cancellation.Token;
                int x = this.controlPanel1.Xaxis;
                int y = this.controlPanel1.Yaxis;
                int runs = this.controlPanel1.Runs;
                int speed = this.controlPanel1.Speed;

                this.controlPanel1.resBar();
                this.controlPanel1.maxBar = this.controlPanel1.Runs;
                this.controlPanel1.setR = false;

                

                if ((!(x <= 0) && !(y <= 0)) && (!(x == xblocks) || !(y == yblocks)))
                {
                    Gruns = 1;
                    xblocks = x;
                    yblocks = y;
                    life = new Life(xblocks, yblocks, this.controlPanelEx1.aC1, this.controlPanelEx1.dC1, this.controlPanelEx1.zC, this.controlPanelEx1.spM, this.controlPanel1.Density);
                }


                Task.Factory.StartNew(() =>
                {
                    Invoke((Action)(() => { this.controlPanel1.btnName = "Stop!"; }));
                    int i = 0;
                    Stopwatch sw = new Stopwatch();
                    


                    while (!token.IsCancellationRequested)
                    {
                        sw.Restart();

                        bool diag = this.controlPanel1.Diagnal;
                        bool self = this.controlPanel1.Self;
                        i++;
                        life.getNextState(diag, false, false, self);
                        btm = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
                        btm = life.getImage(btm);

                        sw.Stop();
                        ats = sw.Elapsed;
                        sourcelist.Add(ats);
                        long averageTicks = Convert.ToInt64(sourcelist.Average(timeSpan => timeSpan.Ticks));
                        ats = new TimeSpan(averageTicks);
                        var framesPerSecond = 1 / (ats.TotalSeconds + (speed / (float)1000));

                        try
                        {
                            BeginInvoke((Action)(() =>
                            {
                                Gruns++;
                                this.label3.Text = String.Format("Generation: {0}", Gruns);
                                pictureBox1.Image = btm;
                                this.controlPanel1.locBar = i;
                                this.label1.Text = String.Format("Frames / Sec: {0:F3}", framesPerSecond);
                            }));
                        }
                        catch (System.InvalidOperationException) { Console.Beep(1500, 50); }
                        System.Threading.Thread.Sleep(speed);

                        if (i == runs)
                            _cancellation.Cancel();
                    }
                }, token).ContinueWith(_ =>
                {
                    swM.Stop();
                    Invoke((Action)(() => {
                        this.controlPanel1.setR = true;
                        this.controlPanel1.filBar();
                        this.controlPanel1.btnName = "Resume!";
                        this.label2.Text = String.Format("Last run time: {0:F3}", swM.Elapsed.TotalSeconds);
                    }));
                    _cancellation = null;
                });
            }
            else
            {
                _cancellation.Cancel();
                swM.Stop();
                Invoke((Action)(() => {
                    this.controlPanel1.setR = true;
                    this.controlPanel1.filBar();
                    this.controlPanel1.btnName = "Resume!";
                    this.label2.Text = String.Format("Last run time: {0:F3}s", swM.Elapsed.TotalSeconds);
                }));
            }
        }

        private void InputChange(object sender, EventArgs e)
        {
            if (_cancellation == null) { }
            else
            {
                _cancellation.Cancel();
            }
        }

        private void RefreshImage(object sender, EventArgs e)
        {
            btm = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            btm = life.getImage(btm);
            pictureBox1.Image = btm;
        }

        private void RefreshColors(object sender, EventArgs e)
        {
            this.life.setColors(this.controlPanelEx1.aC1, this.controlPanelEx1.dC1, this.controlPanelEx1.zC);
            this.RefreshImage(this, e);
        }

        private void setSpecial(object sender, EventArgs e)
        {
            this.life.Special = this.controlPanelEx1.spM;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.life.Draw(e.Location.X, e.Location.Y, this.controlPanelEx1.zM, btm);
            this.RefreshImage(this, e);
        }

        private void controlPanelEx1_ModeChange(object sender, EventArgs e)
        {
            this.life.setmode(this.controlPanelEx1.gM);
        }

        private void controlPanelEx1_seedn(object sender, EventArgs e)
        {
            this.life.seedNew();
            this.RefreshImage(this, e);
        }

        private void controlPanelEx1_seedz(object sender, EventArgs e)
        {
            this.life.seedZombie();
            this.RefreshImage(this, e);
        }

        private void MasterGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
