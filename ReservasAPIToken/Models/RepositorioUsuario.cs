using System.Collections.Generic;
using System.Linq;

namespace ReservasAPIToken.Models
{
    public class RepositorioUsuario
    {
        public static DadosUsuario Get(string usuario, string senha)
        {
            List<DadosUsuario> users = new List<DadosUsuario>();
            users.Add(new DadosUsuario { Id = 1, Usuario = "daniel", Senha = "daniel" });
            users.Add(new DadosUsuario { Id = 2, Usuario = "bruna", Senha = "bruna" });
            users.Add(new DadosUsuario { Id = 3, Usuario = "bernardo", Senha = "bernardo" });
            return users.Where(x => x.Usuario.ToLower() == usuario.ToLower() && x.Senha == senha.ToLower()).FirstOrDefault();
        }
    }
}
