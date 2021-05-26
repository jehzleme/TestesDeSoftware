using FluentValidation.Results;
using System;


namespace NerdStore.Vendas.Domain
{
    public class Voucher
    {
        public string Codigo { get; private set; }
        public decimal? PercentualDesconto { get; private set; }
        public decimal? ValorDesconto { get; private set; }
        public TipoDescontoVoucher TipoDescontoVoucher { get; private set; }
        public int Quantidade { get; private set; }
        public DateTime DataValidade { get; private set; }
        public bool Ativo { get; private set; }
        public bool Utilizado { get; private set; }

        public Voucher(string codigo, decimal? percentual, decimal? valor, TipoDescontoVoucher tipoDesconto,
                        int quantidade, DateTime validade, bool ativo, bool utilizado)
        {
            Codigo = codigo;
            PercentualDesconto = percentual;
            ValorDesconto = valor;
            TipoDescontoVoucher = tipoDesconto;
            Quantidade = quantidade;
            DataValidade = validade;
            Ativo = ativo;
            Utilizado = utilizado;
        }

        public ValidationResult ValidarSeAplicavel()
        {
            return new VoucherValidation().Validate(this);
        }
    }
}
