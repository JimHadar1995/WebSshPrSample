using System;
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
    /// Обработчик запроса получения пользователя по его идентификатору.
    /// </summary>
    public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserService _userService;
        private readonly IOwnSystemLocalizer<UsersConstants> _localizer;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="localizer"></param>
        /// <param name="logger"></param>
        public GetUserByIdQueryHandler(
            IUserService userService,
            IOwnSystemLocalizer<UsersConstants> localizer,
            ILogger logger)
        {
            _userService = userService;
            _logger = logger;
            _localizer = localizer;
        }
        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _userService.GetAsync(request.UserId, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var message = _localizer[UsersConstants.GettingUserAppSettingsError, request.UserId].Value;
                _logger.Error(ex, message);
                throw new IdentityServiceException(message);
            }
        }
    }
}
