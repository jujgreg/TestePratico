using Microsoft.AspNetCore.Mvc;
using Trabalho_CRUD.ApiProdutos.Models;

namespace Trabalho_CRUD.ApiProdutos.Controllers
{
    [ApiController]
    [Route("produtos")]
    public class ProdutosController : ControllerBase
    {
        //Lista em memoria para armazenar os produtos
        private static List<Produto> produtos = new List<Produto>()
        {
            new Produto
            {
                Id = 1,
                Nome = "Regata",
                Preco = 59.99
            },

            new Produto
            {
                Id = 2,
                Nome = "Camiseta",
                Preco = 39.99
            },

            new Produto
            {
                Id = 3,
                Nome = "Body",
                Preco = 49.99
            }


        };

        private static int proximoId = 4;

        //Listar os produtos na memoria
        [HttpGet]
        public IActionResult GetProdutos()
        {
            if (produtos.Count == 0)
            {
                return NotFound("Não a produtos na Lista, por favor adicione um");
            }
            return Ok(produtos);
        }

        //Mostrar apenas o produto selecionado
        [HttpGet("{id}")]
        public IActionResult GetProduto(int id)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null)
            {
                return NotFound("esse ID não existe por favor coloque um ID valido");
            }
            return Ok(produto);
        }

        //Criar um novo Produto
        [HttpPost]
        public IActionResult PostProduto([FromBody] Produto produto)
        {
            if (string.IsNullOrWhiteSpace(produto.Nome))
            {
                return BadRequest("Nome é Obrigatorio");
            }
            else if (produto.Preco <= 0)
            {
                return BadRequest("O preço deve ser Maior que zero");
            }

            produto.Id = proximoId++;
            proximoId++;

            produtos.Add(produto);
            return Ok(produto);

        }

        //Atualizar Produto da lista

        [HttpPut("{id}")]
        public IActionResult PutProduto(int id, [FromBody] Produto produtoatualizado)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado");
            }
            else if (String.IsNullOrWhiteSpace(produtoatualizado.Nome))
            {
                return BadRequest("Nome é obrigatorio");
            }
            else if (produtoatualizado.Preco <= 0)
            {
                return BadRequest("Coloque um preço positivo");
            }
            else if (produtoatualizado.Preco == null)
            {
                return BadRequest("É obrigatorio colocar um preço");
            }
            produto.Nome = produtoatualizado.Nome;
            produto.Preco = produtoatualizado.Preco;

            return Ok(produto);
        }

        //Deletar um produto da lista
        [HttpDelete("{id}")]
        public IActionResult DeleteProduto(int id)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado");
            }
            produtos.Remove(produto);

            return Ok("produto Removido com Sucesso");
        }
    }
}
