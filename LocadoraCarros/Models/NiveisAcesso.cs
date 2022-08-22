using Microsoft.AspNetCore.Identity;

namespace LocadoraCarros.Models
{
    public class NiveisAcesso : IdentityRole
    {
        public string Descricao { get; set; }
    }
}
