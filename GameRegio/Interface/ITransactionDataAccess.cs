﻿using GameRegio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.Interface
{
    public interface ITransactionDataAccess : IMongoRepository<Transactions, string>
    {
    }
}
