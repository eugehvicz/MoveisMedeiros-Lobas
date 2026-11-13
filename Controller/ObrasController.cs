using Microsoft.AspNetCore.Mvc;
using MoveisMedeiros.Api.Data;

namespace MoveisMedeiros.Api.Controller
{
    public class ObrasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ObrasController(AppDbContext context)
        {
            _context = context;
        }


        


    }
}
