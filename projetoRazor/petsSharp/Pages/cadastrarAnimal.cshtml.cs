using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Drawing;

namespace petsSharp.Pages
{
    public class cadastrarAnimalModel : PageModel
    {
		[BindProperty]
		public string Especie { get; set; }

		[BindProperty]
		public string Descricao { get; set; }

		[BindProperty]
		public string Valor { get; set; }

		[BindProperty]
		public string Fornecedor { get; set; }

		[BindProperty]
		public List<string> ListaFornecedores { get; set; }

		public void OnGet()
		{
			ListaFornecedores = new List<string>();
			BancoDeDados bancoDeDados = new BancoDeDados();
			SqlDataReader leitor;
			bancoDeDados.abrirConexao();
			leitor = bancoDeDados.executarQuery("select nome from tb_fornecedor");
			while (leitor.HasRows)
			{
				leitor.Read();
				try
				{
					ListaFornecedores.Add(leitor.GetString(0));
				}
				catch { break; }
			}
		}
		public IActionResult OnPost()
		{
			BancoDeDados bancoDeDados = new BancoDeDados();
			SqlDataReader leitor;
			int codigo_fornecedor;
			int codigo_animal;

			bancoDeDados.abrirConexao();

			leitor = bancoDeDados.executarQuery("select count(distinct codigo_animal) from tb_animal");
			if (leitor.HasRows)
			{
				leitor.Read();
				codigo_animal = leitor.GetInt32(0) + 1;
			}
			else
			{
				codigo_animal = 1;
			}

			bancoDeDados.fechar();

			string query = $"select codigo_fornecedor from tb_fornecedor where nome = '{Fornecedor.ToLower()}'";
			bancoDeDados.abrirConexao();
			leitor = bancoDeDados.executarQuery(query);
			if (leitor.HasRows)
			{
				leitor.Read();
				codigo_fornecedor = leitor.GetInt32(0);
			}
			else
			{
				codigo_fornecedor = -1;
			}
			bancoDeDados.fechar();

			if (codigo_fornecedor == -1)
			{
				Fornecedor = "ERRO: NÃO ENCONTRADO";
				return Page();
			}
			query = $"insert into tb_animal (codigo_animal, especie, valor, descricao, codigo_fornecedor) values " +
				$"({codigo_animal}, '{Especie}', '{Valor}', '{Descricao}', '{codigo_fornecedor}')";

			bancoDeDados.manipularDado(query);
			return RedirectToPage("/dashboard");
		}
	}
}
