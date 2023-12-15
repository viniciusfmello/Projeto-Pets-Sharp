using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace petsSharp.Pages
{
    public class indexAnimaisModel : PageModel
    {
        [BindProperty]
        public List<Dictionary<string, string>> list_animais { get; set; }

        [BindProperty]
        public string pesquisa { get; set; }
        public void generateListAnimais()
        {
            list_animais = new List<Dictionary<string, string>>();

            BancoDeDados bancoDeDados = new BancoDeDados();
            SqlDataReader leitor;
            int totalAnimais = 0;



            bancoDeDados.abrirConexao();

            leitor = bancoDeDados.executarQuery("select count(distinct codigo_animal) from tb_animal");
            if (leitor.HasRows)
            {
                leitor.Read();
                totalAnimais = leitor.GetInt32(0) + 1;
            }

            bancoDeDados.fechar();

            bancoDeDados.abrirConexao();

            leitor = bancoDeDados.executarQuery("select * from tb_animal");
            int count = 1;
            while (leitor.HasRows)
            {
                if (count != totalAnimais)
                {
                    Dictionary<string, string> temp_dict = new Dictionary<string, string>();

                    leitor.Read();
                    int codigo = leitor.GetInt32(0);
                    string especie = leitor.GetString(1);
                    decimal valor = leitor.GetDecimal(2);                         
                    string descricao = leitor.GetString(4);

                    temp_dict.Add("codigo", codigo.ToString());
                    temp_dict.Add("valor", valor.ToString());
                    temp_dict.Add("especie", especie);
                    temp_dict.Add("descricao", descricao);
                    list_animais.Add(temp_dict);
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
            generateListAnimais();
        }

        public void OnPostPesquisa()
        {
            generateListAnimais();
            if (pesquisa != null)
            {
                pesquisa = pesquisa.Trim().ToLower();
                List<Dictionary<string, string>> temp_list_animais = new List<Dictionary<string, string>>();
                foreach (Dictionary<string, string> dicionario in list_animais)
                {
                    if (dicionario["codigo"].Contains(pesquisa, StringComparison.OrdinalIgnoreCase) ||
                        dicionario["valor"].Contains(pesquisa, StringComparison.OrdinalIgnoreCase) ||
                        dicionario["especie"].Contains(pesquisa, StringComparison.OrdinalIgnoreCase) ||
                        dicionario["descricao"].Contains(pesquisa, StringComparison.OrdinalIgnoreCase))
                    {
                        temp_list_animais.Add(dicionario);
                    }
                }

                list_animais = temp_list_animais;
            }
        }

    }
}
