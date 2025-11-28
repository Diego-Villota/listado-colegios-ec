using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Pages.Colegios
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Colegio> ListaColegios { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? FiltroRegion { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? FiltroProvincia { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? FiltroCiudad { get; set; }

        public List<string> Regiones { get; set; } = new();
        public List<string> Provincias { get; set; } = new();
        public List<string> Ciudades { get; set; } = new();

        public async Task OnGet()
        {
            // REGIONES
            Regiones = await _context.Colegios
                .Where(c => c.Region != null && c.Region != "")
                .Select(c => c.Region)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

            // PROVINCIAS dependientes
            var provinciasQuery = _context.Colegios.AsQueryable();
            if (!string.IsNullOrEmpty(FiltroRegion))
                provinciasQuery = provinciasQuery.Where(c => c.Region == FiltroRegion);

            Provincias = await provinciasQuery
                .Where(c => c.Provincia != null && c.Provincia != "")
                .Select(c => c.Provincia)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

            // CIUDADES dependientes
            var ciudadesQuery = _context.Colegios.AsQueryable();
            if (!string.IsNullOrEmpty(FiltroRegion))
                ciudadesQuery = ciudadesQuery.Where(c => c.Region == FiltroRegion);

            if (!string.IsNullOrEmpty(FiltroProvincia))
                ciudadesQuery = ciudadesQuery.Where(c => c.Provincia == FiltroProvincia);

            Ciudades = await ciudadesQuery
                .Where(c => c.Ciudad != null && c.Ciudad != "")
                .Select(c => c.Ciudad)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

            // FILTRADO final
            var query = _context.Colegios.AsQueryable();

            if (!string.IsNullOrEmpty(FiltroRegion))
                query = query.Where(c => c.Region == FiltroRegion);

            if (!string.IsNullOrEmpty(FiltroProvincia))
                query = query.Where(c => c.Provincia == FiltroProvincia);

            if (!string.IsNullOrEmpty(FiltroCiudad))
                query = query.Where(c => c.Ciudad == FiltroCiudad);

            ListaColegios = await query.ToListAsync();
        }
    }
}
