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
    public partial class FORM_GESTAO_FILMES : Form
    {
        FORM_GESTAO FormGestao = (FORM_GESTAO)Application.OpenForms["FORM_GESTAO"];
        FORM_GESTAO_FILMES_RESUMOS FormGestaoResumos = (FORM_GESTAO_FILMES_RESUMOS)Application.OpenForms["FORM_GESTAO_FILMES_RESUMOS"];

        MySqlConnection LigacaoDB = new MySqlConnection(CLASS_GLOBAL_VARIABLES.Servidor);
        MySqlConnection LigacaoDB_StoredProcedures = new MySqlConnection(CLASS_GLOBAL_VARIABLES.ServidorStoredProcedure);

        MySqlDataAdapter Adapter = new MySqlDataAdapter();

        MySqlDataReader Reader;
        MySqlDataReader Reader2;

        DataTable TabelaDados = new DataTable();
        DataSet DS = new DataSet();

        private int UltimaColunaOrdenada = 0;
        private SortOrder UltimaOrdem = SortOrder.Ascending;

        public FORM_GESTAO_FILMES()
        {
            InitializeComponent();
            Preencher_ListView_Filmes();
            Preencher_ListView_Realizadores();
            Preencher_ListView_Productores();
            Preencher_ListView_Actores();
        }

        private void PICTUREBOX_ADICIONAR_FILME_Click(object sender, EventArgs e)
        {
            TABCONTROL_GESTAO_FILMES.SelectedIndex = 1;
        }

        private void PICTUREBOX_GestaoFilmes_PROCURAR_RESUMO_Click(object sender, EventArgs e)
        {
            CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource = "GestaoFilmes";

            try
            {
                CLASS_GLOBAL_VARIABLES.AUX_RESUMO_ID = LISTVIEW_GestaoFilmes_FILMES.SelectedIndices[0].ToString();
                CLASS_GLOBAL_VARIABLES.INT_AUX_RESUMO_ID = Convert.ToInt32(CLASS_GLOBAL_VARIABLES.AUX_RESUMO_ID);
                CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Titulo = TEXTBOX_GestaoFilmes_TITULO.Text;
                CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Duracao = TEXTBOX_GestaoFilmes_DURACAO.Text;

                if (RADIOBUTTON_GestaoFilmes_POR_EXIBIR.Checked == true)
                    CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado = "Por Exibir";
                if (RADIOBUTTON_GestaoFilmes_EM_EXIBICAO.Checked == true)
                    CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado = "Em Exibicao";
                if (RADIOBUTTON_GestaoFilmes_JA_EXIBIDO.Checked == true)
                    CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado = "Ja Exibido";
            }

            catch (Exception EX)
            { }

            try
            {
                if (FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 0)
                {
                    FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                    FORM_GESTAO_FILMES_RESUMOS FormGestaoFilmesResumos = new FORM_GESTAO_FILMES_RESUMOS();
                    FormGestaoFilmesResumos.TopLevel = false;
                    FormGestaoFilmesResumos.AutoScroll = true;                  

                    FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoFilmesResumos);

                    FormGestaoFilmesResumos.Visible = true;

                    return;
                }

                if (FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 1 || FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count > 1)
                {
                    FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                    FORM_GESTAO_FILMES_RESUMOS FormGestaoFilmesResumos = new FORM_GESTAO_FILMES_RESUMOS();
                    FormGestaoFilmesResumos.TopLevel = false;
                    FormGestaoFilmesResumos.AutoScroll = true;
                    
                    FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoFilmesResumos);

                    FormGestaoFilmesResumos.Visible = true;
                    return;
                }
            }

            catch(Exception EX)
            { }
        }

        private void PICTUREBOX_AdicionarFilme_PROCURAR_RESUMO_Click(object sender, EventArgs e)
        {
            CLASS_GLOBAL_VARIABLES.GestaoResumos_RequestSource = "AdicionarFilme";

            try
            {
                CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Titulo = TEXTBOX_AdicionarFilme_TITULO.Text;
                CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Duracao = TEXTBOX_AdicionarFilme_DURACAO.Text;
                CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Resumo = TEXTBOX_AdicionarFilme_NOME_RESUMO.Text;
                CLASS_GLOBAL_VARIABLES.AUX_RESUMO_DuracaoResumo = TEXTBOX_AdicionarFilme_DURACAO_RESUMO.Text;

                if(RADIOBUTTON_AdicionarFilme_POR_EXIBIR.Checked == true)
                    CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado = "Por Exibir";
                if(RADIOBUTTON_AdicionarFilme_EM_EXIBICAO.Checked == true)
                    CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado = "Em Exibicao";
                if(RADIOBUTTON_AdicionarFilme_JA_EXIBIDO.Checked == true)
                    CLASS_GLOBAL_VARIABLES.AUX_RESUMO_Estado = "Ja Exibido";
            }

            catch (Exception EX)
            { }

            try
            {
                if (FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 0)
                {
                    FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                    FORM_GESTAO_FILMES_RESUMOS FormGestaoFilmesResumos = new FORM_GESTAO_FILMES_RESUMOS();
                    FormGestaoFilmesResumos.TopLevel = false;
                    FormGestaoFilmesResumos.AutoScroll = true;

                    FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoFilmesResumos);

                    FormGestaoFilmesResumos.Visible = true;

                    return;
                }

                if (FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count == 1 || FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Count > 1)
                {
                    FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Clear();

                    FORM_GESTAO_FILMES_RESUMOS FormGestaoFilmesResumos = new FORM_GESTAO_FILMES_RESUMOS();
                    FormGestaoFilmesResumos.TopLevel = false;
                    FormGestaoFilmesResumos.AutoScroll = true;

                    FormGestao.SPLITCONTAINER_GESTAO.Panel2.Controls.Add(FormGestaoFilmesResumos);

                    FormGestaoFilmesResumos.Visible = true;
                    return;
                }
            }

            catch (Exception EX)
            { }
        }

        private void LISTVIEW_GestaoFilmes_FILMES_SelectedIndexChanged(object sender, EventArgs e)
        {
            Preencher_TextBox_Filme();

            if (LISTVIEW_GestaoFilmes_FILMES.SelectedItems == null)
                PICTUREBOX_GestaoFilmes_PROCURAR_RESUMO.Enabled = false;
            else
                PICTUREBOX_GestaoFilmes_PROCURAR_RESUMO.Enabled = true;
        }

        private void BUTTON_AdicionarFilme_ADICIONAR_FILME_Click(object sender, EventArgs e)
        {
            Adicionar_Filme();
            ClearListView();
            Preencher_ListView_Filmes();
            Preencher_ListView_Realizadores();
            Preencher_ListView_Productores();
            Preencher_ListView_Actores();
            LimparTudo();

            TABCONTROL_GESTAO_FILMES.SelectedIndex = 0;
        }

        private void PICTUREBOX_REMOVER_FILME_Click(object sender, EventArgs e)
        {
            Remover_Filme();
            ClearListView();
            Preencher_ListView_Filmes();
            Preencher_ListView_Realizadores();
            Preencher_ListView_Productores();
            Preencher_ListView_Actores();
            LimparTudo();
        }

        private void PICTUREBOX_ALTERAR_FILME_Click(object sender, EventArgs e)
        {
            Alterar_Filme();
            ClearListView();
            Preencher_ListView_Filmes();
            Preencher_ListView_Realizadores();
            Preencher_ListView_Productores();
            Preencher_ListView_Actores();
            LimparTudo();
        }

        private void LISTVIEW_GestaoFilmes_FILMES_ColumnClick(object sender, ColumnClickEventArgs e)
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

        private void CHECKBOX_GestaoFilmes_AGRUPAR_FILMES_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_GestaoFilmes_FILMES.ShowGroups = CHECKBOX_GestaoFilmes_AGRUPAR_FILMES.Checked;
            Ordenar_Filmes();
        }

        private void LISTVIEW_GestaoFilmes_ACTORES_ColumnClick(object sender, ColumnClickEventArgs e)
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

            Ordenar_Actores_TabGestao();
        }

        private void CHECKBOX_GestaoFilmes_AGRUPAR_ACTORES_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_GestaoFilmes_ACTORES.ShowGroups = CHECKBOX_GestaoFilmes_AGRUPAR_ACTORES.Checked;
            Ordenar_Actores_TabGestao();
        }

        private void LISTVIEW_GestaoFilmes_REALIZADORES_ColumnClick(object sender, ColumnClickEventArgs e)
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

            Ordenar_Realizadores_TabGestao();
        }

        private void CHECKBOX_GestaoFilmes_AGRUPAR_REALIZADORES_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_GestaoFilmes_REALIZADORES.ShowGroups = CHECKBOX_GestaoFilmes_AGRUPAR_REALIZADORES.Checked;
            Ordenar_Realizadores_TabGestao();
        }

        private void LISTVIEW_GestaoFilmes_PRODUCTORES_ColumnClick(object sender, ColumnClickEventArgs e)
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

            Ordenar_Productores_TabGestao();
        }

        private void CHECKBOX_GestaoFilmes_AGRUPAR_PRODUCTORES_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_GestaoFilmes_PRODUCTORES.ShowGroups = CHECKBOX_GestaoFilmes_AGRUPAR_PRODUCTORES.Checked;
            Ordenar_Productores_TabGestao();
        }

        private void LISTVIEW_AdicionarFilme_ACTORES_ColumnClick(object sender, ColumnClickEventArgs e)
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

            Ordenar_Actores_TabAdicionar();
        }

        private void CHECKBOX_AdicionarFilme_AGRUPAR_ACTORES_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_AdicionarFilme_ACTORES.ShowGroups = CHECKBOX_AdicionarFilme_AGRUPAR_ACTORES.Checked;
            Ordenar_Actores_TabAdicionar();
        }

        private void LISTVIEW_AdicionarFilme_REALIZADORES_ColumnClick(object sender, ColumnClickEventArgs e)
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

            Ordenar_Realizadores_TabAdicionar();
        }

        private void CHECKBOX_AdicionarFilme_AGRUPAR_REALIZADORES_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_AdicionarFilme_REALIZADORES.ShowGroups = CHECKBOX_AdicionarFilme_AGRUPAR_REALIZADORES.Checked;
            Ordenar_Realizadores_TabAdicionar();
        }

        private void LISTVIEW_AdicionarFilme_PRODUCTORES_ColumnClick(object sender, ColumnClickEventArgs e)
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

            Ordenar_Productores_TabAdicionar();
        }

        private void CHECKBOX_AdicionarFilme_AGRUPAR_PRODUCTORES_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_AdicionarFilme_PRODUCTORES.ShowGroups = CHECKBOX_AdicionarFilme_AGRUPAR_PRODUCTORES.Checked;
            Ordenar_Productores_TabAdicionar();
        }


        public void Preencher_ListView_Filmes()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB.Open();

                string Query = "SELECT " + CLASS_GLOBAL_VARIABLES.BD + ".filmes.ID," + CLASS_GLOBAL_VARIABLES.BD + ".filmes.titulo," + CLASS_GLOBAL_VARIABLES.BD + ".estados_filmes.estado FROM " + CLASS_GLOBAL_VARIABLES.BD + ".filmes INNER JOIN " + CLASS_GLOBAL_VARIABLES.BD + ".estados_filmes ON " + CLASS_GLOBAL_VARIABLES.BD + ".filmes.estadoID = " + CLASS_GLOBAL_VARIABLES.BD + ".estados_filmes.ID";

                MySqlCommand Comando = new MySqlCommand(Query, LigacaoDB);

                Reader = Comando.ExecuteReader();

                while (Reader.Read())
                {
                    var ROW = new ListViewItem();
                    ROW.Text = Reader["ID"].ToString();
                    ROW.SubItems.Add(Reader["titulo"].ToString());
                    ROW.SubItems.Add(Reader["estado"].ToString());

                    LISTVIEW_GestaoFilmes_FILMES.Items.Add(ROW);
                }

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        public void Preencher_ListView_Realizadores()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB.Open();

                string Query = "SELECT " + CLASS_GLOBAL_VARIABLES.BD + ".realizadores.nome_completo," + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades.nacionalidade FROM " + CLASS_GLOBAL_VARIABLES.BD + ".realizadores INNER JOIN " + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades ON " + CLASS_GLOBAL_VARIABLES.BD + ".realizadores.nacionalidade = " + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades.ID";

                MySqlCommand Comando = new MySqlCommand(Query, LigacaoDB);

                Reader = Comando.ExecuteReader();

                while (Reader.Read())
                {
                    var ROW = new ListViewItem();
                    ROW.Text = Reader["nome_completo"].ToString();
                    ROW.SubItems.Add(Reader["nacionalidade"].ToString());

                    var ROW2 = new ListViewItem();
                    ROW2.Text = Reader["nome_completo"].ToString();
                    ROW2.SubItems.Add(Reader["nacionalidade"].ToString());

                    LISTVIEW_GestaoFilmes_REALIZADORES.Items.Add(ROW);
                    LISTVIEW_AdicionarFilme_REALIZADORES.Items.Add(ROW2);
                }

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                //MessageBox.Show(EX.Message);
            }
        }

        public void Preencher_ListView_Productores()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB.Open();

                string Query = "SELECT " + CLASS_GLOBAL_VARIABLES.BD + ".productores.nome_completo," + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades.nacionalidade FROM " + CLASS_GLOBAL_VARIABLES.BD + ".productores INNER JOIN " + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades ON " + CLASS_GLOBAL_VARIABLES.BD + ".productores.nacionalidade = " + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades.ID";

                MySqlCommand Comando = new MySqlCommand(Query, LigacaoDB);

                Reader = Comando.ExecuteReader();

                while (Reader.Read())
                {
                    var ROW = new ListViewItem();
                    ROW.Text = Reader["nome_completo"].ToString();
                    ROW.SubItems.Add(Reader["nacionalidade"].ToString());

                    var ROW2 = new ListViewItem();
                    ROW2.Text = Reader["nome_completo"].ToString();
                    ROW2.SubItems.Add(Reader["nacionalidade"].ToString());

                    LISTVIEW_GestaoFilmes_PRODUCTORES.Items.Add(ROW);
                    LISTVIEW_AdicionarFilme_PRODUCTORES.Items.Add(ROW2);
                }

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                //MessageBox.Show(EX.Message);
            }
        }

        public void Preencher_ListView_Actores()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB.Open();

                string Query = "SELECT " + CLASS_GLOBAL_VARIABLES.BD + ".actores.nome_completo," + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades.nacionalidade FROM " + CLASS_GLOBAL_VARIABLES.BD + ".actores INNER JOIN " + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades ON " + CLASS_GLOBAL_VARIABLES.BD + ".actores.nacionalidade = " + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades.ID";

                MySqlCommand Comando = new MySqlCommand(Query, LigacaoDB);

                Reader = Comando.ExecuteReader();

                while (Reader.Read())
                {
                    var ROW = new ListViewItem();
                    ROW.Text = Reader["nome_completo"].ToString();
                    ROW.SubItems.Add(Reader["nacionalidade"].ToString());

                    var ROW2 = new ListViewItem();
                    ROW2.Text = Reader["nome_completo"].ToString();
                    ROW2.SubItems.Add(Reader["nacionalidade"].ToString());

                    LISTVIEW_GestaoFilmes_ACTORES.Items.Add(ROW);
                    LISTVIEW_AdicionarFilme_ACTORES.Items.Add(ROW2);
                }

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                //MessageBox.Show(EX.Message);
            }
        }

        public void ClearListView()
        {
            LISTVIEW_GestaoFilmes_FILMES.Items.Clear();
            LISTVIEW_GestaoFilmes_ACTORES.Items.Clear();
            LISTVIEW_GestaoFilmes_REALIZADORES.Items.Clear();
            LISTVIEW_GestaoFilmes_PRODUCTORES.Items.Clear();

            LISTVIEW_AdicionarFilme_ACTORES.Items.Clear();
            LISTVIEW_AdicionarFilme_REALIZADORES.Items.Clear();
            LISTVIEW_AdicionarFilme_PRODUCTORES.Items.Clear(); 
        }

        public void LimparTudo()
        {
            TEXTBOX_AdicionarFilme_TITULO.Text = "";
            TEXTBOX_AdicionarFilme_DURACAO.Text = "";
            TEXTBOX_AdicionarFilme_NOME_RESUMO.Text = "";
            TEXTBOX_AdicionarFilme_DURACAO_RESUMO.Text = "";
            RADIOBUTTON_AdicionarFilme_POR_EXIBIR.Checked = false;
            RADIOBUTTON_AdicionarFilme_EM_EXIBICAO.Checked = false;
            RADIOBUTTON_AdicionarFilme_JA_EXIBIDO.Checked = false;

            TEXTBOX_GestaoFilmes_TITULO.Text = "";
            TEXTBOX_GestaoFilmes_DURACAO.Text = "";
            TEXTBOX_GestaoFilmes_NOME_RESUMO.Text = "";
            TEXTBOX_GestaoFilmes_DURACAO_RESUMO.Text = "";
            RADIOBUTTON_GestaoFilmes_POR_EXIBIR.Checked = false;
            RADIOBUTTON_GestaoFilmes_EM_EXIBICAO.Checked = false;
            RADIOBUTTON_GestaoFilmes_JA_EXIBIDO.Checked = false;

            LISTVIEW_GestaoFilmes_ACTORES.SelectedItems.Clear();
            LISTVIEW_GestaoFilmes_REALIZADORES.SelectedItems.Clear();
            LISTVIEW_GestaoFilmes_PRODUCTORES.SelectedItems.Clear();
        }

        public void Preencher_TextBox_Filme()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                string ListView_ID = LISTVIEW_GestaoFilmes_FILMES.SelectedItems[0].Text;
                int ID = Convert.ToInt32(ListView_ID);

                LigacaoDB.Open();

                string Titulo;
                string Duracao;
                string Estado;
                string NomeResumo;
                string DuracaoResumo;

                string Query_Titulo = "SELECT titulo FROM " + CLASS_GLOBAL_VARIABLES.BD + ".filmes WHERE ID = " + ID + "";
                string Query_Duracao = "SELECT duracao FROM " + CLASS_GLOBAL_VARIABLES.BD + ".filmes WHERE ID = " + ID + "";

                string Query_Estado = "SELECT " + CLASS_GLOBAL_VARIABLES.BD + ".estados_filmes.estado FROM " + CLASS_GLOBAL_VARIABLES.BD + ".filmes INNER JOIN " + CLASS_GLOBAL_VARIABLES.BD + ".estados_filmes ON " + CLASS_GLOBAL_VARIABLES.BD + ".filmes.estadoID = " + CLASS_GLOBAL_VARIABLES.BD + ".estados_filmes.ID WHERE filmes.ID = " + ID + "";

                string Query_NomeResumo = "SELECT " + CLASS_GLOBAL_VARIABLES.BD + ".resumos_filmes.nome FROM " + CLASS_GLOBAL_VARIABLES.BD + ".filmes INNER JOIN " + CLASS_GLOBAL_VARIABLES.BD + ".resumos_filmes ON " + CLASS_GLOBAL_VARIABLES.BD + ".filmes.resumoID = " + CLASS_GLOBAL_VARIABLES.BD + ".resumos_filmes.ID WHERE filmes.ID = " + ID + "";

                string Query_DuracaoResumo = "SELECT " + CLASS_GLOBAL_VARIABLES.BD + ".resumos_filmes.duracao FROM " + CLASS_GLOBAL_VARIABLES.BD + ".filmes INNER JOIN " + CLASS_GLOBAL_VARIABLES.BD + ".resumos_filmes ON " + CLASS_GLOBAL_VARIABLES.BD + ".filmes.duracao_resumo = " + CLASS_GLOBAL_VARIABLES.BD + ".resumos_filmes.ID WHERE filmes.ID = " + ID + "";

                MySqlCommand Comando_Titulo = new MySqlCommand(Query_Titulo, LigacaoDB);
                MySqlCommand Comando_Duracao = new MySqlCommand(Query_Duracao, LigacaoDB);
                MySqlCommand Comando_Estado = new MySqlCommand(Query_Estado, LigacaoDB);
                MySqlCommand Comando_NomeResumo = new MySqlCommand(Query_NomeResumo, LigacaoDB);
                MySqlCommand Comando_DuracaoResumo = new MySqlCommand(Query_DuracaoResumo, LigacaoDB);

                Titulo = Comando_Titulo.ExecuteScalar().ToString();
                Duracao = Comando_Duracao.ExecuteScalar().ToString();
                Estado = Comando_Estado.ExecuteScalar().ToString();
                NomeResumo = Comando_NomeResumo.ExecuteScalar().ToString();
                DuracaoResumo = Comando_DuracaoResumo.ExecuteScalar().ToString();

                TEXTBOX_GestaoFilmes_TITULO.Text = Titulo;
                TEXTBOX_GestaoFilmes_DURACAO.Text = Duracao;

                if (Estado == "Por Exibir")
                    RADIOBUTTON_GestaoFilmes_POR_EXIBIR.Checked = true;
                if (Estado == "Em Exibição")
                    RADIOBUTTON_GestaoFilmes_EM_EXIBICAO.Checked = true;
                if (Estado == "Já Exibido")
                    RADIOBUTTON_GestaoFilmes_JA_EXIBIDO.Checked = true;

                TEXTBOX_GestaoFilmes_NOME_RESUMO.Text = NomeResumo;
                TEXTBOX_GestaoFilmes_DURACAO_RESUMO.Text = DuracaoResumo;

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                //MessageBox.Show(EX.Message);
                LISTVIEW_GestaoFilmes_FILMES.SelectedItems.Clear();
                TEXTBOX_GestaoFilmes_TITULO.Text = "";
                TEXTBOX_GestaoFilmes_DURACAO.Text = "";
                RADIOBUTTON_GestaoFilmes_POR_EXIBIR.Checked = false;
                RADIOBUTTON_GestaoFilmes_EM_EXIBICAO.Checked = false;
                RADIOBUTTON_GestaoFilmes_JA_EXIBIDO.Checked = false;
                TEXTBOX_GestaoFilmes_NOME_RESUMO.Text = "";
                TEXTBOX_GestaoFilmes_DURACAO_RESUMO.Text = "";
            }
        }

        public void Adicionar_Filme()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB.Open();

                string Titulo = TEXTBOX_AdicionarFilme_TITULO.Text;
                string Duracao = TEXTBOX_AdicionarFilme_DURACAO.Text;
                string NomeResumo = TEXTBOX_AdicionarFilme_NOME_RESUMO.Text;
                int ResumoID;
                int Estado = 0;

                if (RADIOBUTTON_AdicionarFilme_POR_EXIBIR.Checked == true)
                    Estado = 1;
                if (RADIOBUTTON_AdicionarFilme_EM_EXIBICAO.Checked == true)
                    Estado = 2;
                if (RADIOBUTTON_AdicionarFilme_JA_EXIBIDO.Checked == true)
                    Estado = 3;

                string Query_ObterResumoID = "SELECT ID FROM " + CLASS_GLOBAL_VARIABLES.BD + ".resumos_filmes WHERE nome ='" + NomeResumo + "'";
                MySqlCommand Comando_ObterResumoID = new MySqlCommand(Query_ObterResumoID, LigacaoDB);
                ResumoID = Convert.ToInt32(Comando_ObterResumoID.ExecuteScalar());

                LigacaoDB.Close();

                System.Threading.Thread.Sleep(1000);

                LigacaoDB_StoredProcedures.Open();

                MySqlCommand Comando_InserirFilme = new MySqlCommand("SP_FILME_INSERIR", LigacaoDB_StoredProcedures);
                Comando_InserirFilme.CommandType = CommandType.StoredProcedure;

                Comando_InserirFilme.Parameters.AddWithValue("_titulo", Titulo);
                Comando_InserirFilme.Parameters.AddWithValue("_duracao", Duracao);
                Comando_InserirFilme.Parameters.AddWithValue("_resumo", ResumoID);
                Comando_InserirFilme.Parameters.AddWithValue("_duracao_resumo", ResumoID);
                Comando_InserirFilme.Parameters.AddWithValue("_estado", Estado);

                Reader = Comando_InserirFilme.ExecuteReader();

                Reader.Close();

                LigacaoDB_StoredProcedures.Close();

                MessageBox.Show("Filme Inserido com Sucesso", "Filme Inserido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        public void Remover_Filme()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB_StoredProcedures.Open();

                string ListView_ID = LISTVIEW_GestaoFilmes_FILMES.SelectedItems[0].Text;
                string Resumo = TEXTBOX_GestaoFilmes_NOME_RESUMO.Text;
                int ID = Convert.ToInt32(ListView_ID);

                MySqlCommand Command_RemoverFilme = new MySqlCommand("SP_FILME_REMOVER", LigacaoDB_StoredProcedures);
                Command_RemoverFilme.CommandType = CommandType.StoredProcedure;

                Command_RemoverFilme.Parameters.AddWithValue("_id", ID);
                Reader = Command_RemoverFilme.ExecuteReader();
                Reader.Close();

                DialogResult RemoverResumo = MessageBox.Show("Deseja remover o resumo associado a este filme?","Remover Resumo?",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                if(RemoverResumo == DialogResult.Yes)
                {
                    MySqlCommand Command_RemoverResumoFilme = new MySqlCommand("SP_ResumoFilme_REMOVER", LigacaoDB_StoredProcedures);
                    Command_RemoverResumoFilme.CommandType = CommandType.StoredProcedure;

                    Command_RemoverResumoFilme.Parameters.AddWithValue("_nome", Resumo);
                    Reader = Command_RemoverResumoFilme.ExecuteReader();

                    LigacaoDB_StoredProcedures.Close();

                    MessageBox.Show("Resumo removido com sucesso", "Resumo Removido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if(LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                    LigacaoDB_StoredProcedures.Close();

                MessageBox.Show("Filme removido com sucesso", "Filme Removido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception EX)
            { }
        }

        public void Alterar_Filme()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB.Open();

                string ListView_ID = LISTVIEW_GestaoFilmes_FILMES.SelectedItems[0].Text;
                int ID = Convert.ToInt32(ListView_ID);

                string Titulo = TEXTBOX_GestaoFilmes_TITULO.Text;
                string Duracao = TEXTBOX_GestaoFilmes_DURACAO.Text;
                string NomeResumo = TEXTBOX_GestaoFilmes_NOME_RESUMO.Text;
                int ResumoID;
                int Estado = 0;

                if (RADIOBUTTON_GestaoFilmes_POR_EXIBIR.Checked == true)
                    Estado = 1;
                if (RADIOBUTTON_GestaoFilmes_EM_EXIBICAO.Checked == true)
                    Estado = 2;
                if (RADIOBUTTON_GestaoFilmes_JA_EXIBIDO.Checked == true)
                    Estado = 3;

                string Query_ObterResumoID = "SELECT ID FROM " + CLASS_GLOBAL_VARIABLES.BD + ".resumos_filmes WHERE nome ='" + NomeResumo + "'";
                MySqlCommand Comando_ObterResumoID = new MySqlCommand(Query_ObterResumoID, LigacaoDB);
                ResumoID = Convert.ToInt32(Comando_ObterResumoID.ExecuteScalar());

                Reader.Close();

                LigacaoDB.Close();

                System.Threading.Thread.Sleep(1000);

                LigacaoDB_StoredProcedures.Open();

                MySqlCommand Comando_AlterarFilme = new MySqlCommand("SP_FILME_ALTERAR", LigacaoDB_StoredProcedures);
                Comando_AlterarFilme.CommandType = CommandType.StoredProcedure;

                Comando_AlterarFilme.Parameters.AddWithValue("_id", ID);
                Comando_AlterarFilme.Parameters.AddWithValue("_titulo", Titulo);
                Comando_AlterarFilme.Parameters.AddWithValue("_duracao", Duracao);
                Comando_AlterarFilme.Parameters.AddWithValue("_resumo", ResumoID);
                Comando_AlterarFilme.Parameters.AddWithValue("_duracao_resumo", ResumoID); 
                Comando_AlterarFilme.Parameters.AddWithValue("_estado", Estado);

                Reader = Comando_AlterarFilme.ExecuteReader();

                Reader.Close();

                LigacaoDB_StoredProcedures.Close();

                MessageBox.Show("Filme Alterado com Sucesso", "Filme Alterado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        public void Ordenar_Filmes()
        {
            if (CHECKBOX_GestaoFilmes_AGRUPAR_FILMES.Checked == true)
            {
                if (LISTVIEW_GestaoFilmes_FILMES.ShowGroups)
                    BuildGroups_Filmes(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_GestaoFilmes_FILMES.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);
        }

        public void Ordenar_Realizadores_TabGestao()
        {
            if (CHECKBOX_GestaoFilmes_AGRUPAR_REALIZADORES.Checked == true)
            {
                if (LISTVIEW_GestaoFilmes_REALIZADORES.ShowGroups)
                    BuildGroups_Realizadores_TabGestao(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_GestaoFilmes_REALIZADORES.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);
        }

        public void Ordenar_Actores_TabGestao()
        {
            if (CHECKBOX_GestaoFilmes_AGRUPAR_ACTORES.Checked == true)
            {
                if (LISTVIEW_GestaoFilmes_ACTORES.ShowGroups)
                    BuildGroups_Actores_TabGestao(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_GestaoFilmes_ACTORES.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);
        }

        public void Ordenar_Productores_TabGestao()
        {
            if (CHECKBOX_GestaoFilmes_AGRUPAR_PRODUCTORES.Checked == true)
            {
                if (LISTVIEW_GestaoFilmes_PRODUCTORES.ShowGroups)
                    BuildGroups_Productores_TabGestao(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_GestaoFilmes_PRODUCTORES.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);
        }

        public void Ordenar_Realizadores_TabAdicionar()
        {
            if (CHECKBOX_AdicionarFilme_AGRUPAR_REALIZADORES.Checked == true)
            {
                if (LISTVIEW_AdicionarFilme_REALIZADORES.ShowGroups)
                    BuildGroups_Realizadores_TabAdicionar(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_AdicionarFilme_REALIZADORES.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);
        }

        public void Ordenar_Actores_TabAdicionar()
        {
            if (CHECKBOX_AdicionarFilme_AGRUPAR_ACTORES.Checked == true)
            {
                if (LISTVIEW_AdicionarFilme_ACTORES.ShowGroups)
                    BuildGroups_Actores_TabAdicionar(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_AdicionarFilme_ACTORES.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);
        }

        public void Ordenar_Productores_TabAdicionar()
        {
            if (CHECKBOX_AdicionarFilme_AGRUPAR_PRODUCTORES.Checked == true)
            {
                if (LISTVIEW_AdicionarFilme_PRODUCTORES.ShowGroups)
                    BuildGroups_Productores_TabAdicionar(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_AdicionarFilme_PRODUCTORES.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);
        }

        public void BuildGroups_Filmes(int coluna)
        {
            LISTVIEW_GestaoFilmes_FILMES.Groups.Clear();

            int Contagem = LISTVIEW_GestaoFilmes_FILMES.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_GestaoFilmes_FILMES.Items)
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
                LISTVIEW_GestaoFilmes_FILMES.Groups.Add(Grupo);
                Mapear[Grupo.Header].Sort(OrdenarItems);
                Grupo.Items.AddRange(Mapear[Grupo.Header].ToArray());
            }
        }

        public void BuildGroups_Realizadores_TabGestao(int coluna)
        {
            LISTVIEW_GestaoFilmes_REALIZADORES.Groups.Clear();

            int Contagem = LISTVIEW_GestaoFilmes_REALIZADORES.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_GestaoFilmes_REALIZADORES.Items)
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
                LISTVIEW_GestaoFilmes_REALIZADORES.Groups.Add(Grupo);
                Mapear[Grupo.Header].Sort(OrdenarItems);
                Grupo.Items.AddRange(Mapear[Grupo.Header].ToArray());
            }
        }

        public void BuildGroups_Actores_TabGestao(int coluna)
        {
            LISTVIEW_GestaoFilmes_ACTORES.Groups.Clear();

            int Contagem = LISTVIEW_GestaoFilmes_ACTORES.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_GestaoFilmes_ACTORES.Items)
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
                LISTVIEW_GestaoFilmes_ACTORES.Groups.Add(Grupo);
                Mapear[Grupo.Header].Sort(OrdenarItems);
                Grupo.Items.AddRange(Mapear[Grupo.Header].ToArray());
            }
        }

        public void BuildGroups_Productores_TabGestao(int coluna)
        {
            LISTVIEW_GestaoFilmes_PRODUCTORES.Groups.Clear();

            int Contagem = LISTVIEW_GestaoFilmes_PRODUCTORES.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_GestaoFilmes_PRODUCTORES.Items)
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
                LISTVIEW_GestaoFilmes_PRODUCTORES.Groups.Add(Grupo);
                Mapear[Grupo.Header].Sort(OrdenarItems);
                Grupo.Items.AddRange(Mapear[Grupo.Header].ToArray());
            }
        }

        public void BuildGroups_Realizadores_TabAdicionar(int coluna)
        {
            LISTVIEW_AdicionarFilme_REALIZADORES.Groups.Clear();

            int Contagem = LISTVIEW_AdicionarFilme_REALIZADORES.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_AdicionarFilme_REALIZADORES.Items)
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
                LISTVIEW_AdicionarFilme_REALIZADORES.Groups.Add(Grupo);
                Mapear[Grupo.Header].Sort(OrdenarItems);
                Grupo.Items.AddRange(Mapear[Grupo.Header].ToArray());
            }
        }

        public void BuildGroups_Actores_TabAdicionar(int coluna)
        {
            LISTVIEW_AdicionarFilme_ACTORES.Groups.Clear();

            int Contagem = LISTVIEW_AdicionarFilme_ACTORES.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_AdicionarFilme_ACTORES.Items)
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
                LISTVIEW_AdicionarFilme_ACTORES.Groups.Add(Grupo);
                Mapear[Grupo.Header].Sort(OrdenarItems);
                Grupo.Items.AddRange(Mapear[Grupo.Header].ToArray());
            }
        }

        public void BuildGroups_Productores_TabAdicionar(int coluna)
        {
            LISTVIEW_AdicionarFilme_PRODUCTORES.Groups.Clear();

            int Contagem = LISTVIEW_AdicionarFilme_PRODUCTORES.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_AdicionarFilme_PRODUCTORES.Items)
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
                LISTVIEW_AdicionarFilme_PRODUCTORES.Groups.Add(Grupo);
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
