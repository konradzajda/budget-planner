using Microsoft.AspNetCore.Http;
using Tivix.BudgetPlanner.Application.Abstractions;

namespace Tivix.BudgetPlanner.Infrastructure;

public class UserContextAccessor : IUserContextAccessor
{
    private const string EmailClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

    private const string UserIdClaim = "user_id";
    
    private readonly IHttpContextAccessor _contextAccessor;
    
    private string _email = string.Empty;
    private string _userId = string.Empty;

    public string Email
    {
        get
        {
            if (string.IsNullOrEmpty(_email))
                _email = GetClaimValue(EmailClaim);

            return _email;
        }
    }

    public string Id
    {
        get
        {
            if (string.IsNullOrEmpty(_userId))
                _userId = GetClaimValue(UserIdClaim);

            return _userId;
        }
    }
    
    public UserContextAccessor(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
    

    private string GetClaimValue(string claimKey)
    {
        var claim = _contextAccessor.HttpContext!.User.FindFirst(claimKey);
        return claim!.Value;
    }
}