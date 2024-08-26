using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoupangSub
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            if (Global.setting == null)
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            ClearValues();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void ClearValues()
        {
            var setting = Global.setting;
            txtAesKey.Text  = setting.aesKey;
            txtPubKey.Text  = setting.pubKey;
            txtVersion.Text = setting.appVersion;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
