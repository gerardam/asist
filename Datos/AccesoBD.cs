using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisAsis.Datos
{
    public class AccesoBD
    {
        #region Variables
        public static string cadenaBD = @"Data source=GAMAW\GAM;Initial Catalog=ASISDB;Persist Security Info=True;User ID=sa; Password=10TENsaiga";
        public static SqlConnection cnn = new SqlConnection(cadenaBD);
        #endregion

        #region Metodos eventos BD
        public static void AbrirCon()
        {
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
        }

        public static void CerrarCon()
        {
            if (cnn.State == ConnectionState.Open)
            {
                cnn.Close();
            }
        }
        #endregion

        #region Ejecucion de SPs
        protected void EjecutarSP(string nombreSP, IDataParameter[] arrParametros)
        {
            AbrirCon();
            SqlCommand cmd = new SqlCommand(nombreSP, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in arrParametros)
            {
                cmd.Parameters.Add(parameter);
            }
            cmd.ExecuteNonQuery();
            CerrarCon();
        }

        protected DataTable EjecutarDTSP(string nombreSP, IDataParameter[] arrParametros)
        {
            DataTable dt = new DataTable();
            AbrirCon();
            SqlDataAdapter da = new SqlDataAdapter(nombreSP, cnn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in arrParametros)
            {
                da.SelectCommand.Parameters.Add(parameter);
            }
            da.Fill(dt);

            return dt;
        }
        #endregion
    }
}
