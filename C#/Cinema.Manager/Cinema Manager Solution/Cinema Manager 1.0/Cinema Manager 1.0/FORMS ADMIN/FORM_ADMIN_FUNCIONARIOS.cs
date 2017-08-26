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


namespace Cinema_Manager_1._0.FORMS_ADMIN
{
    public partial class FORM_ADMIN_FUNCIONARIOS : Form
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

        public FORM_ADMIN_FUNCIONARIOS()
        {
            InitializeComponent();

            Preencher_Listview_Funcionarios();
        }

        private void PICTUREBOX_REMOVER_FUNCIONARIO_Click(object sender, EventArgs e)
        {
            if (TEXTBOX_FUNCIONARIO_NOME.Text == "")
            {
                MessageBox.Show("Não tem nenhum funcionário selecionado! Não é possivel remover.", "Nenhum funcionário selecionado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (TEXTBOX_FUNCIONARIO_NOME.Text != "")
            {
                DialogResult RemoverFuncionario = MessageBox.Show("Tem a certeza que deseja remover este funcionário?", "Remover Funcionário?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (RemoverFuncionario == DialogResult.Yes)
                {
                    Remover_Funcionario();
                    ClearListView();
                    Preencher_Listview_Funcionarios();
                    Limpar_TextBoxs();
                }
            }
        }

        private void PICTUREBOX_GRAVAR_ALTERACOES_FUNCIONARIO_Click(object sender, EventArgs e)
        {
            if (TEXTBOX_FUNCIONARIO_NOME.Text == "")
            {
                MessageBox.Show("Não tem nenhum funcionário selecionado! Não é possivel alterar.", "Nenhum funcionário selecionado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (TEXTBOX_FUNCIONARIO_NOME.Text != "")
            {
                DialogResult DefinirDados = MessageBox.Show("Tem a certeza que deseja editar as informações deste funcionário?", "Alterar Informações?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DefinirDados == DialogResult.Yes)
                {
                    Alterar_Funcionario();
                    ClearListView();
                    Preencher_Listview_Funcionarios();
                    Limpar_TextBoxs();
                }
            }
        }

        private void PICTUREBOX_ADICIONAR_FUNCIONARIO_Click(object sender, EventArgs e)
        {
            TABCONTROL_FUNCIONARIOS.SelectedIndex = 1;
        }

        private void LISTVIEW_FUNCIONARIOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            Preencher_TextBox_Funcionario();
        }

        private void BUTTON_ADICIONAR_FUNCIONARIO_Click(object sender, EventArgs e)
        {
            Adicionar_Funcionario();
            ClearListView();
            Preencher_Listview_Funcionarios();
            Limpar_TextBoxs();

            TABCONTROL_FUNCIONARIOS.SelectedIndex = 0;
        }

        private void CHECKBOX_ADICIONARFUNCIONARIO_MOSTRAR_PASSWORD_CheckedChanged(object sender, EventArgs e)
        {
            if (CHECKBOX_ADICIONARFUNCIONARIO_MOSTRAR_PASSWORD.Checked == true)
                TEXTBOX_ADICIONARFUNCIONARIO_PASSWORD.PasswordChar = '\0';
            if (CHECKBOX_ADICIONARFUNCIONARIO_MOSTRAR_PASSWORD.Checked == false)
                TEXTBOX_ADICIONARFUNCIONARIO_PASSWORD.PasswordChar = '●';
        }

        private void CHECKBOX_FUNCIONARIO_PASSWORD_CheckedChanged(object sender, EventArgs e)
        {
            if (CHECKBOX_FUNCIONARIO_PASSWORD.Checked == true)
                TEXTBOX_FUNCIONARIO_PASSWORD.PasswordChar = '\0';
            if (CHECKBOX_FUNCIONARIO_PASSWORD.Checked == false)
                TEXTBOX_FUNCIONARIO_PASSWORD.PasswordChar = '●';
        }

        private void TEXTBOX_FUNCIONARIO_NOME_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_FUNCIONARIO_NOME.Text = Regex.Replace(TEXTBOX_FUNCIONARIO_NOME.Text, @"[0-9]", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("\"", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("|", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("!", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace(@"\", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("@", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("#", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("£", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("$", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("§", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("%", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("&", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("/", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("{", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("(", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("[", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace(")", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("]", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("=", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("}", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("?", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("'", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("»", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("«", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("+", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("-", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("*", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("¨", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("ª", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("º", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("<", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace(">", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace(",", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace(";", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace(".", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace(":", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("-", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("_", string.Empty);
            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_NOME.Text.Replace("€", string.Empty);
        }

        private void TEXTBOX_FUNCIONARIO_PRIMEIRO_NOME_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_FUNCIONARIO_PRIMEIRO_NOME.Text = Regex.Replace(TEXTBOX_FUNCIONARIO_PRIMEIRO_NOME.Text, @"[0-9]", string.Empty);
            TEXTBOX_FUNCIONARIO_PRIMEIRO_NOME.Text = Regex.Replace(TEXTBOX_FUNCIONARIO_PRIMEIRO_NOME.Text, @"[| \ ! @ # £ $ § % & / { } ( ) [ ] = ? ' » « * + ¨ ª º - _ . : , ; > <]", string.Empty);

            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_PRIMEIRO_NOME.Text + " " + TEXTBOX_FUNCIONARIO_SEGUNDO_NOME.Text;
        }

        private void TEXTBOX_FUNCIONARIO_SEGUNDO_NOME_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_FUNCIONARIO_SEGUNDO_NOME.Text = Regex.Replace(TEXTBOX_FUNCIONARIO_SEGUNDO_NOME.Text, @"[0-9]", string.Empty);
            TEXTBOX_FUNCIONARIO_SEGUNDO_NOME.Text = Regex.Replace(TEXTBOX_FUNCIONARIO_SEGUNDO_NOME.Text, @"[| \ ! @ # £ $ § % & / { } ( ) [ ] = ? ' » « * + ¨ ª º - _ . : , ; > <]", string.Empty);

            TEXTBOX_FUNCIONARIO_NOME.Text = TEXTBOX_FUNCIONARIO_PRIMEIRO_NOME.Text + " " + TEXTBOX_FUNCIONARIO_SEGUNDO_NOME.Text;
        }

        private void TEXTBOX_FUNCIONARIO_NUMERO_BI_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_FUNCIONARIO_NUMERO_BI.Text = Regex.Replace(TEXTBOX_FUNCIONARIO_NUMERO_BI.Text, @"[^\d]", string.Empty);
        }

        private void TEXTBOX_FUNCIONARIO_PASSWORD_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_FUNCIONARIO_PASSWORD.Text = Regex.Replace(TEXTBOX_FUNCIONARIO_PASSWORD.Text, @"[^ A-Z a-z 0-9]", string.Empty);
        }

        private void TEXTBOX_ADICIONARFUNCIONARIO_PRIMEIRO_NOME_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_ADICIONARFUNCIONARIO_PRIMEIRO_NOME.Text = Regex.Replace(TEXTBOX_ADICIONARFUNCIONARIO_PRIMEIRO_NOME.Text, @"[0-9]", string.Empty);
            TEXTBOX_ADICIONARFUNCIONARIO_PRIMEIRO_NOME.Text = Regex.Replace(TEXTBOX_ADICIONARFUNCIONARIO_PRIMEIRO_NOME.Text, @"[| \ ! @ # £ $ § % & / { } ( ) [ ] = ? ' » « * + ¨ ª º - _ . : , ; > <]", string.Empty);
        }

        private void TEXTBOX_ADICIONARFUNCIONARIO_ULTIMO_NOME_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_ADICIONARFUNCIONARIO_ULTIMO_NOME.Text = Regex.Replace(TEXTBOX_ADICIONARFUNCIONARIO_ULTIMO_NOME.Text, @"[0-9]", string.Empty);
            TEXTBOX_ADICIONARFUNCIONARIO_ULTIMO_NOME.Text = Regex.Replace(TEXTBOX_ADICIONARFUNCIONARIO_ULTIMO_NOME.Text, @"[| \ ! @ # £ $ § % & / { } ( ) [ ] = ? ' » « * + ¨ ª º - _ . : , ; > <]", string.Empty);
        }

        private void TEXTBOX_ADICIONARFUNCIONARIO_NUMERO_BI_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_ADICIONARFUNCIONARIO_NUMERO_BI.Text = Regex.Replace(TEXTBOX_ADICIONARFUNCIONARIO_NUMERO_BI.Text, @"[^\d]", string.Empty);
        }

        private void TEXTBOX_ADICIONARFUNCIONARIO_PASSWORD_TextChanged(object sender, EventArgs e)
        {
            TEXTBOX_ADICIONARFUNCIONARIO_PASSWORD.Text = Regex.Replace(TEXTBOX_ADICIONARFUNCIONARIO_PASSWORD.Text, @"[^ A-Z a-z 0-9]", string.Empty);
        }

        private void LISTVIEW_FUNCIONARIOS_ColumnClick(object sender, ColumnClickEventArgs e)
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

        private void CHECKBOX_AGRUPAR_FUNCIONARIOS_CheckedChanged(object sender, EventArgs e)
        {
            LISTVIEW_FUNCIONARIOS.ShowGroups = CHECKBOX_AGRUPAR_FUNCIONARIOS.Checked;
            Ordenar();
        }

        public void Preencher_Listview_Funcionarios()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB.Open();

                string Query = "SELECT ID,primeiro_nome,segundo_nome,cargo FROM " + CLASS_GLOBAL_VARIABLES.BD + ".funcionarios";
                MySqlCommand Comando = new MySqlCommand(Query, LigacaoDB);

                Reader = Comando.ExecuteReader();

                while (Reader.Read())
                {
                    var ROW = new ListViewItem();
                    ROW.Text = Reader["ID"].ToString();
                    ROW.SubItems.Add(Reader["primeiro_nome"].ToString());
                    ROW.SubItems.Add(Reader["segundo_nome"].ToString());
                    if(Reader["cargo"].ToString() == "1")
                        ROW.SubItems.Add("Funcionario");
                    if(Reader["cargo"].ToString() == "2")
                        ROW.SubItems.Add("Gestor");
                    if (Reader["cargo"].ToString() == "3")
                        ROW.SubItems.Add("Administrador");
                    LISTVIEW_FUNCIONARIOS.Items.Add(ROW);
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
            LISTVIEW_FUNCIONARIOS.Items.Clear();
        }

        public void Preencher_TextBox_Funcionario()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                string ListView_ID = LISTVIEW_FUNCIONARIOS.SelectedItems[0].Text;
                int ID = Convert.ToInt32(ListView_ID);

                LigacaoDB.Open();

                string Nome;
                string PrimeiroNome;
                string SegundoNome;
                string DataNascimento;
                string NumeroBI;
                string Cargo;
                string Password;

                string Query_PrimeiroNome = "SELECT primeiro_nome FROM " + CLASS_GLOBAL_VARIABLES.BD + ".funcionarios WHERE ID = " + ID + "";
                string Query_SegundoNome = "SELECT segundo_nome FROM " + CLASS_GLOBAL_VARIABLES.BD + ".funcionarios WHERE ID = " + ID + "";
                string Query_DataNascimento = "SELECT data_nascimento FROM " + CLASS_GLOBAL_VARIABLES.BD + ".funcionarios WHERE ID = " + ID + "";
                string Query_NumeroBI = "SELECT numero_bi FROM " + CLASS_GLOBAL_VARIABLES.BD + ".funcionarios WHERE ID = " + ID + "";
                string Query_Cargo = "SELECT cargo FROM " + CLASS_GLOBAL_VARIABLES.BD + ".funcionarios WHERE ID = " + ID + "";
                string Query_Password = "SELECT password FROM " + CLASS_GLOBAL_VARIABLES.BD + ".login WHERE id_alias = " + ID + "";

                MySqlCommand Comando_PrimeiroNome = new MySqlCommand(Query_PrimeiroNome, LigacaoDB);
                MySqlCommand Comando_SegundoNome = new MySqlCommand(Query_SegundoNome, LigacaoDB);
                MySqlCommand Comando_DataNascimento = new MySqlCommand(Query_DataNascimento, LigacaoDB);
                MySqlCommand Comando_NumeroBI = new MySqlCommand(Query_NumeroBI, LigacaoDB);
                MySqlCommand Comando_Cargo = new MySqlCommand(Query_Cargo, LigacaoDB);
                MySqlCommand Comando_Password = new MySqlCommand(Query_Password, LigacaoDB);

                PrimeiroNome = Comando_PrimeiroNome.ExecuteScalar().ToString();
                SegundoNome = Comando_SegundoNome.ExecuteScalar().ToString();
                Nome = PrimeiroNome + " " + SegundoNome;
                DataNascimento = Comando_DataNascimento.ExecuteScalar().ToString();
                NumeroBI = Comando_NumeroBI.ExecuteScalar().ToString();
                Cargo = Comando_Cargo.ExecuteScalar().ToString();
                Password = Comando_Password.ExecuteScalar().ToString();

                TEXTBOX_FUNCIONARIO_NOME.Text = Nome;
                TEXTBOX_FUNCIONARIO_PRIMEIRO_NOME.Text = PrimeiroNome;
                TEXTBOX_FUNCIONARIO_SEGUNDO_NOME.Text = SegundoNome;
                DATETIMEPICKER_FUNCIONARIO_DATA_NASCIMENTO.Value = DateTime.Parse(DataNascimento);
                TEXTBOX_FUNCIONARIO_NUMERO_BI.Text = NumeroBI;
                TEXTBOX_FUNCIONARIO_PASSWORD.Text = Password;

                if (Cargo == "1")
                    COMBOBOX_FUNCIONARIO_CARGO.SelectedIndex = 2;
                if(Cargo == "2")
                    COMBOBOX_FUNCIONARIO_CARGO.SelectedIndex = 1;
                if (Cargo == "3")
                    COMBOBOX_FUNCIONARIO_CARGO.SelectedIndex = 0;

                LigacaoDB.Close();
            }

            catch(Exception EX)
            {
                //MessageBox.Show(EX.Message);
            }
        }        

