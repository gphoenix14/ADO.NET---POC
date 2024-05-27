using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.Attributes;
using System.Reflection;

namespace EmployeeManagement
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
                    var cfg = new Configuration();
                    cfg.Configure();
                    var mapper = new HbmSerializer();
                    cfg.AddInputStream(mapper.Serialize(Assembly.GetExecutingAssembly()));
                    _sessionFactory = cfg.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
