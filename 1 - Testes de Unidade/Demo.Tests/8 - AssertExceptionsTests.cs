using System;
using Xunit;

namespace Demo.Tests
{
    public class AssertExceptionsTests
    {
        [Fact]
        public void Deve_Retornar_Erro_Divisao_Por_Zero()
        {
            //Arrange
            var calculadora = new Calculadora();

            //Act & Assert
            Assert.Throws<DivideByZeroException>(() => calculadora.Dividir(100, 0));
        }
        
        [Fact]
        public void Deve_Retornar_Erro_Salario_Inferior_Permitido()
        {
            var exception = Assert.Throws<Exception>(() => FuncionarioFactory.Criar("Eduardo", 250));

            Assert.Equal("Salário inferior ao permitido", exception.Message);
        }
    }
}
