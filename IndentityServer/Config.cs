﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;

namespace IndentityServer
{
    public static class Config
    {

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog") {Scopes = {"catalog_permission"}},
            new ApiResource("resource_photo_stock") {Scopes = {"photo_stock_permission"}},
            new ApiResource("resource_basket") {Scopes = {"basket_permission"}},
             new ApiResource("resource_discount") {Scopes = {"discount_permission"}},
              new ApiResource("resource_order") {Scopes = {"order_permission"}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),
                       new IdentityResource(){Name = "Role",DisplayName = "Roles",Description = "User roles",UserClaims = new[]{"role"}}
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_permission","Catalog Api full permisssion"),
                new ApiScope("photo_stock_permission","photo stock api"),
                 new ApiScope("basket_permission","Basket Api full permisssion"),
                 new ApiScope("discount_permission","Discount Api full permisssion"),
                 new ApiScope("order_permission","Discount Api full permisssion"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientName = "ASP.NET Core MVC",
                    ClientId = "WebMVCClient",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "catalog_permission", "photo_stock_permission",IdentityServerConstants.LocalApi.ScopeName }
                },
                new Client
                {
                    ClientName = "ASP.NET Core MVC",
                    ClientId = "WebMVCClientForUser",
                    AllowOfflineAccess = true,
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "basket_permission", "discount_permission", "order_permission", IdentityServerConstants.StandardScopes.Email,IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile, IdentityServerConstants.StandardScopes.OfflineAccess, IdentityServerConstants.LocalApi.ScopeName,"roles" },
                    AccessTokenLifetime = 1*60*60,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse
                }

            };
    }
}