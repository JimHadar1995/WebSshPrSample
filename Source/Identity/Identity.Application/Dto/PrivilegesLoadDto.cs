using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PrivilegesLoadDto
    {
        /// <summary>
        /// Название на латинице
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Отображаемое описание
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
