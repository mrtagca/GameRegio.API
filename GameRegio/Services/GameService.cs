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
    public class GameService : MongoDbRepositoryBase<Games>, IGameDataAccess
    {
        public GameService(IOptions<MongoDbSettings> options) : base(options)
        {

        }
    }
}
