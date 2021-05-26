using System;

namespace NerdStore.Vendas.Domain
{
    public static class PedidoFactory
    {
        public static Pedido NovoPedidoRascunho(Guid clienteId)
        {
            var pedido = new Pedido
            {
                ClienteId = clienteId,
            };

            pedido.TornarRascunho();
            return pedido;
        }
    }
}
