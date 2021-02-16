using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Commands.Roles;
using Identity.Application.Services.Contracts;
using Identity.Core.Exceptions;
using Identity.ResourceManager.Constants;
using Library.Common.Database;
using Library.Common.Exceptions;
using Library.Common.Kafka;
using Library.Common.Localization;
using Library.Logging.Contracts;
using MediatR;

namespace Identity.Infrastructure.Handlers.Commands.Roles
{
    /// <summary>
    /// Обработчик команды удаления роли.
    /// </summary>
    public sealed class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Unit>
    {
        private readonly IUnitOfWork _ufw;
        private readonly IRoleService _roleService;
        private readonly ILogger _logger;
        private readonly IKafkaService _kafkaService;
        private readonly IOwnSystemLocalizer<RolesConstants> _localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRoleCommandHandler"/> class.
        /// </summary>
        /// <param name="ufw">The ufw.</param>
        /// <param name="roleService">The role service.</param>
        /// <param name="loggerFactory"></param>
        /// <param name="kafkaService"></param>
        public DeleteRoleCommandHandler(
            IUnitOfWork ufw,
            IRoleService roleService,
            ILoggerFactory loggerFactory,
            IKafkaService kafkaService,
            IOwnSystemLocalizer<RolesConstants> localizer)
        {
            _ufw = ufw;
            _roleService = roleService;
            _logger = loggerFactory.GetLogger(ILogger.DefaultLoggerName);
            _kafkaService = kafkaService;
            _localizer = localizer;
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _roleService.GetByIdAsync(request.RoleId, cancellationToken);

                _ufw.BeginTransaction();

                await _roleService.DeleteAsync(request.RoleId, cancellationToken);

                _ufw.CommitTransaction();

                _logger.Info(_localizer[RolesConstants.RoleDeleteSuccess, role.Name]);

                //await _kafkaService.SendInfo(nameof(KeyMessageKafka.Role),
                //        KeyMessageKafka.Role.DeleteRole,
                //        new List<string> { request.RoleId })
                //    .ConfigureAwait(false);
            }
            catch (EntityNotFoundException)
            {
                _ufw.RollbackTransaction();
                return Unit.Value;
            }
            catch (BaseException)
            {
                _ufw.RollbackTransaction();
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, _localizer[RolesConstants.RoleDeleteError, request.RoleId]);
                throw new IdentityServiceException(_localizer[RolesConstants.RoleDeleteError, request.RoleId], ex);
            }
            return Unit.Value;
        }
    }
}
