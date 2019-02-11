using IronShop.Api.Core.Entities;
using IronShop.Api.Core.Entities.Base;

namespace IronShop.Api.Core.IServices
{
    public interface ITokenService
    {
        IronToken Generate(User user);
    }
}
