using System;
using System.ComponentModel.DataAnnotations;

namespace Dados.Infra
{
    public class EntidadeBase
    {
        public long Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
