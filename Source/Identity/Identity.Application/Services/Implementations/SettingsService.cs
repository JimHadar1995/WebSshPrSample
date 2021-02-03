using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Dto;
using Identity.Application.Services.Contracts;
using Identity.Core.Services;
using Library.Common.Database;
using MapsterMapper;

namespace Identity.Application.Services.Implementations
{
    /// <inheritdoc/>
    public sealed class SettingsService : ISettingsService
    {
        private readonly IUnitOfWork _ufw;
        private readonly IMapper _mapper;
        private readonly IIdentityAppSettings _identityAppSettings;

        /// <summary>
        /// 
        /// </summary>
        public SettingsService(
            IUnitOfWork ufw,
            IMapper mapper,
            IIdentityAppSettings identityAppSettings)
        {
            _ufw = ufw;
            _mapper = mapper;
            _identityAppSettings = identityAppSettings;
        }
        /// <inheritdoc/>
        public ValueTask<PasswordPolicyDto> GetPasswordPolicyAsync(CancellationToken token)
        {
            var policy = _identityAppSettings.PasswordPolicy;

            return new ValueTask<PasswordPolicyDto>(_mapper.Map<PasswordPolicyDto>(policy));
        }

        /// <inheritdoc/>
        public async Task SavePasswordPolicyAsync(PasswordPolicyDto model, CancellationToken token)
        {
            var policy = _identityAppSettings.PasswordPolicy;
            
            _mapper.Map(model, policy);

            await _identityAppSettings.Save();
        }
    }
}
