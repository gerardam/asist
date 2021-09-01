using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SisAsis.Datos;
using SisAsis.Logica;

namespace SisAsis.Presentacion
{
    public partial class CtlUsuario : UserControl
    {
        #region VARIABLES

        #endregion

        #region EVENTOS
        public CtlUsuario()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Limpiar();
            HabilitarPaneles();
            MostrarModulos();
        }
        #endregion

        #region FUNCIONES
        private void MostrarModulos()
        {
            DataTable dtDatos = new DModulo().MostrarPersonal();
            dgvPermisos.DataSource = dtDatos;
            Bases.DisenhoDgv(ref dgvPermisos);
        }

        private void Limpiar()
        {
            txtNombre.Clear();
            txtUsuario.Clear();
            txtContrasena.Clear();
        }

        private void HabilitarPaneles()
        {
            pnlRegistro.Visible = true;
            lblAnuncioIcono.Visible = true;
            pnlIcon.Visible = false;
            pnlRegistro.Dock = DockStyle.Fill;
            pnlRegistro.BringToFront();
            btnGuardar.Visible = true;
            btnActualizar.Visible = false;
        }
        #endregion

        

        
    }
}
