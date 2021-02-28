using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Library.Common.Exceptions;
using Library.Common.Localization;
using Library.Logging.Contracts;
using MediatR;
using WebSsh.Application.Queries.Users;
using WebSsh.Application.Services.Contracts;
using WebSsh.Core.Exceptions;
using WebSsh.ResourceManager.Constants;
using WebSsh.Shared.Dto.Users;

namespace WebSsh.Infrastructure.Handlers.Queries.Users
{
    /// <summary>
    /// Обработчик команды получения всех пользователей
    /// </summary>
    public sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IReadOnlyList<UserDto>>
    {
        private readonly IUserService _userService;
        private readonly IOwnSystemLocalizer<UsersConstants> _localizer;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllUsersQueryHandler"/> class.
        /// </summary>
        public GetAllUsersQueryHandler(
            IUserService userService,
            IOwnSystemLocalizer<UsersConstants> localizer,
            ILogger logger)
        {
            _userService = userService;
            _localizer = localizer;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _userService.GetAllAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new WebSshServiceException(_localizer[UsersConstants.GettingTreeUsersError].Value, ex);
            }
        }
    }
}
