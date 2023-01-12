using Microsoft.Extensions.Configuration;

namespace Tivix.BudgetPlanner.Api.Extensions;

public static class FirebaseConfigurationExtensions
{
    private const string FirebaseSectionKey = "Firebase";

    private const string FirebaseProjectIdValueKey = "ProjectId";

    private const string FirebaseAuthenticateEndpointValueKey = "AuthenticateEndpoint";

    private const string FirebaseTokenEndpointValueKey = "TokenEndpoint";
    
    private const string OAuthClientIdValueKey = "OAuthClientId";
    
    private const string OAuthClientSecretValueKey = "OAuthClientSecret";
    
    public static string GetProjectId(this IConfiguration configuration)
    {
        return configuration.GetFirebaseSection()
            .GetValue<string>(FirebaseProjectIdValueKey)!;
    }

    public static string GetAuthenticateEndpointUri(this IConfiguration configuration)
    {
        return configuration.GetFirebaseSection()
            .GetValue<string>(FirebaseAuthenticateEndpointValueKey);
    }

    public static string GetTokenEndpointUri(this IConfiguration configuration)
    {
        return configuration.GetFirebaseSection()
            .GetValue<string>(FirebaseTokenEndpointValueKey);
    }

    public static string GetOAuthClientId(this IConfiguration configuration)
    {
        return configuration.GetFirebaseSection()
            .GetValue<string>(OAuthClientIdValueKey);
    }

    public static string GetOAuthClientSecret(this IConfiguration configuration)
    {
        return configuration.GetFirebaseSection()
            .GetValue<string>(OAuthClientSecretValueKey);
    }


    private static IConfigurationSection GetFirebaseSection(this IConfiguration configuration) =>
        configuration.GetSection(FirebaseSectionKey);
}