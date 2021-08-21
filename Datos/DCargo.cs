using SisAsis.Logica;
using System;
using System.Data;
using System.Data.SqlClient;

//| || //  (( |+|========================================================
//| ||// ((   |+| AsisT   | 05-07-2021                                   
//| ||\\ ((   |+| Kyocode | www.kyocode.com | Gerardo Alvarez Mendoza    
//| || \\  (( |+|========================================================
namespace SisAsis.Datos
{
    public class DCargo : AccesoBD
    {
        #region Numeradores
        /// <summary>
        /// Numerador de opciones del SP
        /// </summary>
        enum SPOpcion
        {
            InsertarCargo = 1,
            //MostrarCargo = 2,
            EditarCargo = 3,
            //EliminarCargo = 4,
            BuscarCargo = 5,
        }
        #endregion

        public bool InsertarCargo(LCargo pm)
        {
            try
            {
                SqlParameter[] parametros =
                {
                    new SqlParameter("@OpcionSP",       SqlDbType.TinyInt),
                    new SqlParameter("@Cargo",          SqlDbType.VarChar),
                    new SqlParameter("@SueldoPorHora",  SqlDbType.Decimal),
                };
                parametros[0].Value = SPOpcion.InsertarCargo;
                parametros[1].Value = pm.Cargo;
                parametros[2].Value = pm.SueldoPorHora;

                EjecutarSP("CARGO_SP", parametros);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EditarCargo(LCargo pm)
        {
            try
            {
                SqlParameter[] parametros =
                {
                    new SqlParameter("@OpcionSP",       SqlDbType.TinyInt),
                    new SqlParameter("@Id_cargo",       SqlDbType.Int),
                    new SqlParameter("@Cargo",          SqlDbType.VarChar),
                    new SqlParameter("@SueldoPorHora",  SqlDbType.Decimal),
                };
                parametros[0].Value = SPOpcion.EditarCargo;
                parametros[1].Value = pm.Id_cargo;
                parametros[2].Value = pm.Cargo;
                parametros[3].Value = pm.SueldoPorHora;

                EjecutarSP("CARGO_SP", parametros);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable BuscarCargo(string buscador)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter("@OpcionSP",   SqlDbType.TinyInt),
                new SqlParameter("@Buscador",   SqlDbType.VarChar),
            };
            parametros[0].Value = SPOpcion.BuscarCargo;
            parametros[1].Value = buscador;

            return EjecutarDTSP("CARGO_SP", parametros);
        }
    }
}
