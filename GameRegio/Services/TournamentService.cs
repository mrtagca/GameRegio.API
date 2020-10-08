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
    public class TournamentService : MongoDbRepositoryBase<Tournaments>, ITournamentDataAccess,IDisposable
    {
        public TournamentService(IOptions<MongoDbSettings> options) : base(options)
        {

        }

        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }
}
