using System.Threading.Tasks;

namespace LocadoraCarros.Services
{
    public interface IEmail
    {
        Task EnviarEmail(string email, string assunto, string mensagem);
    }
}
