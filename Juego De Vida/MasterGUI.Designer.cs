namespace Juego_De_Vida
{
    partial class MasterGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterGUI));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.controlPanel1 = new Juego_De_Vida.ControlPanel();
            this.controlPanelEx1 = new Juego_De_Vida.ControlPanelEx();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(159, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(615, 486);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.SizeChanged += new System.EventHandler(this.RefreshImage);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label1.Location = new System.Drawing.Point(10, 366);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Frames / Sec:  0.0";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(780, 373);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Last run time:  0.0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 379);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Generation: 1";
            // 
            // controlPanel1
            // 
            this.controlPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.controlPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.controlPanel1.btnName = "Resume!";
            this.controlPanel1.Location = new System.Drawing.Point(783, 13);
            this.controlPanel1.Name = "controlPanel1";
            this.controlPanel1.Size = new System.Drawing.Size(140, 357);
            this.controlPanel1.TabIndex = 5;
            this.controlPanel1.ButtonClick += new System.EventHandler(this.UserControl_ButtonClick);
            this.controlPanel1.textBoxChangeState += new System.EventHandler(this.InputChange);
            // 
            // controlPanelEx1
            // 
            this.controlPanelEx1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.controlPanelEx1.Location = new System.Drawing.Point(13, 13);
            this.controlPanelEx1.Name = "controlPanelEx1";
            this.controlPanelEx1.Size = new System.Drawing.Size(140, 350);
            this.controlPanelEx1.TabIndex = 4;
            this.controlPanelEx1.backColorChange += new System.EventHandler(this.RefreshColors);
            this.controlPanelEx1.setSpecial += new System.EventHandler(this.setSpecial);
            this.controlPanelEx1.ModeChange += new System.EventHandler(this.controlPanelEx1_ModeChange);
            this.controlPanelEx1.seedn += new System.EventHandler(this.controlPanelEx1_seedn);
            this.controlPanelEx1.seedz += new System.EventHandler(this.controlPanelEx1_seedz);
            // 
            // MasterGUI
            // 
            this.AccessibleName = "";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 511);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.controlPanel1);
            this.Controls.Add(this.controlPanelEx1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(700, 450);
            this.Name = "MasterGUI";
            this.Text = "El Juego De La Vida!";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MasterGUI_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private ControlPanelEx controlPanelEx1;
        private ControlPanel controlPanel1;
        private System.Windows.Forms.Label label3;
    }
}