using GameRegio.Entities;
using GameRegio.Helpers;
using GameRegio.Interface;
using GameRegio.Repository;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.Services
{
    public class ConfigService : MongoDbRepositoryBase<Config>, IConfigDataAccess
    {
        public ConfigService(IOptions<MongoDbSettings> options) : base(options)
        {

        }
    }
}
