using BierStore.Models;
using Xunit;

namespace BierStoreTestes
{
    public class CalculadoraVendaDeCoposDeCerveja
    {
        [Fact]
        public void CalculaValorDeVendaDoCopoDeCerveja400mlComBarrilDe30Litros()
        {
            //Arrange
            var calculadoraVenda = new CalculadoraVenda();
            var custosDeVendaPorCopoPorBarril = new CustosDeVendaPorCopo(tamanhoBarril: 30000, tamanhoCopo: 400, margemLucro: 120, precoDeCustoBarril: 400.00);

            //Act
            calculadoraVenda.PrecoDeVendaCopoDeCerveja(custosDeVendaPorCopoPorBarril);

            //Assert
            var valorEsperado = 11.73;
            var valorObtido = calculadoraVenda.PrecoDeVendaCopoDeCerveja(custosDeVendaPorCopoPorBarril);
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void CalculaValorDeVendaDoCopoDeCerveja400mlComBarrilDe50Litros()
        {
            //Arrange
            var calculadoraVenda = new CalculadoraVenda();
            var custosDeVendaPorCopoPorBarril = new CustosDeVendaPorCopo(tamanhoBarril: 50000, tamanhoCopo: 400, margemLucro: 120, precoDeCustoBarril: 600.00);

            //Act
            calculadoraVenda.PrecoDeVendaCopoDeCerveja(custosDeVendaPorCopoPorBarril);

            //Assert
            var valorEsperado = 10.56;
            var valorObtido = calculadoraVenda.PrecoDeVendaCopoDeCerveja(custosDeVendaPorCopoPorBarril);
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void CalculaValorDeVendaDoCopoDeCerveja500mlComBarrilDe30Litros()
        {
            //Arrange
            var calculadoraVenda = new CalculadoraVenda();
            var custosDeVendaPorCopoPorBarril = new CustosDeVendaPorCopo(tamanhoBarril: 30000, tamanhoCopo: 500, margemLucro: 120, precoDeCustoBarril: 400.00);

            //Act
            calculadoraVenda.PrecoDeVendaCopoDeCerveja(custosDeVendaPorCopoPorBarril);

            //Assert
            var valorEsperado = 14.67;
            var valorObtido = calculadoraVenda.PrecoDeVendaCopoDeCerveja(custosDeVendaPorCopoPorBarril);
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void CalculaValorDeVendaDoCopoDeCerveja500mlComBarrilDe50Litros()
        {
            //Arrange
            var calculadoraVenda = new CalculadoraVenda();
            var custosDeVendaPorCopoPorBarril = new CustosDeVendaPorCopo(tamanhoBarril: 50000, tamanhoCopo: 500, margemLucro: 120, precoDeCustoBarril: 600.00);

            //Act
            calculadoraVenda.PrecoDeVendaCopoDeCerveja(custosDeVendaPorCopoPorBarril);

            //Assert
            var valorEsperado = 13.20;
            var valorObtido = calculadoraVenda.PrecoDeVendaCopoDeCerveja(custosDeVendaPorCopoPorBarril);
            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
