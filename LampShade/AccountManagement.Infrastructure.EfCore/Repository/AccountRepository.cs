﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
    {
        private readonly AccountContext _context;

        public AccountRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        public Account GetBy(string username)
        {
            return _context.Accounts.FirstOrDefault(x => x.UserName == username);
        }

        public EditAccount GetDetails(long id)
        {
            return _context.Accounts.Select(x => new EditAccount
            {
                Id = x.Id,
                FullName = x.FullName,
                Mobile = x.Mobile,
                RoleId = x.RoleId,
                UserName = x.UserName
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var query = _context.Accounts.Include(x=>x.Role).Select(x => new AccountViewModel
            {
                CreationDate = x.CreationDate.ToFarsi(),
                FullName = x.FullName,
                Id = x.Id,
                Mobile = x.Mobile,
                Role = x.Role.Name,
                RoleId = x.RoleId,
                ProfilePhoto = x.ProfilePhoto,
                UserName = x.UserName
            }).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchModel.FullName))
                query = query.Where(x => x.FullName.Contains(searchModel.FullName));

            if (!string.IsNullOrWhiteSpace(searchModel.UserName))
                query = query.Where(x => x.UserName.Contains(searchModel.UserName));

            if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
                query = query.Where(x => x.Mobile.Contains(searchModel.Mobile));

            if (searchModel.RecordId > 0)
                query = query.Where(x => x.RoleId == searchModel.RecordId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}