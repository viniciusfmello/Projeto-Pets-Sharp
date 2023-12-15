using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace petsSharp.Pages
{
    public class indexBanhoETosaModel : PageModel
    {
		[BindProperty]
		public List<Dictionary<string, string>> list_banhoetosa { get; set; }

		[BindProperty]
		public string pesquisa { get; set; }
		public void generateListBanhoeTosa()
		{
			list_banhoetosa = new List<Dictionary<string, string>>();

			BancoDeDados bancoDeDados = new BancoDeDados();
			SqlDataReader leitor;
			int totalServicos = 0;



			bancoDeDados.abrirConexao();

			leitor = bancoDeDados.executarQuery("select count(distinct codigo_servico) from tb_servicos");
			if (leitor.HasRows)
			{
				leitor.Read();
				totalServicos = leitor.GetInt32(0) + 1;
			}

			bancoDeDados.fechar();

			bancoDeDados.abrirConexao();

			leitor = bancoDeDados.executarQuery("select * from tb_servicos");
			int count = 1;
			while (leitor.HasRows)
			{
				if (count != totalServicos)
				{
					Dictionary<string, string> temp_dict = new Dictionary<string, string>();

					leitor.Read();
					int codigo = leitor.GetInt32(0);
					string servico = leitor.GetString(1);
					string cliente = leitor.GetString(2);
					string especie = leitor.GetString(3);
					string status = leitor.GetString(4);

					temp_dict.Add("codigo", codigo.ToString());
					temp_dict.Add("servico", servico);
					temp_dict.Add("cliente", cliente);
					temp_dict.Add("especie", especie);
					temp_dict.Add("status", status);
					list_banhoetosa.Add(temp_dict);
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
			generateListBanhoeTosa();
		}

		public void OnPostPesquisa()
		{
			generateListBanhoeTosa();
			if (pesquisa != null)
			{
				pesquisa = pesquisa.Trim().ToLower();
				List<Dictionary<string, string>> temp_list_servicos = new List<Dictionary<string, string>>();
				foreach (Dictionary<string, string> dicionario in list_banhoetosa)
				{
					if (dicionario["codigo"].Contains(pesquisa, StringComparison.OrdinalIgnoreCase) ||
						dicionario["servico"].Contains(pesquisa, StringComparison.OrdinalIgnoreCase) ||
						dicionario["cliente"].Contains(pesquisa, StringComparison.OrdinalIgnoreCase) ||
						dicionario["especie"].Contains(pesquisa, StringComparison.OrdinalIgnoreCase) ||
						dicionario["status"].Contains(pesquisa, StringComparison.OrdinalIgnoreCase))

					{
						temp_list_servicos.Add(dicionario);
					}
				}

				list_banhoetosa = temp_list_servicos;
			}
		}

	}
}
