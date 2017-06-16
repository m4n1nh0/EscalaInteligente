using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EscalaInteligente.Models.Conexao
{
    public class DBHelper
    {
        protected SqlConnection con;
        protected SqlCommand cmd;
        public string StringConexao;
        protected SqlDataReader dr;
        protected SqlTransaction tran;
        protected void AbrirConexao()
        {
            try
            {
                this.StringConexao = "Data Source=sql5032.smarterasp.net;Initial Catalog=DB_A1DE4F_elisrenan;User Id=DB_A1DE4F_elisrenan_admin;Password=3E3BJ.C2017";
                this.StringConexao = ConfigurationManager.ConnectionStrings["Smarterasp"].ConnectionString;
                //this.StringConexao = ConfigurationManager.ConnectionStrings["Amazon"].ConnectionString;
                //this.StringConexao = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

                con = new SqlConnection(StringConexao);
                con.Open();
                tran = con.BeginTransaction();
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao conectar: " + ex.Message);
            }
        }

        protected void FecharConexao()
        {
            try
            {
                con.Close();
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao desconectar: " + ex.Message);
            }
        }
    }
}