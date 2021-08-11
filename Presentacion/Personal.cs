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
    public partial class Personal : UserControl
    {
        #region VARIABLES
        int IdCargo = 0;
        int Desde = 1;
        int Hasta = 10;
        int Contador;
        int IdPersona;
        private int ItemPorPag = 10;
        string Estatus;
        int TotalPag;
        #endregion

        #region EVENTOS
        public Personal()
        {
            InitializeComponent();
        }

        private void Personal_Load(object sender, EventArgs e)
        {
            MostrarPersonal();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            LocalizarDgvCargo();
            pnlCargo.Visible = false;
            pnlPaginado.Visible = false;
            pnlRegistro.Visible = true;
            pnlRegistro.Dock = DockStyle.Fill;
            btnGuardarP.Visible = true;
            btnGuardarCP.Visible = false;
            Limpiar();
        }

        private void btnGuardarP_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombres.Text))
            {
                if (!string.IsNullOrEmpty(txtIdentificacion.Text))
                {
                    if (!string.IsNullOrEmpty(cbxPais.Text))
                    {
                        if (IdCargo > 0)
                        {
                            if (!string.IsNullOrEmpty(txtSueldo.Text))
                            {
                                InsertarPersonal();
                            }
                        }
                    }
                }
            }
        }

        private void btnGuardarCP_Click(object sender, EventArgs e)
        {

        }

        private void btnVolverP_Click(object sender, EventArgs e)
        {
            pnlRegistro.Visible = false;
        }

        private void txtSueldo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Decimales(txtSueldo, e);
        }

        private void btnAgregarCargo_Click(object sender, EventArgs e)
        {
            pnlCargo.Visible = true;
            pnlCargo.Dock = DockStyle.Fill;
            pnlCargo.BringToFront();
            btnGuardarC.Visible = true;
            btnGuardarCC.Visible = false;
            txtCargoG.Clear();
            txtSueldoG.Clear();
        }

        private void btnGuardarC_Click(object sender, EventArgs e)
        {
            InsertarCargos();
        }

        private void btnGuardarCC_Click(object sender, EventArgs e)
        {
            EditarCargos();
        }

        private void btnVolverC_Click(object sender, EventArgs e)
        {
            pnlCargo.Visible = false;
        }

        private void txtCargo_TextChanged(object sender, EventArgs e)
        {
            BuscarCargos();
        }

        private void txtSueldoG_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Decimales(txtSueldoG, e);
        }

        private void dgvListadoCargos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvListadoCargos.Columns["EditarC"].Index)
            {
                ObtenerCargosEditar();
            }
            if (e.ColumnIndex == dgvListadoCargos.Columns["Cargo"].Index)
            {
                ObtenerDatosCargo();
            }
        }

        #endregion

        #region FUNCIONES
        private void InsertarPersonal()
        {
            LPersonal pm = new LPersonal();
            pm.Nombres = txtNombres.Text;
            pm.Identificacion = txtIdentificacion.Text;
            pm.Pais = cbxPais.Text;
            pm.Id_cargo = IdCargo;
            pm.SueldoPorHora = Convert.ToDouble(txtSueldo.Text);
            if (new DPersonal().InsertarPersonal(pm))
            {
                MostrarPersonal();
                pnlRegistro.Visible = false;
            }
        }

        private void EditarPersonal()
        {

        }

        private void MostrarPersonal()
        {
            DataTable dtDatos = new DPersonal().MostrarPersonal(Desde, Hasta);
            dgvPersonal.DataSource = dtDatos;
            Bases.DisenhoDgv(ref dgvPersonal);
            dgvPersonal.Columns[2].Visible = false;
            dgvPersonal.Columns[7].Visible = false;
            pnlPaginado.Visible = true;
        }

        private void InsertarCargos()
        {
            if (!string.IsNullOrEmpty(txtCargoG.Text))
            {
                if (!string.IsNullOrEmpty(txtSueldoG.Text))
                {
                    LCargo pm = new LCargo();
                    pm.Cargo = txtCargoG.Text;
                    pm.SueldoPorHora = Convert.ToDouble(txtSueldoG.Text);
                    if (new DCargo().InsertarCargo(pm))
                    {
                        txtCargo.Clear();
                        BuscarCargos();
                        pnlCargo.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Agregar el sueldo", "Falta el sueldo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Agregar el cargo", "Falta el cargo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EditarCargos()
        {
            if (!string.IsNullOrEmpty(txtCargoG.Text))
            {
                if (!string.IsNullOrEmpty(txtSueldoG.Text))
                {
                    LCargo pm = new LCargo();
                    pm.Id_cargo = IdCargo;
                    pm.Cargo = txtCargoG.Text;
                    pm.SueldoPorHora = Convert.ToDouble(txtSueldoG.Text);
                    if (new DCargo().EditarCargo(pm))
                    {
                        txtCargo.Clear();
                        BuscarCargos();
                        pnlCargo.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Agregar el sueldo", "Falta el sueldo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Agregar el cargo", "Falta el cargo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BuscarCargos()
        {
            DataTable dtDatos = new DCargo().BuscarCargo(txtCargo.Text);
            dgvListadoCargos.DataSource = dtDatos;
            Bases.DisenhoDgv(ref dgvListadoCargos);
            dgvListadoCargos.Columns[1].Visible = false;
            dgvListadoCargos.Columns[3].Visible = false;
            dgvListadoCargos.Visible = true;
        }

        private void LocalizarDgvCargo()
        {
            dgvListadoCargos.Location = new Point(panel8.Location.X, panel8.Location.Y + 1);
            dgvListadoCargos.Size = new Size(300, 90);
            dgvListadoCargos.Visible = true;
            lblSueldoP.Visible = false;
            pnlBtnsGuardarP.Visible = false;
        }

        private void ObtenerDatosCargo()
        {
            IdCargo = Convert.ToInt32(dgvListadoCargos.SelectedCells[1].Value);
            txtCargo.Text = dgvListadoCargos.SelectedCells[2].Value.ToString();
            txtSueldo.Text = dgvListadoCargos.SelectedCells[3].Value.ToString();
            dgvListadoCargos.Visible = false;
            pnlBtnsGuardarP.Visible = true;
            lblSueldoP.Visible = true;
        }

        private void ObtenerCargosEditar()
        {
            IdCargo = Convert.ToInt32(dgvListadoCargos.SelectedCells[1].Value);
            txtCargoG.Text = dgvListadoCargos.SelectedCells[2].Value.ToString();
            txtSueldoG.Text = dgvListadoCargos.SelectedCells[3].Value.ToString();
            btnGuardarC.Visible = false;
            btnGuardarCC.Visible = true;
            txtCargoG.Focus();
            txtCargoG.SelectAll();
            pnlCargo.Visible = true;
            pnlCargo.Dock = DockStyle.Fill;
            pnlCargo.BringToFront();
        }

        private void Limpiar()
        {
            txtNombres.Clear();
            txtIdentificacion.Clear();
            txtCargo.Clear();
            txtSueldo.Clear();
            BuscarCargos();
        }


        #endregion

        private void dgvPersonal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvPersonal.Columns["Eliminar"].Index)
            {
                
            }
            if (e.ColumnIndex == dgvPersonal.Columns["Editar"].Index)
            {
                
            }
        }
    }
}
