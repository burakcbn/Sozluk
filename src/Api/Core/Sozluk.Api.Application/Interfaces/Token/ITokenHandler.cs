using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Interfaces.Token
{
    public interface ITokenHandler
    {
        string CreateToken(Claim[] claims);
    }
}
