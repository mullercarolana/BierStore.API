using System;

namespace BierStore.Models
{
    public class Marca
    {
        public Marca(int id, string nome, DateTime dataInclusao, DateTime dataUltimaAlteracao)
        {
            Id = id;
            Nome = nome;
            DataInclusao = dataInclusao;
            DataUltimaAlteracao = dataUltimaAlteracao;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }

        public static Marca Criar(string nome)
        {
            var dataAtual = DateTime.Now;
            return new Marca(
                id: 0,
                nome: nome,
                dataInclusao: dataAtual,
                dataUltimaAlteracao: dataAtual
                );
        }

        public static Marca Atualizar(int id, string nome, DateTime dataInclusao)
        {
            return new Marca(
                id: id,
                nome: nome,
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
