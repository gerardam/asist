using System;
using System.Data;
using System.Data.SqlClient;
using SisAsis.Logica;

//| || //  (( |+|========================================================
//| ||// ((   |+| AsisT   | 17-09-2021                                   
//| ||\\ ((   |+| Kyocode | www.kyocode.com | Gerardo Alvarez Mendoza    
//| || \\  (( |+|========================================================
namespace SisAsis.Datos
{
    public class DPermiso : AccesoBD
    {
        #region Numeradores
        /// <summary>
        /// Numerador de opciones del SP
        /// </summary>
        enum SPOpcion
        {
            InsertarPermiso = 1,
            EliminarPermiso = 2,
            MostrarPermisos = 3,
        }
        #endregion

        public bool InsertarPermiso(LPermiso pm)
        {
            try
            {
                SqlParameter[] parametros =
                {
                    new SqlParameter("@OpcionSP",       SqlDbType.TinyInt),
                    new SqlParameter("@IdModulo",       SqlDbType.Int),
                    new SqlParameter("@IdUsuario",      SqlDbType.Int),
                };
                parametros[0].Value = SPOpcion.InsertarPermiso;
                parametros[1].Value = pm.IdModulo;
                parametros[2].Value = pm.IdUsuario;

                EjecutarSP("PERMISO_SP", parametros);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EliminarPermiso(LPermiso pm)
        {
            try
            {
                SqlParameter[] parametros =
                {
                    new SqlParameter("@OpcionSP",       SqlDbType.TinyInt),
                    new SqlParameter("@IdUsuario",      SqlDbType.Int),
                };
                parametros[0].Value = SPOpcion.EliminarPermiso;
                parametros[1].Value = pm.IdUsuario;

                EjecutarSP("PERMISO_SP", parametros);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable MostrarPermisos(int idUsuario)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter("@OpcionSP",       SqlDbType.TinyInt),
                new SqlParameter("@IdUsuario",      SqlDbType.Int),
            };
            parametros[0].Value = SPOpcion.MostrarPermisos;
            parametros[1].Value = idUsuario;

            return EjecutarDTSP("PERMISO_SP", parametros);
        }
    }
}
