using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string SegundoNombreEmpleado { get; set; }
        public string ApellidoPaternoEmpleado { get; set; }
        public string ApellidoMaternoEmpleado { get; set; }
        public string Edad { get; set; }
        public string Email { get; set; }
        public string Imagen { get; set; }
        public ML.Area Area { get; set; }
        public List<object> Empleados { get; set; }
    }
}
