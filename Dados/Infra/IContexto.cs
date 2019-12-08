using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Dados.Infra
{
    public interface IContexto
    {
        public List<T> Buscar<T>(Func<T, bool> expr) where T: EntidadeBase;
        public T Salvar<T>(T objeto) where T : EntidadeBase;
    }
}
