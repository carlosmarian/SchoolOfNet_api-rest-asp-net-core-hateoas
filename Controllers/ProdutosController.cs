using System;
using Microsoft.AspNetCore.Mvc;
using SchoolOfNet_API_Rest_com_ASPNET_Core_2.Data;
using SchoolOfNet_API_Rest_com_ASPNET_Core_2.Models;
using System.Linq;
using SchoolOfNet_api_rest_asp_net_core_hateoas.HATEOAS;
using System.Collections.Generic;

namespace SchoolOfNet_API_Rest_com_ASPNET_Core_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController: ControllerBase
    {

        private readonly ApplicationDbContext database;
        private HATEOAS hateoas;

        public ProdutosController(ApplicationDbContext database){
            this.database = database;
            this.hateoas = new HATEOAS("localhost:5001/api/Produtos");
            this.hateoas.AddAction("DELETE_PRODUCT", "DELETE");
            this.hateoas.AddAction("EDIT_PRODUCT", "PATCH");
            this.hateoas.AddAction("GET_INFO", "GET");
        }

        [HttpGet]
        public IActionResult Get(){
            var produtos = database.Produtos.ToList();

            List<ProdutoContainer> listContainer = new List<ProdutoContainer>();
            foreach (var produto in produtos)
            {
                ProdutoContainer produtoContainer = new ProdutoContainer();
                produtoContainer.produto = produto;
                produtoContainer.links = hateoas.GetActions(produto.ID.ToString());
                listContainer.Add(produtoContainer);
            }

            return Ok(new {msg = "Lista de produtos", body = listContainer});
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id){
            try{
                var produtos = database.Produtos.First(p => p.ID == id);
                ProdutoContainer produtoContainer = new ProdutoContainer();
                produtoContainer.produto = produtos;
                produtoContainer.links = hateoas.GetActions(produtos.ID.ToString());

                return Ok(produtoContainer);
            }catch(Exception ex){
                return NotFound(new {msg = "Id inv�lido", body = ex});
            }            
        }

        [HttpPatch]
        public IActionResult Patch([FromBody] Produto produto){

            if(produto.ID <=0){
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "ID do produto é inválido."});
            }

            try{
                var prod = database.Produtos.First(p => p.ID == produto.ID);

                if(prod == null){
                    Response.StatusCode = 400;
                    return new ObjectResult(new {msg = "Produto não localizado."});
                }

                prod.Nome = produto.Nome != null ? produto.Nome : prod.Nome;
                prod.Preco = produto.Preco > 0 ? produto.Preco : prod.Preco;

                database.SaveChanges();

                return Ok(new {msg = "Produto cadastrado"});

            }catch{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg = "Produto não localizado ou inválido."});
            }
        }

        [HttpPost()]
        public IActionResult Post([FromBody] ProdutoTemp produto){

            /* == VALIDAÇÃO== */
            if(produto.Preco <=0 ){
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "O preço do produto não pode ser menor ou igual a zero."});
            }

            if(produto.Nome.Length <=1 ){
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "O Nome do produto deve ter mínimo 1 caracter."});
            }

            Produto p = new Produto();
            p.Nome = produto.Nome;
            p.Preco = produto.Preco;

            database.Produtos.Add(p);
            database.SaveChanges();            

            Response.StatusCode = 201;
            //return Ok( new {info = "Retorno positivo", body = produto});
            return new ObjectResult(new {info = "Retorno positivo", body = produto});
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int ID){
            try{
                var produtos = database.Produtos.First(p => p.ID == ID);
                database.Produtos.Remove(produtos);
                database.SaveChanges();

                return Ok(new {msg = "Produto removido"});
            }catch(Exception ex){
                return NotFound(new {msg = "Id inv�lido", body = ex});
            }  
        }

        public class ProdutoTemp{
            public string Nome { get; set; }
            public float Preco { get; set; }
        }

        //Definindo um objeto container
        public class ProdutoContainer{
            public Produto produto;
            public Link[] links;
        }
    }
}