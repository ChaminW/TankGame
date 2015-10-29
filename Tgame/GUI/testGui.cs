using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tgame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            new socket.client().sendData(common.parameters.UP);
        }

        private void btnShoot_Click(object sender, EventArgs e)
        {
            new socket.client().sendData(common.parameters.SHOOT);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            new socket.client().sendData(common.parameters.DOWN);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            new socket.client().sendData(common.parameters.RIGHT);
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            new socket.client().sendData(common.parameters.LEFT);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
