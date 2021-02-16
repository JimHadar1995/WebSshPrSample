using System;
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
    /// Обработчик команды обновления роли.
    /// </summary>
    public sealed class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Unit>
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
        /// <param name="localizer"></param>
        public UpdateRoleCommandHandler(
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
        public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            try
            {
                _ufw.BeginTransaction();
                await _roleService.UpdateAsync(model, cancellationToken);
                _ufw.CommitTransaction();

                _logger.Info(_localizer[RolesConstants.RoleUpdateSuccess, model.Name]);

                var roleUpdated = await _roleService.GetByIdAsync(model.Id, cancellationToken);

                //await _kafkaService.SendInfo(nameof(KeyMessageKafka.Role),
                //                             KeyMessageKafka.Role.UpdateRole,
                //                             new List<RoleDto> { roleUpdated })
                //    .ConfigureAwait(false);

                return Unit.Value;
            }
            catch (BaseException)
            {
                _ufw.RollbackTransaction();
                throw;
            }
            catch (Exception ex)
            {
                _ufw.RollbackTransaction();
                _logger.Error(ex, _localizer[RolesConstants.RoleUpdateError, model.Name]);
                throw new IdentityServiceException(_localizer[RolesConstants.RoleUpdateError, model.Name], ex);
            }
        }
    }
}
