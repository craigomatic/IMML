using Imml.ComponentModel;
using Imml.Runtime.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Imml.Runtime
{
    public class ResourceAcquisitionService : IResourceAcquisitionService
    {
        public ICacheService CacheService { get; private set; }

        public ResourceAcquisitionService(ICacheService cacheService)
        {
            this.CacheService = cacheService;
        }

        public async Task<byte[]> AcquireResource(ISourcedElement sourcedElement)
        {
            //first look on disk
            var fileExtension = sourcedElement.Source.FileExtension();
            var hash = sourcedElement.Source.ToMD5() + fileExtension;
            var bytes = await this.CacheService.Retrieve(hash);

            if (bytes != null)
            {
                System.Diagnostics.Debug.WriteLine($"Cache hit for {sourcedElement.Source}");
                return bytes;
            }

            System.Diagnostics.Debug.WriteLine($"Cache miss on {sourcedElement.Source}");

            using (var httpClient = new HttpClient())
            {
                var resource = await httpClient.GetAsync(sourcedElement.Source);

                if (resource.IsSuccessStatusCode)
                {
                    //cache, then return
                    var resourceBytes = await resource.Content.ReadAsByteArrayAsync();

                    await this.CacheService.Store(hash, resourceBytes);

                    return resourceBytes;
                }
            }

            //TODO: something better
            throw new Exception();
        }
    }
}
