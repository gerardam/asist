using SisAsis.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisAsis.Datos
{
    public class DAsistencia : AccesoBD
    {
        #region Numeradores
        /// <summary>
        /// Numerador de opciones del SP
        /// </summary>
        enum SPOpcion
        {
            InsertarAsistencia = 1,
            ActualizarSalida = 2,
            BuscarAsistenciaId = 3
        }
        #endregion

        public bool InsertarAsistencia(LAsistencia pm)
        {
            try
            {
                SqlParameter[] parametros =
                {
                    new SqlParameter("@OpcionSP",       SqlDbType.TinyInt),
                    new SqlParameter("@Id_personal",    SqlDbType.Int),
                    new SqlParameter("@FechaEntrada",   SqlDbType.DateTime),
                    new SqlParameter("@FechaSalida",    SqlDbType.DateTime),
                    new SqlParameter("@Estado",         SqlDbType.VarChar),
                    new SqlParameter("@Horas",          SqlDbType.Decimal),
                    new SqlParameter("@Observaciones",  SqlDbType.VarChar),
                };
                parametros[0].Value = SPOpcion.InsertarAsistencia;
                parametros[1].Value = pm.Id_personal;
                parametros[2].Value = pm.FechaEntrada;
                parametros[3].Value = pm.FechaSalida;
                parametros[4].Value = pm.Estado;
                parametros[5].Value = pm.Horas;
                parametros[6].Value = pm.Observaciones;

                EjecutarSP("ASISTENCIA_SP", parametros);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ActualizarSalida(LAsistencia pm)
        {
            try
            {
                SqlParameter[] parametros =
                {
                    new SqlParameter("@OpcionSP",       SqlDbType.TinyInt),
                    new SqlParameter("@Id_personal",    SqlDbType.Int),
                    new SqlParameter("@FechaSalida",    SqlDbType.DateTime),
                    new SqlParameter("@Horas",          SqlDbType.Decimal),
                };
                parametros[0].Value = SPOpcion.ActualizarSalida;
                parametros[1].Value = pm.Id_personal;
                parametros[2].Value = pm.FechaSalida;
                parametros[3].Value = pm.Horas;

                EjecutarSP("ASISTENCIA_SP", parametros);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable BuscarAsistenciaId(int idPersona)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter("@OpcionSP",       SqlDbType.TinyInt),
                new SqlParameter("@Id_personal",    SqlDbType.Int),
            };
            parametros[0].Value = SPOpcion.BuscarAsistenciaId;
            parametros[1].Value = idPersona;

            return EjecutarDTSP("ASISTENCIA_SP", parametros);
        }

    }
}
