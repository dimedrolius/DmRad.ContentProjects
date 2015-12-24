using System;
using DmRad.ContentProjects.Common.Services.Implementation;
using DmRad.ContentProjects.Common.Services.Interfaces;
using Microsoft.Practices.Unity;

namespace DmRad.ContentProjects.Common.Services
{
    public sealed class Endpoint
    {
        #region Singletone

        private static Endpoint _instance;
        private static readonly object LockObj = new object();

        public static Endpoint Instance
        {
            get
            {
                if (_instance == null)
                    lock (LockObj)
                        _instance = new Endpoint();

                return _instance;
            }
        }


        private Endpoint()
        {
            try
            {
                UContainer = new UnityContainer();
                RegisterTypes();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        #endregion

        #region IoC

        public static UnityContainer UContainer { get; set; }

        #endregion

        #region Services

        public void RegisterTypes()
        {
            UContainer.RegisterType<ILoggerService, LoggerService>();
            UContainer.RegisterType<ITestDataService, TestDataService>();

            ResolveTypes();
        }

        public void RegisterFakeTypes()
        {
            //TODO:

            ResolveTypes();
        }

        private void ResolveTypes()
        {
            LoggerService = UContainer.Resolve<ILoggerService>();
            TestDataService = UContainer.Resolve<ITestDataService>();
        }

        public ILoggerService LoggerService { get; private set; }
        public ITestDataService TestDataService { get; private set; }

        #endregion Services
    }
}
