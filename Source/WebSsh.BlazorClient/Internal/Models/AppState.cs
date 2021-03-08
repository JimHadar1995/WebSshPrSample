using System.Collections.Generic;
using System.ComponentModel;
using WebSsh.Shared.Dto.Roles;

namespace WebSsh.BlazorClient.Internal.Models
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AppState : INotifyPropertyChanged
    {
        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        private List<RoleDto> _roles = new List<RoleDto>(2);

        /// <summary>
        /// Роли пользователя
        /// </summary>
        public IReadOnlyList<RoleDto> Roles
        {
            get => _roles;
            set
            {
                if (value != null)
                {
                    _roles.Clear();
                    _roles.AddRange(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Roles)));
                }
            }
        }
    }
}
