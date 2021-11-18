using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace EtaIdentityServer2
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> {
                new ApiScope("api1", "My API")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client> {
                new Client {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets = {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = {"api1"}
                },
                new Client {
                    ClientId = "client2",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets = {
                        new Secret("secret2".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = {"api1"}
                },
                new Client {
                    ClientId = "mvcclient",
                    // secret for authentication
                    ClientSecrets = {
                        new Secret("secret2".Sha256())
                    },

                    RedirectUris = { "http://localhost:55121/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:55121/" },

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.Code,
                    //RequireConsent = false,
                    //AllowOfflineAccess = true,
                    //AllowAccessTokensViaBrowser = true,

                   

                   

                    // scopes that client has access to
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                }
            };
    }
}