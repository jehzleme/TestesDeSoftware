using NerdStore.Core.DomainObjects;
using System;
using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{
    public class PedidoItemTests
    {
        [Fact(DisplayName = "Novo Item Pedido Com Unidades Abaixo do Permitido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void Deve_Retornar_Erro_Ao_Adicionar_Unidades_Abaixo_Permitido()
        {
            //Arrange, Act & Assert
            Assert.Throws<DomainException>(() => new PedidoItem(Guid.NewGuid(), "Produto Teste", Pedido.MIN_UNIDADES_ITEM - 1, 100));
        }
    }
}
