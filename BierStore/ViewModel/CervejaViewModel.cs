using System;

namespace BierStore.ViewModel
{
    public sealed class CervejaViewModel
    {
        public int Id { get; set; }
        public int IdMarca { get; set; }
        public string NomeDaMarca { get; set; }
        public string TipoCerveja { get; set; }
        public string Origem { get; set; }
        public string CategoriaAlcoolica { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }
    }
}
