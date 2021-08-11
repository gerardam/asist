using System;
using System.Data;
using System.Data.SqlClient;
using SisAsis.Logica;

namespace SisAsis.Datos
{
    public class DPersonal : AccesoBD
    {
        #region Numeradores
        /// <summary>
        /// Numerador de opciones del SP
        /// </summary>
        enum SPOpcion
        {
            InsertarPersonal = 1,
            MostrarPersonal = 2,
            EditarPersonal = 3,
            EliminarPersonal = 4,
            BuscarPersonal = 5,
        }
        #endregion

        public bool InsertarPersonal(LPersonal pm)
        {
            try
            {
                SqlParameter[] parametros =
                {
                    new SqlParameter("@OpcionSP",       SqlDbType.TinyInt),
                    new SqlParameter("@Nombres",        SqlDbType.VarChar),
                    new SqlParameter("@Identificacion", SqlDbType.VarChar),
                    new SqlParameter("@Pais",           SqlDbType.VarChar),
                    new SqlParameter("@Id_cargo",       SqlDbType.Int),
                    new SqlParameter("@SueldoPorHora",  SqlDbType.Decimal),
                };
                parametros[0].Value = SPOpcion.InsertarPersonal;
                parametros[1].Value = pm.Nombres;
                parametros[2].Value = pm.Identificacion;
                parametros[3].Value = pm.Pais;
                parametros[4].Value = pm.Id_cargo;
                parametros[5].Value = pm.SueldoPorHora;

                EjecutarSP("PERSONAL_SP", parametros);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable MostrarPersonal(int desde, int hasta)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter("@OpcionSP",   SqlDbType.TinyInt),
                new SqlParameter("@Desde",      SqlDbType.Int),
                new SqlParameter("@Hasta",      SqlDbType.Int),
            };
            parametros[0].Value = SPOpcion.MostrarPersonal;
            parametros[1].Value = desde;
            parametros[2].Value = hasta;

            return EjecutarDTSP("PERSONAL_SP", parametros);
        }

        public bool EditarPersonal(LPersonal pm)
        {
            try
            {
                SqlParameter[] parametros =
                {
                    new SqlParameter("@OpcionSP",       SqlDbType.TinyInt),
                    new SqlParameter("@Id_personal",    SqlDbType.Int),
                    new SqlParameter("@Nombres",        SqlDbType.VarChar),
                    new SqlParameter("@Identificacion", SqlDbType.VarChar),
                    new SqlParameter("@Pais",           SqlDbType.VarChar),
                    new SqlParameter("@Id_cargo",       SqlDbType.Int),
                    new SqlParameter("@SueldoPorHora",  SqlDbType.Decimal),
                };
                parametros[0].Value = SPOpcion.EditarPersonal;
                parametros[1].Value = pm.Id_personal;
                parametros[2].Value = pm.Nombres;
                parametros[3].Value = pm.Identificacion;
                parametros[4].Value = pm.Pais;
                parametros[5].Value = pm.Id_cargo;
                parametros[6].Value = pm.SueldoPorHora;

                EjecutarSP("PERSONAL_SP", parametros);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EliminarPersonal(LPersonal pm)
        {
            try
            {
                SqlParameter[] parametros =
                {
                    new SqlParameter("@OpcionSP",       SqlDbType.TinyInt),
                    new SqlParameter("@Id_personal",    SqlDbType.Int),
                };
                parametros[0].Value = SPOpcion.EliminarPersonal;
                parametros[1].Value = pm.Id_personal;

                EjecutarSP("PERSONAL_SP", parametros);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable BuscarPersonal(int desde, int hasta, string buscador)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter("@OpcionSP",   SqlDbType.TinyInt),
                new SqlParameter("@Desde",      SqlDbType.Int),
                new SqlParameter("@Hasta",      SqlDbType.Int),
                new SqlParameter("@Buscador",   SqlDbType.VarChar),
            };
            parametros[0].Value = SPOpcion.BuscarPersonal;
            parametros[1].Value = desde;
            parametros[2].Value = hasta;
            parametros[3].Value = buscador;

            return EjecutarDTSP("PERSONAL_SP", parametros);
        }
    }
}
