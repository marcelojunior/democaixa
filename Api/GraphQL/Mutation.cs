using Api.GraphQL.Tipos;
using Dados.Infra;
using Dados.Servicos;
using GraphQL;
using GraphQL.Types;

namespace Api.GraphQL
{
    public class Mutation : ObjectGraphType
    {
        public Mutation(IContexto contexto)
        {
            Field<ContaCorrenteType>(
                "sacar",
                arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "conta" },
                   new QueryArgument<NonNullGraphType<FloatGraphType>> { Name = "valor" }
               ),
                resolve: context =>
                {
                    var conta = context.GetArgument<int>("conta");
                    var valor = context.GetArgument<double>("valor");

                    var sConta = new ContaService(contexto);
                    var contaCorrente = sConta.Conta(conta);

                    if (contaCorrente == null)
                    {
                        context.Errors.Add(new ExecutionError("Conta Inexistente"));
                        return null;
                    }
                    else if (contaCorrente.Saldo < valor)
                    {
                        context.Errors.Add(new ExecutionError("Saldo Insuficiente"));
                        return null;
                    }

                    return sConta.Sacar(conta, valor);
                }
           );

            Field<ContaCorrenteType>(
                "depositar",
                arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "conta" },
                   new QueryArgument<NonNullGraphType<FloatGraphType>> { Name = "valor" }
               ),
                resolve: context =>
                {
                    var conta = context.GetArgument<int>("conta");
                    var valor = context.GetArgument<double>("valor");

                    var sConta = new ContaService(contexto);
                    return sConta.Depositar(conta, valor);
                }
           );
        }
    }
}
