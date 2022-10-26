using LocadoraCarros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LocadoraCarros.ViewComponents
{
    public class AluguelViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _applicationDBContext;

        public AluguelViewComponent(ApplicationDbContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(int carroId)
        {
            return View(await _applicationDBContext.Carros.FirstOrDefaultAsync(c => c.Id == carroId));
        }

    }
}
