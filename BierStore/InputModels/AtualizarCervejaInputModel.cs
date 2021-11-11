using System;

namespace BierStore.InputModels
{
    public sealed class AtualizarCervejaInputModel
    {
        public int IdMarca { get; set; }
        public string Nome { get; set; }
        public string Origem { get; set; }
        public string CategoriaAlcoolica { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }
    }
}
