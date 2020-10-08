using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context {
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext> {
        public MyContext CreateDbContext (string[] args) {
            // Usar para criar as migracoes, tem que tirar a port
            //string connectionString = "Server=127.0.0.1;Uid=root;Pwd=mudar@123";

            // Usando para criar as migraçoes
            string connectionString = "Server=127.0.0.1;Database=dbApi;Uid=root;Pwd=mudar@123";
            var optionsBuilder = new DbContextOptionsBuilder<MyContext> ();
            optionsBuilder.UseMySql (connectionString);
            return new MyContext (optionsBuilder.Options);
        }
    }
}
