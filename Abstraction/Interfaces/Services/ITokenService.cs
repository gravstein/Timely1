using Models.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt; // библиотека для работы с JWT токенами
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Interfaces.Services
{
    // интерфейс для сервиса генерации и обновления JWT токенов
    public interface ITokenService
    {
        JwtSecurityToken GenerateAccessToken(AppUser user, IList<string> roles);
        string GenerateRefreshToken();
    }
}
