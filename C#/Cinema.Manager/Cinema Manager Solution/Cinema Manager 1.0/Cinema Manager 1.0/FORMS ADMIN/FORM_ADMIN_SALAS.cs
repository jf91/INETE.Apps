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
using System.Text.RegularExpressions;

namespace Cinema_Manager_1._0.FORMS_ADMIN
{
    public partial class FORM_ADMIN_SALAS : Form
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

        public FORM_ADMIN_SALAS()
        {
            InitializeComponent();

            Preencher_ListView();
        }

        private void LISTVIEW_ADMIN_SALAS_SelectedIndexChanged(object sender, EventArgs e)
        {
            Preencher_TextBoxs();
        }

        private void PICTUREBOX_ADICIONAR_SALA_Click(object sender, EventArgs e)
        {
            TABCONTROL_ADMIN_SALAS.SelectedIndex = 1;
        }

        private void BUTTON__ADICIONAR_SALA_Click(object sender, EventArgs e)
        {
            if (TEXTBOX_ADICIONAR_SALA_CAPACIDADE.Text == "")
            {
                MessageBox.Show("Não indicou a capacidade para a nova sala! Não é possivel inserir.", "Capacidade não Indicada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (TEXTBOX_ADICIONAR_SALA_CAPACIDADE.Text != "")
            {
                DialogResult DialogResult_InserirSala = MessageBox.Show("Tem a certeza que deseja inserir uma nova sala com Capacidade de: " + TEXTBOX_ADICIONAR_SALA_CAPACIDADE.Text + " Lugares?", "Inserir Sala?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DialogResult_InserirSala == DialogResult.Yes)
                {
                    Inserir_Sala();
                    Limpar_TextBoxs();
                    ClearListView();
                    Preencher_ListView();

                    TABCONTROL_ADMIN_SALAS.SelectedIndex = 0;
                }
            }
        }

        private void PICTUREBOX_SALAS_GRAVAR_ALTERACOES_Click(object sender, EventArgs e)
        {
            if (TEXTBOX_SALAS_N_IDENTIFICACAO.Text == "")
            {
                MessageBox.Show("Não tem nenhuma sala selecionada! Não é possivel alterar.", "Nenhuma Sala Selecionada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (TEXTBOX_SALAS_N_IDENTIFICACAO.Text != "")
            {
                DialogResult DialogResult_AlterarSala = MessageBox.Show("Tem a certeza que deseja alterar a sala selecionada?", "Alterar Sala?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DialogResult_AlterarSala == DialogResult.Yes)
                {
                    Alterar_Sala();
                    Limpar_TextBoxs();
                    ClearListView();
                    Preencher_ListView();
                }
            }
        }

        private void PICTUREBOX_SALAS_REMOVER_SALA_Click(object sender, EventArgs e)
        {
            if (TEXTBOX_SALAS_N_IDENTIFICACAO.Text == "")
            {
                MessageBox.Show("Não tem nenhuma sala selecionada! Não é possivel remover.", "Nenhuma Sala Selecionada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (TEXTBOX_SALAS_N_IDENTIFICACAO.Text != "")
            {
                DialogResult DialogResult_RemoverSala = MessageBox.Show("Tem a certeza que deseja remover a sala selecionada?", "Remover Sala?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DialogResult_RemoverSala == DialogResult.Yes)
                {
                    Remover_Sala();
                    Limpar_TextBoxs();
                    ClearListView();
                    Preencher_ListView();
                }
            }
        }

        private void TEXTBOX_SALAS_CAPACIDADE_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_SALAS_CAPACIDADE.Text = Regex.Replace(TEXTBOX_SALAS_CAPACIDADE.Text, @"[^\d]", string.Empty);
        }

        private void TEXTBOX_ADICIONAR_SALA_CAPACIDADE_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_ADICIONAR_SALA_CAPACIDADE.Text = Regex.Replace(TEXTBOX_ADICIONAR_SALA_CAPACIDADE.Text, @"[^\d]", string.Empty);
        }

        private void LISTVIEW_ADMIN_SALAS_ColumnClick(object sender, ColumnClickEventArgs e)
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

            Ordenar();
        }

        private void CHECKBOX_AGRUPAR_SALAS_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_ADMIN_SALAS.ShowGroups = CHECKBOX_AGRUPAR_SALAS.Checked;
            Ordenar();
        } 

        public void Preencher_ListView()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB.Open();

                string Query = "SELECT ID,capacidade FROM " + CLASS_GLOBAL_VARIABLES.BD + ".salas";
                MySqlCommand Comando = new MySqlCommand(Query, LigacaoDB);

                Reader = Comando.ExecuteReader();

                while (Reader.Read())
                {
                    var ROW = new ListViewItem();
                    ROW.Text = Reader["ID"].ToString();
                    ROW.SubItems.Add(Reader["capacidade"].ToString());
                    LISTVIEW_ADMIN_SALAS.Items.Add(ROW);
                }

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }
       
        public void ClearListView()
        {
            LISTVIEW_ADMIN_SALAS.Items.Clear();
        }

        public void Preencher_TextBoxs()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                string ListView_ID = LISTVIEW_ADMIN_SALAS.SelectedItems[0].Text;
                int ID = Convert.ToInt32(ListView_ID);
                int Capacidade;

                LigacaoDB.Open();

                string Query_Capacidade = "SELECT capacidade FROM " + CLASS_GLOBAL_VARIABLES.BD + ".salas WHERE ID = " + ID + "";

                MySqlCommand Comando_Capacidade = new MySqlCommand(Query_Capacidade, LigacaoDB);

                Capacidade = Convert.ToInt32(Comando_Capacidade.ExecuteScalar());

                TEXTBOX_SALAS_N_IDENTIFICACAO.Text = ID.ToString();
                TEXTBOX_SALAS_CAPACIDADE.Text = Capacidade.ToString();

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                //MessageBox.Show(EX.Message);
            }
        }

        public void Limpar_TextBoxs()
        {
            TEXTBOX_SALAS_N_IDENTIFICACAO.Text = "";
            TEXTBOX_SALAS_CAPACIDADE.Text =

            TEXTBOX_ADICIONAR_SALA_CAPACIDADE.Text = "";
        }

        //public void Inserir_IdSala() { }

        public void Inserir_Sala()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB_StoredProcedures.Open();

                int Capacidade = Convert.ToInt32(TEXTBOX_ADICIONAR_SALA_CAPACIDADE.Text);

                MySqlCommand Comando_SalaInserir = new MySqlCommand("SP_SALAS_INSERIR", LigacaoDB_StoredProcedures);
                Comando_SalaInserir.CommandType = CommandType.StoredProcedure;

                Comando_SalaInserir.Parameters.AddWithValue("_capacidade", Capacidade);

                Reader = Comando_SalaInserir.ExecuteReader();

                Reader.Close();

                LigacaoDB_StoredProcedures.Close();

                MessageBox.Show("Sala inserida com Sucesso", "Sala Inserida", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        public void Alterar_Sala()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB_StoredProcedures.Open();

                int ID = Convert.ToInt32(TEXTBOX_SALAS_N_IDENTIFICACAO.Text);
                int Capacidade = Convert.ToInt32(TEXTBOX_SALAS_CAPACIDADE.Text);

                MySqlCommand Comando = new MySqlCommand("SP_SALAS_ALTERAR", LigacaoDB_StoredProcedures);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Parameters.AddWithValue("_id", ID);
                Comando.Parameters.AddWithValue("_capacidade", Capacidade);

                Comando.ExecuteNonQuery();

                LigacaoDB_StoredProcedures.Close();

                MessageBox.Show("Sala alterada com Sucesso", "Sala Alterada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception EX)
            { }
        }

        public void Remover_Sala()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB_StoredProcedures.Open();

                int ID = Convert.ToInt32(TEXTBOX_SALAS_N_IDENTIFICACAO.Text);

                MySqlCommand Comando = new MySqlCommand("SP_SALAS_REMOVER", LigacaoDB_StoredProcedures);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Parameters.AddWithValue("_id", ID);

                Comando.ExecuteNonQuery();

                LigacaoDB_StoredProcedures.Close();

                MessageBox.Show("Sala removida com Sucesso", "Sala Removida", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception EX)
            { }
        }

        public void Ordenar()
        {
            if (CHECKBOX_AGRUPAR_SALAS.Checked == true)
            {
                if (LISTVIEW_ADMIN_SALAS.ShowGroups)
                    BuildGroups(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_ADMIN_SALAS.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);
        }

        public void BuildGroups(int coluna)
        {
            LISTVIEW_ADMIN_SALAS.Groups.Clear();

            int Contagem = LISTVIEW_ADMIN_SALAS.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_ADMIN_SALAS.Items)
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
                LISTVIEW_ADMIN_SALAS.Groups.Add(Grupo);
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
