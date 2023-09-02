﻿using Application.ViewModel.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountAppService
    {
        Task<int> Create(CreateNewAccountViewModel newAccount);

        Task<string> LogIn(LoginViewModel login);
    }
}
