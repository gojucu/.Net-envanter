using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Envanter
{
    public partial class Form1 : Form
    {
        public static string Role = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Open_Technical_Click(object sender, EventArgs e)
        {
            Technical frm = new Technical();
            frm.Show();
            //frm.ShowDialog();
        }
    }
}
