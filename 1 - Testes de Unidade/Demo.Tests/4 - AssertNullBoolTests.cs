using Xunit;

namespace Demo.Tests
{
    public class AssertNullBoolTests
    {
        [Fact]
        public void Funcionario_Nome_NaoDeveSerNuloOuVazio()
        {
            var funcionario = new Funcionario("", 1000);

            Assert.False(string.IsNullOrEmpty(funcionario.Nome));
        }
        
        [Fact]
        public void Funcionario_Nome_NaoDeveTerApelido()
        {
            var funcionario = new Funcionario("Eduardo", 1000);

            Assert.Null(funcionario.Apelido);

            Assert.True(string.IsNullOrEmpty(funcionario.Apelido));
            Assert.False(funcionario.Apelido?.Length > 0);
        }
    }
}
