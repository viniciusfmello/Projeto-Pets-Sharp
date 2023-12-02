using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Numerics;

namespace petsSharp.Pages
{
    public class cadastrarFornecedorModel : PageModel
    {
        [BindProperty]
        public string Nome { get; set; }
        [BindProperty]
        public string Categoria { get; set; }
        [BindProperty]
        public string Endereco { get; set; }
        [BindProperty]
        public string Bairro { get; set; }
        [BindProperty]
        public string Cidade { get; set; }
        [BindProperty]
        public string Estado { get; set; }
        [BindProperty]
        public string CNPJ { get; set; }
        [BindProperty]
        public string Contato { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            BancoDeDados bancoDeDados = new BancoDeDados();
            SqlDataReader leitor;

            int codigo_fornecedor;

            bancoDeDados.abrirConexao();

            leitor = bancoDeDados.executarQuery("select count(distinct codigo_fornecedor) from tb_fornecedor");
            if (leitor.HasRows)
            {
                leitor.Read();
                codigo_fornecedor = leitor.GetInt32(0) + 1;
            }
            else
            {
                codigo_fornecedor = 1;
            }

            bancoDeDados.fechar();

            string query = $"insert into tb_fornecedor (codigo_fornecedor, nome, categoria, endereco, bairro, cidade, estado, cnpj, contato) values " +
                $"({codigo_fornecedor}, '{Nome}', '{Categoria}', '{Endereco}', '{Bairro}', '{Cidade}', '{Estado}', '{CNPJ}', '{Contato}')";

            bancoDeDados.manipularDado(query);
            return RedirectToPage("/dashboard");
        }
    }
}
