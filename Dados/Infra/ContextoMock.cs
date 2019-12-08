using System;
using System.Collections.Generic;
using System.Linq;

namespace Dados.Infra
{
    public class ContextoMock<T2> : IContexto where T2 : EntidadeBase
    {
        private List<T2> _lista = new List<T2>();

        public List<T> Buscar<T>(Func<T, bool> expr) where T : EntidadeBase
        {
            var listaConvertida = (List<T>)Convert.ChangeType(_lista, typeof(List<T>));
            return listaConvertida.Where(expr).ToList();
        }

        public T Salvar<T>(T objeto) where T : EntidadeBase
        {
            if (objeto.Id == 0)
            {
                var maiorCodigo = 0L;
                if (this._lista.Count > 0)
                    maiorCodigo = this._lista.Max(m => m.Id);
                objeto.Id = maiorCodigo + 1;
                var item = (T2)Convert.ChangeType(objeto, typeof(T2));
                item.DataAlteracao = DateTime.Now;
                item.DataCadastro = DateTime.Now;
                this._lista.Add(item);
            }
            else
            {
                objeto.DataAlteracao = DateTime.Now;
                var objLista = this._lista.Where(m => m.Id == objeto.Id).FirstOrDefault();
                objLista = (T2)Convert.ChangeType(objeto, typeof(T2));
            }
            return objeto;
        }    
    }
}
