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
    public partial class FORM_GESTAO_PESSOAS : Form
    {
        MySqlConnection LigacaoDB = new MySqlConnection(CLASS_GLOBAL_VARIABLES.Servidor);
        MySqlConnection LigacaoDB_StoredProcedures = new MySqlConnection(CLASS_GLOBAL_VARIABLES.ServidorStoredProcedure);

        MySqlDataAdapter Adapter = new MySqlDataAdapter();

        MySqlDataReader Reader;
        MySqlDataReader Reader2;

        DataTable TabelaDados = new DataTable();
        DataSet DS = new DataSet();

        private int UltimaColunaOrdenada = 0;
        private SortOrder UltimaOrdem = SortOrder.Ascending; 

        public FORM_GESTAO_PESSOAS()
        {
            InitializeComponent();

            Preencher_ListView_Realizadores();
            Preencher_ListView_Actores();
            Preencher_ListView_Productores();
            Preencher_ListView_Nacionalidade();
        }

        private void PICTUREBOX_GESTAO_PESSOAS_REALIZADORES_Click(object sender, EventArgs e)
        {
            if (SPLITCONTAINER_PESSOAS.Panel2.Controls.Count == 0)
            {
                SPLITCONTAINER_PESSOAS.Panel2.Controls.Clear();

                FORMS_GESTAO.FORM_GESTAO_PESSOAS_REALIZADORES FormGestaoRealizadores = new FORM_GESTAO_PESSOAS_REALIZADORES();
                FormGestaoRealizadores.TopLevel = false;
                FormGestaoRealizadores.AutoScroll = true;

                SPLITCONTAINER_PESSOAS.Panel2.Controls.Add(FormGestaoRealizadores);

                FormGestaoRealizadores.Visible = true;
                return;
            }

            if (SPLITCONTAINER_PESSOAS.Panel2.Controls.Count == 1 || SPLITCONTAINER_PESSOAS.Panel2.Controls.Count > 1)
            {
                SPLITCONTAINER_PESSOAS.Panel2.Controls.Clear();

                FORMS_GESTAO.FORM_GESTAO_PESSOAS_REALIZADORES FormGestaoRealizadores = new FORM_GESTAO_PESSOAS_REALIZADORES();
                FormGestaoRealizadores.TopLevel = false;
                FormGestaoRealizadores.AutoScroll = true;

                SPLITCONTAINER_PESSOAS.Panel2.Controls.Add(FormGestaoRealizadores);

                FormGestaoRealizadores.Visible = true;
                return;
            }
        }

        private void PICTUREBOX_GESTAO_PESSOAS_ACTORES_Click(object sender, EventArgs e)
        {
            if (SPLITCONTAINER_PESSOAS.Panel2.Controls.Count == 0)
            {
                SPLITCONTAINER_PESSOAS.Panel2.Controls.Clear();

                FORMS_GESTAO.FORM_GESTAO_PESSOAS_ACTORES FormGestaoActores = new FORM_GESTAO_PESSOAS_ACTORES();
                FormGestaoActores.TopLevel = false;
                FormGestaoActores.AutoScroll = true;

                SPLITCONTAINER_PESSOAS.Panel2.Controls.Add(FormGestaoActores);

                FormGestaoActores.Visible = true;
                return;
            }

            if (SPLITCONTAINER_PESSOAS.Panel2.Controls.Count == 1 || SPLITCONTAINER_PESSOAS.Panel2.Controls.Count > 1)
            {
                SPLITCONTAINER_PESSOAS.Panel2.Controls.Clear();

                FORMS_GESTAO.FORM_GESTAO_PESSOAS_ACTORES FormGestaoActores = new FORM_GESTAO_PESSOAS_ACTORES();
                FormGestaoActores.TopLevel = false;
                FormGestaoActores.AutoScroll = true;

                SPLITCONTAINER_PESSOAS.Panel2.Controls.Add(FormGestaoActores);

                FormGestaoActores.Visible = true;
                return;
            }
        }

        private void PICTUREBOX_GESTAO_PESSOAS_PRODUCTORES_Click(object sender, EventArgs e)
        {
            if (SPLITCONTAINER_PESSOAS.Panel2.Controls.Count == 0)
            {
                SPLITCONTAINER_PESSOAS.Panel2.Controls.Clear();

                FORMS_GESTAO.FORM_GESTAO_PESSOAS_PRODUCTORES FormGestaoProductores = new FORM_GESTAO_PESSOAS_PRODUCTORES();
                FormGestaoProductores.TopLevel = false;
                FormGestaoProductores.AutoScroll = true;

                SPLITCONTAINER_PESSOAS.Panel2.Controls.Add(FormGestaoProductores);

                FormGestaoProductores.Visible = true;
                return;
            }

            if (SPLITCONTAINER_PESSOAS.Panel2.Controls.Count == 1 || SPLITCONTAINER_PESSOAS.Panel2.Controls.Count > 1)
            {
                SPLITCONTAINER_PESSOAS.Panel2.Controls.Clear();

                FORMS_GESTAO.FORM_GESTAO_PESSOAS_PRODUCTORES FormGestaoProductores = new FORM_GESTAO_PESSOAS_PRODUCTORES();
                FormGestaoProductores.TopLevel = false;
                FormGestaoProductores.AutoScroll = true;

                SPLITCONTAINER_PESSOAS.Panel2.Controls.Add(FormGestaoProductores);

                FormGestaoProductores.Visible = true;
                return;
            }
        }

        private void PICTUREBOX_GESTAO_PESSOAS_NACIONALIDADES_Click(object sender, EventArgs e)
        {
            if (SPLITCONTAINER_PESSOAS.Panel2.Controls.Count == 0)
            {
                SPLITCONTAINER_PESSOAS.Panel2.Controls.Clear();

                FORMS_GESTAO.FORM_GESTAO_NACIONALIDADES FormGestaoNacionalidades = new FORM_GESTAO_NACIONALIDADES();
                FormGestaoNacionalidades.TopLevel = false;
                FormGestaoNacionalidades.AutoScroll = true;

                SPLITCONTAINER_PESSOAS.Panel2.Controls.Add(FormGestaoNacionalidades);

                FormGestaoNacionalidades.Visible = true;
                return;
            }

            if (SPLITCONTAINER_PESSOAS.Panel2.Controls.Count == 1 || SPLITCONTAINER_PESSOAS.Panel2.Controls.Count > 1)
            {
                SPLITCONTAINER_PESSOAS.Panel2.Controls.Clear();

                FORMS_GESTAO.FORM_GESTAO_NACIONALIDADES FormGestaoNacionalidades = new FORM_GESTAO_NACIONALIDADES();
                FormGestaoNacionalidades.TopLevel = false;
                FormGestaoNacionalidades.AutoScroll = true;

                SPLITCONTAINER_PESSOAS.Panel2.Controls.Add(FormGestaoNacionalidades);

                FormGestaoNacionalidades.Visible = true;
                return;
            }
        }

        private void LISTVIEW_GestaoPessoas_REALIZADORES_ColumnClick(object sender, ColumnClickEventArgs e)
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

            Ordenar_Realizadores();
        }

        private void CHECKBOX_GestaoPessoas_AGRUPAR_REALIZADORES_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_GestaoPessoas_REALIZADORES.ShowGroups = CHECKBOX_GestaoPessoas_AGRUPAR_REALIZADORES.Checked;
            Ordenar_Realizadores();
        }

        private void LISTVIEW_GestaoPessoas_ACTORES_ColumnClick(object sender, ColumnClickEventArgs e)
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

            Ordenar_Actores();
        }

        private void CHECKBOX_GestaoPessoas_AGRUPAR_ACTORES_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_GestaoPessoas_ACTORES.ShowGroups = CHECKBOX_GestaoPessoas_AGRUPAR_ACTORES.Checked;
            Ordenar_Actores();
        }

        private void LISTVIEW_GestaoPessoas_PRODUCTORES_ColumnClick(object sender, ColumnClickEventArgs e)
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

            Ordenar_Productores();
        }

        private void CHECKBOX_GestaoPessoas_AGRUPAR_PRODUCTORES_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_GestaoPessoas_PRODUCTORES.ShowGroups = CHECKBOX_GestaoPessoas_AGRUPAR_PRODUCTORES.Checked;
            Ordenar_Productores();
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

                    LISTVIEW_GestaoPessoas_REALIZADORES.Items.Add(ROW);
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

                    LISTVIEW_GestaoPessoas_ACTORES.Items.Add(ROW);
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

                    LISTVIEW_GestaoPessoas_PRODUCTORES.Items.Add(ROW);
                }

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                //MessageBox.Show(EX.Message);
            }
        }

        public void Preencher_ListView_Nacionalidade()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB.Open();

                string Query = "SELECT nacionalidade FROM " + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades";
                MySqlCommand Comando = new MySqlCommand(Query, LigacaoDB);

                Reader = Comando.ExecuteReader();

                while (Reader.Read())
                {
                    var ROW = new ListViewItem();
                    ROW.Text = Reader["nacionalidade"].ToString();
                    LISTVIEW_GestaoPessoas_NACIONALIDADES.Items.Add(ROW);
                }

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                //MessageBox.Show(EX.Message);
            }
        }

        public void Ordenar_Realizadores()
        {
            if (CHECKBOX_GestaoPessoas_AGRUPAR_REALIZADORES.Checked == true)
            {
                if (LISTVIEW_GestaoPessoas_REALIZADORES.ShowGroups)
                    BuildGroups_Realizadores(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_GestaoPessoas_REALIZADORES.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);
        }

        public void Ordenar_Actores()
        {
            if (CHECKBOX_GestaoPessoas_AGRUPAR_ACTORES.Checked == true)
            {
                if (LISTVIEW_GestaoPessoas_ACTORES.ShowGroups)
                    BuildGroups_Actores(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_GestaoPessoas_ACTORES.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);
        }

        public void Ordenar_Productores()
        {
            if (CHECKBOX_GestaoPessoas_AGRUPAR_PRODUCTORES.Checked == true)
            {
                if (LISTVIEW_GestaoPessoas_PRODUCTORES.ShowGroups)
                    BuildGroups_Productores(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_GestaoPessoas_PRODUCTORES.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);
        }

        public void BuildGroups_Realizadores(int coluna)
        {
            LISTVIEW_GestaoPessoas_REALIZADORES.Groups.Clear();

            int Contagem = LISTVIEW_GestaoPessoas_REALIZADORES.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_GestaoPessoas_REALIZADORES.Items)
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
                LISTVIEW_GestaoPessoas_REALIZADORES.Groups.Add(Grupo);
                Mapear[Grupo.Header].Sort(OrdenarItems);
                Grupo.Items.AddRange(Mapear[Grupo.Header].ToArray());
            }
        }

        public void BuildGroups_Actores(int coluna)
        {
            LISTVIEW_GestaoPessoas_ACTORES.Groups.Clear();

            int Contagem = LISTVIEW_GestaoPessoas_ACTORES.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_GestaoPessoas_ACTORES.Items)
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
                LISTVIEW_GestaoPessoas_ACTORES.Groups.Add(Grupo);
                Mapear[Grupo.Header].Sort(OrdenarItems);
                Grupo.Items.AddRange(Mapear[Grupo.Header].ToArray());
            }
        }

        public void BuildGroups_Productores(int coluna)
        {
            LISTVIEW_GestaoPessoas_PRODUCTORES.Groups.Clear();

            int Contagem = LISTVIEW_GestaoPessoas_PRODUCTORES.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_GestaoPessoas_PRODUCTORES.Items)
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
                LISTVIEW_GestaoPessoas_PRODUCTORES.Groups.Add(Grupo);
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
