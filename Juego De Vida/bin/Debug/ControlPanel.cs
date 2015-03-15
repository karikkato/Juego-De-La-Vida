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
    public partial class ControlPanel : UserControl
    {
        public ControlPanel()
        {
            InitializeComponent();
        }


        public int maxBar { set { this.progressBar1.Maximum = value; } }
        public int locBar { set { this.progressBar1.Value = value; } }
        
        public void resBar()
        {
            this.progressBar1.Value = 0;
        }
        public void filBar()
        {
            this.progressBar1.Value = this.progressBar1.Maximum;
        }

        public string btnName
        {
            set
            {
                this.button1.Text = value;
            }
            get
            {
                return this.button1.Text;
            }
        }

        int xaxis = 101;
        public int Xaxis
        {
            get 
            {
                int x;
                int.TryParse(this.textBox1.Text, out x);
                int y;
                int.TryParse(this.textBox2.Text, out y);
                if (x > 0 && y > 0) xaxis = x;
                return x; 
            }
        }
        int yaxis = 61;
        public int Yaxis
        {
            get
            {
                int y;
                int.TryParse(this.textBox2.Text, out y);
                int x;
                int.TryParse(this.textBox1.Text, out x);
                if (y > 0 && x > 0) yaxis = y;
                return y;
            }
        }
        public int Runs
        {
            get
            {
                int x = 1; 
                int.TryParse(this.textBox3.Text, out x);
                if (x > 0)
                    return x;
                else
                    return 1;
            }
        }
        public int Speed
        {
            get
            {
                switch (this.comboBox1.SelectedIndex)
                {
                    case  0: return 1000;
                    case  1: return 550;
                    case  2: return 150;
                    case  3: return 30;
                    case  4: return 10;
                    case  5: return 7;
                    case  6: return 1;
                    default: return 1;
                }
            }
        }
        public int Density
        {
            get
            {
                return this.trackBar1.Value;
            }
        }

        public bool Diagnal
        {
            get { return this.checkBox2.Checked; }
        }
        public bool Self
        {
            get { return this.checkBox3.Checked; }
        }

        public bool setR
        {
            set
            {
                this.comboBox1.Enabled = value;
            }
        }

        public event EventHandler ButtonClick;
        protected void button1_Click(object sender, EventArgs e)
        {
            if (this.ButtonClick != null)
                this.ButtonClick(this, e);
        }


        public event EventHandler textBoxChangeState;
        private void btn_StartNew(object sender, EventArgs e)
        {
            int x = 0;
            int.TryParse(this.textBox3.Text, out x);

            if (!(x > 0) && !(this.button1.Text == "Stop!"))
            {
                this.button1.Enabled = false;
                this.button1.Text = "Enter runs";
            }
            else
            {
                this.button1.Enabled = true;
                if ((this.textBox1.Text == "" && this.textBox2.Text == "") || (this.textBox1.Text == xaxis.ToString() && this.textBox2.Text == yaxis.ToString()))
                    this.button1.Text = "Resume!";
                else
                    this.button1.Text = "Run new!";
            }

            if (this.textBoxChangeState != null)
                this.textBoxChangeState(this, e);
        }
    }
}
