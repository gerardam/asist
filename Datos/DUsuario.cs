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
    public class DUsuario : AccesoBD
    {
        #region Numeradores
        /// <summary>
        /// Numerador de opciones del SP
        /// </summary>
        enum SPOpcion
        {
            InsertarUsuario = 1,
            MostrarUsuarios = 2,
            ObtenerIdUsuario = 3
        }
        #endregion

        public bool InsertarUsuario(LUsuario pm)
        {
            try
            {
                SqlParameter[] parametros =
                {
                    new SqlParameter("@OpcionSP",       SqlDbType.TinyInt),
                    new SqlParameter("@Nombre",         SqlDbType.VarChar),
                    new SqlParameter("@Login",          SqlDbType.VarChar),
                    new SqlParameter("@Password",       SqlDbType.VarChar),
                    new SqlParameter("@Icono",          SqlDbType.Image),
                    new SqlParameter("@Estado",         SqlDbType.VarChar),
                };
                parametros[0].Value = SPOpcion.InsertarUsuario;
                parametros[1].Value = pm.Nombre;
                parametros[2].Value = pm.Login;
                parametros[3].Value = pm.Password;
                parametros[4].Value = pm.Icono;
                parametros[5].Value = pm.Estado;

                EjecutarSP("USUARIO_SP", parametros);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable MostrarUsuarios()
        {
            SqlParameter[] parametros =
            {
                new SqlParameter("@OpcionSP",       SqlDbType.TinyInt),
            };
            parametros[0].Value = SPOpcion.MostrarUsuarios;

            return EjecutarDTSP("USUARIO_SP", parametros);
        }

        public int ObtenerIdUsuario(string usuario)
        {
            try
            {
                SqlParameter[] parametros =
                {
                    new SqlParameter("@OpcionSP",       SqlDbType.TinyInt),
                    new SqlParameter("@Login",          SqlDbType.VarChar),
                };
                parametros[0].Value = SPOpcion.ObtenerIdUsuario;
                parametros[1].Value = usuario;

                return EjecutarIntSP("USUARIO_SP", parametros);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
