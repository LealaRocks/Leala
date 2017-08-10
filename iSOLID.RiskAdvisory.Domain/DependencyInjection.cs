using iSOLID.RiskAdvisory.Domain.Repository;
using iSOLID.RiskAdvisory.Domain.Repository.Concrete;
using iSOLID.RiskAdvisory.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace iSOLID.RiskAdvisory.Domain
{
    public static class DependencyInjection
    {
        public static void Configure(IServiceCollection services)
        {

            services.Add(new ServiceDescriptor(typeof(IMongoClient), typeof(MongoClient), ServiceLifetime.Scoped));


            services.Add(new ServiceDescriptor(typeof(IUserRepository), typeof(UserRepository), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(IProjectRepository), typeof(ProjectRepository), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(IJobTypeRepository), typeof(JobTypeRepository), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(IScriptRepository), typeof(ScriptRepository), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(IClientRepository), typeof(ClientRepository), ServiceLifetime.Scoped));

            services.Add(new ServiceDescriptor(typeof(IUserService), typeof(UserService), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(IProjectService), typeof(ProjectService), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(IClientService), typeof(ClientService), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(IJobTypeService), typeof(JobTypeService), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(IScriptService), typeof(ScriptService), ServiceLifetime.Scoped));
        }
    }
}
