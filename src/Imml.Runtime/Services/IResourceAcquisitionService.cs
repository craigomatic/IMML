using System.Threading.Tasks;
using Imml.ComponentModel;

namespace Imml.Runtime
{
    public interface IResourceAcquisitionService
    {
        Task<byte[]> AcquireResource(ISourcedElement sourcedElement);
    }
}