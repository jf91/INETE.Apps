using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cinema_Manager_1._0.FORMS_GESTAO
{
    public partial class FORM_GESTAO : Form
    {
        public FORM_GESTAO()
        {
            InitializeComponent();
        }

        private void PICTUREBOX_GESTAO_SESSOES_Click(object sender, EventArgs e)
        {
            if (SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 0)
            {
                SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                FORMS_GESTAO.FORM_GESTAO_SESSOES FormGestaoSessoes = new FORM_GESTAO_SESSOES();
                FormGestaoSessoes.TopLevel = false;
                FormGestaoSessoes.AutoScroll = true;

                SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoSessoes);

                FormGestaoSessoes.Visible = true;
                return;
            }

            if (SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 1 || SPLITCONTAINER_GESTAO.Panel2.Controls.Count > 1)
            {
                SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                FORMS_GESTAO.FORM_GESTAO_SESSOES FormGestaoSessoes = new FORM_GESTAO_SESSOES();
                FormGestaoSessoes.TopLevel = false;
                FormGestaoSessoes.AutoScroll = true;

                SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoSessoes);

                FormGestaoSessoes.Visible = true;
                return;
            }
        }

        private void PICTUREBOX_GESTAO_FILMES_Click(object sender, EventArgs e)
        {
            if (SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 0)
            {
                SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                FORMS_GESTAO.FORM_GESTAO_FILMES FormGestaoFilmes = new FORM_GESTAO_FILMES();
                FormGestaoFilmes.TopLevel = false;
                FormGestaoFilmes.AutoScroll = true;

                SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoFilmes);

                FormGestaoFilmes.Visible = true;
                return;
            }

            if (SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 1 || SPLITCONTAINER_GESTAO.Panel2.Controls.Count > 1)
            {
                SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                FORMS_GESTAO.FORM_GESTAO_FILMES FormGestaoFilmes = new FORM_GESTAO_FILMES();
                FormGestaoFilmes.TopLevel = false;
                FormGestaoFilmes.AutoScroll = true;

                SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoFilmes);

                FormGestaoFilmes.Visible = true;
                return;
            }
        }

        private void PICTUREBOX_GESTAO_PESSOAS_Click(object sender, EventArgs e)
        {
            if (SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 0)
            {
                SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                FORMS_GESTAO.FORM_GESTAO_PESSOAS FormGestaoPessoas = new FORM_GESTAO_PESSOAS();
                FormGestaoPessoas.TopLevel = false;
                FormGestaoPessoas.AutoScroll = true;

                SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoPessoas);

                FormGestaoPessoas.Visible = true;
                return;
            }

            if (SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 1 || SPLITCONTAINER_GESTAO.Panel2.Controls.Count > 1)
            {
                SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                FORMS_GESTAO.FORM_GESTAO_PESSOAS FormGestaoPessoas = new FORM_GESTAO_PESSOAS();
                FormGestaoPessoas.TopLevel = false;
                FormGestaoPessoas.AutoScroll = true;

                SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoPessoas);

                FormGestaoPessoas.Visible = true;
                return;
            }
        }        
    }
}
