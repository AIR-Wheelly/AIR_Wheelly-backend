﻿using AIR_Wheelly_Common.DTO;
using AIR_Wheelly_Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AIR_Wheelly_Common.Interfaces.Service
{
    public interface IPaymentService
    {
        Task<string> GenerateClientToken();

        Task CreateTransaction(CreatePaymentDTO dto);
    }
}
