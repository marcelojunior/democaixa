using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Dados.Infra
{
    public partial class Contexto : DbContext, IContexto
    {
        public string ConnectionString;

        public List<T> Buscar<T>(Func<T, bool> expr) where T : EntidadeBase
        {
            return this.Set<T>().Where(expr).ToList();
        }

        public T Salvar<T>(T objeto) where T : EntidadeBase
        {
            if (objeto.Id == 0)
            {
                objeto.DataCadastro = DateTime.Now;
                objeto.DataAlteracao = DateTime.Now;
                this.Add(objeto);
            }
            else
            {
                objeto.DataAlteracao = DateTime.Now;
                this.Update(objeto);
            }
                

            this.SaveChanges();
            return objeto;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseNpgsql(this.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Replace table names
                entity.SetTableName(entity.GetTableName().ToLower());

                // Replace column names            
                foreach (var property in entity.GetProperties())
                    property.SetColumnName(property.GetColumnName().ToLower());

                foreach (var key in entity.GetKeys())
                    key.SetName(key.GetName().ToLower());

                foreach (var key in entity.GetForeignKeys())
                    key.SetConstraintName(key.GetConstraintName().ToLower());

                foreach (var index in entity.GetIndexes())
                    index.SetName(index.GetName().ToLower());
            }
        }
    }
}
