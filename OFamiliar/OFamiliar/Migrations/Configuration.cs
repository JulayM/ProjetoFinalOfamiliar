namespace OFamiliar.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(OFamiliar.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //#############################################################
            // criação das classes Pessoa, Convite, Movimentos, Familia e Categoria
            //#############################################################

            // Configuration --- SEED
            //=============================================================

            // ############################################################################################
            // adiciona PESSOAS
            var pessoas = new List<Pessoas> {
                new Pessoas  {PessoaID=1, Nome = "Luís Freitas", NIF ="813635582", Email="luis.freitas@mail.pt", Genero="M",Telefone ="920000100",DataNascimento = new DateTime(1975,3,25) },
                new Pessoas  {PessoaID=2, Nome = "Maria Freitas", NIF ="203635501", Email="maria.freitas@mail.pt", Genero ="F",Telefone ="920000111" ,DataNascimento = new DateTime(1979,4,12) },
                new Pessoas  {PessoaID=3, Nome = "Rita Maria Freitas", NIF ="923635545", Email="rita.freitas@mail.pt", Genero="M",Telefone ="922000133",DataNascimento = new DateTime(1987,8,15)  },
                new Pessoas  {PessoaID=4, Nome = "João Paulo Mendes", NIF ="353635510", Email="Joao.Paulo@mail.pt", Genero ="M",Telefone ="920000586" ,DataNascimento = new DateTime(1983,3,22) },
                new Pessoas  {PessoaID=5, Nome = "Ana Paula Mendes", NIF ="143635565", Email="Ana.Paula@mail.pt", Genero="F",Telefone ="960000888",DataNascimento = new DateTime(2007,3,12)  },
                new Pessoas  {PessoaID=6, Nome = "Rui Silva", NIF ="443635500", Email="Rui_Silva@mail.pt", Genero ="M",Telefone ="920002000" ,DataNascimento = new DateTime(1985,3,12) },
                new Pessoas  {PessoaID=7, Nome = "Pedro Guerra", NIF ="773635522", Email="Pedro_Guerra@mail.pt", Genero="M",Telefone ="920000106",DataNascimento = new DateTime(1978,5,24)  },
                new Pessoas  {PessoaID=8, Nome = "Bruno de Carvalho", NIF ="353635582", Email="Bruno_Carvalho@mail.pt", Genero ="M",Telefone ="920000108",DataNascimento = new DateTime(2000,9,27)  },
                new Pessoas  {PessoaID=9, Nome = "Patrizia.Ligas", NIF ="123635500", Email="Patrizia12@mail.pt", Genero ="F",Telefone ="920000177",DataNascimento = new DateTime(1980,1,31)  },
                new Pessoas  {PessoaID=10, Nome = "Sofia Varela", NIF ="1453635522", Email="Sofia_Guerra@mail.pt", Genero="F",Telefone ="960000133" ,DataNascimento = new DateTime(1992,4,29) },
                new Pessoas  {PessoaID=11, Nome = "Vasco Marques", NIF ="353635582", Email="Vasco.Marques@mail.pt", Genero ="M",Telefone ="960000185",DataNascimento = new DateTime(2006,3,3)  }

            };
            pessoas.ForEach(pp => context.Pessoas.AddOrUpdate(p => p.Nome, pp));
            context.SaveChanges();


            // ############################################################################################
            // Criar a lista de PESSOAS que pertencem a uma FAMÍLIA
            var familiaFreitas = new List<Pessoas>
            {
                pessoas[1],
                pessoas[2],
                pessoas[3]
            };
            var familiaMendes = new List<Pessoas>
            {
                pessoas[4],
                pessoas[5]
            };

            var familiaSilva = new List<Pessoas>
           {
               pessoas[6]
           };

            var familiaGuerra = new List<Pessoas>
           {
               pessoas[7]
           };
            // definir as Famílias
            var familia = new List<Familia>
            {
                new Familia {FamiliaID=1, Nome = "Freitas", DataDeCriacao = new DateTime(2017,3,10), ListaDeMembros =familiaFreitas},
                new Familia {FamiliaID=2, Nome = "Mendes", DataDeCriacao = new DateTime(2017,3,10), ListaDeMembros =familiaMendes},
                new Familia {FamiliaID=3, Nome = "Silva", DataDeCriacao = new DateTime(2017,4,26), ListaDeMembros =familiaSilva},
                new Familia {FamiliaID=4, Nome = "Guerra", DataDeCriacao = new DateTime(2017,4,27), ListaDeMembros =familiaGuerra}

            };

            familia.ForEach(ff => context.Familias.AddOrUpdate(f => f.Nome, ff));
            context.SaveChanges();


            // ############################################################################################
            //adiciona Convite
            var convite = new List<Convite>
            {
                new Convite {ConviteID=1, Token=Guid.NewGuid().ToString(), Data = new DateTime(2016,2,8), DestinatarioFK =6, EmissorFK=1,FamiliarFK=1, EstadoDoConvite="Pendente" },
                new Convite {ConviteID=2, Token=Guid.NewGuid().ToString(), Data = new DateTime(2017,3,1), DestinatarioFK =7, EmissorFK=1,FamiliarFK=1, EstadoDoConvite="Pendente"  },
                new Convite {ConviteID=3, Token=Guid.NewGuid().ToString(), Data = new DateTime(2014,2,25), DestinatarioFK =7, EmissorFK=4,FamiliarFK=2, EstadoDoConvite="Pendente"  },
                new Convite {ConviteID=4, Token=Guid.NewGuid().ToString(), Data = new DateTime(2016,2,16), DestinatarioFK =8, EmissorFK=5,FamiliarFK=2, EstadoDoConvite="Pendente"  },
                new Convite {ConviteID=5, Token=Guid.NewGuid().ToString(), Data = new DateTime(2017,3,30), DestinatarioFK =9, EmissorFK=5,FamiliarFK=2, EstadoDoConvite="Recusado"  },
                new Convite {ConviteID=6, Token=Guid.NewGuid().ToString(), Data = new DateTime(2016,4,26), DestinatarioFK =10, EmissorFK=6,FamiliarFK=3, EstadoDoConvite="Pendente"  },
                new Convite {ConviteID=7, Token=Guid.NewGuid().ToString(), Data = new DateTime(2017,4,27), DestinatarioFK =11, EmissorFK=7,FamiliarFK=4, EstadoDoConvite="Recusado"  }

            };
            convite.ForEach(cc => context.Convites.AddOrUpdate(c => c.Token, cc));
            context.SaveChanges();



            // ############################################################################################
            // adiciona CATEGORIA
            var categoria = new List<Categoria>
            {
                new Categoria {CategoriaID =1, Tipo="Rendimento",Nome="Ordenado" },
                new Categoria {CategoriaID =2, Tipo="Despesa",Nome="Compras supermercado"},
                new Categoria {CategoriaID =3, Tipo="Despesa",Nome="Saúde" },
                new Categoria {CategoriaID =4, Tipo="Despesa",Nome="Educação"},
                new Categoria {CategoriaID =5, Tipo="Rendimento",Nome="Herança" },
                new Categoria {CategoriaID =6, Tipo="Despesa",Nome="Lazer"}


            };
            categoria.ForEach(cc => context.Categorias.AddOrUpdate(c => c.CategoriaID, cc));
            context.SaveChanges();


            // ############################################################################################
            // adiciona MOVIMENTOS
            var movimento = new List<Movimentos>
            {
                new Movimentos {MovimentosID = 1,  Data = new DateTime(2017,3,12), Valor="€100,00", Descricao ="Salário do mês de fevereiro", DonoDoMovimentoFK=1, FamiliasFK=1, CategoriaFK=1},
                new Movimentos {MovimentosID = 2,  Data = new DateTime(2017,4,1),  Valor="€80,00",  Descricao ="Arroz,feijão,leite,Azeite,carnes,pão", DonoDoMovimentoFK =2, FamiliasFK=1, CategoriaFK=2},
                new Movimentos {MovimentosID = 3,  Data = new DateTime(2017,4,27), Valor="€70,00",  Descricao ="Medicamentos da Ana", DonoDoMovimentoFK=4, FamiliasFK=3, CategoriaFK=3},
                new Movimentos {MovimentosID = 4,  Data = new DateTime(2017,4,16), Valor="€500,00", Descricao ="Viagem a Roma", DonoDoMovimentoFK =6, FamiliasFK=3, CategoriaFK=6},
                new Movimentos {MovimentosID = 5,  Data = new DateTime(2017,4,16), Valor="€50,00", Descricao ="Jantar no restaurante", DonoDoMovimentoFK =7, FamiliasFK=4, CategoriaFK=6},
                new Movimentos {MovimentosID = 6,  Data = new DateTime(2017,4,19), Valor="€30,00", Descricao ="Visita de estudo", DonoDoMovimentoFK =6, FamiliasFK=2, CategoriaFK=6}
            };

            movimento.ForEach(mm => context.Movimentos.AddOrUpdate(c => c.Data, mm));
            context.SaveChanges();
        }
    }
}
