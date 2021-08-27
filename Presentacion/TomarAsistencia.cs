using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SisAsis.Presentacion
{
    public partial class TomarAsistencia : Form
    {
        public TomarAsistencia()
        {
            InitializeComponent();
        }

        private void TomarAsistencia_Load(object sender, EventArgs e)
        {

        }

        private void timerHora_Tick(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToShortDateString();
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void txtIdentificacion_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
