using System;
using MagicCommander.Domain.Users;
using MagicCommander.Domain.Users.Entites;
using MagicCommander.Infra.Data.Database._Shared;
using Microsoft.EntityFrameworkCore;

namespace MagicCommander.Infra.Data.Database.Repositories;

public class UsersRepository : Repository<User>, IUsersRepository
{
    public UsersRepository(DbContext dbContext) : base(dbContext)
    {
    }
}
