using MagicCommander.Application._Shared.Dtos;
using MagicCommander.Application._Shared.Dtos.Users;
using MagicCommander.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCommander.Application.Users.GetAllUsers
{
	public class GetAllUsersRequestHandler : IRequestHandler<GetAllUsersRequest, PaginatedResult<UserDto>>
	{
		private readonly IUsersRepository _usersRepository;

		public GetAllUsersRequestHandler(IUsersRepository usersRepository)
		{
			_usersRepository = usersRepository;
		}

		public async Task<PaginatedResult<UserDto>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
		{
			var query = _usersRepository.GetQueryable();

			var total = await query.CountAsync();

			var queryResult = query
				.Skip(request.Pagination.Quantity * (request.Pagination.Page - 1))
				.Take(request.Pagination.Quantity)
				.Select(u => UserDto.FromEntity(u));

			return new PaginatedResult<UserDto>(
					request.Pagination.Page,
					request.Pagination.Quantity,
					total,
					await queryResult.ToListAsync()
				);
		}
	}
}
