using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {

        [Route("[action]")]
        [HttpPost]
        public IActionResult Salvar(ViewModels.TarefaViewModel tarefaVM)
        {
            Models.Tarefa t = new Models.Tarefa();
            t.Id = tarefaVM.Id;
            t.Descricao = tarefaVM.Descricao;

            API.Services.TarefaService ts = new Services.TarefaService();
            var sucesso = ts.Gravar(t);
            string msg = "";

            if (!sucesso)
            {
                msg = "Não salvou";
            }

            return Ok(new
            {
                sucesso,
                msg
            });
        }

        [Route("[action]")]
        [HttpDelete]
        public IActionResult Excluir(int id)
        {
            API.Services.TarefaService ts = new Services.TarefaService();
            var sucesso = ts.Excluir(id);

            if (sucesso)
            {
                return Ok(new
                {
                    sucesso
                });
            }
            else
            {
                return BadRequest();
            }
        }


        [Route("[action]")]
        [HttpGet]
        public IActionResult Obter(int id)
        {
            API.Services.TarefaService ts = new Services.TarefaService();
            var tarefa = ts.Obter(id);

            ViewModels.TarefaViewModel tarefaVM = new ViewModels.TarefaViewModel()
            {
                Id = tarefa.Id,
                Descricao = tarefa.Descricao,
            };

            return Ok(tarefaVM);
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ObterTodos()
        {
            API.Services.TarefaService ts = new Services.TarefaService();

            var tarefas = ts.ObterTodos();

            List<ViewModels.TarefaViewModel> tarefasVM = new();

            foreach (var tarefa in tarefas)
            {
                tarefasVM.Add(new ViewModels.TarefaViewModel()
                {
                    Id = tarefa.Id,
                    Descricao = tarefa.Descricao,
                });
            }

            return Ok(tarefasVM);

        }


    }
}
