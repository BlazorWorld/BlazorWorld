using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http.Connections.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Services
{
    /// <summary>
    /// A factory for creating <see cref="HttpConnection"/> instances.
    /// </summary>
    public class HttpConnectionFactoryInternal : IConnectionFactory
    {
        private readonly HttpConnectionOptions _httpConnectionOptions;
        private readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpConnectionFactory"/> class.
        /// </summary>
        /// <param name="options">The connection options.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public HttpConnectionFactoryInternal(ILoggerFactory loggerFactory, IOptions<HttpConnectionOptions> httpConnectionOptions)
        {
            _httpConnectionOptions = httpConnectionOptions.Value;
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        internal static HttpConnectionOptions CreateHttpConnectionOptions()
        {
            var o = System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(HttpConnectionOptions))
                as HttpConnectionOptions;

            o.Headers = new Dictionary<string, string>();
            o.Cookies = new System.Net.CookieContainer();
            o.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransports.All;
            o.DefaultTransferFormat = TransferFormat.Binary;
            o.CloseTimeout = TimeSpan.FromSeconds(5);
            return o;
        }

        /// <summary>
        /// Creates a new connection to an <see cref="UriEndPoint"/>.
        /// </summary>
        /// <param name="endPoint">The <see cref="UriEndPoint"/> to connect to.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None" />.</param>
        /// <returns>
        /// A <see cref="ValueTask{TResult}" /> that represents the asynchronous connect, yielding the <see cref="ConnectionContext" /> for the new connection when completed.
        /// </returns>
        public async ValueTask<ConnectionContext> ConnectAsync(EndPoint endPoint, CancellationToken cancellationToken = default)
        {
            if (endPoint == null)
            {
                throw new ArgumentNullException(nameof(endPoint));
            }

            if (!(endPoint is UriEndPoint uriEndPoint))
            {
                throw new NotSupportedException($"The provided {nameof(EndPoint)} must be of type {nameof(UriEndPoint)}.");
            }

            if (_httpConnectionOptions.Url != null && _httpConnectionOptions.Url != uriEndPoint.Uri)
            {
                throw new InvalidOperationException($"If {nameof(HttpConnectionOptions)}.{nameof(HttpConnectionOptions.Url)} was set, it must match the {nameof(UriEndPoint)}.{nameof(UriEndPoint.Uri)} passed to {nameof(ConnectAsync)}.");
            }

            // Shallow copy before setting the Url property so we don't mutate the user-defined options object.
            var shallowCopiedOptions = ShallowCopyHttpConnectionOptions(_httpConnectionOptions);
            shallowCopiedOptions.Url = uriEndPoint.Uri;

            var connection = new HttpConnection(shallowCopiedOptions, _loggerFactory);

            try
            {
                await connection.StartAsync(cancellationToken);
                return connection;
            }
            catch
            {
                // Make sure the connection is disposed, in case it allocated any resources before failing.
                await connection.DisposeAsync();
                throw;
            }
        }

        // Internal for testing
        internal static HttpConnectionOptions ShallowCopyHttpConnectionOptions(HttpConnectionOptions options)
        {
            return options;
        }
    }
}
