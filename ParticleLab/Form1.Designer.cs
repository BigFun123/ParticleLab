namespace ParticleLab
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.AddPhotonButton = new System.Windows.Forms.ToolStripButton();
            this.AddGravityExperiment = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 40;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddPhotonButton,
            this.AddGravityExperiment});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1104, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // AddPhotonButton
            // 
            this.AddPhotonButton.Image = ((System.Drawing.Image)(resources.GetObject("AddPhotonButton.Image")));
            this.AddPhotonButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddPhotonButton.Name = "AddPhotonButton";
            this.AddPhotonButton.Size = new System.Drawing.Size(91, 22);
            this.AddPhotonButton.Text = "Add Photon";
            this.AddPhotonButton.Click += new System.EventHandler(this.AddPhotonButton_Click);
            // 
            // AddGravityExperiment
            // 
            this.AddGravityExperiment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.AddGravityExperiment.Image = ((System.Drawing.Image)(resources.GetObject("AddGravityExperiment.Image")));
            this.AddGravityExperiment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddGravityExperiment.Name = "AddGravityExperiment";
            this.AddGravityExperiment.Size = new System.Drawing.Size(111, 22);
            this.AddGravityExperiment.Text = "Gravity Experiment";
            this.AddGravityExperiment.Click += new System.EventHandler(this.AddGravityExperiment_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 732);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton AddPhotonButton;
        private System.Windows.Forms.ToolStripButton AddGravityExperiment;
    }
}

