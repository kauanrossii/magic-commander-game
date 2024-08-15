using MagicCommander.Application._Shared.Dtos;
using MediatR;

namespace MagicCommander.Application.Users.CreateUser;

public class CreateUserRequest : IRequest<EntityKeyDto?>
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}
