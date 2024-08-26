using CoupangSub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoupangSub.Orders
{
    public partial class BlockMenuItemCtrl : UserControl
    {
        private TOrderItem mMenu;


        public BlockMenuItemCtrl()
        {
            InitializeComponent();
        }

        public void SetData(TOrderItem menu)
        {
            mMenu = menu;
            chkMenu.Text = menu.name;
            pnlOptions.Controls.Clear();
            foreach (var opt in menu.itemOptions)
            {
                    CheckBox chkOption = new CheckBox();
                    chkOption.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    chkOption.Margin = new Padding(0, (pnlOptions.Controls.Count < 1) ? 0 : 5, 0, 0);
                    chkOption.Size = new Size(180, 30);
                    chkOption.Tag = opt;
                    chkOption.Text = opt.optionName;
                    chkOption.TextAlign = ContentAlignment.MiddleLeft;
                    chkOption.UseVisualStyleBackColor = true;
                    pnlOptions.Controls.Add(chkOption);
            }
            if (pnlOptions.Controls.Count < 1)
            {
                Size = new Size(Width, pnlOptions.Top + 5);
            }
        }

        private void EnableOptions(bool enabled)
        {
            foreach (var ctrl in pnlOptions.Controls)
            {
                if (ctrl is CheckBox)
                    (ctrl as CheckBox).Enabled = enabled;
            }
        }

        private void CheckOptions(bool isChecked)
        {
            foreach (var ctrl in pnlOptions.Controls)
            {
                if (ctrl is CheckBox)
                    (ctrl as CheckBox).Checked = isChecked;
            }
        }

        public void EnableMenu(bool enabled)
        {
            chkMenu.Enabled = enabled;
            EnableOptions(enabled);
        }


    }
}
