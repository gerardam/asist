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
using System.IO;

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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombre.Text))
            {
                if (!string.IsNullOrEmpty(txtUsuario.Text))
                {
                    if (!string.IsNullOrEmpty(txtContrasena.Text))
                    {
                        if (lblAnuncioIcono.Visible == false)
                        {
                            InsertarUsuarios();
                        }
                        else
                        {
                            MessageBox.Show("Seleccione un icono");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ingrese la contraseña");
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese el usuario");
                }
            }
            else
            {
                MessageBox.Show("Ingrese el nombre");
            }
        }

        private void InsertarUsuarios()
        {
            LUsuario pm = new LUsuario();
            pm.Nombre = txtNombre.Text;
            pm.Login = txtUsuario.Text;
            pm.Password = txtContrasena.Text;
            pm.Estado = "ACTIVO";

            MemoryStream ms = new MemoryStream();
            pbIcono.Image.Save(ms, pbIcono.Image.RawFormat);
            pm.Icono = ms.GetBuffer();

            if (new DUsuario().InsertarUsuario(pm))
            {

            }
        }
    }
}
