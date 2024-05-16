using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using System;
using ChildHDT.Infrastructure.InfrastructureServices.Mapping;
using FluentNHibernate.Cfg.Db;

namespace ChildHDT.Infrastructure.InfrastructureServices.Helper
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    string connStr = "Server=localhost;Port=5432;Database=my_database;User Id=user;Password=my_password;";

                    _sessionFactory = Fluently.Configure()
                        .Database(
                             PostgreSQLConfiguration.Standard.ConnectionString(connStr)
                        )
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ChildMap>())
                        .BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession GetSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}

