using AIMLChatBot.Conversion;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AIMLChatBot.Helper.Extension
{
    public static class HttpRequestExtensions 
    {
        /// <summary>
        /// Get specific language sent by custom header "Language"
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetLanguageCultures(this HttpRequest request)
        {
            var requestedLanguages = request.Headers["Language"];
            if (StringValues.IsNullOrEmpty(requestedLanguages) || requestedLanguages.Count == 0)
            {
                return null;
            }

            var preferredCultures = requestedLanguages.ToString().Split(',')
                // Parse the header values
                .Select(s => new StringSegment(s))
                .Select(StringWithQualityHeaderValue.Parse)
                // Ignore the "any language" rule
                .Where(sv => sv.Value != "*")
                // Remove duplicate rules with a lower value
                .GroupBy(sv => sv.Value).Select(svg => svg.OrderByDescending(sv => sv.Quality.GetValueOrDefault(1)).First())
                // Sort by preference level
                .OrderByDescending(sv => sv.Quality.GetValueOrDefault(1))
                .Select(sv => new CultureInfo(sv.Value.ToString()))
                .ToList();

            return StringConversion.ReturnSafeString(preferredCultures.FirstOrDefault());
        }

        /// <summary>
        /// get list of accepted language send by default header "Accept-Language"
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static IList<CultureInfo> GetListLanguageCultures(this HttpRequest request)
        {
            var requestedLanguages = request.Headers["Accept-Language"];
            if (StringValues.IsNullOrEmpty(requestedLanguages) || requestedLanguages.Count == 0)
            {
                return null;
            }

            var preferredCultures = requestedLanguages.ToString().Split(',')
                // Parse the header values
                .Select(s => new StringSegment(s))
                .Select(StringWithQualityHeaderValue.Parse)
                // Ignore the "any language" rule
                .Where(sv => sv.Value != "*")
                // Remove duplicate rules with a lower value
                .GroupBy(sv => sv.Value).Select(svg => svg.OrderByDescending(sv => sv.Quality.GetValueOrDefault(1)).First())
                // Sort by preference level
                .OrderByDescending(sv => sv.Quality.GetValueOrDefault(1))
                .Select(sv => new CultureInfo(sv.Value.ToString()))
                .ToList();

            return preferredCultures;
        }
    }
}
