using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisAsis.Logica
{
    public class LAsistencia
    {
        public int Id_asistencia { get; set; }
        public int Id_personal { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public double Horas { get; set; }
        public string Observaciones { get; set; }
        public string Estado { get; set; }
    }
}
