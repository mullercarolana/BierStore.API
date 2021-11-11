using BierStore.Models;
using Xunit;

namespace BierStoreTestes
{
    public class CalculadoraVendaQuantidadeDeCopos
    {
        [Fact]
        public void CalculaQuantidadeDeCoposDe400mlComBarrilDe30Litros()
        {
            //Arrange
            var calculadoraVenda = new CalculadoraVenda();
            var custosDeVendaPorCopoPorBarril = new CustosDeVendaPorCopo(tamanhoBarril: 30000, tamanhoCopo: 400, margemLucro: 120, precoDeCustoBarril: 400.00);

            //Act
            calculadoraVenda.CalculaQuantidadeDeCoposPorBarril(custosDeVendaPorCopoPorBarril);

            //Assert
            var valorEsperado = 75;
            var valorObtido = calculadoraVenda.CalculaQuantidadeDeCoposPorBarril(custosDeVendaPorCopoPorBarril);
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void CalculaQuantidadeDeCoposDe500mlComBarrilDe30Litros()
        {
            //Arrange
            var calculadoraVenda = new CalculadoraVenda();
            var custosDeVendaPorCopoPorBarril = new CustosDeVendaPorCopo(tamanhoBarril: 30000, tamanhoCopo: 500, margemLucro: 120, precoDeCustoBarril: 400.00);

            //Act
            calculadoraVenda.CalculaQuantidadeDeCoposPorBarril(custosDeVendaPorCopoPorBarril);

            //Assert
            var valorEsperado = 60;
            var valorObtido = calculadoraVenda.CalculaQuantidadeDeCoposPorBarril(custosDeVendaPorCopoPorBarril);
            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
