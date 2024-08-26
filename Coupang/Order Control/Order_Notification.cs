
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Coupang
{
    public partial class Order_Notification : Form
    {
        public Order_Notification()
        {
            InitializeComponent();
        }
        public string orderId;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Width  >= 386)
            {

                this.timer1.Enabled = false;
                this.Width = 386;

                this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.MinimumSize.Height);

            }
            else
            {
       
                this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - (this.Width+10), Screen.PrimaryScreen.WorkingArea.Height - this.MinimumSize.Height);
                this.Width += 15;
      
            }
          
        }

        private void Order_Notification_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void orderStoreId_Click(object sender, EventArgs e)
        {

        }
    }
}
