using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coupang
{
    public partial class Accept_Order : Form
    {
        public Accept_Order()
        {
            InitializeComponent();
        }

        private void Accept_Order_Load(object sender, EventArgs e)
        {
            foreach (RadioButton r in this.Accept_Buttons.Controls)
            {
                r.CheckedChanged += Time_CheckedChanged;
            }
        }

        private void Time_CheckedChanged(object sender, EventArgs e)
        {

            RadioButton r = (RadioButton)sender;

            if (r.Checked == true)
            {
                this.Accept_Button.Text = $"Deliver in {r.Tag.ToString()} min";
                this.Accept_Button.Tag = r.Tag;
                this.Accept_Button.Enabled = true;
            }

        }

        private void Accept_Button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
