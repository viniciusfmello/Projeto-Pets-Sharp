using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace petsSharp.Pages
{
    public class dashboardModel : PageModel
    {
        [BindProperty]
        //public string nome { get; set; }
        public SqlDataReader leitor { get; set; }
        public int totalVenda { get; set; }
        public int totalProdutosCadastrados { get; set; }

        public int totalServicos { get; set; }
        public int totalServicosPendentes { get; set; }
        public int totalFornecedores { get; set; }
        public decimal valorTotal { get; set; }
        public List<string> ListaFornecedores { get; set; }
        public List<int> valorFornecedores { get; set; }
        public dashboardModel()
        {
            
        }

        public void OnGet()
        {
           BancoDeDados bancoDeDados = new BancoDeDados();

            bancoDeDados.abrirConexao();

            leitor = bancoDeDados.executarQuery("select count(distinct codigo_venda) from tb_venda");
            if (leitor.HasRows)
            {
                leitor.Read();
                totalVenda = leitor.GetInt32(0);
            }
            else
            {
                totalVenda = 0;
            }

            bancoDeDados.fechar();
            bancoDeDados.abrirConexao();

            leitor = bancoDeDados.executarQuery("select count(distinct codigo_produto) from tb_produto");

            if (leitor.HasRows)
            {
                leitor.Read();
                totalProdutosCadastrados = leitor.GetInt32(0);
            }
            else
            {
                totalProdutosCadastrados = 0;
            }

            bancoDeDados.fechar();
            bancoDeDados.abrirConexao();

            leitor = bancoDeDados.executarQuery("select count(distinct codigo_servico) from tb_servicos");

            if (leitor.HasRows)
            {
                leitor.Read();
                totalServicos = leitor.GetInt32(0);
            }
            else
            {
                totalServicos = 0;
            }

            bancoDeDados.fechar();
            bancoDeDados.abrirConexao();

            leitor = bancoDeDados.executarQuery("select count( servico_realizado) from tb_servicos where servico_realizado = 'nao'");

            if (leitor.HasRows)
            {
                leitor.Read();
                totalServicosPendentes = leitor.GetInt32(0);
            }
            else
            {
                totalServicosPendentes = 0;
            }

            bancoDeDados.fechar();

            bancoDeDados.abrirConexao();

            leitor = bancoDeDados.executarQuery("select count(distinct codigo_fornecedor) from tb_fornecedor");

            if (leitor.HasRows)
            {
                leitor.Read();
                totalFornecedores = leitor.GetInt32(0);
            }
            else
            {
                totalFornecedores = 0;
            }

            bancoDeDados.fechar();

            bancoDeDados.abrirConexao();

            leitor = bancoDeDados.executarQuery("select sum(valor) from tb_produto");

            if (leitor.HasRows)
            {
                leitor.Read();
                valorTotal = leitor.GetDecimal(0);
            }
            else
            {
                valorTotal = 0;
            }

            bancoDeDados.fechar();

            ListaFornecedores = new List<string>();
            valorFornecedores = new List<int>();

            bancoDeDados.abrirConexao();
            leitor = bancoDeDados.executarQuery("select nome, codigo_fornecedor from tb_fornecedor");
            List<int> codigos = new List<int>();
            while (leitor.HasRows)
            {
                leitor.Read();
                try
                {
                    ListaFornecedores.Add(leitor.GetString(0));
                    codigos.Add(leitor.GetInt32(1));
                }
                catch { break; }
            }
            bancoDeDados.fechar();
            foreach(int codigo in codigos)
            {
                bancoDeDados.abrirConexao();

                leitor = bancoDeDados.executarQuery($"select count(*) from tb_produto where codigo_fornecedor = {codigo}");
                
                
                leitor.Read();
                try
                {
                    valorFornecedores.Add(leitor.GetInt32(0));
                    bancoDeDados.fechar();

                }
                catch { bancoDeDados.fechar();  }
                
            }
            
        }
        public void OnPost() { }

        public void OnPostTeste()
        {
            
        }
    }
}
