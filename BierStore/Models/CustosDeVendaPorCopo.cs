namespace BierStore.Models
{
    public class CustosDeVendaPorCopo
    {
        public CustosDeVendaPorCopo(double tamanhoBarril, double tamanhoCopo, double margemLucro, double precoDeCustoBarril)
        {
            TamanhoBarril = tamanhoBarril;
            TamanhoCopo = tamanhoCopo;
            MargemLucro = margemLucro;
            PrecoDeCustoBarril = precoDeCustoBarril;
        }

        public double TamanhoBarril { get; set; }
        public double TamanhoCopo { get; set; }
        public double MargemLucro { get; set; }
        public double PrecoDeCustoBarril { get; set; }
    }
}
