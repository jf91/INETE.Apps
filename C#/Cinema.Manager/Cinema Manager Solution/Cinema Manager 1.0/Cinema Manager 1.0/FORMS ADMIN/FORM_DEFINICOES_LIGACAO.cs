using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cinema_Manager_1._0.FORMS_ADMIN
{
    public partial class FORM_DEFINICOES_LIGACAO : Form
    {
        public FORM_DEFINICOES_LIGACAO()
        {
            InitializeComponent();
        }

        private void FORM_DEFINICOES_LIGACAO_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(Properties.Settings.Default.connection_request_login == true)
            {
                Properties.Settings.Default.connection_request_login = false;
                this.Close();
                FORM_LOGIN FormLogin = new FORM_LOGIN();
                FormLogin.Visible = true;
            }

            if(Properties.Settings.Default.connection_request_admin == true)
            {
                Properties.Settings.Default.connection_request_admin = false;
                this.Close();
            }
        }
    }
}
