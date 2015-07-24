namespace TodoApp.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TodoApp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TodoApp.Models.AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TodoApp.Models.AppContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            User user1 = new User()
            {
                Id = 1,
                UserName = "kimura",
                Password = "password",
                Roles = new List<Role>(),
                Todoes = new List<Todo>()
            };

            Role role1 = new Role()
            {
                Id = 1,
                RoleName = "Administrators",
                Users = new List<User>()
            };
            Role role2 = new Role()
            {
                Id = 2,
                RoleName = "Users",
                Users = new List<User>()
            };

            user1.Roles.Add(role1);
            role1.Users.Add(user1);

            context.Users.AddOrUpdate(u => u.Id, user1);
            context.Roles.AddOrUpdate(r => r.Id, role1, role2);
        }
    }
}
