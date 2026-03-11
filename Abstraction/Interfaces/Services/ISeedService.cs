    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Interfaces.Services
{
    // интерфейс для сервиса сидера
    public interface ISeedService
    {
        Task SeedUsersAndRolesAsync();
    }
}
