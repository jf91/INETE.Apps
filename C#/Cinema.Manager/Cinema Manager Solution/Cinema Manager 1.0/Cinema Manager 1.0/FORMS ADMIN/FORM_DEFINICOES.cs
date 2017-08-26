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
    public partial class FORM_DEFINICOES : Form
    {
        public FORM_DEFINICOES()
        {
            InitializeComponent();
        }

        private void MENUSTRIP_DEFINICOES_BUTTON_LIGACAO_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.connection_request_admin = true;

        }
    }
}
