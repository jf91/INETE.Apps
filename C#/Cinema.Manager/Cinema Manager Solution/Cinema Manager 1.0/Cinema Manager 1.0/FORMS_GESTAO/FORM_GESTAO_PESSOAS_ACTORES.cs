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
    public partial class FORM_GESTAO_PESSOAS_ACTORES : Form
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

        public FORM_GESTAO_PESSOAS_ACTORES()
        {
            InitializeComponent();

            Preencher_ListView_Actores();
            Preencher_ListView_Filmes();
            Preencher_ComboBox_Nacionalidades();
        }

        private void PICTUREBOX_GestaoActores_ADICIONAR_ACTOR_Click(object sender, EventArgs e)
        {
            TABCONTROL_GestaoActores.SelectedIndex = 1;
        }

        private void LISTVIEW_GestaoActores_ACTORES_SelectedIndexChanged(object sender, EventArgs e)
        {
            Preencher_TextBox_Actor();
        }

        private void LISTVIEW_GestaoActores_ACTORES_ColumnClick(object sender, ColumnClickEventArgs e)
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

        private void CHECKBOX_GestaoActores_AGRUPAR_ACTORES_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_GestaoActores_ACTORES.ShowGroups = CHECKBOX_GestaoActores_AGRUPAR_ACTORES.Checked;
            Ordenar_Actores();
        }

        private void LISTVIEW_GestaoActores_FILMES_ColumnClick(object sender, ColumnClickEventArgs e)
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

        private void CHECKBOX_GestaoActores_AGRUPAR_FILMES_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_GestaoActores_FILMES.ShowGroups = CHECKBOX_GestaoActores_AGRUPAR_FILMES.Checked;
            Ordenar_Filmes();
        }

        private void LISTVIEW_AdicionarActor_FILMES_ColumnClick(object sender, ColumnClickEventArgs e)
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

        private void CHECKBOX_AdicionarActor_AGRUPAR_FILMES_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_AdicionarActor_FILMES.ShowGroups = CHECKBOX_AdicionarActor_AGRUPAR_FILMES.Checked;
            Ordenar_Filmes();
        }

        private void BUTTON_AdicionarActor_ADICIONAR_ACTOR_Click(object sender, EventArgs e)
        {
            Adicionar_Actor();
            ClearListView();
            Preencher_ListView_Actores();
            LimparTudo();

            TABCONTROL_GestaoActores.SelectedIndex = 0;
        }

        private void PICTUREBOX_GestaoActores_REMOVER_ACTOR_Click(object sender, EventArgs e)
        {
            if (TEXTBOX_GestaoActores_NOME_COMPLETO.Text == "")
            {
                MessageBox.Show("Não tem nenhum actor selecionado! Não é possivel remover.", "Nenhum actor selecionado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (TEXTBOX_GestaoActores_NOME_COMPLETO.Text != "")
            {
                DialogResult RemoverActor = MessageBox.Show("Tem a certeza que deseja remover este actor?", "Remover Actor?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (RemoverActor == DialogResult.Yes)
                {
                    Remover_Actor();
                    ClearListView();
                    Preencher_ListView_Actores();
                    LimparTudo();
                }
            }
        }

        private void PICTUREBOX_GestaoActores_GRAVAR_ALTERACOES_Click(object sender, EventArgs e)
        {
            if (TEXTBOX_GestaoActores_NOME_COMPLETO.Text == "")
            {
                MessageBox.Show("Não tem nenhum actor selecionado! Não é possivel alterar.", "Nenhum Actor Selecionado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (TEXTBOX_GestaoActores_NOME_COMPLETO.Text != "")
            {
                DialogResult AlterarActor = MessageBox.Show("Tem a certeza que deseja alterar este actor?", "Alterar Actor?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (AlterarActor == DialogResult.Yes)
                {
                    Alterar_Actor();
                    ClearListView();
                    Preencher_ListView_Actores();
                    LimparTudo();
                }
            }
        }

        private void TEXTBOX_GestaoActores_PRIMEIRO_NOME_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_GestaoActores_PRIMEIRO_NOME.Text = Regex.Replace(TEXTBOX_GestaoActores_PRIMEIRO_NOME.Text, @"[0-9]", string.Empty);
            TEXTBOX_GestaoActores_PRIMEIRO_NOME.Text = Regex.Replace(TEXTBOX_GestaoActores_PRIMEIRO_NOME.Text, @"[| \ ! @ # £ $ § % & / { } ( ) [ ] = ? ' » « * + ¨ ª º - _ . : , ; > <]", string.Empty);

            TEXTBOX_GestaoActores_NOME_COMPLETO.Text = TEXTBOX_GestaoActores_PRIMEIRO_NOME.Text + " " + TEXTBOX_GestaoActores_APELIDO.Text;
        }

        private void TEXTBOX_GestaoActores_APELIDO_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_GestaoActores_APELIDO.Text = Regex.Replace(TEXTBOX_GestaoActores_APELIDO.Text, @"[0-9]", string.Empty);
            TEXTBOX_GestaoActores_APELIDO.Text = Regex.Replace(TEXTBOX_GestaoActores_APELIDO.Text, @"[| \ ! @ # £ $ § % & / { } ( ) [ ] = ? ' » « * + ¨ ª º - _ . : , ; > <]", string.Empty);

            TEXTBOX_GestaoActores_NOME_COMPLETO.Text = TEXTBOX_GestaoActores_PRIMEIRO_NOME.Text + " " + TEXTBOX_GestaoActores_APELIDO.Text;
        }

        private void TEXTBOX_AdicionarActor_PRIMEIRO_NOME_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_AdicionarActor_PRIMEIRO_NOME.Text = Regex.Replace(TEXTBOX_AdicionarActor_PRIMEIRO_NOME.Text, @"[0-9]", string.Empty);
            TEXTBOX_AdicionarActor_PRIMEIRO_NOME.Text = Regex.Replace(TEXTBOX_AdicionarActor_PRIMEIRO_NOME.Text, @"[| \ ! @ # £ $ § % & / { } ( ) [ ] = ? ' » « * + ¨ ª º - _ . : , ; > <]", string.Empty);
        }

        private void TEXTBOX_AdicionarActor_APELIDO_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_AdicionarActor_APELIDO.Text = Regex.Replace(TEXTBOX_AdicionarActor_APELIDO.Text, @"[0-9]", string.Empty);
            TEXTBOX_AdicionarActor_APELIDO.Text = Regex.Replace(TEXTBOX_AdicionarActor_APELIDO.Text, @"[| \ ! @ # £ $ § % & / { } ( ) [ ] = ? ' » « * + ¨ ª º - _ . : , ; > <]", string.Empty);
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
                
                string Query = "SELECT " + CLASS_GLOBAL_VARIABLES.BD + ".actores.ID," + CLASS_GLOBAL_VARIABLES.BD + ".actores.primeiro_nome," + CLASS_GLOBAL_VARIABLES.BD + ".actores.apelido," + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades.nacionalidade FROM " + CLASS_GLOBAL_VARIABLES.BD + ".actores INNER JOIN " + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades ON " + CLASS_GLOBAL_VARIABLES.BD + ".actores.nacionalidade = " + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades.ID";

                MySqlCommand Comando = new MySqlCommand(Query, LigacaoDB);               

                Reader = Comando.ExecuteReader();

                while (Reader.Read())
                {
                    var ROW = new ListViewItem();
                    ROW.Text = Reader["ID"].ToString();
                    ROW.SubItems.Add(Reader["primeiro_nome"].ToString());
                    ROW.SubItems.Add(Reader["apelido"].ToString());
                    ROW.SubItems.Add(Reader["nacionalidade"].ToString());

                    LISTVIEW_GestaoActores_ACTORES.Items.Add(ROW);
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

                    LISTVIEW_GestaoActores_FILMES.Items.Add(ROW);
                    LISTVIEW_AdicionarActor_FILMES.Items.Add(ROW2);
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
                    COMBOBOX_GestaoActores_NACIONALIDADE.Items.Add(Reader.GetString("nacionalidade"));
                    COMBOBOX_AdicionarActor_NACIONALIDADE.Items.Add(Reader.GetString("nacionalidade"));
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
            LISTVIEW_GestaoActores_ACTORES.Items.Clear();
        }

        public void LimparTudo()
        {
            TEXTBOX_AdicionarActor_APELIDO.Text = "";
            TEXTBOX_AdicionarActor_PRIMEIRO_NOME.Text = "";
            COMBOBOX_AdicionarActor_NACIONALIDADE.SelectedItem = null;

            TEXTBOX_GestaoActores_APELIDO.Text = "";
            TEXTBOX_GestaoActores_NOME_COMPLETO.Text = "";
            TEXTBOX_GestaoActores_PRIMEIRO_NOME.Text = "";
            COMBOBOX_GestaoActores_NACIONALIDADE.SelectedItem = null;
        }

        public void Preencher_TextBox_Actor()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                string ListView_ID = LISTVIEW_GestaoActores_ACTORES.SelectedItems[0].Text;
                int ID = Convert.ToInt32(ListView_ID);

                LigacaoDB.Open();

                string Nome;
                string PrimeiroNome;
                string Apelido;
                int IDNacionalidade;

                string Query_PrimeiroNome = "SELECT primeiro_nome FROM " + CLASS_GLOBAL_VARIABLES.BD + ".actores WHERE ID = " + ID + "";
                string Query_SegundoNome = "SELECT apelido FROM " + CLASS_GLOBAL_VARIABLES.BD + ".actores WHERE ID = " + ID + "";
                string Query_IDNacionalidade = "SELECT nacionalidade FROM " + CLASS_GLOBAL_VARIABLES.BD + ".actores WHERE ID = " + ID + "";                

                MySqlCommand Comando_PrimeiroNome = new MySqlCommand(Query_PrimeiroNome, LigacaoDB);
                MySqlCommand Comando_SegundoNome = new MySqlCommand(Query_SegundoNome, LigacaoDB);
                MySqlCommand Comando_IDNacionalidade = new MySqlCommand(Query_IDNacionalidade, LigacaoDB);

                PrimeiroNome = Comando_PrimeiroNome.ExecuteScalar().ToString();
                Apelido = Comando_SegundoNome.ExecuteScalar().ToString();
                Nome = PrimeiroNome + " " + Apelido;
                IDNacionalidade = Convert.ToInt32(Comando_IDNacionalidade.ExecuteScalar());

                TEXTBOX_GestaoActores_NOME_COMPLETO.Text = Nome;
                TEXTBOX_GestaoActores_PRIMEIRO_NOME.Text = PrimeiroNome;
                TEXTBOX_GestaoActores_APELIDO.Text = Apelido;

                if(IDNacionalidade == 1)
                    COMBOBOX_GestaoActores_NACIONALIDADE.SelectedIndex = 0;
                else
                    COMBOBOX_GestaoActores_NACIONALIDADE.SelectedIndex = IDNacionalidade - 1;

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                //MessageBox.Show(EX.Message);
                LISTVIEW_GestaoActores_FILMES.SelectedItems.Clear();
                TEXTBOX_GestaoActores_NOME_COMPLETO.Text = "";
                TEXTBOX_GestaoActores_PRIMEIRO_NOME.Text = "";
                TEXTBOX_GestaoActores_APELIDO.Text = "";
                COMBOBOX_GestaoActores_NACIONALIDADE.SelectedItem = null;
            }
        }

        public void Adicionar_Actor()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB.Open();

                string Nome = TEXTBOX_AdicionarActor_PRIMEIRO_NOME.Text + " " + TEXTBOX_AdicionarActor_APELIDO.Text;
                string PrimeiroNome = TEXTBOX_AdicionarActor_PRIMEIRO_NOME.Text;
                string Apelido = TEXTBOX_AdicionarActor_APELIDO.Text;
                string Nacionalidade = COMBOBOX_AdicionarActor_NACIONALIDADE.Text;
                int IDNacionalidade;

                string Query_IDNacionalidade = "SELECT ID FROM " + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades WHERE nacionalidade='" + Nacionalidade + "'";

                MySqlCommand Comando_IDNacionalidade = new MySqlCommand(Query_IDNacionalidade, LigacaoDB);
                IDNacionalidade = Convert.ToInt32(Comando_IDNacionalidade.ExecuteScalar());

                LigacaoDB.Close();

                if (LigacaoDB.State == ConnectionState.Open)
                    LigacaoDB.Close();

                LigacaoDB_StoredProcedures.Open();

                MySqlCommand Comando_InserirActor = new MySqlCommand("SP_ACTOR_INSERIR", LigacaoDB_StoredProcedures);
                Comando_InserirActor.CommandType = CommandType.StoredProcedure;

                Comando_InserirActor.Parameters.AddWithValue("_nome_completo", Nome);
                Comando_InserirActor.Parameters.AddWithValue("_primeiro_nome", PrimeiroNome);
                Comando_InserirActor.Parameters.AddWithValue("_apelido", Apelido);
                Comando_InserirActor.Parameters.AddWithValue("_nacionalidade", IDNacionalidade);

                Reader = Comando_InserirActor.ExecuteReader();

                Reader.Close();

                LigacaoDB_StoredProcedures.Close();

                MessageBox.Show("Actor Inserido com Sucesso", "Actor Inserido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        public void Remover_Actor()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB_StoredProcedures.Open();

                string ListView_ID = LISTVIEW_GestaoActores_ACTORES.SelectedItems[0].Text;
                int ID = Convert.ToInt32(ListView_ID);

                MySqlCommand Command_RemoverActor = new MySqlCommand("SP_ACTOR_REMOVER", LigacaoDB_StoredProcedures);
                Command_RemoverActor.CommandType = CommandType.StoredProcedure;

                Command_RemoverActor.Parameters.AddWithValue("_id", ID);
                Reader = Command_RemoverActor.ExecuteReader();

                LigacaoDB_StoredProcedures.Close();

                MessageBox.Show("Actor removido com sucesso", "Actor Removido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception EX)
            { }
        }

        public void Alterar_Actor()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB.Open();

                string ListView_ID = LISTVIEW_GestaoActores_ACTORES.SelectedItems[0].Text;
                int ID = Convert.ToInt32(ListView_ID);

                string Nome = TEXTBOX_GestaoActores_PRIMEIRO_NOME.Text + " " + TEXTBOX_GestaoActores_APELIDO.Text;
                string PrimeiroNome = TEXTBOX_GestaoActores_PRIMEIRO_NOME.Text;
                string Apelido = TEXTBOX_GestaoActores_APELIDO.Text;
                string Nacionalidade = COMBOBOX_GestaoActores_NACIONALIDADE.Text;
                int IDNacionalidade;

                string Query_IDNacionalidade = "SELECT ID FROM " + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades WHERE nacionalidade='" + Nacionalidade + "'";

                MySqlCommand Comando_IDNacionalidade = new MySqlCommand(Query_IDNacionalidade, LigacaoDB);
                IDNacionalidade = Convert.ToInt32(Comando_IDNacionalidade.ExecuteScalar());

                LigacaoDB.Close();

                if (LigacaoDB.State == ConnectionState.Open)
                    LigacaoDB.Close();

                LigacaoDB_StoredProcedures.Open();

                MySqlCommand Comando_AlterarActor = new MySqlCommand("SP_ACTOR_ALTERAR", LigacaoDB_StoredProcedures);
                Comando_AlterarActor.CommandType = CommandType.StoredProcedure;

                Comando_AlterarActor.Parameters.AddWithValue("_id", ID);
                Comando_AlterarActor.Parameters.AddWithValue("_nome_completo", Nome);
                Comando_AlterarActor.Parameters.AddWithValue("_primeiro_nome", PrimeiroNome);
                Comando_AlterarActor.Parameters.AddWithValue("_apelido", Apelido);
                Comando_AlterarActor.Parameters.AddWithValue("_nacionalidade", IDNacionalidade);

                Reader = Comando_AlterarActor.ExecuteReader();

                Reader.Close();

                LigacaoDB_StoredProcedures.Close();

                MessageBox.Show("Actor Alterado com Sucesso", "Actor Alterado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        public void Ordenar_Actores()
        {
            if (CHECKBOX_GestaoActores_AGRUPAR_ACTORES.Checked == true)
            {
                if (LISTVIEW_GestaoActores_ACTORES.ShowGroups)
                    BuildGroups_Actores(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_GestaoActores_ACTORES.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);
        }

        public void Ordenar_Filmes()
        {
            if (CHECKBOX_GestaoActores_AGRUPAR_FILMES.Checked == true)
            {
                if (LISTVIEW_GestaoActores_FILMES.ShowGroups)
                    BuildGroups_GestaoFilmes(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_GestaoActores_FILMES.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);

            if (CHECKBOX_AdicionarActor_AGRUPAR_FILMES.Checked == true)
            {
                if (LISTVIEW_AdicionarActor_FILMES.ShowGroups)
                    BuildGroups_AdicionarFilmes(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_AdicionarActor_FILMES.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);
        }

        public void BuildGroups_Actores(int coluna)
        {
            LISTVIEW_GestaoActores_ACTORES.Groups.Clear();

            int Contagem = LISTVIEW_GestaoActores_ACTORES.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_GestaoActores_ACTORES.Items)
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
                LISTVIEW_GestaoActores_ACTORES.Groups.Add(Grupo);
                Mapear[Grupo.Header].Sort(OrdenarItems);
                Grupo.Items.AddRange(Mapear[Grupo.Header].ToArray());
            }
        }        

        public void BuildGroups_GestaoFilmes(int coluna)
        {
            LISTVIEW_GestaoActores_FILMES.Groups.Clear();

            int Contagem = LISTVIEW_GestaoActores_FILMES.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_GestaoActores_FILMES.Items)
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
                LISTVIEW_GestaoActores_FILMES.Groups.Add(Grupo);
                Mapear[Grupo.Header].Sort(OrdenarItems);
                Grupo.Items.AddRange(Mapear[Grupo.Header].ToArray());
            }
        }

        public void BuildGroups_AdicionarFilmes(int coluna)
        {
            LISTVIEW_AdicionarActor_FILMES.Groups.Clear();

            int Contagem = LISTVIEW_AdicionarActor_FILMES.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_AdicionarActor_FILMES.Items)
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
                LISTVIEW_AdicionarActor_FILMES.Groups.Add(Grupo);
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
