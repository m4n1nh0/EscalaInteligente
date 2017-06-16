using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EscalaInteligente.Models.Funcionario
{
    public class FuncionarioDAO : DBHelper
    {

        #region Atributos
        private Usuario.Usuario usuario;
        private Usuario.UsuarioDAO usuarioDAO;
        #endregion

        #region Contrutor
        public FuncionarioDAO()
        {
            this.usuario = new Usuario.Usuario();
            this.usuarioDAO = new Usuario.UsuarioDAO();
        }
        #endregion

        public FuncCursos ObterFuncCursos(String chave)
        {
            try
            {

                this.AbrirConexao();
                cmd = new SqlCommand("SELECT * FROM [FUNC_CURSOS] WHERE [CPF_FUNC_CUR] = @CPF_FUNC_CUR", con, tran);
                cmd.Parameters.AddWithValue("@CPF_FUNC_CUR", chave);
                FuncCursos func = new FuncCursos();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    func.CPF_FUNC1 = Convert.ToString((dr["CPF_FUNC_CUR"]));
                    func.NOME_FUNC1 = Convert.ToString((dr["NOME_FUNC_CUR"]));
                    func.EMAIL1 = Convert.ToString((dr["EMAIL_CUR"]));
                    func.QTD_CURSO_CADST1 = Convert.ToInt32((dr["QTD_CURSO_CADST"]));
                    func.QTD_ALU_CADST1 = Convert.ToInt32((dr["QTD_ALU_CADST"]));
                    func.QTD_PROF_CADST1 = Convert.ToInt32((dr["QTD_PROF_CADST"]));
                    func.COD_INSTITUICAO_FK1 = Convert.ToInt32((dr["COD_INST_ENSINO_FK"]));

                }
                return func;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter dados  do Funcionario: " + ex.Message);
            }
            finally
            {
                this.FecharConexao();
            }
        }

        public void Insert(FuncCursos func)
        {
            try
            {
                this.AbrirConexao();

                //set os dados do usuario

                this.usuario.CPF_USU_FUNC_CUS_FK1 = func.CPF_FUNC1;
                this.usuario.USUARIO1 = Convert.ToString(func.CPF_FUNC1);
                this.usuario.SENHA1 = "123456";
                this.usuario.NIVEL_ACESSO1 = 7;

                cmd = new SqlCommand(@"INSERT INTO [FUNC_CURSOS] 
                                            ([CPF_FUNC_CUR], 
                                             [NOME_FUNC_CUR], 
                                             [EMAIL_CUR], 
                                             [QTD_CURSO_CADST], 
                                             [QTD_PROF_CADST],
                                             [QTD_ALU_CADST],
                                             [COD_INST_ENSINO_FK]) 
                                    VALUES ( @CPF_FUNC_CUR, 
                                             @NOME_FUNC_CUR, 
                                             @EMAIL_CUR, 
                                             @QTD_CURSO_CADST, 
                                             @QTD_PROF_CADST,
                                             @QTD_ALU_CADST,
                                             @COD_INST_ENSINO_FK)", con);

                func.QTD_ALU_CADST1 = 0;
                func.QTD_CURSO_CADST1 = 0;
                func.QTD_PROF_CADST1 = 0;

                cmd.Parameters.AddWithValue("@CPF_FUNC_CUR", func.CPF_FUNC1);
                cmd.Parameters.AddWithValue("@NOME_FUNC_CUR", func.NOME_FUNC1);
                cmd.Parameters.AddWithValue("@EMAIL_CUR", func.EMAIL1);
                cmd.Parameters.AddWithValue("@QTD_CURSO_CADST", func.QTD_CURSO_CADST1);
                cmd.Parameters.AddWithValue("@QTD_PROF_CADST", func.QTD_PROF_CADST1);
                cmd.Parameters.AddWithValue("@QTD_ALU_CADST", func.QTD_ALU_CADST1);
                cmd.Parameters.AddWithValue("@COD_INST_ENSINO_FK", func.COD_INSTITUICAO_FK1);

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                this.usuarioDAO.Insert(this.usuario);
                tran.Commit();


            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new Exception("Erro ao cadastrar Funcionario: " + ex.Message);
            }
            finally
            {
                this.FecharConexao();
            }
        }


        public List<FuncCursos> ListarFuncionarios(String cpf, String nome)
        {
            try
            {
                this.AbrirConexao();

                string query = "SELECT * FROM [FUNC_CURSOS]  WHERE (@CPF_FUNC_CUR IS NULL OR [CPF_FUNC_CUR] = @CPF_FUNC_CUR) AND (@NOME_FUNC_CUR IS NULL OR [NOME_FUNC_CUR] = @NOME_FUNC_CUR OR NOME_FUNC_CUR LIKE '%' + @NOME_FUNC_CUR + '%') ";

                cmd = new SqlCommand(query, tran.Connection, tran);
                if (String.IsNullOrEmpty(cpf))
                {
                    cmd.Parameters.AddWithValue("@CPF_FUNC_CUR", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CPF_FUNC_CUR", cpf);
                }
                if (String.IsNullOrEmpty(nome))
                {
                    cmd.Parameters.AddWithValue("@NOME_FUNC_CUR", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@NOME_FUNC_CUR", nome);
                }
                dr = cmd.ExecuteReader();
                List<FuncCursos> lista = new List<FuncCursos>();
                while (dr.Read())
                {
                    FuncCursos func = new FuncCursos();
                    func.CPF_FUNC1 = Convert.ToString((dr["CPF_FUNC_CUR"]));
                    func.NOME_FUNC1 = Convert.ToString((dr["NOME_FUNC_CUR"]));
                    func.EMAIL1 = Convert.ToString((dr["EMAIL_CUR"]));
                    func.QTD_CURSO_CADST1 = Convert.ToInt32((dr["QTD_CURSO_CADST"]));
                    func.QTD_ALU_CADST1 = Convert.ToInt32((dr["QTD_ALU_CADST"]));
                    func.QTD_PROF_CADST1 = Convert.ToInt32((dr["QTD_PROF_CADST"]));
                    func.COD_INSTITUICAO_FK1 = Convert.ToInt32((dr["COD_INST_ENSINO_FK"]));
                    lista.Add(func);
                }
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao listar Funcionarios: " + ex.Message);
            }
            finally
            {
                this.FecharConexao();
            }
        }

        public void Delete(String chave)
        {
            try
            {
                this.AbrirConexao();
                cmd = new SqlCommand(@"DELETE FROM [FUNC_CURSOS] WHERE [CPF_FUNC_CUR] = @CPF_FUNC_CUR", con);
                cmd.Parameters.AddWithValue("@CPF_FUNC_CUR", chave);
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new Exception("Erro ao excluir o Funcionario." + ex.Message);
            }
            finally
            {
                this.FecharConexao();
            }
        }

        public void Update(String cpf, FuncCursos func)
        {
            try
            {
                this.AbrirConexao();

                cmd = new SqlCommand(@"UPDATE [FUNC_CURSOS] SET 
                                                        [NOME_FUNC_CUR]      = @NOME_FUNC_CUR,    
                                                        [EMAIL_CUR]          = @EMAIL_CUR, 
                                                        [QTD_CURSO_CADST]    = @QTD_CURSO_CADST, 
                                                        [QTD_PROF_CADST]     = @QTD_PROF_CADST,
                                                        [QTD_ALU_CADST]      = @QTD_ALU_CADST,
                                                        [COD_INST_ENSINO_FK] = @COD_INST_ENSINO_FK
                                                 WHERE [CPF_FUNC_CUR] = @CPF_FUNC_CUR", con);

                cmd.Parameters.AddWithValue("@CPF_FUNC_CUR", cpf);
                cmd.Parameters.AddWithValue("@NOME_FUNC_CUR", func.NOME_FUNC1);
                cmd.Parameters.AddWithValue("@EMAIL_CUR", func.EMAIL1);
                cmd.Parameters.AddWithValue("@QTD_CURSO_CADST", func.QTD_CURSO_CADST1);
                cmd.Parameters.AddWithValue("@QTD_PROF_CADST", func.QTD_PROF_CADST1);
                cmd.Parameters.AddWithValue("@QTD_ALU_CADST", func.QTD_ALU_CADST1);
                cmd.Parameters.AddWithValue("@COD_INST_ENSINO_FK", func.COD_INSTITUICAO_FK1);
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new Exception("Erro ao autalizar os dados do Funcionario" + ex.Message);
            }
            finally
            {
                this.FecharConexao();
            }

        }
    }
}