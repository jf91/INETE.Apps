using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace Cinema_Manager_1._0
{
    public partial class FORM_LOGIN : Form
    {
        MySqlConnection LigacaoDB = new MySqlConnection(CLASS_GLOBAL_VARIABLES.Servidor);

        MySqlConnection LigacaoDB_StoredProcedures = new MySqlConnection(CLASS_GLOBAL_VARIABLES.ServidorStoredProcedure);

        MySqlDataAdapter Adapter = new MySqlDataAdapter();

        MySqlDataReader Reader;

        DataTable TabelaDados = new DataTable();

        public int UserNum;
        public string UserPass;

        public string UserRank;

        public FORM_LOGIN()
        {
            InitializeComponent();
        }

        private void BUTTON_LOGIN_Click(object sender, EventArgs e)
        {
            UserNum = Convert.ToInt32(TEXTBOX_NOME.Text);
            UserPass = TEXTBOX_PASSWORD.Text;
            Properties.Settings.Default.succes_increment = 0;
            
            if(RADIOBUTTON_ADMIN.Checked == true)
            {
                Properties.Settings.Default.administrador = true;
                Properties.Settings.Default.gestor = false;
                Properties.Settings.Default.funcionario = false;
                Properties.Settings.Default.permissoes = 3;
                UserRank = "Administrador";
            }

            if(RADIOBUTTON_GESTOR.Checked == true)
            {
                Properties.Settings.Default.administrador = false;
                Properties.Settings.Default.gestor = true;
                Properties.Settings.Default.funcionario = false;
                Properties.Settings.Default.permissoes = 2;
                UserRank = "Gestor";
            }

            if(RADIOBUTTON_FUNCIONARIO.Checked == true)
            {
                Properties.Settings.Default.administrador = false;
                Properties.Settings.Default.gestor = false;
                Properties.Settings.Default.funcionario = true;
                Properties.Settings.Default.permissoes = 1;
                UserRank = "Funcionário";
            }

            if (UserExiste() == true)
            {
                if (VerificarCargo() == true)
                {
                    if (VerificarPWD() == true)
                    {
                        IniciarSessao();
                    }
                }
            }

            LigacaoDB.Close();
        }

/*
---------------------------------------------------------------------------------------------------
*/

        public bool UserExiste()
        {
            try
            {
                int AUX_User;
                LigacaoDB.Open();

                string Query_SelectUser = "SELECT ID FROM cinema.funcionarios WHERE ID = " + UserNum + "";
                MySqlCommand SELECT_User = new MySqlCommand(Query_SelectUser, LigacaoDB);
                AUX_User = Convert.ToInt32(SELECT_User.ExecuteScalar());

                if(AUX_User == UserNum)
                {
                    LigacaoDB.Close();

                    Properties.Settings.Default.succes_increment = Properties.Settings.Default.succes_increment + 1;

                    return true; 
                }

                else
                {
                    LigacaoDB.Close();

                    MessageBox.Show("Ocorreu um erro ao verificar a existência do " + UserRank + ". Excepção: \n\n", "Verificar Utilizador", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false; 
                }
            }

            catch(Exception EX)
            {
                LigacaoDB.Close();

                MessageBox.Show("Ocorreu um erro ao verificar a existência do " + UserRank + ".\n\nDescrição do Erro:\n" + EX.Message, "Verificar Utilizador", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;               
            }
        }

        public bool VerificarCargo()
        {
            try
            {
                LigacaoDB.Open();

                string Query_SelectCargo = "SELECT cargo FROM cinema.funcionarios WHERE ID = " + UserNum + "";
                MySqlCommand SELECT_Cargo = new MySqlCommand(Query_SelectCargo, LigacaoDB);
                CLASS_GLOBAL_VARIABLES.PermissionCheck = Convert.ToInt32(SELECT_Cargo.ExecuteScalar());

                if (CLASS_GLOBAL_VARIABLES.PermissionCheck == Properties.Settings.Default.permissoes)
                {
                    LigacaoDB.Close();

                    CLASS_GLOBAL_VARIABLES.RankMatch = true;

                    Properties.Settings.Default.succes_increment = Properties.Settings.Default.succes_increment + 1;

                    return true;                    
                }

                else
                {
                    LigacaoDB.Close();

                    MessageBox.Show("Ocorreu um erro ao confirmar o cargo " + UserRank + "\n\n Não tem permissões de " + UserRank + "", "Verificar Cargo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;                    
                }
            }

            catch(Exception EX)
            {
                LigacaoDB.Close();

                MessageBox.Show("Ocorreu um erro ao confirmar o cargo " + UserRank + ". Excepção: \n\n " + EX.Message + "", "Verificar Cargo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;                
            }

            
        }

        public bool VerificarPWD()
        {
            try
            {
                LigacaoDB.Open();

                string Query_SelectPWD = "SELECT password FROM cinema.login WHERE id_alias = " + UserNum + "";
                MySqlCommand SELECT_PWD = new MySqlCommand(Query_SelectPWD, LigacaoDB);
                CLASS_GLOBAL_VARIABLES.PwdCheck = SELECT_PWD.ExecuteScalar().ToString();

                if (CLASS_GLOBAL_VARIABLES.PwdCheck == UserPass)
                {
                    LigacaoDB.Close();

                    CLASS_GLOBAL_VARIABLES.PwdMatch = true;

                    Properties.Settings.Default.succes_increment = Properties.Settings.Default.succes_increment + 1;

                    return true;
                }

                else
                {
                    LigacaoDB.Close();

                    MessageBox.Show("A Password está incorrecta.", "Password Incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
            }

            catch(Exception EX)
            {
                LigacaoDB.Close();

                MessageBox.Show("Ocorreu um erro ao confirmar a passsword " + UserRank + ". Excepção: \n\n " + EX.Message + "", "Verificar Password", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        public void IniciarSessao()
        {
            if(Properties.Settings.Default.succes_increment == 3)
            {
                Properties.Settings.Default.credentials_correct = true;
            }

            if(Properties.Settings.Default.credentials_correct == true)
            {
                this.Hide();

                FORM_INICIO FormInicio = new FORM_INICIO();
                FormInicio.Show();
            }
        }

        private void BUTTON_LIGACAO_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.connection_request_login = true;
            this.Hide();
            FORMS_ADMIN.FORM_DEFINICOES_LIGACAO FormLigacoes = new FORMS_ADMIN.FORM_DEFINICOES_LIGACAO();
            FormLigacoes.ShowDialog();            
        }

        private void FORM_LOGIN_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
