using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Cinema_Manager_1._0
{
    class CLASS_GLOBAL_VARIABLES
    {
        public static string Servidor = "datasource=localhost;port=3306;username=cinema;password=cinema";
        public static string ServidorStoredProcedure = "Server=localhost;Database=cinema;Uid=cinema;Pwd=cinema;Use Procedure Bodies=false;";

        public static string Endereco = "localhost";
        public static string Porta = "3306";
        public static string Utilizador = "cinema";
        public static string Password = "cinema";

        public static string BD = "cinema";

        public static bool RankMatch = false;
        public static bool PwdMatch = false;
        public static int PermissionCheck;
        public static string PwdCheck;

        public static string GestaoResumos_RequestSource = "";
        public static bool NovoResumo = false;
        public static string AUX_RESUMO_ID;
        public static int INT_AUX_RESUMO_ID;
        public static string AUX_RESUMO_Titulo;
        public static string AUX_RESUMO_Duracao;
        public static string AUX_RESUMO_Estado;
        public static string AUX_RESUMO_Resumo;
        public static string AUX_RESUMO_DuracaoResumo;
    }
}
