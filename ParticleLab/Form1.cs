using ParticleLab.src;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParticleLab
{
    public partial class Form1 : Form
    {
        BulletInit bullet = new BulletInit();
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bullet.Pulse();
            Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //bullet.Start();
            timer1.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            bullet.PaintTo(e.Graphics);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            bullet.Dispose();
        }

        private void AddPhotonButton_Click(object sender, EventArgs e)
        {
            bullet.AddPhoton();
        }

        private void AddGravityExperiment_Click(object sender, EventArgs e)
        {
            bullet.AddGravityExperiment();
        }
    }
}
