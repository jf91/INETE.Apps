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
    public partial class FORM_GESTAO_PESSOAS_REALIZADORES : Form
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

        public FORM_GESTAO_PESSOAS_REALIZADORES()
        {
            InitializeComponent();

            Preencher_ListView_Realizadores();
            Preencher_ListView_Filmes();
            Preencher_ComboBox_Nacionalidades();
        }

        private void PICTUREBOX_GestaoRealizadores_ADICIONAR_REALIZADOR_Click(object sender, EventArgs e)
        {
            TABCONTROL_GestaoRealizadores.SelectedIndex = 1;
        }

        private void PICTUREBOX_GestaoRealizadores_REMOVER_REALIZADOR_Click(object sender, EventArgs e)
        {
            if (TEXTBOX_GestaoRealizadores_NOME_COMPLETO.Text == "")
            {
                MessageBox.Show("Não tem nenhum realizador selecionado! Não é possivel remover.", "Nenhum realizador selecionado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (TEXTBOX_GestaoRealizadores_NOME_COMPLETO.Text != "")
            {
                DialogResult RemoverRealizador = MessageBox.Show("Tem a certeza que deseja remover este realizador?", "Remover Realizador?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (RemoverRealizador == DialogResult.Yes)
                {
                    Remover_Realizador();
                    ClearListView();
                    Preencher_ListView_Realizadores();
                    LimparTudo();
                }
            }
        }

        private void PICTUREBOX_GestaoRealizadores_GRAVAR_ALTERACOES_Click(object sender, EventArgs e)
        {
            if (TEXTBOX_GestaoRealizadores_NOME_COMPLETO.Text == "")
            {
                MessageBox.Show("Não tem nenhum realizador selecionado! Não é possivel remover.", "Nenhum realizador selecionado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (TEXTBOX_GestaoRealizadores_NOME_COMPLETO.Text != "")
            {
                DialogResult AlterarRealizador = MessageBox.Show("Tem a certeza que deseja alterar este realizador?", "Alterar Realizador?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (AlterarRealizador == DialogResult.Yes)
                {
                    Alterar_Actor();
                    ClearListView();
                    Preencher_ListView_Realizadores();
                    LimparTudo();
                }
            }
        }

        private void BUTTON_AdicionarRealizador_ADICIONAR_REALIZADOR_Click(object sender, EventArgs e)
        {
            Adicionar_Realizador();
            ClearListView();
            Preencher_ListView_Realizadores();
            LimparTudo();

            TABCONTROL_GestaoRealizadores.SelectedIndex = 0;
        }

        private void LISTVIEW_GestaoRealizadores_REALIZADORES_SelectedIndexChanged(object sender, EventArgs e)
        {
            Preencher_TextBox_Realizador();
        }

        private void TEXTBOX_GestaoRealizadores_PRIMEIRO_NOME_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_GestaoRealizadores_PRIMEIRO_NOME.Text = Regex.Replace(TEXTBOX_GestaoRealizadores_PRIMEIRO_NOME.Text, @"[0-9]", string.Empty);
            TEXTBOX_GestaoRealizadores_PRIMEIRO_NOME.Text = Regex.Replace(TEXTBOX_GestaoRealizadores_PRIMEIRO_NOME.Text, @"[| \ ! @ # £ $ § % & / { } ( ) [ ] = ? ' » « * + ¨ ª º - _ . : , ; > <]", string.Empty);

            TEXTBOX_GestaoRealizadores_NOME_COMPLETO.Text = TEXTBOX_GestaoRealizadores_PRIMEIRO_NOME.Text + " " + TEXTBOX_GestaoRealizadores_APELIDO.Text;
        }

        private void TEXTBOX_GestaoRealizadores_APELIDO_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_GestaoRealizadores_APELIDO.Text = Regex.Replace(TEXTBOX_GestaoRealizadores_APELIDO.Text, @"[0-9]", string.Empty);
            TEXTBOX_GestaoRealizadores_APELIDO.Text = Regex.Replace(TEXTBOX_GestaoRealizadores_APELIDO.Text, @"[| \ ! @ # £ $ § % & / { } ( ) [ ] = ? ' » « * + ¨ ª º - _ . : , ; > <]", string.Empty);

            TEXTBOX_GestaoRealizadores_NOME_COMPLETO.Text = TEXTBOX_GestaoRealizadores_PRIMEIRO_NOME.Text + " " + TEXTBOX_GestaoRealizadores_APELIDO.Text;
        }

        private void TEXTBOX_AdicionarRealizador_PRIMEIRO_NOME_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_AdicionarRealizador_PRIMEIRO_NOME.Text = Regex.Replace(TEXTBOX_AdicionarRealizador_PRIMEIRO_NOME.Text, @"[0-9]", string.Empty);
            TEXTBOX_AdicionarRealizador_PRIMEIRO_NOME.Text = Regex.Replace(TEXTBOX_AdicionarRealizador_PRIMEIRO_NOME.Text, @"[| \ ! @ # £ $ § % & / { } ( ) [ ] = ? ' » « * + ¨ ª º - _ . : , ; > <]", string.Empty);
        }

        private void TEXTBOX_AdicionarRealizador_APELIDO_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_AdicionarRealizador_APELIDO.Text = Regex.Replace(TEXTBOX_AdicionarRealizador_APELIDO.Text, @"[0-9]", string.Empty);
            TEXTBOX_AdicionarRealizador_APELIDO.Text = Regex.Replace(TEXTBOX_AdicionarRealizador_APELIDO.Text, @"[| \ ! @ # £ $ § % & / { } ( ) [ ] = ? ' » « * + ¨ ª º - _ . : , ; > <]", string.Empty);
        }

        private void LISTVIEW_GestaoRealizadores_REALIZADORES_ColumnClick(object sender, ColumnClickEventArgs e)
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

        private void CHECKBOX_GestaoRealizadores_AGRUPAR_REALIZADORES_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_GestaoRealizadores_REALIZADORES.ShowGroups = CHECKBOX_GestaoRealizadores_AGRUPAR_REALIZADORES.Checked;
            Ordenar_Realizadores();
        }

        private void LISTVIEW_GestaoRealizadores_FILMES_ColumnClick(object sender, ColumnClickEventArgs e)
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

        private void CHECKBOX_GestaoRealizadores_AGRUPAR_FILMES_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_GestaoRealizadores_FILMES.ShowGroups = CHECKBOX_GestaoRealizadores_AGRUPAR_FILMES.Checked;
            Ordenar_Filmes();
        }

        private void LISTVIEW_AdicionarRealizador_FILMES_ColumnClick(object sender, ColumnClickEventArgs e)
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

        private void CHECKBOX_AdicionarRealizador_AGRUPAR_FILMES_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_AdicionarRealizador_FILMES.ShowGroups = CHECKBOX_AdicionarRealizador_AGRUPAR_FILMES.Checked;
            Ordenar_Filmes();
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

                string Query = "SELECT " + CLASS_GLOBAL_VARIABLES.BD + ".realizadores.ID," + CLASS_GLOBAL_VARIABLES.BD + ".realizadores.primeiro_nome," + CLASS_GLOBAL_VARIABLES.BD + ".realizadores.apelido," + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades.nacionalidade FROM " + CLASS_GLOBAL_VARIABLES.BD + ".realizadores INNER JOIN " + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades ON " + CLASS_GLOBAL_VARIABLES.BD + ".realizadores.nacionalidade = " + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades.ID";

                MySqlCommand Comando = new MySqlCommand(Query, LigacaoDB);

                Reader = Comando.ExecuteReader();

                while (Reader.Read())
                {
                    var ROW = new ListViewItem();
                    ROW.Text = Reader["ID"].ToString();
                    ROW.SubItems.Add(Reader["primeiro_nome"].ToString());
                    ROW.SubItems.Add(Reader["apelido"].ToString());
                    ROW.SubItems.Add(Reader["nacionalidade"].ToString());

                    LISTVIEW_GestaoRealizadores_REALIZADORES.Items.Add(ROW);
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

                    LISTVIEW_GestaoRealizadores_FILMES.Items.Add(ROW);
                    LISTVIEW_AdicionarRealizador_FILMES.Items.Add(ROW2);
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
                    COMBOBOX_GestaoRealizadores_NACIONALIDADE.Items.Add(Reader.GetString("nacionalidade"));
                    COMBOBOX_AdicionarRealizador_NACIONALIDADE.Items.Add(Reader.GetString("nacionalidade"));
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
            LISTVIEW_GestaoRealizadores_REALIZADORES.Items.Clear();
        }

        public void LimparTudo()
        {
            TEXTBOX_AdicionarRealizador_APELIDO.Text = "";
            TEXTBOX_AdicionarRealizador_PRIMEIRO_NOME.Text = "";
            COMBOBOX_AdicionarRealizador_NACIONALIDADE.SelectedItem = null;

            TEXTBOX_GestaoRealizadores_APELIDO.Text = "";
            TEXTBOX_GestaoRealizadores_NOME_COMPLETO.Text = "";
            TEXTBOX_GestaoRealizadores_PRIMEIRO_NOME.Text = "";
            COMBOBOX_GestaoRealizadores_NACIONALIDADE.SelectedItem = null;
        }

        public void Preencher_TextBox_Realizador()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                string ListView_ID = LISTVIEW_GestaoRealizadores_REALIZADORES.SelectedItems[0].Text;
                int ID = Convert.ToInt32(ListView_ID);

                LigacaoDB.Open();

                string Nome;
                string PrimeiroNome;
                string Apelido;
                int IDNacionalidade;

                string Query_PrimeiroNome = "SELECT primeiro_nome FROM " + CLASS_GLOBAL_VARIABLES.BD + ".realizadores WHERE ID = " + ID + "";
                string Query_SegundoNome = "SELECT apelido FROM " + CLASS_GLOBAL_VARIABLES.BD + ".realizadores WHERE ID = " + ID + "";
                string Query_IDNacionalidade = "SELECT nacionalidade FROM " + CLASS_GLOBAL_VARIABLES.BD + ".realizadores WHERE ID = " + ID + "";

                MySqlCommand Comando_PrimeiroNome = new MySqlCommand(Query_PrimeiroNome, LigacaoDB);
                MySqlCommand Comando_SegundoNome = new MySqlCommand(Query_SegundoNome, LigacaoDB);
                MySqlCommand Comando_IDNacionalidade = new MySqlCommand(Query_IDNacionalidade, LigacaoDB);

                PrimeiroNome = Comando_PrimeiroNome.ExecuteScalar().ToString();
                Apelido = Comando_SegundoNome.ExecuteScalar().ToString();
                Nome = PrimeiroNome + " " + Apelido;
                IDNacionalidade = Convert.ToInt32(Comando_IDNacionalidade.ExecuteScalar());

                TEXTBOX_GestaoRealizadores_NOME_COMPLETO.Text = Nome;
                TEXTBOX_GestaoRealizadores_PRIMEIRO_NOME.Text = PrimeiroNome;
                TEXTBOX_GestaoRealizadores_APELIDO.Text = Apelido;

                if (IDNacionalidade == 1)
                    COMBOBOX_GestaoRealizadores_NACIONALIDADE.SelectedIndex = 0;
                else
                    COMBOBOX_GestaoRealizadores_NACIONALIDADE.SelectedIndex = IDNacionalidade - 1;

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                //MessageBox.Show(EX.Message);
                LISTVIEW_GestaoRealizadores_REALIZADORES.SelectedItems.Clear();
                TEXTBOX_GestaoRealizadores_NOME_COMPLETO.Text = "";
                TEXTBOX_GestaoRealizadores_PRIMEIRO_NOME.Text = "";
                TEXTBOX_GestaoRealizadores_APELIDO.Text = "";
                COMBOBOX_GestaoRealizadores_NACIONALIDADE.SelectedItem = null;
            }
        }

        public void Adicionar_Realizador()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB.Open();

                string Nome = TEXTBOX_AdicionarRealizador_PRIMEIRO_NOME.Text + " " + TEXTBOX_AdicionarRealizador_APELIDO.Text;
                string PrimeiroNome = TEXTBOX_AdicionarRealizador_PRIMEIRO_NOME.Text;
                string Apelido = TEXTBOX_AdicionarRealizador_APELIDO.Text;
                string Nacionalidade = COMBOBOX_AdicionarRealizador_NACIONALIDADE.Text;
                int IDNacionalidade;

                string Query_IDNacionalidade = "SELECT ID FROM " + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades WHERE nacionalidade='" + Nacionalidade + "'";

                MySqlCommand Comando_IDNacionalidade = new MySqlCommand(Query_IDNacionalidade, LigacaoDB);
                IDNacionalidade = Convert.ToInt32(Comando_IDNacionalidade.ExecuteScalar());

                LigacaoDB.Close();

                if (LigacaoDB.State == ConnectionState.Open)
                    LigacaoDB.Close();

                LigacaoDB_StoredProcedures.Open();

                MySqlCommand Comando_InserirRealizador = new MySqlCommand("SP_REALIZADOR_INSERIR", LigacaoDB_StoredProcedures);
                Comando_InserirRealizador.CommandType = CommandType.StoredProcedure;

                Comando_InserirRealizador.Parameters.AddWithValue("_nome_completo", Nome);
                Comando_InserirRealizador.Parameters.AddWithValue("_primeiro_nome", PrimeiroNome);
                Comando_InserirRealizador.Parameters.AddWithValue("_apelido", Apelido);
                Comando_InserirRealizador.Parameters.AddWithValue("_nacionalidade", IDNacionalidade);

                Reader = Comando_InserirRealizador.ExecuteReader();

                Reader.Close();

                LigacaoDB_StoredProcedures.Close();

                MessageBox.Show("Realizador Inserido com Sucesso", "Realizador Inserido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        public void Remover_Realizador()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB_StoredProcedures.Open();

                string ListView_ID = LISTVIEW_GestaoRealizadores_REALIZADORES.SelectedItems[0].Text;
                int ID = Convert.ToInt32(ListView_ID);

                MySqlCommand Command_RemoverRealizador = new MySqlCommand("SP_REALIZADOR_REMOVER", LigacaoDB_StoredProcedures);
                Command_RemoverRealizador.CommandType = CommandType.StoredProcedure;

                Command_RemoverRealizador.Parameters.AddWithValue("_id", ID);
                Reader = Command_RemoverRealizador.ExecuteReader();

                LigacaoDB_StoredProcedures.Close();

                MessageBox.Show("Realizador removido com sucesso", "Realizador Removido", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                string ListView_ID = LISTVIEW_GestaoRealizadores_REALIZADORES.SelectedItems[0].Text;
                int ID = Convert.ToInt32(ListView_ID);

                string Nome = TEXTBOX_GestaoRealizadores_PRIMEIRO_NOME.Text + " " + TEXTBOX_GestaoRealizadores_APELIDO.Text;
                string PrimeiroNome = TEXTBOX_GestaoRealizadores_PRIMEIRO_NOME.Text;
                string Apelido = TEXTBOX_GestaoRealizadores_APELIDO.Text;
                string Nacionalidade = COMBOBOX_GestaoRealizadores_NACIONALIDADE.Text;
                int IDNacionalidade;

                string Query_IDNacionalidade = "SELECT ID FROM " + CLASS_GLOBAL_VARIABLES.BD + ".nacionalidades WHERE nacionalidade='" + Nacionalidade + "'";

                MySqlCommand Comando_IDNacionalidade = new MySqlCommand(Query_IDNacionalidade, LigacaoDB);
                IDNacionalidade = Convert.ToInt32(Comando_IDNacionalidade.ExecuteScalar());

                LigacaoDB.Close();

                if (LigacaoDB.State == ConnectionState.Open)
                    LigacaoDB.Close();

                LigacaoDB_StoredProcedures.Open();

                MySqlCommand Comando_AlterarRealizador = new MySqlCommand("SP_REALIZADOR_ALTERAR", LigacaoDB_StoredProcedures);
                Comando_AlterarRealizador.CommandType = CommandType.StoredProcedure;

                Comando_AlterarRealizador.Parameters.AddWithValue("_id", ID);
                Comando_AlterarRealizador.Parameters.AddWithValue("_nome_completo", Nome);
                Comando_AlterarRealizador.Parameters.AddWithValue("_primeiro_nome", PrimeiroNome);
                Comando_AlterarRealizador.Parameters.AddWithValue("_apelido", Apelido);
                Comando_AlterarRealizador.Parameters.AddWithValue("_nacionalidade", IDNacionalidade);

                Reader = Comando_AlterarRealizador.ExecuteReader();

                Reader.Close();

                LigacaoDB_StoredProcedures.Close();

                MessageBox.Show("Realizador Alterado com Sucesso", "Realizador Alterado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        public void Ordenar_Realizadores()
        {
            if (CHECKBOX_GestaoRealizadores_AGRUPAR_REALIZADORES.Checked == true)
            {
                if (LISTVIEW_GestaoRealizadores_REALIZADORES.ShowGroups)
                    BuildGroups_Realizadores(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_GestaoRealizadores_REALIZADORES.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);
        }

        public void Ordenar_Filmes()
        {
            if (CHECKBOX_GestaoRealizadores_AGRUPAR_FILMES.Checked == true)
            {
                if (LISTVIEW_GestaoRealizadores_FILMES.ShowGroups)
                    BuildGroups_GestaoFilmes(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_GestaoRealizadores_FILMES.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);

            if (CHECKBOX_AdicionarRealizador_AGRUPAR_FILMES.Checked == true)
            {
                if (LISTVIEW_AdicionarRealizador_FILMES.ShowGroups)
                    BuildGroups_AdicionarFilmes(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_AdicionarRealizador_FILMES.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);
        }

        public void BuildGroups_Realizadores(int coluna)
        {
            LISTVIEW_GestaoRealizadores_REALIZADORES.Groups.Clear();

            int Contagem = LISTVIEW_GestaoRealizadores_REALIZADORES.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_GestaoRealizadores_REALIZADORES.Items)
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
                LISTVIEW_GestaoRealizadores_REALIZADORES.Groups.Add(Grupo);
                Mapear[Grupo.Header].Sort(OrdenarItems);
                Grupo.Items.AddRange(Mapear[Grupo.Header].ToArray());
            }
        }

        public void BuildGroups_GestaoFilmes(int coluna)
        {
            LISTVIEW_GestaoRealizadores_FILMES.Groups.Clear();

            int Contagem = LISTVIEW_GestaoRealizadores_FILMES.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_GestaoRealizadores_FILMES.Items)
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
                LISTVIEW_GestaoRealizadores_FILMES.Groups.Add(Grupo);
                Mapear[Grupo.Header].Sort(OrdenarItems);
                Grupo.Items.AddRange(Mapear[Grupo.Header].ToArray());
            }
        }

        public void BuildGroups_AdicionarFilmes(int coluna)
        {
            LISTVIEW_AdicionarRealizador_FILMES.Groups.Clear();

            int Contagem = LISTVIEW_AdicionarRealizador_FILMES.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_AdicionarRealizador_FILMES.Items)
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
                LISTVIEW_AdicionarRealizador_FILMES.Groups.Add(Grupo);
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
