using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace OFamiliar.Models
{
    //superclasse DbContext
    //seqüência de conexão
    public class OrcamentoF_DB : DbContext
    {
        //Representar um 'construtor' desta classe
        //Representa onde se encontra a Base de Dados
        public OrcamentoF_DB() : base("DefaultConnection") { }

        //Descreve as tabelas que estao na DB
        public virtual DbSet<Pessoas> Pessoas { get; set; }
        public virtual DbSet<Convite> Convites { get; set; }
        public virtual DbSet<Familia> Familias { get; set; }
        public virtual DbSet<Movimentos> Movimentos { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // criar a FK para a tabela dos Convites,
            // relacionando os 'destinatários' com um 'convite'
            modelBuilder.Entity<Convite>()
                       .HasRequired(c => c.Destinatario)
                       .WithMany(p => p.ListaConvitesRecebidos)
                       .HasForeignKey(c => c.DestinatarioFK)
                       .WillCascadeOnDelete(false);

            // criar a FK para a tabela dos Convites,
            // relacionando os 'emissores' com um 'convite'
            modelBuilder.Entity<Convite>()
                       .HasRequired(c => c.Emissor)
                       .WithMany(p => p.ListaConvitesEmitidos)
                       .HasForeignKey(c => c.EmissorFK)
                       .WillCascadeOnDelete(false);
            // não podemos usar a chave seguinte, nesta geração de tabelas
            // por causa das tabelas do Identity (gestão de utilizadores)
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}