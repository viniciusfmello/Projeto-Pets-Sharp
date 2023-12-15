using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace petsSharp.Pages
{
	public class indexVendasModel : PageModel
	{
		[BindProperty]
		public List<Dictionary<string, string>> list_vendas { get; set; }

		[BindProperty]
		public string pesquisa { get; set; }
		public void generateListVendas()
		{
			list_vendas = new List<Dictionary<string, string>>();

			BancoDeDados bancoDeDados = new BancoDeDados();
			SqlDataReader leitor;
			int totalVendas = 0;



			bancoDeDados.abrirConexao();

			leitor = bancoDeDados.executarQuery("select count(distinct codigo_venda) from tb_venda");
			if (leitor.HasRows)
			{
				leitor.Read();
				totalVendas = leitor.GetInt32(0) + 1;
			}

			bancoDeDados.fechar();

			bancoDeDados.abrirConexao();

			leitor = bancoDeDados.executarQuery("select * from tb_venda");
			int count = 1;
			while (leitor.HasRows)
			{
				if (count != totalVendas)
				{
					Dictionary<string, string> temp_dict = new Dictionary<string, string>();

					leitor.Read();
					int codigo = leitor.GetInt32(0);
					decimal valor = leitor.GetDecimal(1);
					string produto = leitor.GetString(5);
					string pagamento = leitor.GetString(3);
					string cliente = leitor.GetString(4);

					temp_dict.Add("codigo", codigo.ToString());
					temp_dict.Add("valor", valor.ToString());
					temp_dict.Add("produto", produto);
					temp_dict.Add("pagamento", pagamento);
					temp_dict.Add("cliente", cliente);

					list_vendas.Add(temp_dict);
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
			generateListVendas();
		}

		public void OnPostPesquisa()
		{
			generateListVendas();
			if (pesquisa != null)
			{
				pesquisa = pesquisa.Trim().ToLower();
				List<Dictionary<string, string>> temp_list_produtos = new List<Dictionary<string, string>>();
				foreach (Dictionary<string, string> dicionario in list_vendas)
				{
					if (dicionario["codigo"].Contains(pesquisa, StringComparison.OrdinalIgnoreCase) ||
						dicionario["valor"].Contains(pesquisa, StringComparison.OrdinalIgnoreCase) ||
						dicionario["produto"].Contains(pesquisa, StringComparison.OrdinalIgnoreCase) ||
						dicionario["cliente"].Contains(pesquisa, StringComparison.OrdinalIgnoreCase) ||
						dicionario["pagamento"].Contains(pesquisa, StringComparison.OrdinalIgnoreCase))
					{
						temp_list_produtos.Add(dicionario);
					}
				}

				list_vendas = temp_list_produtos;
			}
		}
	}
}