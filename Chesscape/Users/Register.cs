using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chesscape.Users
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void cb_secure_CheckedChanged(object sender, EventArgs e)
        {
            if(cb_secure.Checked)
            {
                tb_password.Enabled = true;
            } else
            {
                tb_password.Enabled = false;
            }
        }

        private void register_click(object sender, EventArgs e)
        {
            if(cb_secure.Checked == true)
            {
                if(tb_password.Text.Length <= 0)
                {
                    errorProvider1.SetError(tb_password, "You must provide a passord");
                    return;
                }
                else
                {
                    User user = new User(tb_username.Text, tb_password.Text);
                }
            }
            else
            {
                User user = new User(tb_password.Text);
            }
            DialogResult = DialogResult.OK;
        }

        private void cancel_click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void tb_username_Validating(object sender, CancelEventArgs e)
        {
            if (tb_username.Text.Length == 0)
            {
                errorProvider1.SetError(tb_username, "Please enter a username");
                e.Cancel = true;
            }
        }

        private void tb_password_Validating(object sender, CancelEventArgs e)
        {
            if(tb_password.Text.Length == 0 && tb_password.Enabled == true)
            {
                errorProvider1.SetError(tb_password, "You must provide a passord");
                e.Cancel = true;
            }
        }

        private void tb_username_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(tb_username, null);
        }

        private void tb_password_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(tb_password, null);
        }
    }
}
