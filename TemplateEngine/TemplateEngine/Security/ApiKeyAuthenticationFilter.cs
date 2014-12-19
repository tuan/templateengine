using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace TemplateEngine.Security
{
    /// <summary>
    /// Implements simple IAuthenticationFilter
    /// See http://www.asp.net/web-api/overview/security/authentication-filters
    /// </summary>
    public class ApiKeyAuthenticationFilter : IAuthenticationFilter
    {
        private const string AuthorizationScheme = "AuthorizationKey";
        private const string ApiKeyParameterName = "apikey";
        private static readonly char[] CommaSeparator = {','};
        private static readonly char[] KeyValueSeparator = {'='};
        private static readonly TraceSource traceSource = new TraceSource("TemplateEngine.Security.ApiKeyAuthenticationFilter");

        /// <summary>
        /// Gets or sets a value indicating whether more than one instance of the indicated attribute can be specified for a single program element
        /// </summary>
        public bool AllowMultiple
        {
            get { return false; }
        }

        /// <summary>
        /// Authenticates the request.
        /// </summary>
        /// <param name="context">The authentication context.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> that will perform authentication</returns>
        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            // looks for credential in the request
            var request = context.Request;
            var authorization = request.Headers.Authorization;

            // if there are no credentials
            // or filter does not recognize the authentication scheme, do nothing
            if (authorization == null || !authorization.Scheme.Equals(AuthorizationScheme, StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(0);
            }

            // validate credentials that this filter recognizes
            if (String.IsNullOrEmpty(authorization.Parameter))
            {
                var message = "Missing credentials";
                traceSource.TraceInformation(message);
                context.ErrorResult = new AuthenticationFailureResult(message, request);
                return Task.FromResult(0);
            }
            
            var apiKey = ExtractApiKey(authorization.Parameter);
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                var message = "Missing API key";
                traceSource.TraceInformation(message);
                context.ErrorResult = new AuthenticationFailureResult(message, request);
                return Task.FromResult(0);
            }

            // secret api key value is overwritten when running in Azure
            var secretApiKey = ConfigurationManager.AppSettings[ApiKeyParameterName];
            if (secretApiKey.Equals(apiKey, StringComparison.OrdinalIgnoreCase))
            {
                traceSource.TraceInformation("The provided API key is authorized.");

                // if the credentials are valid, set the principle
                context.Principal = new GenericPrincipal(new GenericIdentity("AuthenticatedUser"), new string[]{"User"} );
            }
            else
            {
                var message = string.Format(CultureInfo.InvariantCulture,
                                            "Api key \"{0}\" is not one of the authorized keys", apiKey);
                traceSource.TraceInformation(message);
                context.ErrorResult = new AuthenticationFailureResult(message, request);
                return Task.FromResult(0);
            }

            return Task.FromResult(0);
        }

        private string ExtractApiKey(string authParam)
        {
            var dict = ParseAllKeyValues(authParam);

            if (dict == null)
            {
                return null;
            }

            string value;
            if (dict.TryGetValue(ApiKeyParameterName, out value))
            {
                return value.Replace("\"","");
            }

            return null;
        }

        private IDictionary<string, string> ParseAllKeyValues(string authParam)
        {
            var dict = authParam.Split(CommaSeparator).
                        Select(kv => kv.Split(KeyValueSeparator)).
                        Where(kv => kv.Length == 2).
                        ToDictionary(kv => kv.First(), kv => kv.Last());
            return dict;
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            // not implemented
            return Task.FromResult(0);
        }
    }
}
