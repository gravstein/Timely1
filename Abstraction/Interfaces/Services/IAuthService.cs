using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Interfaces.Services
{
    // интерфейс для нашего сервиса Аутентификации
    public interface IAuthService
    {
        Task<AuthResponseDTO> LoginUser(LoginDTO userForLogin);
        Task<AuthResponseDTO> RefreshToken(string refreshToken);
    }
}
