namespace BierStore.InputModels
{
    public class NovoCervejaInputModel
    {
        public int IdMarca { get; set; }
        public string Nome { get; set; }
        public string Origem { get; set; }
        public string CategoriaAlcoolica { get; set; }
        public decimal Preco { get; set; }
    }
}
