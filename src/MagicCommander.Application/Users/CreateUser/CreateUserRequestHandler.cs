using MagicCommander.Application._Shared.Dtos;
using MagicCommander.Domain._Shared.Entities;
using MagicCommander.Domain._Shared.Notifications;
using MagicCommander.Domain.Users;
using MagicCommander.Domain.Users.Entites;
using MediatR;

namespace MagicCommander.Application.Users.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserRequest, EntityKeyDto?>
{
    private readonly INotificationContext _notificationContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUsersRepository _usersRepository;

    public CreateUserCommandHandler(INotificationContext notificationContext, IUnitOfWork unitOfWork, IUsersRepository usersRepository)
    {
        _notificationContext = notificationContext;
        _unitOfWork = unitOfWork;
        _usersRepository = usersRepository;
    }

    public async Task<EntityKeyDto?> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var duplicatedUser = await _usersRepository.ExistsAsync(user =>
            user.Email == request.Email  ||
            user.Name == request.Name
        );
    
        if (duplicatedUser)
        {
            _notificationContext.AddNotification(new Notification("Email", "DuplicatedEmail", "There is already an user with the same email."));
            return null;
        }

        var user = new User(
            request.Name,
            request.Email,
            request.Password
        );

        await _usersRepository.InsertAsync(user);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new EntityKeyDto(user.Key);
    }
}
