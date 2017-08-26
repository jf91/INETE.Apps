using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Cinema_Manager_1._0.FORMS_GESTAO
{
    public partial class FORM_GESTAO_FILMES_RESUMOS : Form
    {
        FORM_GESTAO FormGestao = (FORM_GESTAO)Application.OpenForms["FORM_GESTAO"];
        FORM_GESTAO_FILMES FormGestaoFilmes = (FORM_GESTAO_FILMES)Application.OpenForms["FORM_GESTAO_FILMES"];

        MySqlConnection LigacaoDB = new MySqlConnection(CLASS_GLOBAL_VARIABLES.Servidor);
        MySqlConnection LigacaoDB_StoredProcedures = new MySqlConnection(CLASS_GLOBAL_VARIABLES.ServidorStoredProcedure);

        MySqlDataAdapter Adapter = new MySqlDataAdapter();

        MySqlDataReader Reader;
        MySqlDataReader Reader2;

        DataTable TabelaDados = new DataTable();
        DataSet DS = new DataSet();

        private int UltimaColunaOrdenada = 0;
        private SortOrder UltimaOrdem = SortOrder.Ascending; 

        public FORM_GESTAO_FILMES_RESUMOS()
        {
            InitializeComponent();

            Preencher_ListView_Resumos();
            Preencher_ListView_Filmes();
        }

        private void PICTUREBOX_GestaoResumos_RETROCEDER_Click(object sender, EventArgs e)
        {
            if(CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource == "GestaoFilmes")
            {
                try
                {
                    if (FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 0)
                    {
                        FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                        FORMS_GESTAO.FORM_GESTAO_FILMES FormGestaoFilmes = new FORM_GESTAO_FILMES();
                        FormGestaoFilmes.TopLevel = false;
                        FormGestaoFilmes.AutoScroll = true;

                        FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoFilmes);

                        FormGestaoFilmes.Visible = true;

                        FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.Focus();
                        FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.Items[CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID].Selected = true;
                        FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.EnsureVisible(CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID);
                        FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.Items[CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID].Focused = true;                        

                        CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource = "";

                        return;
                    }

                    if (FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 1 || FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count > 1)
                    {
                        FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                        FORMS_GESTAO.FORM_GESTAO_FILMES FormGestaoFilmes = new FORM_GESTAO_FILMES();
                        FormGestaoFilmes.TopLevel = false;
                        FormGestaoFilmes.AutoScroll = true;

                        FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoFilmes);

                        FormGestaoFilmes.Visible = true;

                        FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.Focus();
                        FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.Items[CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID].Selected = true;
                        FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.EnsureVisible(CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID);
                        FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.Items[CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID].Focused = true;                        

                        CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource = "";

                        return;
                    }
                }

                catch (Exception EX)
                { }
            }

            if (CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource == "AdicionarFilme")
            {
                try
                {
                    if (FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 0)
                    {
                        FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                        FORMS_GESTAO.FORM_GESTAO_FILMES FormGestaoFilmes = new FORM_GESTAO_FILMES();
                        FormGestaoFilmes.TopLevel = false;
                        FormGestaoFilmes.AutoScroll = true;

                        FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoFilmes);

                        FormGestaoFilmes.Visible = true;

                        FormGestaoFilmes.TABCONTROL_GESTAO_FILMES.SelectedIndex = 1;

                        FormGestaoFilmes.TEXTBOX_AdicionarFilme_TITULO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Titulo;
                        FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Duracao;
                        FormGestaoFilmes.TEXTBOX_AdicionarFilme_NOME_RESUMO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Resumo;
                        FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO_RESUMO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_DuracaoResumo;

                        if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Por Exibir")
                            FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_POR_EXIBIR.Checked = true;
                        if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Em Exibicao")
                            FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_EM_EXIBICAO.Checked = true;
                        if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Ja Exibido")
                            FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_JA_EXIBIDO.Checked = true;

                        CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource = "";

                        return;
                    }

                    if (FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 1 || FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count > 1)
                    {
                        FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                        FORMS_GESTAO.FORM_GESTAO_FILMES FormGestaoFilmes = new FORM_GESTAO_FILMES();
                        FormGestaoFilmes.TopLevel = false;
                        FormGestaoFilmes.AutoScroll = true;

                        FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoFilmes);

                        FormGestaoFilmes.Visible = true;

                        FormGestaoFilmes.TABCONTROL_GESTAO_FILMES.SelectedIndex = 1;

                        FormGestaoFilmes.TEXTBOX_AdicionarFilme_TITULO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Titulo;
                        FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Duracao;
                        FormGestaoFilmes.TEXTBOX_AdicionarFilme_NOME_RESUMO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Resumo;
                        FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO_RESUMO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_DuracaoResumo;

                        if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Por Exibir")
                            FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_POR_EXIBIR.Checked = true;
                        if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Em Exibicao")
                            FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_EM_EXIBICAO.Checked = true;
                        if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Ja Exibido")
                            FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_JA_EXIBIDO.Checked = true;

                        CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource = "";

                        return;
                    }
                }

                catch (Exception EX)
                { }
            }
        }

        private void BUTTON_GestaoResumos_SELECIONAR_Click(object sender, EventArgs e)
        {
            string ConfirmarExistencia = TEXTBOX_GestaoResumos_RESUMO.Text;
            string ConfirmarDuracao = TEXTBOX_GestaoResumos_DURACAO.Text;
            bool Existe = false;;
            bool ResumoInserido = false;
            bool EfectuarCopia = false;

            if (TEXTBOX_GestaoResumos_RESUMO.Text == LISTVIEW_GestaoResumos_RESUMOS.SelectedItems[0].SubItems[1].ToString() && TEXTBOX_GestaoResumos_DURACAO.Text == LISTVIEW_GestaoResumos_RESUMOS.SelectedItems[0].SubItems[2].ToString())
            {
                if (CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource == "GestaoFilmes")
                {
                    try
                    {
                        if (FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 0)
                        {
                            FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                            FORMS_GESTAO.FORM_GESTAO_FILMES FormGestaoFilmes = new FORM_GESTAO_FILMES();
                            FormGestaoFilmes.TopLevel = false;
                            FormGestaoFilmes.AutoScroll = true;

                            FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoFilmes);

                            FormGestaoFilmes.Visible = true;

                            FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.Focus();
                            FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.Items[CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID].Selected = true;
                            FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.EnsureVisible(CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID);
                            FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.Items[CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID].Focused = true;

                            if (TEXTBOX_GestaoResumos_RESUMO.Text != "" && TEXTBOX_GestaoResumos_DURACAO.Text != "")
                            {
                                FormGestaoFilmes.TEXTBOX_GestaoFilmes_NOME_RESUMO.Text = TEXTBOX_GestaoResumos_RESUMO.Text;
                                FormGestaoFilmes.TEXTBOX_GestaoFilmes_DURACAO_RESUMO.Text = TEXTBOX_GestaoResumos_DURACAO.Text;
                            }

                            if (TEXTBOX_GestaoResumos_RESUMO.Text == "" && TEXTBOX_GestaoResumos_DURACAO.Text == "")
                            {
                                return;
                            }

                            FormGestaoFilmes.TEXTBOX_GestaoFilmes_TITULO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Titulo;
                            FormGestaoFilmes.TEXTBOX_GestaoFilmes_DURACAO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Duracao;

                            if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Por Exibir")
                                FormGestaoFilmes.RADIOBUTTON_GestaoFilmes_POR_EXIBIR.Checked = true;
                            if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Em Exibicao")
                                FormGestaoFilmes.RADIOBUTTON_GestaoFilmes_EM_EXIBICAO.Checked = true;
                            if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Ja Exibido")
                                FormGestaoFilmes.RADIOBUTTON_GestaoFilmes_JA_EXIBIDO.Checked = true;

                            CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource = "";

                            return;
                        }

                        if (FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 1 || FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count > 1)
                        {
                            FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                            FORMS_GESTAO.FORM_GESTAO_FILMES FormGestaoFilmes = new FORM_GESTAO_FILMES();
                            FormGestaoFilmes.TopLevel = false;
                            FormGestaoFilmes.AutoScroll = true;

                            FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoFilmes);

                            FormGestaoFilmes.Visible = true;

                            FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.Focus();
                            FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.Items[CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID].Selected = true;
                            FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.EnsureVisible(CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID);
                            FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.Items[CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID].Focused = true;

                            if (TEXTBOX_GestaoResumos_RESUMO.Text != "" && TEXTBOX_GestaoResumos_DURACAO.Text != "")
                            {
                                FormGestaoFilmes.TEXTBOX_GestaoFilmes_NOME_RESUMO.Text = TEXTBOX_GestaoResumos_RESUMO.Text;
                                FormGestaoFilmes.TEXTBOX_GestaoFilmes_DURACAO_RESUMO.Text = TEXTBOX_GestaoResumos_DURACAO.Text;
                            }

                            if (TEXTBOX_GestaoResumos_RESUMO.Text == "" && TEXTBOX_GestaoResumos_DURACAO.Text == "")
                            {
                                return;
                            }

                            FormGestaoFilmes.TEXTBOX_GestaoFilmes_TITULO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Titulo;
                            FormGestaoFilmes.TEXTBOX_GestaoFilmes_DURACAO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Duracao;

                            CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource = "";

                            return;
                        }
                    }

                    catch (Exception EX)
                    { }
                }

                if (CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource == "AdicionarFilme")
                {
                    try
                    {
                        if (FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 0)
                        {
                            FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                            FORMS_GESTAO.FORM_GESTAO_FILMES FormGestaoFilmes = new FORM_GESTAO_FILMES();
                            FormGestaoFilmes.TopLevel = false;
                            FormGestaoFilmes.AutoScroll = true;

                            FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoFilmes);

                            FormGestaoFilmes.Visible = true;

                            FormGestaoFilmes.TABCONTROL_GESTAO_FILMES.SelectedIndex = 1;

                            if (TEXTBOX_GestaoResumos_RESUMO.Text != "" && TEXTBOX_GestaoResumos_DURACAO.Text != "")
                            {
                                FormGestaoFilmes.TEXTBOX_AdicionarFilme_TITULO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Titulo;
                                FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Duracao;
                                FormGestaoFilmes.TEXTBOX_AdicionarFilme_NOME_RESUMO.Text = TEXTBOX_GestaoResumos_RESUMO.Text;
                                FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO_RESUMO.Text = TEXTBOX_GestaoResumos_DURACAO.Text;

                                if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Por Exibir")
                                    FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_POR_EXIBIR.Checked = true;
                                if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Em Exibicao")
                                    FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_EM_EXIBICAO.Checked = true;
                                if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Ja Exibido")
                                    FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_JA_EXIBIDO.Checked = true;
                            }

                            if (TEXTBOX_GestaoResumos_RESUMO.Text == "" && TEXTBOX_GestaoResumos_DURACAO.Text == "")
                            {
                                FormGestaoFilmes.TEXTBOX_AdicionarFilme_TITULO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Titulo;
                                FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Duracao;
                                FormGestaoFilmes.TEXTBOX_AdicionarFilme_NOME_RESUMO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Resumo;
                                FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO_RESUMO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_DuracaoResumo;

                                if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Por Exibir")
                                    FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_POR_EXIBIR.Checked = true;
                                if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Em Exibicao")
                                    FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_EM_EXIBICAO.Checked = true;
                                if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Ja Exibido")
                                    FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_JA_EXIBIDO.Checked = true;
                            }

                            CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource = "";

                            return;
                        }

                        if (FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 1 || FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count > 1)
                        {
                            FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                            FORMS_GESTAO.FORM_GESTAO_FILMES FormGestaoFilmes = new FORM_GESTAO_FILMES();
                            FormGestaoFilmes.TopLevel = false;
                            FormGestaoFilmes.AutoScroll = true;

                            FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoFilmes);

                            FormGestaoFilmes.Visible = true;

                            FormGestaoFilmes.TABCONTROL_GESTAO_FILMES.SelectedIndex = 1;

                            if (TEXTBOX_GestaoResumos_RESUMO.Text != "" && TEXTBOX_GestaoResumos_DURACAO.Text != "")
                            {
                                FormGestaoFilmes.TEXTBOX_AdicionarFilme_TITULO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Titulo;
                                FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Duracao;
                                FormGestaoFilmes.TEXTBOX_AdicionarFilme_NOME_RESUMO.Text = TEXTBOX_GestaoResumos_RESUMO.Text;
                                FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO_RESUMO.Text = TEXTBOX_GestaoResumos_DURACAO.Text;

                                if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Por Exibir")
                                    FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_POR_EXIBIR.Checked = true;
                                if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Em Exibicao")
                                    FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_EM_EXIBICAO.Checked = true;
                                if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Ja Exibido")
                                    FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_JA_EXIBIDO.Checked = true;
                            }

                            if (TEXTBOX_GestaoResumos_RESUMO.Text == "" && TEXTBOX_GestaoResumos_DURACAO.Text == "")
                            {
                                FormGestaoFilmes.TEXTBOX_AdicionarFilme_TITULO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Titulo;
                                FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Duracao;
                                FormGestaoFilmes.TEXTBOX_AdicionarFilme_NOME_RESUMO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Resumo;
                                FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO_RESUMO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_DuracaoResumo;

                                if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Por Exibir")
                                    FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_POR_EXIBIR.Checked = true;
                                if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Em Exibicao")
                                    FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_EM_EXIBICAO.Checked = true;
                                if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Ja Exibido")
                                    FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_JA_EXIBIDO.Checked = true;
                            }

                            CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource = "";

                            return;
                        }
                    }

                    catch (Exception EX)
                    { }
                }

                return;
            }

            if (TEXTBOX_GestaoResumos_RESUMO.Text != LISTVIEW_GestaoResumos_RESUMOS.SelectedItems[0].SubItems[1].ToString() || TEXTBOX_GestaoResumos_DURACAO.Text != LISTVIEW_GestaoResumos_RESUMOS.SelectedItems[0].SubItems[2].ToString())
            {
                foreach (ListViewItem ItemExiste in LISTVIEW_GestaoResumos_RESUMOS.Items)
                {
                    for (int i = 0; i < ItemExiste.SubItems.Count; i++)
                    {
                        if (ItemExiste.SubItems[i].Text.ToLower() == ConfirmarExistencia.ToLower() && ItemExiste.SubItems[2].Text == ConfirmarDuracao)
                        {
                            Existe = true;
                        }                            
                    }
                }

                if (Existe == true)
                {
                    if (CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource == "GestaoFilmes")
                    {
                        try
                        {
                            if (FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 0)
                            {
                                FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                                FORMS_GESTAO.FORM_GESTAO_FILMES FormGestaoFilmes = new FORM_GESTAO_FILMES();
                                FormGestaoFilmes.TopLevel = false;
                                FormGestaoFilmes.AutoScroll = true;

                                FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoFilmes);

                                FormGestaoFilmes.Visible = true;

                                FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.Focus();
                                FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.Items[CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID].Selected = true;
                                FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.EnsureVisible(CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID);
                                FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.Items[CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID].Focused = true;

                                if (TEXTBOX_GestaoResumos_RESUMO.Text != "" && TEXTBOX_GestaoResumos_DURACAO.Text != "")
                                {
                                    FormGestaoFilmes.TEXTBOX_GestaoFilmes_NOME_RESUMO.Text = TEXTBOX_GestaoResumos_RESUMO.Text;
                                    FormGestaoFilmes.TEXTBOX_GestaoFilmes_DURACAO_RESUMO.Text = TEXTBOX_GestaoResumos_DURACAO.Text;
                                }

                                if (TEXTBOX_GestaoResumos_RESUMO.Text == "" && TEXTBOX_GestaoResumos_DURACAO.Text == "")
                                {
                                    return;
                                }

                                FormGestaoFilmes.TEXTBOX_GestaoFilmes_TITULO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Titulo;
                                FormGestaoFilmes.TEXTBOX_GestaoFilmes_DURACAO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Duracao;

                                if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Por Exibir")
                                    FormGestaoFilmes.RADIOBUTTON_GestaoFilmes_POR_EXIBIR.Checked = true;
                                if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Em Exibicao")
                                    FormGestaoFilmes.RADIOBUTTON_GestaoFilmes_EM_EXIBICAO.Checked = true;
                                if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Ja Exibido")
                                    FormGestaoFilmes.RADIOBUTTON_GestaoFilmes_JA_EXIBIDO.Checked = true;

                                CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource = "";

                                return;
                            }

                            if (FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 1 || FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count > 1)
                            {
                                FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                                FORMS_GESTAO.FORM_GESTAO_FILMES FormGestaoFilmes = new FORM_GESTAO_FILMES();
                                FormGestaoFilmes.TopLevel = false;
                                FormGestaoFilmes.AutoScroll = true;

                                FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoFilmes);

                                FormGestaoFilmes.Visible = true;

                                FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.Focus();
                                FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.Items[CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID].Selected = true;
                                FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.EnsureVisible(CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID);
                                FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.Items[CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID].Focused = true;

                                if (TEXTBOX_GestaoResumos_RESUMO.Text != "" && TEXTBOX_GestaoResumos_DURACAO.Text != "")
                                {
                                    FormGestaoFilmes.TEXTBOX_GestaoFilmes_NOME_RESUMO.Text = TEXTBOX_GestaoResumos_RESUMO.Text;
                                    FormGestaoFilmes.TEXTBOX_GestaoFilmes_DURACAO_RESUMO.Text = TEXTBOX_GestaoResumos_DURACAO.Text;
                                }

                                if (TEXTBOX_GestaoResumos_RESUMO.Text == "" && TEXTBOX_GestaoResumos_DURACAO.Text == "")
                                {
                                    return;
                                }

                                FormGestaoFilmes.TEXTBOX_GestaoFilmes_TITULO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Titulo;
                                FormGestaoFilmes.TEXTBOX_GestaoFilmes_DURACAO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Duracao;

                                if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Por Exibir")
                                    FormGestaoFilmes.RADIOBUTTON_GestaoFilmes_POR_EXIBIR.Checked = true;
                                if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Em Exibicao")
                                    FormGestaoFilmes.RADIOBUTTON_GestaoFilmes_EM_EXIBICAO.Checked = true;
                                if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Ja Exibido")
                                    FormGestaoFilmes.RADIOBUTTON_GestaoFilmes_JA_EXIBIDO.Checked = true;

                                CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource = "";

                                return;
                            }
                        }

                        catch (Exception EX)
                        { }
                    }

                    if (CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource == "AdicionarFilme")
                    {
                        try
                        {
                            if (FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 0)
                            {
                                FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                                FORMS_GESTAO.FORM_GESTAO_FILMES FormGestaoFilmes = new FORM_GESTAO_FILMES();
                                FormGestaoFilmes.TopLevel = false;
                                FormGestaoFilmes.AutoScroll = true;

                                FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoFilmes);

                                FormGestaoFilmes.Visible = true;

                                FormGestaoFilmes.TABCONTROL_GESTAO_FILMES.SelectedIndex = 1;

                                if (TEXTBOX_GestaoResumos_RESUMO.Text != "" && TEXTBOX_GestaoResumos_DURACAO.Text != "")
                                {
                                    FormGestaoFilmes.TEXTBOX_AdicionarFilme_TITULO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Titulo;
                                    FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Duracao;
                                    FormGestaoFilmes.TEXTBOX_AdicionarFilme_NOME_RESUMO.Text = TEXTBOX_GestaoResumos_RESUMO.Text;
                                    FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO_RESUMO.Text = TEXTBOX_GestaoResumos_DURACAO.Text;

                                    if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Por Exibir")
                                        FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_POR_EXIBIR.Checked = true;
                                    if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Em Exibicao")
                                        FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_EM_EXIBICAO.Checked = true;
                                    if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Ja Exibido")
                                        FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_JA_EXIBIDO.Checked = true;
                                }

                                if (TEXTBOX_GestaoResumos_RESUMO.Text == "" && TEXTBOX_GestaoResumos_DURACAO.Text == "")
                                {
                                    FormGestaoFilmes.TEXTBOX_AdicionarFilme_TITULO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Titulo;
                                    FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Duracao;
                                    FormGestaoFilmes.TEXTBOX_AdicionarFilme_NOME_RESUMO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Resumo;
                                    FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO_RESUMO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_DuracaoResumo;

                                    if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Por Exibir")
                                        FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_POR_EXIBIR.Checked = true;
                                    if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Em Exibicao")
                                        FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_EM_EXIBICAO.Checked = true;
                                    if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Ja Exibido")
                                        FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_JA_EXIBIDO.Checked = true;
                                }

                                CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource = "";

                                return;
                            }

                            if (FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 1 || FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count > 1)
                            {
                                FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                                FORMS_GESTAO.FORM_GESTAO_FILMES FormGestaoFilmes = new FORM_GESTAO_FILMES();
                                FormGestaoFilmes.TopLevel = false;
                                FormGestaoFilmes.AutoScroll = true;

                                FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoFilmes);

                                FormGestaoFilmes.Visible = true;

                                FormGestaoFilmes.TABCONTROL_GESTAO_FILMES.SelectedIndex = 1;

                                if (TEXTBOX_GestaoResumos_RESUMO.Text != "" && TEXTBOX_GestaoResumos_DURACAO.Text != "")
                                {
                                    FormGestaoFilmes.TEXTBOX_AdicionarFilme_TITULO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Titulo;
                                    FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Duracao;
                                    FormGestaoFilmes.TEXTBOX_AdicionarFilme_NOME_RESUMO.Text = TEXTBOX_GestaoResumos_RESUMO.Text;
                                    FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO_RESUMO.Text = TEXTBOX_GestaoResumos_DURACAO.Text;

                                    if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Por Exibir")
                                        FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_POR_EXIBIR.Checked = true;
                                    if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Em Exibicao")
                                        FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_EM_EXIBICAO.Checked = true;
                                    if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Ja Exibido")
                                        FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_JA_EXIBIDO.Checked = true;
                                }

                                if (TEXTBOX_GestaoResumos_RESUMO.Text == "" && TEXTBOX_GestaoResumos_DURACAO.Text == "")
                                {
                                    FormGestaoFilmes.TEXTBOX_AdicionarFilme_TITULO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Titulo;
                                    FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Duracao;
                                    FormGestaoFilmes.TEXTBOX_AdicionarFilme_NOME_RESUMO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Resumo;
                                    FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO_RESUMO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_DuracaoResumo;

                                    if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Por Exibir")
                                        FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_POR_EXIBIR.Checked = true;
                                    if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Em Exibicao")
                                        FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_EM_EXIBICAO.Checked = true;
                                    if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Ja Exibido")
                                        FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_JA_EXIBIDO.Checked = true;
                                }

                                CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource = "";

                                return;
                            }
                        }

                        catch (Exception EX)
                        { }
                    }

                    return;
                }

                if (Existe == false)
                {
                    DialogResult NovoResumo = MessageBox.Show("O resumo que indicou ainda não existe. Pretende Adiciona-lo?", "Novo Resumo?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (NovoResumo == DialogResult.Yes)
                    {
                        bool JaExiste = false;

                        foreach (ListViewItem ItemExiste in LISTVIEW_GestaoResumos_RESUMOS.Items)
                        {
                            for (int i = 0; i < ItemExiste.SubItems.Count; i++)
                            {
                                if (ItemExiste.SubItems[i].Text.ToLower() == ConfirmarExistencia.ToLower())
                                {
                                    MessageBox.Show("O resumo que está a tentar inserir já existe. Não é possivel adicionar resumos com o mesmo nome", "Resumo Já Existe", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    JaExiste = true;
                                    return;
                                }
                            }
                        }

                        if (JaExiste == false)
                        {
                            Adicionar_Resumo();
                            ClearListView();
                            Preencher_ListView_Resumos();
                            Preencher_ListView_Filmes();

                            DialogResult Copiar = MessageBox.Show("Resumo Inserido com Sucesso. Deseja continuar a cópia?", "Continuar Cópia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (Copiar == DialogResult.Yes)
                            {
                                EfectuarCopia = true;
                            }
                            if (Copiar == DialogResult.No)
                            {
                                EfectuarCopia = false;
                                LimparTudo();
                            }
                        }                        
                    }

                    if (NovoResumo == DialogResult.No)
                    {
                        MessageBox.Show("Não é possivel copiar um resumo que não tenha sido inserido. Terá de adiciona-lo para que possa copiar.","Impossivel Copiar!",MessageBoxButtons.OK,MessageBoxIcon.Error);

                        return;
                    }                        

                    if(EfectuarCopia == true)
                    {
                        if (CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource == "GestaoFilmes")
                        {
                            try
                            {
                                if (FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 0)
                                {
                                    FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                                    FORMS_GESTAO.FORM_GESTAO_FILMES FormGestaoFilmes = new FORM_GESTAO_FILMES();
                                    FormGestaoFilmes.TopLevel = false;
                                    FormGestaoFilmes.AutoScroll = true;

                                    FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoFilmes);

                                    FormGestaoFilmes.Visible = true;

                                    FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.Focus();
                                    FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.Items[CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID].Selected = true;
                                    FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.EnsureVisible(CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID);
                                    FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.Items[CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID].Focused = true;

                                    if (TEXTBOX_GestaoResumos_RESUMO.Text != "" && TEXTBOX_GestaoResumos_DURACAO.Text != "")
                                    {
                                        FormGestaoFilmes.TEXTBOX_GestaoFilmes_NOME_RESUMO.Text = TEXTBOX_GestaoResumos_RESUMO.Text;
                                        FormGestaoFilmes.TEXTBOX_GestaoFilmes_DURACAO_RESUMO.Text = TEXTBOX_GestaoResumos_DURACAO.Text;
                                    }

                                    if (TEXTBOX_GestaoResumos_RESUMO.Text == "" && TEXTBOX_GestaoResumos_DURACAO.Text == "")
                                    {
                                        return;
                                    }

                                    FormGestaoFilmes.TEXTBOX_GestaoFilmes_TITULO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Titulo;
                                    FormGestaoFilmes.TEXTBOX_GestaoFilmes_DURACAO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Duracao;

                                    if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Por Exibir")
                                        FormGestaoFilmes.RADIOBUTTON_GestaoFilmes_POR_EXIBIR.Checked = true;
                                    if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Em Exibicao")
                                        FormGestaoFilmes.RADIOBUTTON_GestaoFilmes_EM_EXIBICAO.Checked = true;
                                    if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Ja Exibido")
                                        FormGestaoFilmes.RADIOBUTTON_GestaoFilmes_JA_EXIBIDO.Checked = true;

                                    CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource = "";

                                    return;
                                }

                                if (FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 1 || FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count > 1)
                                {
                                    FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                                    FORMS_GESTAO.FORM_GESTAO_FILMES FormGestaoFilmes = new FORM_GESTAO_FILMES();
                                    FormGestaoFilmes.TopLevel = false;
                                    FormGestaoFilmes.AutoScroll = true;

                                    FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoFilmes);

                                    FormGestaoFilmes.Visible = true;

                                    FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.Focus();
                                    FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.Items[CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID].Selected = true;
                                    FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.EnsureVisible(CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID);
                                    FormGestaoFilmes.LISTVIEW_GestaoFilmes_FILMES.Items[CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID].Focused = true;

                                    if (TEXTBOX_GestaoResumos_RESUMO.Text != "" && TEXTBOX_GestaoResumos_DURACAO.Text != "")
                                    {
                                        FormGestaoFilmes.TEXTBOX_GestaoFilmes_NOME_RESUMO.Text = TEXTBOX_GestaoResumos_RESUMO.Text;
                                        FormGestaoFilmes.TEXTBOX_GestaoFilmes_DURACAO_RESUMO.Text = TEXTBOX_GestaoResumos_DURACAO.Text;
                                    }

                                    if (TEXTBOX_GestaoResumos_RESUMO.Text == "" && TEXTBOX_GestaoResumos_DURACAO.Text == "")
                                    {
                                        return;
                                    }

                                    FormGestaoFilmes.TEXTBOX_GestaoFilmes_TITULO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Titulo;
                                    FormGestaoFilmes.TEXTBOX_GestaoFilmes_DURACAO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Duracao;

                                    if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Por Exibir")
                                        FormGestaoFilmes.RADIOBUTTON_GestaoFilmes_POR_EXIBIR.Checked = true;
                                    if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Em Exibicao")
                                        FormGestaoFilmes.RADIOBUTTON_GestaoFilmes_EM_EXIBICAO.Checked = true;
                                    if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Ja Exibido")
                                        FormGestaoFilmes.RADIOBUTTON_GestaoFilmes_JA_EXIBIDO.Checked = true;

                                    CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource = "";

                                    return;
                                }
                            }

                            catch (Exception EX)
                            { }
                        }

                        if (CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource == "AdicionarFilme")
                        {
                            try
                            {
                                if (FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 0)
                                {
                                    FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                                    FORMS_GESTAO.FORM_GESTAO_FILMES FormGestaoFilmes = new FORM_GESTAO_FILMES();
                                    FormGestaoFilmes.TopLevel = false;
                                    FormGestaoFilmes.AutoScroll = true;

                                    FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoFilmes);

                                    FormGestaoFilmes.Visible = true;

                                    FormGestaoFilmes.TABCONTROL_GESTAO_FILMES.SelectedIndex = 1;

                                    if (TEXTBOX_GestaoResumos_RESUMO.Text != "" && TEXTBOX_GestaoResumos_DURACAO.Text != "")
                                    {
                                        FormGestaoFilmes.TEXTBOX_AdicionarFilme_TITULO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Titulo;
                                        FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Duracao;
                                        FormGestaoFilmes.TEXTBOX_AdicionarFilme_NOME_RESUMO.Text = TEXTBOX_GestaoResumos_RESUMO.Text;
                                        FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO_RESUMO.Text = TEXTBOX_GestaoResumos_DURACAO.Text;

                                        if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Por Exibir")
                                            FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_POR_EXIBIR.Checked = true;
                                        if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Em Exibicao")
                                            FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_EM_EXIBICAO.Checked = true;
                                        if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Ja Exibido")
                                            FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_JA_EXIBIDO.Checked = true;
                                    }

                                    if (TEXTBOX_GestaoResumos_RESUMO.Text == "" && TEXTBOX_GestaoResumos_DURACAO.Text == "")
                                    {
                                        FormGestaoFilmes.TEXTBOX_AdicionarFilme_TITULO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Titulo;
                                        FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Duracao;
                                        FormGestaoFilmes.TEXTBOX_AdicionarFilme_NOME_RESUMO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Resumo;
                                        FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO_RESUMO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_DuracaoResumo;

                                        if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Por Exibir")
                                            FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_POR_EXIBIR.Checked = true;
                                        if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Em Exibicao")
                                            FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_EM_EXIBICAO.Checked = true;
                                        if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Ja Exibido")
                                            FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_JA_EXIBIDO.Checked = true;
                                    }

                                    CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource = "";

                                    return;
                                }

                                if (FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 1 || FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count > 1)
                                {
                                    FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                                    FORMS_GESTAO.FORM_GESTAO_FILMES FormGestaoFilmes = new FORM_GESTAO_FILMES();
                                    FormGestaoFilmes.TopLevel = false;
                                    FormGestaoFilmes.AutoScroll = true;

                                    FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoFilmes);

                                    FormGestaoFilmes.Visible = true;

                                    FormGestaoFilmes.TABCONTROL_GESTAO_FILMES.SelectedIndex = 1;

                                    if (TEXTBOX_GestaoResumos_RESUMO.Text != "" && TEXTBOX_GestaoResumos_DURACAO.Text != "")
                                    {
                                        FormGestaoFilmes.TEXTBOX_AdicionarFilme_TITULO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Titulo;
                                        FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Duracao;
                                        FormGestaoFilmes.TEXTBOX_AdicionarFilme_NOME_RESUMO.Text = TEXTBOX_GestaoResumos_RESUMO.Text;
                                        FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO_RESUMO.Text = TEXTBOX_GestaoResumos_DURACAO.Text;

                                        if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Por Exibir")
                                            FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_POR_EXIBIR.Checked = true;
                                        if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Em Exibicao")
                                            FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_EM_EXIBICAO.Checked = true;
                                        if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Ja Exibido")
                                            FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_JA_EXIBIDO.Checked = true;
                                    }

                                    if (TEXTBOX_GestaoResumos_RESUMO.Text == "" && TEXTBOX_GestaoResumos_DURACAO.Text == "")
                                    {
                                        FormGestaoFilmes.TEXTBOX_AdicionarFilme_TITULO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Titulo;
                                        FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Duracao;
                                        FormGestaoFilmes.TEXTBOX_AdicionarFilme_NOME_RESUMO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Resumo;
                                        FormGestaoFilmes.TEXTBOX_AdicionarFilme_DURACAO_RESUMO.Text = CLASS_GLOBAL_VARIABLES.AUX_RESUMO_DuracaoResumo;

                                        if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Por Exibir")
                                            FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_POR_EXIBIR.Checked = true;
                                        if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Em Exibicao")
                                            FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_EM_EXIBICAO.Checked = true;
                                        if (CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado == "Ja Exibido")
                                            FormGestaoFilmes.RADIOBUTTON_AdicionarFilme_JA_EXIBIDO.Checked = true;
                                    }

                                    CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource = "";

                                    return;
                                }
                            }

                            catch (Exception EX)
                            { }
                        }
                    }
                }                
            }
        }

        private void TEXTBOX_GestaoResumos_RESUMO_TextChanged(object sender, EventArgs e)
        {
            if (TEXTBOX_GestaoResumos_RESUMO.Text != "" && TEXTBOX_GestaoResumos_DURACAO.Text != "")
                BUTTON_GestaoResumos_SELECIONAR.Enabled = true;

            if (TEXTBOX_GestaoResumos_RESUMO.Text == "")
                BUTTON_GestaoResumos_SELECIONAR.Enabled = false;

            if (LISTVIEW_GestaoResumos_RESUMOS.SelectedItems == null)
                BUTTON_GestaoResumos_SELECIONAR.Enabled = false;
        }

        private void TEXTBOX_GestaoResumos_DURACAO_TextChanged(object sender, EventArgs e)
        {
            if (TEXTBOX_GestaoResumos_RESUMO.Text != "" && TEXTBOX_GestaoResumos_DURACAO.Text != "")
                BUTTON_GestaoResumos_SELECIONAR.Enabled = true;

            if (TEXTBOX_GestaoResumos_DURACAO.Text == "")
                BUTTON_GestaoResumos_SELECIONAR.Enabled = false;

            if (LISTVIEW_GestaoResumos_RESUMOS.SelectedItems == null)
                BUTTON_GestaoResumos_SELECIONAR.Enabled = false;
        }

        private void LISTVIEW_GestaoResumos_RESUMOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LISTVIEW_GestaoResumos_RESUMOS.SelectedItems == null)
                BUTTON_GestaoResumos_SELECIONAR.Enabled = false;

            Preencher_TextBox_Resumo();
        }

        public void Preencher_ListView_Resumos()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB.Open();

                string Query = "SELECT ID,nome,duracao FROM " + CLASS_GLOBAL_VARIABLES.BD + ".resumos_filmes";
                MySqlCommand Comando = new MySqlCommand(Query, LigacaoDB);

                Reader = Comando.ExecuteReader();

                while (Reader.Read())
                {
                    var ROW = new ListViewItem();
                    ROW.Text = Reader["ID"].ToString();
                    ROW.SubItems.Add(Reader["nome"].ToString());
                    ROW.SubItems.Add(Reader["duracao"].ToString());
                    LISTVIEW_GestaoResumos_RESUMOS.Items.Add(ROW);
                }

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                //MessageBox.Show(EX.Message);
            }
        }

        public void Preencher_ListView_Filmes()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB_StoredProcedures.Open();

                MySqlCommand Command = new MySqlCommand("SP_FILMES_SELECIONAR", LigacaoDB_StoredProcedures);
                Command.CommandType = CommandType.StoredProcedure;

                Reader = Command.ExecuteReader();

                while (Reader.Read())
                {
                    var ROW = new ListViewItem();
                    ROW.Text = Reader["ID"].ToString();
                    ROW.SubItems.Add(Reader["titulo"].ToString());
                    ROW.SubItems.Add(Reader["duracao"].ToString());
                    ROW.SubItems.Add(Reader["nome"].ToString());
                    ROW.SubItems.Add(Reader["duracao_resumo"].ToString());
                    ROW.SubItems.Add(Reader["estado"].ToString());

                    LISTVIEW_GestaoResumos_FILMES.Items.Add(ROW);
                }

                LigacaoDB_StoredProcedures.Close();
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        public void Preencher_TextBox_Resumo()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                string ListView_ID = LISTVIEW_GestaoResumos_RESUMOS.SelectedItems[0].Text;
                int ID = Convert.ToInt32(ListView_ID);

                LigacaoDB.Open();

                string Resumo;
                string Duracao;

                string Query_Resumo = "SELECT nome FROM " + CLASS_GLOBAL_VARIABLES.BD + ".resumos_filmes WHERE ID = " + ID + "";
                string Query_Duracao = "SELECT duracao FROM " + CLASS_GLOBAL_VARIABLES.BD + ".resumos_filmes WHERE ID = " + ID + "";

                MySqlCommand Comando_Resumo = new MySqlCommand(Query_Resumo, LigacaoDB);
                MySqlCommand Comando_Duracao = new MySqlCommand(Query_Duracao, LigacaoDB);

                Resumo = Comando_Resumo.ExecuteScalar().ToString();
                Duracao = Comando_Duracao.ExecuteScalar().ToString();

                TEXTBOX_GestaoResumos_RESUMO.Text = Resumo;
                TEXTBOX_GestaoResumos_DURACAO.Text = Duracao;

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                //MessageBox.Show(EX.Message);
                LISTVIEW_GestaoResumos_RESUMOS.SelectedItems.Clear();
                TEXTBOX_GestaoResumos_RESUMO.Text = "";
                TEXTBOX_GestaoResumos_DURACAO.Text = "";
            }
        }

        public void ClearListView()
        {
            LISTVIEW_GestaoResumos_RESUMOS.Items.Clear();
            LISTVIEW_GestaoResumos_FILMES.Items.Clear();
        }

        public void LimparTudo()
        {
            TEXTBOX_GestaoResumos_RESUMO.Text = "";
            TEXTBOX_GestaoResumos_DURACAO.Text = "";
        }

        public void Adicionar_Resumo()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB_StoredProcedures.Open();

                string NomeResumo = TEXTBOX_GestaoResumos_RESUMO.Text;
                string DuracaoResumo = TEXTBOX_GestaoResumos_DURACAO.Text;

                MySqlCommand Comando_InserirResumo = new MySqlCommand("SP_RESUMO_INSERIR", LigacaoDB_StoredProcedures);
                Comando_InserirResumo.CommandType = CommandType.StoredProcedure;

                Comando_InserirResumo.Parameters.AddWithValue("_nome", NomeResumo);
                Comando_InserirResumo.Parameters.AddWithValue("_duracao", DuracaoResumo);

                Reader = Comando_InserirResumo.ExecuteReader();

                Reader.Close();

                LigacaoDB_StoredProcedures.Close();

                MessageBox.Show("Resumo inserido com Sucesso", "Resumo Inserido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        public void Remover_Resumo()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB_StoredProcedures.Open();

                string ListView_ID = LISTVIEW_GestaoResumos_RESUMOS.SelectedItems[0].Text;
                int ID = Convert.ToInt32(ListView_ID);

                MySqlCommand Command_RemoverResumo = new MySqlCommand("SP_RESUMO_REMOVER", LigacaoDB_StoredProcedures);
                Command_RemoverResumo.CommandType = CommandType.StoredProcedure;

                Command_RemoverResumo.Parameters.AddWithValue("_id", ID);
                Reader = Command_RemoverResumo.ExecuteReader();

                LigacaoDB_StoredProcedures.Close();

                MessageBox.Show("Resumo removido com sucesso", "Resumo Removido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception EX)
            { }
        }

        public void Alterar_Resumo()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB_StoredProcedures.Open();

                string ListView_ID = LISTVIEW_GestaoResumos_RESUMOS.SelectedItems[0].Text;
                int ID = Convert.ToInt32(ListView_ID);

                string NomeResumo = TEXTBOX_GestaoResumos_RESUMO.Text;
                string DuracaoResumo = TEXTBOX_GestaoResumos_DURACAO.Text;

                MySqlCommand Comando_AlterarResumo = new MySqlCommand("SP_RESUMO_ALTERAR", LigacaoDB_StoredProcedures);
                Comando_AlterarResumo.CommandType = CommandType.StoredProcedure;

                Comando_AlterarResumo.Parameters.AddWithValue("_ID", ID);
                Comando_AlterarResumo.Parameters.AddWithValue("_nome", NomeResumo);
                Comando_AlterarResumo.Parameters.AddWithValue("_duracao", DuracaoResumo);

                Reader = Comando_AlterarResumo.ExecuteReader();

                Reader.Close();

                LigacaoDB_StoredProcedures.Close();

                MessageBox.Show("Resumo Alterado com Sucesso", "Resumo Alterado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        private void PICTUREBOX_GestaoResumos_ADICIONAR_RESUMO_Click(object sender, EventArgs e)
        {
            if (TEXTBOX_GestaoResumos_RESUMO.Text != "" && TEXTBOX_GestaoResumos_DURACAO.Text != "")
            {
                string ConfirmarExistencia = TEXTBOX_GestaoResumos_RESUMO.Text;
                bool JaExiste = false;

                foreach(ListViewItem ItemExiste in LISTVIEW_GestaoResumos_RESUMOS.Items)
                {
                    for (int i = 0; i < ItemExiste.SubItems.Count; i++)
                    {
                        if (ItemExiste.SubItems[i].Text.ToLower() == ConfirmarExistencia.ToLower())
                        {
                            MessageBox.Show("O resumo que está a tentar inserir já existe. Não é possivel adicionar resumos com o mesmo nome", "Resumo Já Existe", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            JaExiste = true;
                        }
                    }                    
                }

                if (JaExiste == false)
                {
                    Adicionar_Resumo();
                    ClearListView();
                    Preencher_ListView_Resumos();
                    Preencher_ListView_Filmes();
                    LimparTudo();
                }
            }

            else
            {
                MessageBox.Show("Os campos não estão totalmente preenchidos.", "Campos Inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
        }

        private void PICTUREBOX_GestaoResumos_GRAVAR_ALTERACOES_Click(object sender, EventArgs e)
        {
            if(LISTVIEW_GestaoResumos_RESUMOS.SelectedItems == null)
            {
                MessageBox.Show("Não tem nenhum resumo selecionado! Não é possivel alterar.", "Nenhum Resumo Selecionado", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (LISTVIEW_GestaoResumos_RESUMOS.SelectedItems != null)
            {
                if (TEXTBOX_GestaoResumos_RESUMO.Text == "" || TEXTBOX_GestaoResumos_DURACAO.Text == "")
                {
                    MessageBox.Show("Não tem nenhum resumo selecionado! Não é possivel alterar.", "Nenhum Resumo Selecionado", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                else
                {
                    DialogResult AlterarResumo = MessageBox.Show("Tem a certeza que deseja alterar este resumo?", "Alterar Resumo?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (AlterarResumo == DialogResult.Yes)
                    {
                        Alterar_Resumo();
                        ClearListView();
                        Preencher_ListView_Resumos();
                        Preencher_ListView_Filmes();
                        LimparTudo();
                    }
                }
            }            
        }

        private void PICTUREBOX_GestaoResumos_REMOVER_RESUMO_Click(object sender, EventArgs e)
        {
            if (LISTVIEW_GestaoResumos_RESUMOS.SelectedItems == null)
            {
                MessageBox.Show("Não tem nenhum resumo selecionado! Não é possivel remover.", "Nenhum Resumo Selecionado", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            else
            {
                DialogResult RemoverResumo = MessageBox.Show("Tem a certeza que deseja remover este resumo?", "Remover Resumo?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (RemoverResumo == DialogResult.Yes)
                {
                    Remover_Resumo();
                    ClearListView();
                    Preencher_ListView_Resumos();
                    Preencher_ListView_Filmes();
                    LimparTudo();
                }
            }
        }

        private void CHECKBOX_GestaoResumos_AGRUPAR_FILMES_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_GestaoResumos_FILMES.ShowGroups = CHECKBOX_GestaoResumos_AGRUPAR_FILMES.Checked;
            Ordenar_Filmes();
        }

        private void LISTVIEW_GestaoResumos_FILMES_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == UltimaColunaOrdenada)
            {
                if (UltimaOrdem == SortOrder.Ascending)
                    UltimaOrdem = SortOrder.Descending;
                else
                    UltimaOrdem = SortOrder.Ascending;
            }
            else
            {
                UltimaOrdem = SortOrder.Ascending;
                UltimaColunaOrdenada = e.Column;
            }

            Ordenar_Filmes();
        }

        public void Ordenar_Filmes()
        {
            if (CHECKBOX_GestaoResumos_AGRUPAR_FILMES.Checked == true)
            {
                if (LISTVIEW_GestaoResumos_FILMES.ShowGroups)
                    BuildGroups_Filmes(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_GestaoResumos_FILMES.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);
        }

        public void BuildGroups_Filmes(int coluna)
        {
            LISTVIEW_GestaoResumos_FILMES.Groups.Clear();

            int Contagem = LISTVIEW_GestaoResumos_FILMES.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_GestaoResumos_FILMES.Items)
            {
                String Item = LVI.SubItems[coluna].Text;

                if (coluna == 0 && Item.Length > 0)
                    Item = Item.Substring(0, 1);

                if (!Mapear.ContainsKey(Item))
                    Mapear[Item] = new List<ListViewItem>();

                Mapear[Item].Add(LVI);
            }

            List<ListViewGroup> Grupos = new List<ListViewGroup>();
            foreach (String Item in Mapear.Keys)
            {
                Grupos.Add(new ListViewGroup(Item));
            }

            Grupos.Sort(new ListViewCompararGrupo(UltimaOrdem));

            CompararColuna OrdenarItems = new CompararColuna(coluna, UltimaOrdem);
            foreach (ListViewGroup Grupo in Grupos)
            {
                LISTVIEW_GestaoResumos_FILMES.Groups.Add(Grupo);
                Mapear[Grupo.Header].Sort(OrdenarItems);
                Grupo.Items.AddRange(Mapear[Grupo.Header].ToArray());
            }
        }

        internal class ListViewCompararGrupo : IComparer<ListViewGroup>
        {
            public ListViewCompararGrupo(SortOrder ordem)
            {
                this.Ordem = ordem;
            }

            public int Compare(ListViewGroup x, ListViewGroup y)
            {
                int Resultado = String.Compare(x.Header, y.Header, true);

                if (this.Ordem == SortOrder.Descending)
                    Resultado = 0 - Resultado;

                return Resultado;
            }

            private SortOrder Ordem;
        }

        internal class CompararColuna : IComparer, IComparer<ListViewItem>
        {
            private int Coluna;
            private SortOrder Ordem;

            public CompararColuna(int coluna, SortOrder ordem)
            {
                Coluna = coluna;
                Ordem = ordem;
            }

            public int Compare(object x, object y)
            {
                return Compare((ListViewItem)x, (ListViewItem)y);
            }

            public int Compare(ListViewItem x, ListViewItem y)
            {
                int Resultado = String.Compare(x.SubItems[Coluna].Text, y.SubItems[Coluna].Text, true);

                if (Ordem == SortOrder.Descending)
                    Resultado = 0 - Resultado;

                return Resultado;
            }
        }          
    }
}
