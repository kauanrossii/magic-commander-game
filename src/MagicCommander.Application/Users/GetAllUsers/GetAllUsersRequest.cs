using MagicCommander.Application._Shared.Dtos;
using MagicCommander.Application._Shared.Dtos.Users;
using MediatR;

namespace MagicCommander.Application.Users.GetAllUsers
{
	public class GetAllUsersRequest : IRequest<PaginatedResult<UserDto>>
	{
		public required Pagination Pagination { get; init; }
	}
}
