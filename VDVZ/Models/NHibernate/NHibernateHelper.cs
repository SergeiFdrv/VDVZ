using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDVZ.Models.NHibernate
{
    public class NHibernateHelper
    {
        public static ISession OpenSession()
        {
            ISessionFactory sessionFactory = Fluently.Configure().Database(
                MySQLConfiguration.Standard.ConnectionString(
                    @"Server=localhost; uid=root; pwd=admin; database=VDVZ; SslMode=Preferred;")
                .ShowSql()
            )
            .Mappings(m => m.FluentMappings
            .AddFromAssemblyOf<Division>()
            .AddFromAssemblyOf<Employee>()
            .AddFromAssemblyOf<Item>()
            .AddFromAssemblyOf<Order>())
            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
            .BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}
