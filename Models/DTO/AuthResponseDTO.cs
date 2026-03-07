using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    // данные ответа Аутентификации
    public class AuthResponseDTO
    {
        // JWT токен чтобы пользователь мог делать запросы на сервер
        // в JWT храняться данные пользователя
        public string AccessToken { get; set; }

        // Токен чтобы пользователь мог получать новые JWT токены
        public string RefreshToken { get; set; }
    }
}
