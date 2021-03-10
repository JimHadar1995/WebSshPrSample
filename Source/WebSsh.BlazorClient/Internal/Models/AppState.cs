using System;
using System.Collections.Generic;
using System.ComponentModel;
using WebSsh.BlazorClient.Internal.Models.Ssh;
using WebSsh.Shared.Dto.Roles;

namespace WebSsh.BlazorClient.Internal.Models
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AppState
    {
        /// <inheritdoc/>
        public event Action? OnChange;

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
                    OnChange?.Invoke();
                }
            }
        }

        private Dictionary<Guid, ActiveSession> _sessions = new Dictionary<Guid, ActiveSession>();

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyDictionary<Guid, ActiveSession> SshSessions => _sessions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public void AddSession(ActiveSession session)
        {
            _sessions.Add(session.SessionsId, session);
            OnChange?.Invoke();
        }

        public void RemoveSession(Guid sessionId)
        {
            if (_sessions.ContainsKey(sessionId))
            {
                _sessions.Remove(sessionId);
                OnChange?.Invoke();
            }
        }
    }
}
