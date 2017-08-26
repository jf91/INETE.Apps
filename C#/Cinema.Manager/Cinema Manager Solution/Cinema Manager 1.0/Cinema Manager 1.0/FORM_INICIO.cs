using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cinema_Manager_1._0
{
    public partial class FORM_INICIO : Form
    {
        public FORM_INICIO()
        {               
            InitializeComponent();

            if (Properties.Settings.Default.permissoes < 3)
            {
                foreach (ToolStripMenuItem BTN in MENUSTRIP_FORM_INICIO.Items)
                {
                    if (BTN.Text == "Administração")
                        BTN.Visible = false;
                }
            }

            if (Properties.Settings.Default.permissoes == 2)
            {
                foreach (ToolStripMenuItem BTN in MENUSTRIP_FORM_INICIO.Items)
                {
                    if (BTN.Text == "Bilheteira")
                        BTN.Visible = false;
                }
            } 

            if (Properties.Settings.Default.permissoes == 1)
            {
                foreach (ToolStripMenuItem BTN in MENUSTRIP_FORM_INICIO.Items)
                {
                    if (BTN.Text == "Gestão")
                        BTN.Visible = false;
                }
            } 

            foreach (Control cntrl in this.Controls)
            {
                if (cntrl is MdiClient)
                {
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                    cntrl.BackgroundImage = Properties.Resources.Cinema_Background;
                    //cntrl.BackgroundImage = System.Drawing.Image.FromFile(@"C:\Desert.jpg");
                }
            }
        }

        private void MENUSTRIP_BUTTON_ADMIN_Click(object sender, EventArgs e)
        {
            //FORM_ADMIN FormAdmin = new FORM_ADMIN();
            //FormAdmin.MdiParent = this;
            //if(FormAdmin != null)
            //    FormAdmin.Show();

            FORM_ADMIN FormAberto = new FORM_ADMIN();
            Verificar_FormsAbertos(FormAberto);
        }

        private void FORM_INICIO_FormClosed(object sender, FormClosedEventArgs e)
        {
            FORM_LOGIN FormLogin = new FORM_LOGIN();
            FormLogin.Visible = true;
        }

        private void MENUSTRIP_FORM_INICIO_BUTTON_FECHAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void OcultarAdmin()
        {
            MENUSTRIP_BUTTON_ADMIN.Visible = false;
        }

        public void Verificar_FormsAbertos(Form FormAberto)
        {
            foreach (Form FRM in this.MdiChildren)
            {
                if (FRM.GetType() == FormAberto.GetType())
                {
                    FRM.Focus();
                    return;
                }
            }
            FormAberto.MdiParent = this;
            FormAberto.Show();
        }

        private void MENUSTRIP_BUTTON_GESTAO_Click(object sender, EventArgs e)
        {
            FORMS_GESTAO.FORM_GESTAO FormAberto = new FORMS_GESTAO.FORM_GESTAO();
            Verificar_FormsAbertos(FormAberto);
        }
    }
}
