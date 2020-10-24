using BlazorWorld.Web.Client.Modules.Videos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Client.Modules.Videos.Services
{
    public interface IVideoService
    {
        Task SetVideoAttributesAsync(Video video);
    }
}
