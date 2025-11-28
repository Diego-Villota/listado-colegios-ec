using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Colegio
    {
        public long Id { get; set; }

        public string? Nombre { get; set; }

        public string? Direccion { get; set; }

        public string? Telefono { get; set; }

        public string? Correo { get; set; }

        [Column("Sitio web")]
        public string? SitioWeb { get; set; }

        [Column("Red social")]
        public string? RedSocial { get; set; }

        public string? Sector { get; set; }

        public string? Ciudad { get; set; }

        public string? Provincia { get; set; }

        public string? Region { get; set; }
    }
}

