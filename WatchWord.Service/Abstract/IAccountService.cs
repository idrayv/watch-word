﻿using System.Threading.Tasks;
using WatchWord.Domain.Entity;

namespace WatchWord.Service
{
    public interface IAccountsService
    {
        /// <summary>Gets user's account by external identifier.</summary>
        /// <param name="id">External identifier, asp.net identity id, etc.</param>
        /// <returns>User's account.</returns>
        Task<Account> GetByExternalId(int id);
    }
}