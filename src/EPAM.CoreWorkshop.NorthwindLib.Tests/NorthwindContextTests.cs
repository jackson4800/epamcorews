using EPAM.CoreWorkshop.NorthwindLib.Model;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EPAM.CoreWorkshop.NorthwindLib.Tests
{
    [TestClass]
    public class NorthwindContextTests
    {

        private static NorthwindContext __GetContext(string url,bool local = false)
        {
            var optionsBuilder = new DbContextOptionsBuilder<NorthwindContext>();
            if (!local)
            {
                optionsBuilder.UseMySql("Server=" + url + ";Database=Northwind;User=user;Password=123;Compress=true",
                    opt =>
                    {
                        opt.CommandTimeout(200);
                    });
                return new NorthwindContext(optionsBuilder.Options);
            }
            optionsBuilder.UseSqlServer("Server=" + url + ";Database=Northwind;Integrated Security=True;");

            return new NorthwindContext(optionsBuilder.Options);
        }
        [TestMethod]
        public void RevertMigrations()
        {

            var context = __GetContext("epam-corewsvm25.northeurope.cloudapp.azure.com");
            context.GetService<IMigrator>().Migrate("Init");
        }
        [TestMethod]
        public void GreateMySQLDbFromMigrations()
        {
            var context = __GetContext("epam-corewsvm25.northeurope.cloudapp.azure.com");

            context.Database.Migrate();
        }
        [TestMethod]
        public void GreateMySQLDb()
        {
            var context = __GetContext("epam-corewsvm25.northeurope.cloudapp.azure.com");

            context.Database.EnsureCreated();
        }
        [TestMethod]
        public void GetDataFromSqlServer()
        {

            var context = __GetContext("(local)", true);
            context.Products.CountAsync().Result.Should().Be(77);
            context.Categories.CountAsync().Result.Should().Be(8);
        }
    }
}
