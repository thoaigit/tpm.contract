using Autofac;
using Core.DataAccess.Implement;
using Core.DataAccess.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace CoC.MoonSheep.Business
{
    public static class BusinessDependency
    {
        public static void Init(IServiceCollection services)
        {
            #region Register Repository

            #endregion
        }
    }
}
