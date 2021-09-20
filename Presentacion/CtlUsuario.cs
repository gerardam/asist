using SisAsis.Datos;
using SisAsis.Logica;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SisAsis.Presentacion
{
    public partial class CtlUsuario : UserControl
    {
        #region VARIABLES
        int IdUsuario;
        #endregion

        #region EVENTOS
        public CtlUsuario()
        {
            InitializeComponent();
        }

        private void CtlUsuario_Load(object sender, EventArgs e)
        {
            MostrarUsuarios();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Limpiar();
            HabilitarPaneles();
            MostrarModulos();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            pnlRegistro.Visible = false;
        }

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

        private void txtContrasena_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        #region Seleccion de icono
        private void lblAnuncioIcono_Click(object sender, EventArgs e)
        {
            MostrarPanelIcon();
        }

        private void pbIcono_Click(object sender, EventArgs e)
        {
            MostrarPanelIcon();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pbIcono.Image = pictureBox3.Image;
            OcultarPanelIcon();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pbIcono.Image = pictureBox4.Image;
            OcultarPanelIcon();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            pbIcono.Image = pictureBox10.Image;
            OcultarPanelIcon();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            pbIcono.Image = pictureBox5.Image;
            OcultarPanelIcon();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            pbIcono.Image = pictureBox6.Image;
            OcultarPanelIcon();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            pbIcono.Image = pictureBox7.Image;
            OcultarPanelIcon();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            pbIcono.Image = pictureBox9.Image;
            OcultarPanelIcon();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            pbIcono.Image = pictureBox8.Image;
            OcultarPanelIcon();
        }

        private void pbAgregaIconPC_Click(object sender, EventArgs e)
        {
            ofdIcono.InitialDirectory = "";
            ofdIcono.Filter = "Images|*.jpg;*.png";
            ofdIcono.FilterIndex = 2;
            ofdIcono.Title = "Cargar imagen";
            if (ofdIcono.ShowDialog() == DialogResult.OK)
            {
                pbIcono.BackgroundImage = null;
                pbIcono.Image = new Bitmap(ofdIcono.FileName);
                OcultarPanelIcon();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            OcultarPanelIcon();
        }
        #endregion
        #endregion

        #region FUNCIONES
        private void MostrarModulos()
        {
            DataTable dtDatos = new DModulo().MostrarPersonal();
            dgvPermisos.DataSource = dtDatos;
            Bases.DisenhoDgv(ref dgvPermisos);
            dgvPermisos.Columns[1].Visible = false;
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
                ObtenerIdUsuario();
                InsertarPermisos();
            }
        }

        private void ObtenerIdUsuario()
        {
            IdUsuario = new DUsuario().ObtenerIdUsuario(txtUsuario.Text);
        }

        private void InsertarPermisos()
        {
            foreach (DataGridViewRow row in dgvPermisos.Rows)
            {
                int idModulo = Convert.ToInt32(row.Cells["IdModulo"].Value);
                bool marcado = Convert.ToBoolean(row.Cells["Marcar"].Value);
                if (marcado == true)
                {
                    LPermiso pm = new LPermiso();
                    pm.IdModulo = idModulo;
                    pm.IdUsuario = IdUsuario;
                    new DPermiso().InsertarPermiso(pm);
                }
            }
            MostrarUsuarios();
            pnlRegistro.Visible = false;
        }

        private void MostrarUsuarios()
        {
            DataTable dtDatos = new DUsuario().MostrarUsuarios();
            dgvPersonal.DataSource = dtDatos;
            DisenoDgvUsuarios();
        }

        private void MostrarPanelIcon()
        {
            pnlIcon.Visible = true;
            pnlIcon.Dock = DockStyle.Fill;
            pnlIcon.BringToFront();
        }

        private void OcultarPanelIcon()
        {
            pnlIcon.Visible = false;
            lblAnuncioIcono.Visible = false;
            pbIcono.Visible = true;
        }

        private void DisenoDgvUsuarios()
        {
            Bases.DisenhoDgv(ref dgvPersonal);
            Bases.DiseñoDtvEliminar(ref dgvPersonal);
            dgvPersonal.Columns[2].Visible = false;
            dgvPersonal.Columns[5].Visible = false;
            dgvPersonal.Columns[6].Visible = false;
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
