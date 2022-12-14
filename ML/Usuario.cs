using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    //ATRIBUTOS REFLEJO DE LA BASE DE DATOS
    //CAPA ML MODELO
    public class Usuario
    {
        public int? IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string SegundoNombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Edad { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public bool Estatus { get; set; }
        public string NombreUsuario { get; set; }
        public List<object> Usuarios { get; set; }
        public ML.Rol Rol { get; set; }

    }
}
