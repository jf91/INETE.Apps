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
    public partial class FORM_ADMIN : Form
    {
        public FORM_ADMIN()
        {
            InitializeComponent();
        }

        private void MENUSTRIP_FORM_ADMIN_BUTTON_FUNCIONARIOS_Click(object sender, EventArgs e)
        {

        }

        private void PICTUREBOX_FormAdmin_FUNCIONARIOS_Click(object sender, EventArgs e)
        {
            
        }

        private void LABEL_FormAdmin_FUNCIONARIOS_Click(object sender, EventArgs e)
        {

        }

        private void PICTUREBOX_ADMIN_FUNCIONARIOS_Click(object sender, EventArgs e)
        {
            if (SPLITCONTAINER_ADMIN.Panel2.Controls.Count == 0)
            {
                SPLITCONTAINER_ADMIN.Panel2.Controls.Clear();

                FORMS_ADMIN.FORM_ADMIN_FUNCIONARIOS FormAdminFuncionarios = new FORMS_ADMIN.FORM_ADMIN_FUNCIONARIOS();
                FormAdminFuncionarios.TopLevel = false;
                FormAdminFuncionarios.AutoScroll = true;

                SPLITCONTAINER_ADMIN.Panel2.Controls.Add(FormAdminFuncionarios);

                FormAdminFuncionarios.Visible = true;
                return;
            }
       
            if(SPLITCONTAINER_ADMIN.Panel2.Controls.Count == 1 || SPLITCONTAINER_ADMIN.Panel2.Controls.Count > 1)
            {
                SPLITCONTAINER_ADMIN.Panel2.Controls.Clear();

                FORMS_ADMIN.FORM_ADMIN_FUNCIONARIOS FormAdminFuncionarios = new FORMS_ADMIN.FORM_ADMIN_FUNCIONARIOS();
                FormAdminFuncionarios.TopLevel = false;
                FormAdminFuncionarios.AutoScroll = true;

                SPLITCONTAINER_ADMIN.Panel2.Controls.Add(FormAdminFuncionarios);

                FormAdminFuncionarios.Visible = true;
                return;
            }
        }

        private void PICTUREBOX_ADMIN_SALAS_Click(object sender, EventArgs e)
        {
            if (SPLITCONTAINER_ADMIN.Panel2.Controls.Count == 0)
            {
                SPLITCONTAINER_ADMIN.Panel2.Controls.Clear();

                FORMS_ADMIN.FORM_ADMIN_SALAS FormAdminSalas = new FORMS_ADMIN.FORM_ADMIN_SALAS();
                FormAdminSalas.TopLevel = false;
                FormAdminSalas.AutoScroll = true;

                SPLITCONTAINER_ADMIN.Panel2.Controls.Add(FormAdminSalas);

                FormAdminSalas.Visible = true;
                return;
            }

            if (SPLITCONTAINER_ADMIN.Panel2.Controls.Count == 1 || SPLITCONTAINER_ADMIN.Panel2.Controls.Count > 1)
            {
                SPLITCONTAINER_ADMIN.Panel2.Controls.Clear();

                FORMS_ADMIN.FORM_ADMIN_SALAS FormAdminSalas = new FORMS_ADMIN.FORM_ADMIN_SALAS();
                FormAdminSalas.TopLevel = false;
                FormAdminSalas.AutoScroll = true;

                SPLITCONTAINER_ADMIN.Panel2.Controls.Add(FormAdminSalas);

                FormAdminSalas.Visible = true;
                return;
            }
        }

        private void PICTUREBOX_ADMIN_CONTABILIDADE_Click(object sender, EventArgs e)
        {
            if (SPLITCONTAINER_ADMIN.Panel2.Controls.Count == 0)
            {
                SPLITCONTAINER_ADMIN.Panel2.Controls.Clear();

                FORMS_ADMIN.FORM_ADMIN_CONTABILIDADE FormAdminContabilidade = new FORMS_ADMIN.FORM_ADMIN_CONTABILIDADE();
                FormAdminContabilidade.TopLevel = false;
                FormAdminContabilidade.AutoScroll = true;

                SPLITCONTAINER_ADMIN.Panel2.Controls.Add(FormAdminContabilidade);

                FormAdminContabilidade.Visible = true;
                return;
            }

            if (SPLITCONTAINER_ADMIN.Panel2.Controls.Count == 1 || SPLITCONTAINER_ADMIN.Panel2.Controls.Count > 1)
            {
                SPLITCONTAINER_ADMIN.Panel2.Controls.Clear();

                FORMS_ADMIN.FORM_ADMIN_CONTABILIDADE FormAdminContabilidade = new FORMS_ADMIN.FORM_ADMIN_CONTABILIDADE();
                FormAdminContabilidade.TopLevel = false;
                FormAdminContabilidade.AutoScroll = true;

                SPLITCONTAINER_ADMIN.Panel2.Controls.Add(FormAdminContabilidade);

                FormAdminContabilidade.Visible = true;
                return;
            }
        }

        private void MENUSTRIP_FORM_ADMIN_BUTTON_FECHAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
