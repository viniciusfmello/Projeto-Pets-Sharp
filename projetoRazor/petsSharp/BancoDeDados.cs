using System.Data;
using System.Data.SqlClient;

namespace petsSharp
{
    public class BancoDeDados
    {
        private string Connection;
        public SqlConnection conexao;
        public BancoDeDados() {
            if (Connection == null)
                Connection = File.ReadAllText(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString()), "connection.txt"));
        }

        public void abrirConexao ()
        {
            conexao = new SqlConnection(Connection);
            conexao.Open ();

        }
        public void fechar()
        {
            conexao.Close ();
        }
        public SqlDataReader executarQuery(string query)
        {
            //abrirConexao();
            SqlCommand cmd = new SqlCommand(query,conexao);
            SqlDataReader resultado = cmd.ExecuteReader();
            //fechar();

            return resultado;
           
        }

        public void manipularDado(string query)
        {
            abrirConexao();
            SqlCommand cmd = new SqlCommand(query, conexao);
            cmd.ExecuteNonQuery();
            fechar();
        }
        public void teste()
        {
            abrirConexao();
            Console.WriteLine(conexao.State); 
        }
    }
}
