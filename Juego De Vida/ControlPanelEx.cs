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
    public partial class ControlPanelEx : UserControl
    {
        public Color aC1 { get { return this.button1.BackColor; } }
        public Color dC1 { get { return this.button2.BackColor; } }
        public Color zC { get { return this.button3.BackColor; } }

        public bool spM { get { return this.checkBox1.Checked; } }

        private bool zm = false;
        public bool zM { get { return zm; } }

        private bool gm = false;
        public bool gM { get { return gm; } }

        public ControlPanelEx()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.button1.BackColor = colorDialog1.Color;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.button2.BackColor = colorDialog1.Color;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.button3.BackColor = colorDialog1.Color;
            }
        }


        public event EventHandler backColorChange;

        private void ChangeColors(object sender, EventArgs e)
        {
            if (this.backColorChange != null)
                this.backColorChange(this, e);
        }
        public event EventHandler setSpecial;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.setSpecial != null)
                this.setSpecial(this, e);
        }



        private void checkBox2_Click(object sender, EventArgs e)
        {
            if (this.checkBox3.Checked)
            {
                zm = false;
                this.checkBox3.Checked = false;
            }
            if (!this.checkBox2.Checked)
            {
                zm = false;
                this.checkBox2.Checked = true;
            }
        }

        private void checkBox3_Click(object sender, EventArgs e)
        {
            if (this.checkBox2.Checked)
            {
                zm = true;
                this.checkBox2.Checked = false;
            }
            if (!this.checkBox3.Checked)
            {
                zm = true;
                this.checkBox3.Checked = true;
            }
        }

        public event EventHandler ModeChange;
        private void checkBox4_Click(object sender, EventArgs e)
        {
            if (this.checkBox5.Checked)
            {
                gm = false;
                this.checkBox5.Checked = false;
            }
            if (!this.checkBox4.Checked)
            {
                gm = false;
                this.checkBox4.Checked = true;
            }

            if (this.ModeChange != null)
                this.ModeChange(this, e);
        }

        private void checkBox5_Click(object sender, EventArgs e)
        {
            if (this.checkBox4.Checked)
            {
                gm = true;
                this.checkBox4.Checked = false;
            }
            if (!this.checkBox5.Checked)
            {
                gm = true;
                this.checkBox5.Checked = true;
            }

            if (this.ModeChange != null)
                this.ModeChange(this, e);
        }

        public event EventHandler seedn;
        private void button4_Click(object sender, EventArgs e)
        {
            if (this.seedn != null)
                this.seedn(this, e);
        }

        public event EventHandler seedz;
        private void button5_Click(object sender, EventArgs e)
        {
            if (this.seedz != null)
                this.seedz(this, e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hi there, if you were expecting this button to work, then let me apologize for making you think so, for now this button is merely a"
            + " a useless button with no true function, there was going to be a function, but I got lazy and just made this messagebox instead, again, sorry.", "sorry.", MessageBoxButtons.OK);
        }
    }
}
