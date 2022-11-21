namespace API.Repository
{
    public class TarefaRepository
    {
        WrapperMySQL _mysql = new WrapperMySQL();

        public bool Salvar(Models.Tarefa tarefa)
        {
            bool sucesso = false;

            try
            {
                if (tarefa.Id == 0)
                {
                    _mysql.Comando.CommandText = $@"insert into 
                                                     Tarefa (Descricao) 
                                                     values (@Descricao)";
                }
                else
                {
                    _mysql.Comando.CommandText = @$"update Tarefa 
                                                     set Descricao = @Descricao
                                                     where TarefaId = @TarefaId";

                    _mysql.Comando.Parameters.AddWithValue("@TarefaId", tarefa.Id);
                }

                _mysql.Comando.Parameters.AddWithValue("@Descricao", tarefa.Descricao);

                _mysql.Abrir();
                int linhasAfetadas = _mysql.Comando.ExecuteNonQuery();

                sucesso = linhasAfetadas > 0;

                if (sucesso)
                {
                    if (tarefa.Id == 0)
                    {
                        tarefa.Id = (int)_mysql.Comando.LastInsertedId;
                    }
                }

            }
            catch (Exception ex)
            {
                //ex.Message
            }
            finally
            {
                _mysql.Fechar();
            }

            return sucesso;

        }

        public bool Excluir(int id)
        {
            bool sucesso = false;

            try
            {

                _mysql.Comando.CommandText = @$"delete from Tarefa where TarefaId = {id}";

                _mysql.Abrir();
                int linhasAfetadas = _mysql.Comando.ExecuteNonQuery();

                sucesso = linhasAfetadas > 0;

            }
            catch (Exception ex)
            {
                //ex.Message
            }
            finally
            {
                _mysql.Fechar();
            }

            return sucesso;

        }

        public Models.Tarefa Obter(int id)
        {
            Models.Tarefa tarefa = null;

            try
            {

                _mysql.Comando.CommandText = $@"select * 
                                                from Tarefa
                                                where TarefaId = {id}";

                _mysql.Abrir();
                var dr = _mysql.Comando.ExecuteReader();

                if (dr.Read())
                {
                    tarefa = new Models.Tarefa();
                    tarefa.Id = Convert.ToInt32(dr["TarefaId"]);
                    tarefa.Descricao = dr["Descricao"].ToString();
                }
            }
            catch (Exception ex)
            {
                //ex.Message
            }
            finally
            {
                _mysql.Fechar();
            }

            return tarefa;

        }

        public List<Models.Tarefa> ObterTodos()
        {
            List<Models.Tarefa> tarefas = new List<Models.Tarefa>();

            try
            {
                _mysql.Comando.CommandText = $@"select * 
                                                from Tarefa
                                                order by TarefaId desc";


                _mysql.Abrir();
                var dr = _mysql.Comando.ExecuteReader();

                while (dr.Read())
                {
                    Models.Tarefa t = new();
                    t.Id = Convert.ToInt32(dr["TarefaId"]);
                    t.Descricao= dr["Descricao"].ToString();

                    tarefas.Add(t);
                }
            }
            catch (Exception ex)
            {
                //ex.Message
            }
            finally
            {
                _mysql.Fechar();
            }

            return tarefas;

        }


    }
}