        public void Adicionar_Funcionario()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB_StoredProcedures.Open();

                string Alias;
                string PrimeiroNome = TEXTBOX_ADICIONARFUNCIONARIO_PRIMEIRO_NOME.Text;
                string UltimoNome = TEXTBOX_ADICIONARFUNCIONARIO_ULTIMO_NOME.Text;
                string DataNascimento = DATEPICKER_ADICIONARFUNCIONARIO_DATA_NASCIMENTO.Value.ToShortDateString();
                string NumeroBI = TEXTBOX_ADICIONARFUNCIONARIO_NUMERO_BI.Text;
                int INT_NumeroBI = Convert.ToInt32(NumeroBI);
                
                string Cargo = COMBOBOX_ADICIONARFUNCIONARIO_CARGO.Text;
                int CargoID = 0;
                if (Cargo == "Funcionário")
                    CargoID = 1;
                if (Cargo == "Gestor")
                    CargoID = 2;
                if (Cargo == "Administrador")
                    CargoID = 3;

                string Password = TEXTBOX_ADICIONARFUNCIONARIO_PASSWORD.Text;
                if(Password == "")
                {
                    string Password_Part1 = NumeroBI.Substring(0, 3);
                    string Password_Part2 = PrimeiroNome.ToLower();
                    string Password_Part3 = UltimoNome.ToLower();
                    Password = Password_Part1 + Password_Part2 + Password_Part3;
                }

