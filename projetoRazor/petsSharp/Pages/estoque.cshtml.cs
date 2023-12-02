using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;

namespace petsSharp.Pages
{
    public class estoqueModel : PageModel
    {
        [BindProperty]
        public List<Dictionary<string, string>> list_produtos { get; set; }
        [BindProperty]
        public string pesquisa { get; set; }
        [BindProperty]
        public decimal valorTotal { get; set; }
        public estoqueModel() {
            
        }

        public void generateListProdutos()
        {
            valorTotal = 0;
            list_produtos = new List<Dictionary<string, string>>();


            BancoDeDados bancoDeDados = new BancoDeDados();
            SqlDataReader leitor;
            int totalProdutos = 0;



            bancoDeDados.abrirConexao();

            leitor = bancoDeDados.executarQuery("select count(distinct codigo_produto) from tb_produto");
            if (leitor.HasRows)
            {
                leitor.Read();
                totalProdutos = leitor.GetInt32(0) + 1;
            }

            bancoDeDados.fechar();

            bancoDeDados.abrirConexao();

            leitor = bancoDeDados.executarQuery("select * from tb_produto");
            int count = 1;
            while (leitor.HasRows)
            {
                if (count != totalProdutos)
                {
                    Dictionary<string, string> temp_dict = new Dictionary<string, string>();

                    leitor.Read();
                    string nome = leitor.GetString(1);
                    decimal valor = leitor.GetDecimal(2);
                    string categoria = leitor.GetString(3);

                    temp_dict.Add("nome", nome);
                    temp_dict.Add("valor", valor.ToString());
                    valorTotal += valor;
                    temp_dict.Add("categoria", categoria);

                    list_produtos.Add(temp_dict);
                    count++;
                }
                else
                {
                    break;
                }
            }


            bancoDeDados.fechar();
            
        }
        public void OnGet()
        {
            generateListProdutos();
        }

        public void OnPostPesquisa() {
            generateListProdutos();
            if (pesquisa  != null)
            {
                valorTotal = 0;
                pesquisa = pesquisa.Trim().ToLower();
                List<Dictionary<string, string>> temp_list_produtos = new List<Dictionary<string, string>>();
                foreach (Dictionary<string, string> dicionario in list_produtos)
                {
                    if (dicionario["nome"].Contains(pesquisa, StringComparison.OrdinalIgnoreCase) ||
                        dicionario["categoria"].Contains(pesquisa, StringComparison.OrdinalIgnoreCase) ||
                        dicionario["valor"].Contains(pesquisa, StringComparison.OrdinalIgnoreCase))
                    {
                        temp_list_produtos.Add(dicionario);
                        valorTotal += decimal.Parse(dicionario["valor"]);
                    }
                }

                list_produtos = temp_list_produtos;
            }
        }
    }
}
