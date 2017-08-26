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
    public partial class FORM_GESTAO_PESSOAS_PRODUCTORES : Form
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

        public FORM_GESTAO_PESSOAS_PRODUCTORES()
        {
            InitializeComponent();

            Preencher_ListView_Productores();
            Preencher_ListView_Filmes();
            Preencher_ComboBox_Nacionalidades();
        }

        private void PICTUREBOX_GestaoProductores_ADICIONAR_PRODUCTOR_Click(object sender, EventArgs e)
        {
            TABCONTROL_GestaoProductores.SelectedIndex = 1;
        }

        private void PICTUREBOX_GestaoProductores_REMOVER_PRODUCTOR_Click(object sender, EventArgs e)
        {
            if (TEXTBOX_GestaoProductores_NOME_COMPLETO.Text == "")
            {
                MessageBox.Show("Não tem nenhum productor selecionado! Não é possivel remover.", "Nenhum productor selecionado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (TEXTBOX_GestaoProductores_NOME_COMPLETO.Text != "")
            {
                DialogResult RemoverProductor = MessageBox.Show("Tem a certeza que deseja remover este productor?", "Remover Actor?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (RemoverProductor == DialogResult.Yes)
                {
                    Remover_Productor();
                    ClearListView();
                    Preencher_ListView_Productores();
                    LimparTudo();
                }
            }
        }

        private void PICTUREBOX_GestaoProductores_GRAVAR_ALTERACOES_Click(object sender, EventArgs e)
        {
            if (TEXTBOX_GestaoProductores_NOME_COMPLETO.Text == "")
            {
                MessageBox.Show("Não tem nenhum productor selecionado! Não é possivel remover.", "Nenhum productor selecionado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (TEXTBOX_GestaoProductores_NOME_COMPLETO.Text != "")
            {
                DialogResult AlterarActor = MessageBox.Show("Tem a certeza que deseja alterar este productor?", "Alterar Productor?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (AlterarActor == DialogResult.Yes)
                {
                    Alterar_Productor();
                    ClearListView();
                    Preencher_ListView_Productores();
                    LimparTudo();
                }
            }
        }

        private void BUTTON_AdicionarProductor_ADICIONAR_PRODUCTOR_Click(object sender, EventArgs e)
        {
            Adicionar_Productor();
            ClearListView();
            Preencher_ListView_Productores();
            LimparTudo();

            TABCONTROL_GestaoProductores.SelectedIndex = 0;
        }

        private void LISTVIEW_GestaoProductores_PRODUCTORES_SelectedIndexChanged(object sender, EventArgs e)
        {
            Preencher_TextBox_Productor();
        }

        private void TEXTBOX_GestaoProductores_PRIMEIRO_NOME_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_GestaoProductores_PRIMEIRO_NOME.Text = Regex.Replace(TEXTBOX_GestaoProductores_PRIMEIRO_NOME.Text, @"[0-9]", string.Empty);
            TEXTBOX_GestaoProductores_PRIMEIRO_NOME.Text = Regex.Replace(TEXTBOX_GestaoProductores_PRIMEIRO_NOME.Text, @"[| \ ! @ # £ $ § % & / { } ( ) [ ] = ? ' » « * + ¨ ª º - _ . : , ; > <]", string.Empty);

            TEXTBOX_GestaoProductores_NOME_COMPLETO.Text = TEXTBOX_GestaoProductores_PRIMEIRO_NOME.Text + " " + TEXTBOX_GestaoProductores_APELIDO.Text;
        }

        private void TEXTBOX_GestaoProductores_APELIDO_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_GestaoProductores_APELIDO.Text = Regex.Replace(TEXTBOX_GestaoProductores_APELIDO.Text, @"[0-9]", string.Empty);
            TEXTBOX_GestaoProductores_APELIDO.Text = Regex.Replace(TEXTBOX_GestaoProductores_APELIDO.Text, @"[| \ ! @ # £ $ § % & / { } ( ) [ ] = ? ' » « * + ¨ ª º - _ . : , ; > <]", string.Empty);

            TEXTBOX_GestaoProductores_NOME_COMPLETO.Text = TEXTBOX_GestaoProductores_PRIMEIRO_NOME.Text + " " + TEXTBOX_GestaoProductores_APELIDO.Text;
        }

        private void TEXTBOX_AdicionarProductor_PRIMEIRO_NOME_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_AdicionarProductor_PRIMEIRO_NOME.Text = Regex.Replace(TEXTBOX_AdicionarProductor_PRIMEIRO_NOME.Text, @"[0-9]", string.Empty);
            TEXTBOX_AdicionarProductor_PRIMEIRO_NOME.Text = Regex.Replace(TEXTBOX_AdicionarProductor_PRIMEIRO_NOME.Text, @"[| \ ! @ # £ $ § % & / { } ( ) [ ] = ? ' » « * + ¨ ª º - _ . : , ; > <]", string.Empty);
        }

        private void TEXTBOX_AdicionarProductor_APELIDO_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_AdicionarProductor_APELIDO.Text = Regex.Replace(TEXTBOX_AdicionarProductor_APELIDO.Text, @"[0-9]", string.Empty);
            TEXTBOX_AdicionarProductor_APELIDO.Text = Regex.Replace(TEXTBOX_AdicionarProductor_APELIDO.Text, @"[| \ ! @ # £ $ § % & / { } ( ) [ ] = ? ' » « * + ¨ ª º - _ . : , ; > <]", string.Empty);
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

                string Query = "SELECT " + CLASS_GLOBAL_VARIABLES.BD + ".productores.ID," + CLASS_GLOBAL_VARIABLES.BD + ".productores.primeiro_nome," + CLASS_GLOBAL_VARIABLES.BD + ".productores.apelido," + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades.nacionalidade FROM " + CLASS_GLOBAL_VARIABLES.BD + ".productores INNER JOIN " + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades ON " + CLASS_GLOBAL_VARIABLES.BD + ".productores.nacionalidade = " + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades.ID";

                MySqlCommand Comando = new MySqlCommand(Query, LigacaoDB);

                Reader = Comando.ExecuteReader();

                while (Reader.Read())
                {
                    var ROW = new ListViewItem();
                    ROW.Text = Reader["ID"].ToString();
                    ROW.SubItems.Add(Reader["primeiro_nome"].ToString());
                    ROW.SubItems.Add(Reader["apelido"].ToString());
                    ROW.SubItems.Add(Reader["nacionalidade"].ToString());

                    LISTVIEW_GestaoProductores_PRODUCTORES.Items.Add(ROW);
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
                LigacaoDB.Open();

                string Query = "SELECT ID,titulo FROM " + CLASS_GLOBAL_VARIABLES.BD + ".filmes";
                MySqlCommand Comando = new MySqlCommand(Query, LigacaoDB);

                Reader = Comando.ExecuteReader();

                while (Reader.Read())
                {
                    var ROW = new ListViewItem();
                    ROW.Text = Reader["ID"].ToString();
                    ROW.SubItems.Add(Reader["titulo"].ToString());

                    var ROW2 = new ListViewItem();
                    ROW2.Text = Reader["ID"].ToString();
                    ROW2.SubItems.Add(Reader["titulo"].ToString());

                    LISTVIEW_GestaoProductores_FILMES.Items.Add(ROW);
                    LISTVIEW_AdicionarProductor_FILMES.Items.Add(ROW2);
                }

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                //MessageBox.Show(EX.Message);
            }
        }

        public void Preencher_ComboBox_Nacionalidades()
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
                    COMBOBOX_GestaoProductores_NACIONALIDADE.Items.Add(Reader.GetString("nacionalidade"));
                    COMBOBOX_AdicionarProductor_NACIONALIDADE.Items.Add(Reader.GetString("nacionalidade"));
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
            LISTVIEW_GestaoProductores_PRODUCTORES.Items.Clear();
        }

        public void LimparTudo()
        {
            TEXTBOX_AdicionarProductor_APELIDO.Text = "";
            TEXTBOX_AdicionarProductor_PRIMEIRO_NOME.Text = "";
            COMBOBOX_AdicionarProductor_NACIONALIDADE.SelectedItem = null;

            TEXTBOX_GestaoProductores_APELIDO.Text = "";
            TEXTBOX_GestaoProductores_NOME_COMPLETO.Text = "";
            TEXTBOX_GestaoProductores_PRIMEIRO_NOME.Text = "";
            COMBOBOX_GestaoProductores_NACIONALIDADE.SelectedItem = null;
        }

        public void Preencher_TextBox_Productor()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                string ListView_ID = LISTVIEW_GestaoProductores_PRODUCTORES.SelectedItems[0].Text;
                int ID = Convert.ToInt32(ListView_ID);

                LigacaoDB.Open();

                string Nome;
                string PrimeiroNome;
                string Apelido;
                int IDNacionalidade;

                string Query_PrimeiroNome = "SELECT primeiro_nome FROM " + CLASS_GLOBAL_VARIABLES.BD + ".productores WHERE ID = " + ID + "";
                string Query_SegundoNome = "SELECT apelido FROM " + CLASS_GLOBAL_VARIABLES.BD + ".productores WHERE ID = " + ID + "";
                string Query_IDNacionalidade = "SELECT nacionalidade FROM " + CLASS_GLOBAL_VARIABLES.BD + ".productores WHERE ID = " + ID + "";

                MySqlCommand Comando_PrimeiroNome = new MySqlCommand(Query_PrimeiroNome, LigacaoDB);
                MySqlCommand Comando_SegundoNome = new MySqlCommand(Query_SegundoNome, LigacaoDB);
                MySqlCommand Comando_IDNacionalidade = new MySqlCommand(Query_IDNacionalidade, LigacaoDB);

                PrimeiroNome = Comando_PrimeiroNome.ExecuteScalar().ToString();
                Apelido = Comando_SegundoNome.ExecuteScalar().ToString();
                Nome = PrimeiroNome + " " + Apelido;
                IDNacionalidade = Convert.ToInt32(Comando_IDNacionalidade.ExecuteScalar());

                TEXTBOX_GestaoProductores_NOME_COMPLETO.Text = Nome;
                TEXTBOX_GestaoProductores_PRIMEIRO_NOME.Text = PrimeiroNome;
                TEXTBOX_GestaoProductores_APELIDO.Text = Apelido;

                if (IDNacionalidade == 1)
                    COMBOBOX_GestaoProductores_NACIONALIDADE.SelectedIndex = 0;
                else
                    COMBOBOX_GestaoProductores_NACIONALIDADE.SelectedIndex = IDNacionalidade - 1;

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                //MessageBox.Show(EX.Message);
                LISTVIEW_GestaoProductores_PRODUCTORES.SelectedItems.Clear();
                TEXTBOX_GestaoProductores_NOME_COMPLETO.Text = "";
                TEXTBOX_GestaoProductores_PRIMEIRO_NOME.Text = "";
                TEXTBOX_GestaoProductores_APELIDO.Text = "";
                COMBOBOX_GestaoProductores_NACIONALIDADE.SelectedItem = null;
            }
        }

        public void Adicionar_Productor()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB.Open();

                string Nome = TEXTBOX_AdicionarProductor_PRIMEIRO_NOME.Text + " " + TEXTBOX_AdicionarProductor_APELIDO.Text;
                string PrimeiroNome = TEXTBOX_AdicionarProductor_PRIMEIRO_NOME.Text;
                string Apelido = TEXTBOX_AdicionarProductor_APELIDO.Text;
                string Nacionalidade = COMBOBOX_AdicionarProductor_NACIONALIDADE.Text;
                int IDNacionalidade;

                string Query_IDNacionalidade = "SELECT ID FROM " + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades WHERE nacionalidade='" + Nacionalidade + "'";

                MySqlCommand Comando_IDNacionalidade = new MySqlCommand(Query_IDNacionalidade, LigacaoDB);
                IDNacionalidade = Convert.ToInt32(Comando_IDNacionalidade.ExecuteScalar());

                LigacaoDB.Close();

                if (LigacaoDB.State == ConnectionState.Open)
                    LigacaoDB.Close();

                LigacaoDB_StoredProcedures.Open();

                MySqlCommand Comando_InserirProductor = new MySqlCommand("SP_PRODUCTOR_INSERIR", LigacaoDB_StoredProcedures);
                Comando_InserirProductor.CommandType = CommandType.StoredProcedure;

                Comando_InserirProductor.Parameters.AddWithValue("_nome_completo", Nome);
                Comando_InserirProductor.Parameters.AddWithValue("_primeiro_nome", PrimeiroNome);
                Comando_InserirProductor.Parameters.AddWithValue("_apelido", Apelido);
                Comando_InserirProductor.Parameters.AddWithValue("_nacionalidade", IDNacionalidade);

                Reader = Comando_InserirProductor.ExecuteReader();

                Reader.Close();

                LigacaoDB_StoredProcedures.Close();

                MessageBox.Show("Productor Inserido com Sucesso", "Productor Inserido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        public void Remover_Productor()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB_StoredProcedures.Open();

                string ListView_ID = LISTVIEW_GestaoProductores_PRODUCTORES.SelectedItems[0].Text;
                int ID = Convert.ToInt32(ListView_ID);

                MySqlCommand Command_RemoverProductor = new MySqlCommand("SP_PRODUCTOR_REMOVER", LigacaoDB_StoredProcedures);
                Command_RemoverProductor.CommandType = CommandType.StoredProcedure;

                Command_RemoverProductor.Parameters.AddWithValue("_id", ID);
                Reader = Command_RemoverProductor.ExecuteReader();

                LigacaoDB_StoredProcedures.Close();

                MessageBox.Show("Productor removido com sucesso", "Productor Removido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception EX)
            { }
        }

        public void Alterar_Productor()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB.Open();

                string ListView_ID = LISTVIEW_GestaoProductores_PRODUCTORES.SelectedItems[0].Text;
                int ID = Convert.ToInt32(ListView_ID);

                string Nome = TEXTBOX_GestaoProductores_PRIMEIRO_NOME.Text + " " + TEXTBOX_GestaoProductores_APELIDO.Text;
                string PrimeiroNome = TEXTBOX_GestaoProductores_PRIMEIRO_NOME.Text;
                string Apelido = TEXTBOX_GestaoProductores_APELIDO.Text;
                string Nacionalidade = COMBOBOX_GestaoProductores_NACIONALIDADE.Text;
                int IDNacionalidade;

                string Query_IDNacionalidade = "SELECT ID FROM " + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades WHERE nacionalidade='" + Nacionalidade + "'";

                MySqlCommand Comando_IDNacionalidade = new MySqlCommand(Query_IDNacionalidade, LigacaoDB);
                IDNacionalidade = Convert.ToInt32(Comando_IDNacionalidade.ExecuteScalar());

                LigacaoDB.Close();

                if (LigacaoDB.State == ConnectionState.Open)
                    LigacaoDB.Close();

                LigacaoDB_StoredProcedures.Open();

                MySqlCommand Comando_AlterarProductor = new MySqlCommand("SP_PRODUCTOR_ALTERAR", LigacaoDB_StoredProcedures);
                Comando_AlterarProductor.CommandType = CommandType.StoredProcedure;

                Comando_AlterarProductor.Parameters.AddWithValue("_id", ID);
                Comando_AlterarProductor.Parameters.AddWithValue("_nome_completo", Nome);
                Comando_AlterarProductor.Parameters.AddWithValue("_primeiro_nome", PrimeiroNome);
                Comando_AlterarProductor.Parameters.AddWithValue("_apelido", Apelido);
                Comando_AlterarProductor.Parameters.AddWithValue("_nacionalidade", IDNacionalidade);

                Reader = Comando_AlterarProductor.ExecuteReader();

                Reader.Close();

                LigacaoDB_StoredProcedures.Close();

                MessageBox.Show("Productor Alterado com Sucesso", "Productor Alterado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        public void Ordenar_Productores()
        {
            if (CHECKBOX_GestaoProductores_AGRUPAR_PRODUCTORES.Checked == true)
            {
                if (LISTVIEW_GestaoProductores_PRODUCTORES.ShowGroups)
                    BuildGroups_Productores(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_GestaoProductores_PRODUCTORES.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);
        }

        public void Ordenar_Filmes()
        {
            if (CHECKBOX_GestaoProductores_AGRUPAR_FILMES.Checked == true)
            {
                if (LISTVIEW_GestaoProductores_FILMES.ShowGroups)
                    BuildGroups_GestaoFilmes(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_GestaoProductores_FILMES.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);

            if (CHECKBOX_AdicionarProductor_AGRUPAR_FILMES.Checked == true)
            {
                if (LISTVIEW_AdicionarProductor_FILMES.ShowGroups)
                    BuildGroups_AdicionarFilmes(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_AdicionarProductor_FILMES.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);
        }

        public void BuildGroups_Productores(int coluna)
        {
            LISTVIEW_GestaoProductores_PRODUCTORES.Groups.Clear();

            int Contagem = LISTVIEW_GestaoProductores_PRODUCTORES.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_GestaoProductores_PRODUCTORES.Items)
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
                LISTVIEW_GestaoProductores_PRODUCTORES.Groups.Add(Grupo);
                Mapear[Grupo.Header].Sort(OrdenarItems);
                Grupo.Items.AddRange(Mapear[Grupo.Header].ToArray());
            }
        }

        public void BuildGroups_GestaoFilmes(int coluna)
        {
            LISTVIEW_GestaoProductores_FILMES.Groups.Clear();

            int Contagem = LISTVIEW_GestaoProductores_FILMES.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_GestaoProductores_FILMES.Items)
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
                LISTVIEW_GestaoProductores_FILMES.Groups.Add(Grupo);
                Mapear[Grupo.Header].Sort(OrdenarItems);
                Grupo.Items.AddRange(Mapear[Grupo.Header].ToArray());
            }
        }

        public void BuildGroups_AdicionarFilmes(int coluna)
        {
            LISTVIEW_AdicionarProductor_FILMES.Groups.Clear();

            int Contagem = LISTVIEW_AdicionarProductor_FILMES.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_AdicionarProductor_FILMES.Items)
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
                LISTVIEW_AdicionarProductor_FILMES.Groups.Add(Grupo);
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

        private void CHECKBOX_GestaoProductores_AGRUPAR_PRODUCTORES_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_GestaoProductores_PRODUCTORES.ShowGroups = CHECKBOX_GestaoProductores_AGRUPAR_PRODUCTORES.Checked;
            Ordenar_Productores();
        }

        private void CHECKBOX_GestaoProductores_AGRUPAR_FILMES_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_GestaoProductores_FILMES.ShowGroups = CHECKBOX_GestaoProductores_AGRUPAR_FILMES.Checked;
            Ordenar_Filmes();
        }

        private void LISTVIEW_GestaoProductores_PRODUCTORES_ColumnClick(object sender, ColumnClickEventArgs e)
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

        private void LISTVIEW_GestaoProductores_FILMES_ColumnClick(object sender, ColumnClickEventArgs e)
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

        private void CHECKBOX_AdicionarProductor_AGRUPAR_FILMES_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_AdicionarProductor_FILMES.ShowGroups = CHECKBOX_AdicionarProductor_AGRUPAR_FILMES.Checked;
            Ordenar_Filmes();
        }        

        private void LISTVIEW_AdicionarProductor_FILMES_ColumnClick(object sender, ColumnClickEventArgs e)
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

        
    }
}
