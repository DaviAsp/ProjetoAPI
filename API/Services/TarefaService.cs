namespace API.Services
{
    public class TarefaService
    {
        Repository.TarefaRepository _tarefaRepository = new Repository.TarefaRepository();
        public bool Gravar(Models.Tarefa tarefa)
        {
            return _tarefaRepository.Salvar(tarefa);

        }

        public Models.Tarefa Obter(int id)
        {
            return _tarefaRepository.Obter(id);

        }

        public bool Excluir(int id)
        {
            return _tarefaRepository.Excluir(id);

        }

        public List<Models.Tarefa> ObterTodos()
        {
            return _tarefaRepository.ObterTodos();
        }
    }
}
