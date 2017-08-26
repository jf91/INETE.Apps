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
    public partial class FORM_GESTAO_NACIONALIDADES : Form
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

        public FORM_GESTAO_NACIONALIDADES()
        {
            InitializeComponent();

            Preencher_ListView_Nacionalidades();
            Preencher_ListView_Realizadores();
            Preencher_ListView_Actores();
            Preencher_ListView_Productores();
        }

        private void LISTVIEW_GestaoNacionalidades_NACIONALIDADES_SelectedIndexChanged(object sender, EventArgs e)
        {
            Preencher_TextBox_Nacionalidade();
        }

        private void BUTTON_AdicionarNacionalidade_ADICIONAR_NACIONALIDADE_Click(object sender, EventArgs e)
        {
            Adicionar_Nacionalidade();
            ClearListView();
            Preencher_ListView_Nacionalidades();
            LimparTudo();

            TABCONTROL_GestaoNacionalidades.SelectedIndex = 0;
        }

        private void PICTUREBOX_GestaoNacionalidades_ADICIONAR_NACIONALIDADE_Click(object sender, EventArgs e)
        {
            TABCONTROL_GestaoNacionalidades.SelectedIndex = 1;
        }

        private void PICTUREBOX_GestaoNacionalidades_GRAVAR_ALTERACOES_Click(object sender, EventArgs e)
        {
            if (TEXTBOX_GestaoNacionalidades_ID.Text == "" && TEXTBOX_GestaoNacionalidades_NACIONALIDADE.Text == "")
            {
                MessageBox.Show("Não tem nenhuma nacionalidade selecionada! Não é possivel alterar.", "Nenhuma Nacionalidade Selecionada", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (TEXTBOX_GestaoNacionalidades_NACIONALIDADE.Text == "" && TEXTBOX_GestaoNacionalidades_ID.Text != "")
            {
                MessageBox.Show("O campo Nacionalidade está vazio. Não é possivel alterar.", "Campo Nacionalidade está Vazio", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            else
            {
                DialogResult AlterarNacionalidade = MessageBox.Show("Tem a certeza que deseja alterar esta nacionalidade?", "Alterar Nacionalidade?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (AlterarNacionalidade == DialogResult.Yes)
                {
                    Alterar_Nacionalidade();
                    ClearListView();
                    Preencher_ListView_Nacionalidades();
                    Preencher_ListView_Realizadores();
                    Preencher_ListView_Actores();
                    Preencher_ListView_Productores();
                    LimparTudo();
                }
            }
        }

        private void PICTUREBOX_GestaoNacionalidades_REMOVER_NACIONALIDADE_Click(object sender, EventArgs e)
        {
            if (TEXTBOX_GestaoNacionalidades_ID.Text == "" && TEXTBOX_GestaoNacionalidades_NACIONALIDADE.Text == "")
            {
                MessageBox.Show("Não tem nenhuma nacionalidade selecionada! Não é possivel remover.", "Nenhuma Nacionalidade Selecionada", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            else
            {
                DialogResult RemoverNacionalidade = MessageBox.Show("Tem a certeza que deseja remover esta nacionalidade?", "Remover Nacionalidade?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (RemoverNacionalidade == DialogResult.Yes)
                {
                    Remover_Nacionalidade();
                    ClearListView();
                    Preencher_ListView_Nacionalidades();
                    Preencher_ListView_Realizadores();
                    Preencher_ListView_Actores();
                    Preencher_ListView_Productores();
                    LimparTudo();
                }
            }
        }

        private void LISTVIEW_GestaoNacionalidades_REALIZADORES_ColumnClick(object sender, ColumnClickEventArgs e)
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

        private void CHECKBOX_GestaoNacionalidades_AGRUPAR_REALIZADORES_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_GestaoNacionalidades_REALIZADORES.ShowGroups = CHECKBOX_GestaoNacionalidades_AGRUPAR_REALIZADORES.Checked;
            Ordenar_Realizadores();
        }

        private void LISTVIEW_GestaoNacionalidades_ACTORES_ColumnClick(object sender, ColumnClickEventArgs e)
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

        private void CHECKBOX_GestaoNacionalidades_AGRUPAR_ACTORES_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_GestaoNacionalidades_ACTORES.ShowGroups = CHECKBOX_GestaoNacionalidades_AGRUPAR_ACTORES.Checked;
            Ordenar_Actores();
        }

        private void LISTVIEW_GestaoNacionalidades_PRODUCTORES_ColumnClick(object sender, ColumnClickEventArgs e)
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

        private void CHECKBOX_GestaoNacionalidades_AGRUPAR_PRODUCTORES_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_GestaoNacionalidades_PRODUCTORES.ShowGroups = CHECKBOX_GestaoNacionalidades_AGRUPAR_PRODUCTORES.Checked;
            Ordenar_Productores();
        }

        private void TEXTBOX_GestaoNacionalidades_NACIONALIDADE_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_GestaoNacionalidades_NACIONALIDADE.Text = Regex.Replace(TEXTBOX_GestaoNacionalidades_NACIONALIDADE.Text, @"[0-9]", string.Empty);
            TEXTBOX_GestaoNacionalidades_NACIONALIDADE.Text = Regex.Replace(TEXTBOX_GestaoNacionalidades_NACIONALIDADE.Text, @"[| \ ! @ # £ $ § % & / { } ( ) [ ] = ? ' » « * + ¨ ª º - _ . : , ; > <]", string.Empty);
        }

        private void TEXTBOX_AdicionarNacionalidade_NACIONALIDADE_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_AdicionarNacionalidade_NACIONALIDADE.Text = Regex.Replace(TEXTBOX_AdicionarNacionalidade_NACIONALIDADE.Text, @"[0-9]", string.Empty);
            TEXTBOX_AdicionarNacionalidade_NACIONALIDADE.Text = Regex.Replace(TEXTBOX_AdicionarNacionalidade_NACIONALIDADE.Text, @"[| \ ! @ # £ $ § % & / { } ( ) [ ] = ? ' » « * + ¨ ª º - _ . : , ; > <]", string.Empty);
        }        

        public void Preencher_ListView_Nacionalidades()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB.Open();

                string Query = "SELECT ID,nacionalidade FROM " + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades";
                MySqlCommand Comando = new MySqlCommand(Query, LigacaoDB);

                Reader = Comando.ExecuteReader();

                while (Reader.Read())
                {
                    var ROW = new ListViewItem();
                    ROW.Text = Reader["ID"].ToString();
                    ROW.SubItems.Add(Reader["nacionalidade"].ToString());
                    LISTVIEW_GestaoNacionalidades_NACIONALIDADES.Items.Add(ROW);
                }

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                //MessageBox.Show(EX.Message);
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

                    LISTVIEW_GestaoNacionalidades_REALIZADORES.Items.Add(ROW);
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

                    LISTVIEW_GestaoNacionalidades_ACTORES.Items.Add(ROW);
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

                    LISTVIEW_GestaoNacionalidades_PRODUCTORES.Items.Add(ROW);
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
            LISTVIEW_GestaoNacionalidades_NACIONALIDADES.Items.Clear();
            LISTVIEW_GestaoNacionalidades_REALIZADORES.Items.Clear();
            LISTVIEW_GestaoNacionalidades_ACTORES.Items.Clear();
            LISTVIEW_GestaoNacionalidades_PRODUCTORES.Items.Clear();
        }

        public void LimparTudo()
        {
            TEXTBOX_AdicionarNacionalidade_NACIONALIDADE.Text = "";

            TEXTBOX_GestaoNacionalidades_ID.Text = "";
            TEXTBOX_GestaoNacionalidades_NACIONALIDADE.Text = "";
        }

        public void Preencher_TextBox_Nacionalidade()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                string ListView_ID = LISTVIEW_GestaoNacionalidades_NACIONALIDADES.SelectedItems[0].Text;
                int ID = Convert.ToInt32(ListView_ID);

                LigacaoDB.Open();

                string IDNacionalidade;
                string Nacionalidade;

                string Query_ID = "SELECT ID FROM " + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades WHERE ID = " + ID + "";
                string Query_Nacionalidade = "SELECT nacionalidade FROM " + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades";

                MySqlCommand Comando_ID = new MySqlCommand(Query_ID, LigacaoDB);
                MySqlCommand Comando_Nacionalidade = new MySqlCommand(Query_Nacionalidade, LigacaoDB);

                IDNacionalidade = Comando_ID.ExecuteScalar().ToString();
                Nacionalidade = Comando_Nacionalidade.ExecuteScalar().ToString();

                TEXTBOX_GestaoNacionalidades_ID.Text = IDNacionalidade;
                TEXTBOX_GestaoNacionalidades_NACIONALIDADE.Text = Nacionalidade;
                
                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                //MessageBox.Show(EX.Message);
                LISTVIEW_GestaoNacionalidades_NACIONALIDADES.SelectedItems.Clear();
                TEXTBOX_GestaoNacionalidades_ID.Text = "";
                TEXTBOX_GestaoNacionalidades_NACIONALIDADE.Text = "";
            }
        }

        public void Adicionar_Nacionalidade()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB_StoredProcedures.Open();

                string Nacionalidade = TEXTBOX_AdicionarNacionalidade_NACIONALIDADE.Text;

                MySqlCommand Comando_InserirNacionalidade = new MySqlCommand("SP_NACIONALIDADE_INSERIR", LigacaoDB_StoredProcedures);
                Comando_InserirNacionalidade.CommandType = CommandType.StoredProcedure;

                Comando_InserirNacionalidade.Parameters.AddWithValue("_nacionalidade", Nacionalidade);

                Reader = Comando_InserirNacionalidade.ExecuteReader();

                Reader.Close();

                LigacaoDB_StoredProcedures.Close();

                MessageBox.Show("Nacionalidade Inserida com Sucesso", "Nacionalidade Inserida", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception EX)
            {
                //MessageBox.Show(EX.Message);
            }
        }

        public void Remover_Nacionalidade()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB_StoredProcedures.Open();

                string ListView_ID = LISTVIEW_GestaoNacionalidades_NACIONALIDADES.SelectedItems[0].Text;
                int ID = Convert.ToInt32(ListView_ID);

                MySqlCommand Command_RemoverNacionalidade = new MySqlCommand("SP_NACIONALIDADE_REMOVER", LigacaoDB_StoredProcedures);
                Command_RemoverNacionalidade.CommandType = CommandType.StoredProcedure;

                Command_RemoverNacionalidade.Parameters.AddWithValue("_id", ID);
                Reader = Command_RemoverNacionalidade.ExecuteReader();

                LigacaoDB_StoredProcedures.Close();

                MessageBox.Show("Nacionalidade removida com sucesso", "Nacionalidade Removida", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception EX)
            { }
        }

        public void Alterar_Nacionalidade()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB_StoredProcedures.Open();

                string ListView_ID = LISTVIEW_GestaoNacionalidades_NACIONALIDADES.SelectedItems[0].Text;
                int ID = Convert.ToInt32(ListView_ID);

                string Nacionalidade = TEXTBOX_GestaoNacionalidades_NACIONALIDADE.Text;

                MySqlCommand Comando_AlterarNacionalidade = new MySqlCommand("SP_NACIONALIDADE_ALTERAR", LigacaoDB_StoredProcedures);
                Comando_AlterarNacionalidade.CommandType = CommandType.StoredProcedure;

                Comando_AlterarNacionalidade.Parameters.AddWithValue("_id", ID);
                Comando_AlterarNacionalidade.Parameters.AddWithValue("_nacionalidade", Nacionalidade);

                Reader = Comando_AlterarNacionalidade.ExecuteReader();

                Reader.Close();

                LigacaoDB_StoredProcedures.Close();

                MessageBox.Show("Nacionalidade alterada com Sucesso", "Nacionalidade Alterada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        public void Ordenar_Realizadores()
        {
            if (CHECKBOX_GestaoNacionalidades_AGRUPAR_REALIZADORES.Checked == true)
            {
                if (LISTVIEW_GestaoNacionalidades_REALIZADORES.ShowGroups)
                    BuildGroups_Realizadores(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_GestaoNacionalidades_REALIZADORES.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);
        }

        public void Ordenar_Actores()
        {
            if (CHECKBOX_GestaoNacionalidades_AGRUPAR_ACTORES.Checked == true)
            {
                if (LISTVIEW_GestaoNacionalidades_ACTORES.ShowGroups)
                    BuildGroups_Actores(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_GestaoNacionalidades_ACTORES.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);
        }

        public void Ordenar_Productores()
        {
            if (CHECKBOX_GestaoNacionalidades_AGRUPAR_PRODUCTORES.Checked == true)
            {
                if (LISTVIEW_GestaoNacionalidades_PRODUCTORES.ShowGroups)
                    BuildGroups_Productores(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_GestaoNacionalidades_PRODUCTORES.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);
        }

        public void BuildGroups_Realizadores(int coluna)
        {
            LISTVIEW_GestaoNacionalidades_REALIZADORES.Groups.Clear();

            int Contagem = LISTVIEW_GestaoNacionalidades_REALIZADORES.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_GestaoNacionalidades_REALIZADORES.Items)
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
                LISTVIEW_GestaoNacionalidades_REALIZADORES.Groups.Add(Grupo);
                Mapear[Grupo.Header].Sort(OrdenarItems);
                Grupo.Items.AddRange(Mapear[Grupo.Header].ToArray());
            }
        }

        public void BuildGroups_Actores(int coluna)
        {
            LISTVIEW_GestaoNacionalidades_ACTORES.Groups.Clear();

            int Contagem = LISTVIEW_GestaoNacionalidades_ACTORES.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_GestaoNacionalidades_ACTORES.Items)
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
                LISTVIEW_GestaoNacionalidades_ACTORES.Groups.Add(Grupo);
                Mapear[Grupo.Header].Sort(OrdenarItems);
                Grupo.Items.AddRange(Mapear[Grupo.Header].ToArray());
            }
        }

        public void BuildGroups_Productores(int coluna)
        {
            LISTVIEW_GestaoNacionalidades_PRODUCTORES.Groups.Clear();

            int Contagem = LISTVIEW_GestaoNacionalidades_PRODUCTORES.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_GestaoNacionalidades_PRODUCTORES.Items)
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
                LISTVIEW_GestaoNacionalidades_PRODUCTORES.Groups.Add(Grupo);
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
