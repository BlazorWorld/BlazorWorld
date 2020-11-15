using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Shell.Components
{
    public partial class Embed : ComponentBase
    {
        [Parameter]
        public string Source { get; set; }
        private string Provider { get; set; }
        private string Extension { get; set; }
        private string Id { get; set; }
        private string[][] _embedPatterns = {
            new string[] {
                "youtube",
                "(?:https?:/{2})?(?:w{3}.)?youtu(?:be)?.(?:com|be)(?:/watch?v=|/)([^&]+)",
                "1"
            },
            new string[] {
                "imgur",
                "(?:https?:/{2})?(?:w{3}.)?imgur.com/(gallery|a)/(.*?)(?:[#/].*|$)",
                "2"
            },
            new string[] {
                "instagram",
                "(?:https?:/{2})?(?:w{3}.)?instagram.com/p/(.*?)(?:[#/].*|$)",
                "1"
            }
        };

        protected override async Task OnParametersSetAsync()
        {
            Source = Source.Replace("{{", "").Replace("}}", "");

            foreach (var embedPattern in _embedPatterns)
            {
                var match = Regex.Match(Source, embedPattern[1]);
                if (match.Success)
                {
                    try
                    {
                        Provider = embedPattern[0];
                        int.TryParse(embedPattern[2], out int index);
                        Id = match.Groups[index].Value;
                    }
                    catch // ignore any error due to improper tag
                    {

                    }
                }
            }
        }
    }
}