using SisAsis.Datos;
using SisAsis.Logica;
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
        #region VARIABLES
        string Identificacion;
        int IdPersonal;
        int Contador;
        DateTime FechaReg;
        #endregion

        #region EVENTOS
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
            BuscarPersonalIdentidad();
            if (Identificacion == txtIdentificacion.Text)
            {
                BuscarAsistenciaId();
                if (Contador == 0)
                {
                    DialogResult resultado = MessageBox.Show("Agregar una observacion?", "Observacion", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (resultado == DialogResult.OK)
                    {
                        pnlObservacion.Visible = true;
                        pnlObservacion.Location = new Point(pnlRegistro.Location.X, pnlRegistro.Location.Y);
                        pnlObservacion.Size = new Size(pnlRegistro.Width, pnlRegistro.Height);
                        pnlObservacion.BringToFront();
                        rtbObservacion.Clear();
                        rtbObservacion.Focus();
                    }
                    else
                    {
                        InsertarAsistencia();
                    }
                }
                else
                {
                    ConfirmarSalida();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertarAsistencia();
        }
        #endregion

        #region FUNCIONES
        private void InsertarAsistencia()
        {
            if (string.IsNullOrEmpty(rtbObservacion.Text))
            {
                rtbObservacion.Text = "-";
            }
            LAsistencia pm = new LAsistencia();
            pm.Id_personal = IdPersonal;
            pm.FechaEntrada = DateTime.Now;
            pm.FechaSalida = DateTime.Now;
            pm.Estado = "ENTRADA";
            pm.Horas = 0;
            pm.Observaciones = rtbObservacion.Text;
            if (new DAsistencia().InsertarAsistencia(pm))
            {
                lblAviso.Text = "ENTRADA REGISTRADA";
                txtIdentificacion.Clear();
                txtIdentificacion.Focus();
                pnlObservacion.Visible = false;
            }
        }

        private void ConfirmarSalida()
        {
            LAsistencia pm = new LAsistencia();
            pm.Id_personal = IdPersonal;
            pm.FechaSalida = DateTime.Now;
            pm.Horas = Bases.DateDiff(Bases.DateInterval.Hour, FechaReg, DateTime.Now);
            if (new DAsistencia().ActualizarSalida(pm))
            {
                lblAviso.Text = "SALIDA REGISTRADA";
                txtIdentificacion.Clear();
                txtIdentificacion.Focus();
            }
        }

        private void BuscarPersonalIdentidad()
        {
            DataTable dtDatos = new DPersonal().BuscarPersonalIdentidad(txtIdentificacion.Text);
            if (dtDatos.Rows.Count > 0)
            {
                Identificacion = dtDatos.Rows[0]["Identificacion"].ToString();
                IdPersonal = Convert.ToInt32(dtDatos.Rows[0]["Id_personal"]);
                lblNombre.Text = dtDatos.Rows[0]["Nombres"].ToString();
            }
        }

        private void BuscarAsistenciaId()
        {
            DataTable dtDatos = new DAsistencia().BuscarAsistenciaId(IdPersonal);
            Contador = dtDatos.Rows.Count;
            if (Contador > 0)
            {
                FechaReg = Convert.ToDateTime(dtDatos.Rows[0]["FechaEntrada"]);
            }
        }
        #endregion
    }
}
