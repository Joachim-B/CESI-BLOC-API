using ClientLourdBloc.API;
using System;
using System.Windows.Forms;

namespace ClientLourdBloc
{
    public partial class AdminLoginPage : Form
    {
        public EventHandler<EventArgs> AdminLogged;

        public AdminLoginPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string password = tbMdp.Text;

            if (string.IsNullOrEmpty(password))
            {
                lbMdp.Visible = true;
                lbMdp.Text = "Mot de passe vide";
                return;
            }

            bool correctPassword = APIRequest.TryLogin(password);

            if (correctPassword)
            {
                AdminLogged(this, null);
                Close();
            }
            else
            {
                lbMdp.Visible = true;
                lbMdp.Text = "Mot de passe incorrect";
            }
        }
    }
}
