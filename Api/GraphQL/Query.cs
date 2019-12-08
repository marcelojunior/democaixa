using Dados.Infra;
using Dados.Servicos;
using GraphQL;
using GraphQL.Types;

namespace Api.GraphQL
{
    public class Query : ObjectGraphType
    {
        public Query(IContexto contexto)
        {
            Field<FloatGraphType>(
                "saldo",
                arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "conta" }
               ),
                resolve: context =>
                {
                    var conta = context.GetArgument<int>("conta");

                    var sConta = new ContaService(contexto);
                    var contaCorrente = sConta.Conta(conta);

                    if (contaCorrente == null)
                    {
                        context.Errors.Add(new ExecutionError("Conta Inexistente"));
                        return null;
                    }

                    return contaCorrente.Saldo;
                }
           );
        }
    }
}
