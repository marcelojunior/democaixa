using Dados;
using GraphQL.Types;

namespace Api.GraphQL.Tipos
{
    public class ContaCorrenteType : ObjectGraphType<ContaCorrente>
    {
        public ContaCorrenteType()
        {
            Name = "ContaCorrente";
            Field(m => m.Conta);
            Field(m => m.Saldo);
        }
    }
}
