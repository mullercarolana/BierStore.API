using BierStore.Models;
using Xunit;

namespace BierStoreTestes
{
    public class CalculoVendaPorCopo
    {
        [Fact]
        public void CalculaPrecoPorCoposComBarrilDe30Litros()
        {
            //Arrange
            var calculadoraVenda = new CalculadoraVenda();
            var custosDeVendaPorCopoPorBarril = new CustosDeVendaPorCopo(tamanhoBarril: 30000, tamanhoCopo: 400, margemLucro: 120, precoDeCustoBarril: 400.00);

            //Act
            calculadoraVenda.CalculoPrecoPorCopo(custosDeVendaPorCopoPorBarril);

            //Assert
            var valorEsperado = 5.33;
            var valorObtido = calculadoraVenda.CalculoPrecoPorCopo(custosDeVendaPorCopoPorBarril);
            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
