using System;

namespace BierStore.Models
{
    public class Cerveja
    {
        public Cerveja(int id, int idMarca, string nome, string origem, string categoriaAlcoolica, decimal preco,
            DateTime dataInclusao, DateTime dataUltimaAlteracao)
        {
            Id = id;
            IdMarca = idMarca;
            Nome = nome;
            Origem = origem;
            CategoriaAlcoolica = categoriaAlcoolica;
            Preco = preco;
            DataInclusao = dataInclusao;
            DataUltimaAlteracao = dataUltimaAlteracao;
        }

        public int Id { get; set; }
        public int IdMarca { get; set; }
        public string Nome { get; set; }
        public string Origem { get; set; }
        public string CategoriaAlcoolica { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }

        public static Cerveja Criar(int idMarca, string nome, string origem, string categoriaAlcoolica, decimal preco)
        {
            var dataAtual = DateTime.Now;
            return new Cerveja(
                id: 0,
                idMarca: idMarca,
                nome: nome,
                origem: origem,
                categoriaAlcoolica: categoriaAlcoolica,
                preco: preco,
                dataInclusao: dataAtual,
                dataUltimaAlteracao: dataAtual
                );
        }

        public static Cerveja Atualizar(int id, int idMarca, string nome, string origem, string categoriaAlcoolica, decimal preco, DateTime dataInclusao)
        {
            return new Cerveja(
                id: id,
                idMarca: idMarca,
                nome: nome,
                origem: origem,
                categoriaAlcoolica: categoriaAlcoolica,
                preco: preco,
                dataInclusao: dataInclusao,
                dataUltimaAlteracao: DateTime.Now
                );
        }

        public void NovoId(int id)
        {
            Id = id;
        }
    }
}
