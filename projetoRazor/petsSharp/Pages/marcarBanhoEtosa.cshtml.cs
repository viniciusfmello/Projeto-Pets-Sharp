using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Drawing;

namespace petsSharp.Pages
{
    public class marcarBanhoEtosaModel : PageModel
    {
		
		[BindProperty]
        public string NomeCliente { get; set; }
		[BindProperty]
        public string NomePET { get; set; }
		[BindProperty]
        public string cpf_cliente { get; set; }
		[BindProperty]
        public string porte {  get; set; }

		[BindProperty]
        public string status { get; set; }
		
		public IActionResult OnPost()
		{
			BancoDeDados bancoDeDados = new BancoDeDados();
			SqlDataReader leitor;
			
			int codigo_servico;

			bancoDeDados.abrirConexao();

			leitor = bancoDeDados.executarQuery("select count(distinct codigo_servico) from tb_servicos");
			if (leitor.HasRows)
			{
				leitor.Read();
				codigo_servico = leitor.GetInt32(0) + 1;
			}
			else
			{
				codigo_servico = 1;
			}

			bancoDeDados.fechar();

			string query = $"insert into tb_servicos (codigo_servico, nome_cliente, status_servico, nome_pet, cpf, porte) values " +
				$"({codigo_servico}, '{NomeCliente}', '{status}', '{NomePET}', '{cpf_cliente}', '{porte}')";

			bancoDeDados.manipularDado(query);
			return RedirectToPage("/dashboard");
		}
	}
}
