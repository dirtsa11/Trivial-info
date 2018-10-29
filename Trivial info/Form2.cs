using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trivial_info
{
    public partial class Form2 : Form
    {
        public bool modeBug;
        public bool chrono;

        public Form2()
        {
            InitializeComponent();

            modeBug = false;
            chrono = false;
        }

        private void btn_quitter_Click(object sender, EventArgs e)
        {
            this.Close();            
        }

        private void btn_Jouer_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1(modeBug, chrono);
            f1.ShowDialog(this);
        }

        private void btn_regles_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog(this);
        }

        private void btn_chrono_Click(object sender, EventArgs e)
        {
            if (chrono == false)
            {
                chrono = true;
                btn_chrono.Text = "On";
            }
            else
            {
                chrono = false;
                btn_chrono.Text = "Off";
            }
        }

        private void btn_modeBug_Click(object sender, EventArgs e)
        {
            if (modeBug == false)
            {
                modeBug = true;
                btn_modeBug.Text = "On";
            }
            else
            {
                modeBug = false;
                btn_modeBug.Text = "Off";
            }
        }
    }
}
