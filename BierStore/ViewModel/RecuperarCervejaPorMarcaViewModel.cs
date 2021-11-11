namespace BierStore.ViewModel
{
    public sealed class RecuperarCervejaPorMarcaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string TipoDaCerveja { get; set; }
        public string Origem { get; set; }
        public string CategoriaAlcoolica { get; set; }
        public decimal Preco { get; set; }
    }
}
