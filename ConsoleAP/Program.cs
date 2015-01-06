using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Services;
using Ninject;
using System.Configuration;
using ORM;

namespace ConsoleAP
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = ConfigurationManager.AppSettings["repository"];
            Assembly assembly = null;
            try
            {
                assembly = Assembly.LoadFrom(path);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            IKernel kernel = new StandardKernel();
            kernel.Load(assembly);
            var service = kernel.Get<IUserService>();
            var list = service.GetAllUsers().ToList();
            //list = service.GetUsersByRole(RoleEntity.User).ToList();
            foreach (var user in list)
            {
                Console.WriteLine(user.Login);
            }
            //using (var db = new SiteModel())
            //{
            //    var query = from b in db.Users
            //        select b;
            //    foreach (var item in query)
            //    {
            //        Console.WriteLine("{0},{1},{2}",item.Login,item.Password,item.Role.Name);
            //    }
            //    Console.ReadKey();
            //}
        }
    }
}
