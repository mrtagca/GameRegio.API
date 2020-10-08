using GameRegio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.Interface
{
    public interface IConfigDataAccess : IMongoRepository<Config, string>
    {
    }
}
