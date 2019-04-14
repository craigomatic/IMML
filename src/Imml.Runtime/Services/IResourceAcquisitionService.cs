using System.Threading.Tasks;
using Imml.ComponentModel;

namespace Imml.Runtime.Services
{
    public interface IResourceAcquisitionService
    {
        Task<byte[]> AcquireResource(ISourcedElement sourcedElement);
    }
}