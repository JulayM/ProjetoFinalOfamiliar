using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OFamiliar.Models;
using Owin;

namespace OFamiliar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            // inicializar os ROLES e os UTILIZADORES
            iniciaAplicacao();
        }

        /// <summary>
        /// cria, caso não existam, as Roles de suporte à aplicação: Veterinario, Funcionario, Dono
        /// cria, nesse caso, também, um utilizador...
        /// </summary>
        private void iniciaAplicacao()
        {

            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            //*********************************************************************
            // Criar a role 'Administrador'
            if (!roleManager.RoleExists("Administrador"))
            {
                var role = new IdentityRole();
                role.Name = "Administrador";
                roleManager.Create(role);

                // criar um utilizador 'Administrador'
                var user = new ApplicationUser();
                user.UserName = "admin@email.pt";
                user.Email = "admin@email.pt";
                string userPWD = "123_Asd";
                var chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-Dono-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Administrador");
                }
            }

            //*********************************************************************
            // criar a Role 'MembroFamiliar'
            if (!roleManager.RoleExists("MembroFamiliar"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "MembroFamiliar";
                roleManager.Create(role);


                string userPWD = "123_Asd";
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++
                // criar um utilizador 'MembroFamiliar'
                var user = new ApplicationUser();
                user.UserName = "luis.freitas@mail.pt";
                user.Email = "luis.freitas@mail.pt";
                var chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-Dono-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "MembroFamiliar");
                }
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++
                // criar um utilizador 'MembroFamiliar'
                user = new ApplicationUser();
                user.UserName = "maria.freitas@mail.pt";
                user.Email = "maria.freitas@mail.pt";
                chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-Dono-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "MembroFamiliar");
                }
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++
                // criar um utilizador 'MembroFamiliar'
                user = new ApplicationUser();
                user.UserName = "rita.freitas@mail.pt";
                user.Email = "rita.freitas@mail.pt";
                chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-Dono-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "MembroFamiliar");
                }
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++
                // criar um utilizador 'MembroFamiliar'
                user = new ApplicationUser();
                user.UserName = "Joao.Paulo@mail.pt";
                user.Email = "Joao.Paulo@mail.pt";
                chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-Dono-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "MembroFamiliar");
                }
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++
                // criar um utilizador 'MembroFamiliar'
                user = new ApplicationUser();
                user.UserName = "Ana.Paula@mail.pt";
                user.Email = "Ana.Paula@mail.pt";
                chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-Dono-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "MembroFamiliar");
                }
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++
                // criar um utilizador 'MembroFamiliar'
                user = new ApplicationUser();
                user.UserName = "Rui_Silva@mail.pt";
                user.Email = "Rui_Silva@mail.pt";
                chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-Dono-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "MembroFamiliar");
                }
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++
                // criar um utilizador 'MembroFamiliar'
                user = new ApplicationUser();
                user.UserName = "Pedro_Guerra@mail.pt";
                user.Email = "Pedro_Guerra@mail.pt";
                chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-Dono-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "MembroFamiliar");
                }
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++
                // criar um utilizador 'MembroFamiliar'
                user = new ApplicationUser();
                user.UserName = "Bruno_Carvalho@mail.pt";
                user.Email = "Bruno_Carvalho@mail.pt";
                chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-Dono-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "MembroFamiliar");
                }
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++
                // criar um utilizador 'MembroFamiliar'
                user = new ApplicationUser();
                user.UserName = "Patrizia12@mail.pt";
                user.Email = "Patrizia12@mail.pt";
                chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-Dono-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "MembroFamiliar");
                }
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++
                // criar um utilizador 'MembroFamiliar'
                user = new ApplicationUser();
                user.UserName = "Sofia_Guerra@mail.pt";
                user.Email = "Sofia_Guerra@mail.pt";
                chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-Dono-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "MembroFamiliar");
                }
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++
                // criar um utilizador 'MembroFamiliar'
                user = new ApplicationUser();
                user.UserName = "Vasco.Marques@mail.pt";
                user.Email = "Vasco.Marques@mail.pt";
                chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-Dono-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "MembroFamiliar");
                }
            }
            //*********************************************************************

            // https://code.msdn.microsoft.com/ASPNET-MVC-5-Security-And-44cbdb97
        }




    }
}
