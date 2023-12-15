using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Drawing;

namespace petsSharp.Pages
{
    public class registrarNovaVendaModel : PageModel
    {
		[BindProperty]
		public string Cliente { get; set; }

		[BindProperty]
		public string Produto { get; set; }

		[BindProperty]
		public string Quantidade { get; set; }

		[BindProperty]
		public string Valor { get; set; }

		[BindProperty]
		public string Pagamento { get; set; }

		public IActionResult OnPost()
		{
			BancoDeDados bancoDeDados = new BancoDeDados();
			SqlDataReader leitor;
			int codigo_venda;

			bancoDeDados.abrirConexao();

			leitor = bancoDeDados.executarQuery("select count(distinct codigo_venda) from tb_venda");
			if (leitor.HasRows)
			{
				leitor.Read();
				codigo_venda = leitor.GetInt32(0) + 1;
			}
			else
			{
				codigo_venda = 1;
			}

			bancoDeDados.fechar();

			string query = $"insert into tb_venda (codigo_venda, nome_cliente, nome_produto, quantidade, valor, forma_pagamento) values " +
				$"({codigo_venda}, '{Cliente}', '{Produto}', '{Quantidade}', '{Valor}', '{Pagamento}')";

			bancoDeDados.manipularDado(query);
			return RedirectToPage("/dashboard");
		}
	}
}
