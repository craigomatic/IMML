using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imml.Runtime.Services
{
    public interface ICacheService
    {
        Task Store(string key, byte[] value);

        Task<byte[]> Retrieve(string key);
    }
}
