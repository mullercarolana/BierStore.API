using System;

namespace BierStore.Models
{
    public class CalculadoraVenda
    {
        public CustosDeVendaPorCopo Custos { get; set; }

        public double PrecoDeVendaCopoCerveja => PrecoDeVendaCopoDeCerveja(Custos);

        public double CalculaQuantidadeDeCoposPorBarril(CustosDeVendaPorCopo custos)
        {
            return custos.TamanhoBarril / custos.TamanhoCopo;
        }

        public double CalculoPrecoPorCopo(CustosDeVendaPorCopo custos)
        {
            var quantidadeDeCopos = CalculaQuantidadeDeCoposPorBarril(custos);
            var precoPorCopo = custos.PrecoDeCustoBarril / quantidadeDeCopos;

            return Math.Round(precoPorCopo, 2);
        }

        public double CalculoMargemDeLucroPorCopo(CustosDeVendaPorCopo custos)
        {
            var precoPorCopo = CalculoPrecoPorCopo(custos);
            var margemDeLucro = precoPorCopo * (custos.MargemLucro / 100);

            return Math.Round(margemDeLucro, 2);
        }

        public double PrecoDeVendaCopoDeCerveja(CustosDeVendaPorCopo custos)
        {
            var quantidadeDeCoposPorBarril = CalculaQuantidadeDeCoposPorBarril(custos);
            var precoPorCopo = CalculoPrecoPorCopo(custos);
            var margemDeLucro = CalculoMargemDeLucroPorCopo(custos);

            var resultado = precoPorCopo + margemDeLucro;

            return Math.Round(resultado, 2);
        }

    }
}
