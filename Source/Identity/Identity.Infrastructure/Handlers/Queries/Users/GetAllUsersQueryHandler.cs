using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Dto.Users;
using Identity.Application.Queries.Users;
using Identity.Application.Services.Contracts;
using Identity.Core.Exceptions;
using Identity.ResourceManager.Constants;
using Library.Common.Exceptions;
using Library.Common.Localization;
using Library.Logging.Contracts;
using MediatR;

namespace Identity.Infrastructure.Handlers.Queries.Users
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
        /// <param name="userService">The user service.</param>
        /// <param name="localizer"></param>
        /// <param name="logger"></param>
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
                var message = _localizer[UsersConstants.GettingTreeUsersError].Value;
                _logger.Error(ex, message);
                throw new IdentityServiceException(message);
            }
        }
    }
}
