using SisAsis.Logica;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SisAsis.Datos
{
    public class DModulo : AccesoBD
    {
        #region Numeradores
        /// <summary>
        /// Numerador de opciones del SP
        /// </summary>
        enum SPOpcion
        {
            MostrarPersonal = 1,
        }
        #endregion

        public DataTable MostrarPersonal()
        {
            SqlParameter[] parametros =
            {
                new SqlParameter("@OpcionSP",   SqlDbType.TinyInt),
            };
            parametros[0].Value = SPOpcion.MostrarPersonal;

            return EjecutarDTSP("MODULO_SP", parametros);
        }
    }
}
