using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservasAPIToken.Models;
using ReservasAPIToken.Services;

namespace ReservasAPIToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : Controller
    {
        private IRepositorioReserva repository;
        public ReservasController(IRepositorioReserva repositorio) => repository = repositorio;

        [HttpGet]
        [Authorize]
        public IEnumerable<Reserva> Get() => repository.Reservas;

        [HttpGet("{id}")]
        [Authorize]
        public Reserva Get(int id) => repository[id];

        [HttpPost]
        [Authorize]
        public Reserva Post([FromBody] Reserva reserva) =>
        repository.AdicionarReserva(new Reserva
        {
            Nome = reserva.Nome,
            LocalInicioLocacao = reserva.LocalInicioLocacao,
            LocalFimLocacao = reserva.LocalFimLocacao,
            Veiculo = reserva.Veiculo
        });

        [HttpPut]
        [Authorize]
        public Reserva Put([FromBody] Reserva reserva) => repository.AlterarReserva(reserva);

        [HttpPatch]
        [Authorize]
        public Reserva Patch([FromBody] Reserva reserva) => repository.InserirAlterarReserva(reserva);
        
        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(int id) => repository.DeletarReserva(id);
    }

    public class AutenticacaoController: Controller
    {
        [HttpPost]
        [Route("token")]
        public ActionResult<dynamic> Authenticate([FromHeader(Name = "usuario")] string usuario, [FromHeader(Name = "senha")] string senha)
        {

            if (Funcao.CampoEhNulo(usuario))
                return NotFound(new { message = "Valor do parâmetro usuario não encontrado" });

            if (Funcao.CampoEhNulo(senha))
                return NotFound(new { message = "Valor do parâmetro senha não encontrado" });

            // Recupera o usuário
            DadosUsuario dadosUsuario = RepositorioUsuario.Get(usuario, senha);

            // Verifica se o usuário existe
            if (Funcao.CampoEhNulo(dadosUsuario))
                return NotFound(new { message = "Usuário ou senha inválido!" });

            // Gera o Token
            string token = Token.Geracao(dadosUsuario);

            // Retorna os dados
            return new
            {
                access_token = token,
                token_type = "Bearer",
                user = dadosUsuario.Usuario,
            };
        }
    }
}
