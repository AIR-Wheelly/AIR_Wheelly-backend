﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIR_Wheelly_Common.Interfaces {
    public interface IUnitOfWork {
        IUserRepository UserRepository { get; }


        public Task<int> CompleteAsync();
    }
}