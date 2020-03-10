using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LetterFrequencies
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void btnCloseAbout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
