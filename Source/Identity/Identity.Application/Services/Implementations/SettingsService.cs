using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Dto.Users;
using Identity.Application.Services.Contracts;
using Identity.Core.Services;
using Library.Common.Database;
using MapsterMapper;

namespace Identity.Application.Services.Implementations
{
    /// <inheritdoc/>
    public sealed class SettingsService : ISettingsService
    {
        private readonly IMapper _mapper;
        private readonly IIdentityAppSettings _identityAppSettings;

        /// <summary>
        /// 
        /// </summary>
        public SettingsService(
            IMapper mapper,
            IIdentityAppSettings identityAppSettings)
        {
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