                Alias = CargoID + "-" + PrimeiroNome.ToLower() + "-" + UltimoNome.ToLower();                

                MySqlCommand Comando_FuncionarioInserir = new MySqlCommand("SP_FUNCIONARIO_INSERIR", LigacaoDB_StoredProcedures);
                Comando_FuncionarioInserir.CommandType = CommandType.StoredProcedure;

                Comando_FuncionarioInserir.Parameters.AddWithValue("_alias", Alias);
                Comando_FuncionarioInserir.Parameters.AddWithValue("_primeiro_nome", PrimeiroNome);
                Comando_FuncionarioInserir.Parameters.AddWithValue("_ultimo_nome", UltimoNome);
                Comando_FuncionarioInserir.Parameters.AddWithValue("_data_nascimento", DataNascimento);
                Comando_FuncionarioInserir.Parameters.AddWithValue("_numero_bi", INT_NumeroBI);
                Comando_FuncionarioInserir.Parameters.AddWithValue("_cargo", CargoID);

                Reader = Comando_FuncionarioInserir.ExecuteReader();

                Reader.Close();

                if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                {
                    LigacaoDB_StoredProcedures.Close();

                    if (LigacaoDB_StoredProcedures.State == ConnectionState.Closed)
                    {
                        System.Threading.Thread.Sleep(2000);

                        LigacaoDB.Open();

                        string Query_GetAlias = "SELECT ID FROM " + CLASS_GLOBAL_VARIABLES.BD + ".funcionarios WHERE alias='" + Alias + "'";
                        string GetID;
                        int INT_GetID;

                        MySqlCommand Comando_GetAlias = new MySqlCommand(Query_GetAlias, LigacaoDB);
                        GetID = Comando_GetAlias.ExecuteScalar().ToString();
                        INT_GetID = Convert.ToInt32(GetID);

                        if (LigacaoDB.State == ConnectionState.Open)
                        {
                            LigacaoDB.Close();

                            if (LigacaoDB.State == ConnectionState.Closed)
                            {
                                LigacaoDB_StoredProcedures.Open();

                                MySqlCommand Comando_LoginInserir = new MySqlCommand("SP_LOGIN_INSERIR", LigacaoDB_StoredProcedures);
                                Comando_LoginInserir.CommandType = CommandType.StoredProcedure;

                                Comando_LoginInserir.Parameters.AddWithValue("_id_alias", INT_GetID);
                                Comando_LoginInserir.Parameters.AddWithValue("_alias", Alias);
                                Comando_LoginInserir.Parameters.AddWithValue("_password", Password);

                                Reader = Comando_LoginInserir.ExecuteReader();

                                Reader.Close();

                                LigacaoDB_StoredProcedures.Close();

                                MessageBox.Show("Funcionario Inserido com Sucesso", "Funcionario Inserido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        public void Remover_Funcionario()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB_StoredProcedures.Open();

                string ListView_ID = LISTVIEW_FUNCIONARIOS.SelectedItems[0].Text;
                int ID = Convert.ToInt32(ListView_ID);

                MySqlCommand Command_LoginRemover = new MySqlCommand("SP_LOGIN_REMOVER", LigacaoDB_StoredProcedures);
                Command_LoginRemover.CommandType = CommandType.StoredProcedure;

                Command_LoginRemover.Parameters.AddWithValue("_id_alias", ID);
                Reader = Command_LoginRemover.ExecuteReader();

                System.Threading.Thread.Sleep(2000);

                if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                {
                    LigacaoDB_StoredProcedures.Close();

                    if (LigacaoDB.State == ConnectionState.Open)
                        LigacaoDB.Close();

                    LigacaoDB_StoredProcedures.Open();

                    MySqlCommand Command = new MySqlCommand("SP_FUNCIONARIO_REMOVER", LigacaoDB_StoredProcedures);
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("_id", ID);

                    Reader = Command.ExecuteReader();

                    Reader.Close();                    

                    MessageBox.Show("Funcionário removido com sucesso", "Funcionario Removido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LigacaoDB_StoredProcedures.Close();
                }
            }

            catch(Exception EX)
            { }
        }

        public void Alterar_Funcionario()
        {
            if (LigacaoDB.State == ConnectionState.Open)
                LigacaoDB.Close();

            if (LigacaoDB_StoredProcedures.State == ConnectionState.Open)
                LigacaoDB_StoredProcedures.Close();

            try
            {
                LigacaoDB_StoredProcedures.Open();

                string ListView_ID = LISTVIEW_FUNCIONARIOS.SelectedItems[0].Text;
                int ID = Convert.ToInt32(ListView_ID);

                string Alias;
                string PrimeiroNome = TEXTBOX_FUNCIONARIO_PRIMEIRO_NOME.Text;
                string SegundoNome = TEXTBOX_FUNCIONARIO_SEGUNDO_NOME.Text;
                string DataNascimento = DATETIMEPICKER_FUNCIONARIO_DATA_NASCIMENTO.Value.ToShortDateString();
                int NumeroBI = Convert.ToInt32(TEXTBOX_FUNCIONARIO_NUMERO_BI.Text);
                int Cargo = 0;
                string Password = TEXTBOX_FUNCIONARIO_PASSWORD.Text;

                if (COMBOBOX_FUNCIONARIO_CARGO.Text == "Funcionário")
                    Cargo = 1;
                if (COMBOBOX_FUNCIONARIO_CARGO.Text == "Gestor")
                    Cargo = 2;
                if (COMBOBOX_FUNCIONARIO_CARGO.Text == "Administrador")
                    Cargo = 3;

                Alias = Cargo + "-" + PrimeiroNome.ToLower() + "-" + SegundoNome.ToLower();

                MySqlCommand Comando = new MySqlCommand("SP_FUNCIONARIO_ALTERAR", LigacaoDB_StoredProcedures);
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Parameters.AddWithValue("_id", ID);
                Comando.Parameters.AddWithValue("_alias", Alias);
                Comando.Parameters.AddWithValue("_primeiro_nome", PrimeiroNome);
                Comando.Parameters.AddWithValue("_segundo_nome", SegundoNome);
                Comando.Parameters.AddWithValue("_data_nascimento", DataNascimento);
                Comando.Parameters.AddWithValue("_numero_bi", NumeroBI);
                Comando.Parameters.AddWithValue("_cargo", Cargo);

                Comando.ExecuteNonQuery();

                MySqlCommand Comando_Login = new MySqlCommand("SP_LOGIN_ALTERAR", LigacaoDB_StoredProcedures);
                Comando_Login.CommandType = CommandType.StoredProcedure;

                Comando_Login.Parameters.AddWithValue("_id_alias", ID);
                Comando_Login.Parameters.AddWithValue("_alias", Alias);
                Comando_Login.Parameters.AddWithValue("_password", Password);

                Comando_Login.ExecuteNonQuery();

                LigacaoDB_StoredProcedures.Close();

                MessageBox.Show("Funcionario Alterado com Sucesso", "Funcionario Alterado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception EX)
            { }
        }

        public void Limpar_TextBoxs()
        {
            TEXTBOX_FUNCIONARIO_NOME.Text = "";
            TEXTBOX_FUNCIONARIO_PRIMEIRO_NOME.Text = "";
            TEXTBOX_FUNCIONARIO_SEGUNDO_NOME.Text = "";
            TEXTBOX_FUNCIONARIO_NUMERO_BI.Text = "";
            COMBOBOX_FUNCIONARIO_CARGO.SelectedItem = null;
            TEXTBOX_FUNCIONARIO_PASSWORD.Text = "";

            TEXTBOX_ADICIONARFUNCIONARIO_PRIMEIRO_NOME.Text = "";
            TEXTBOX_ADICIONARFUNCIONARIO_ULTIMO_NOME.Text = "";
            TEXTBOX_ADICIONARFUNCIONARIO_NUMERO_BI.Text = "";
            COMBOBOX_ADICIONARFUNCIONARIO_CARGO.SelectedItem = null;
            TEXTBOX_ADICIONARFUNCIONARIO_PASSWORD.Text = "";
        }               

        public void Ordenar()
        {
            if (CHECKBOX_AGRUPAR_FUNCIONARIOS.Checked == true)
            {
                if (LISTVIEW_FUNCIONARIOS.ShowGroups)
                    BuildGroups(UltimaColunaOrdenada);
            }
            else
                LISTVIEW_FUNCIONARIOS.ListViewItemSorter = new CompararColuna(UltimaColunaOrdenada, UltimaOrdem);
        }

        public void BuildGroups(int coluna)
        {
            LISTVIEW_FUNCIONARIOS.Groups.Clear();

            int Contagem = LISTVIEW_FUNCIONARIOS.Items.Count;

            Dictionary<String, List<ListViewItem>> Mapear = new Dictionary<String, List<ListViewItem>>();
            foreach (ListViewItem LVI in LISTVIEW_FUNCIONARIOS.Items)
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
                LISTVIEW_FUNCIONARIOS.Groups.Add(Grupo);
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
