// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Security.Claims;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;
using Owin.Security.Providers.ArcGISPortal.Provider;
using System;

namespace Owin.Security.Providers.ArcGISPortal
{
    /// <summary>
    /// Contains information about the login session as well as the user <see cref="System.Security.Claims.ClaimsIdentity"/>.
    /// </summary>
    public class ArcGISPortalAuthenticatedContext : BaseContext
    {
        /// <summary>
        /// Initializes a <see cref="ArcGISPortalAuthenticatedContext"/>
        /// </summary>
        /// <param name="context">The OWIN environment</param>
        /// <param name="user">The ArcGIS Portal user</param>
        /// <param name="accessToken">ArcGIS Portal access token</param>
        /// <param name="expiresIn">Number of seconds ArcGIS Portal access token expires in</param>
        /// <param name="refreshToken">ArcGIS Portal refresh token</param>
        /// <param name="host">ArcGIS Portal host</param>
        public ArcGISPortalAuthenticatedContext(IOwinContext context, ArcGISPortalUser user, string accessToken, int expiresIn, string refreshToken, string host)
            : base(context)
        {
            Uri hostUri = new Uri(host);

            AccessToken = accessToken;
            AccessTokenExpiresIn = expiresIn;
            RefreshToken = refreshToken;
            Id = user.Username;
            Name = user.FullName;
            Link = new Uri(hostUri, "arcgis/sharing/rest/community/users/" + Id).ToString();
            UserName = Id;
            Email = user.Email;
        }

        /// <summary>
        /// Gets the ArcGIS Portal access token
        /// </summary>
        public string AccessToken { get; private set; }

        /// <summary>
        /// Number of seconds access token will expire in
        /// </summary>
        public int AccessTokenExpiresIn { get; private set; }

        /// <summary>
        /// Gets the ArcGIS Portal refresh token
        /// </summary>
        public string RefreshToken { get; private set; }

        /// <summary>
        /// Gets the ArcGIS Portal user ID
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the user's name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the user's email
        /// </summary>
        public string Email { get; private set; }

        public string Link { get; private set; }

        /// <summary>
        /// Gets the ArcGIS Portal username
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Gets the <see cref="ClaimsIdentity"/> representing the user
        /// </summary>
        public ClaimsIdentity Identity { get; set; }

        /// <summary>
        /// Gets or sets a property bag for common authentication properties
        /// </summary>
        public AuthenticationProperties Properties { get; set; }
    }
}
