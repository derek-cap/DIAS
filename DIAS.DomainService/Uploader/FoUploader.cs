using Dicom.Network;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIAS.DomainService
{
    public class FoUploader
    {
        private string _callingAE;

        public FoUploader(string callingAE = null)
        {
            _callingAE = callingAE ?? "CallingAE";
        }


        public void Upload(IEnumerable<string> filenames, DicomStation dicomStation)
        {
            var client = new DicomClient();
            // Create request and push to queue.
            foreach (var item in filenames)
            {
                client.AddRequest(new DicomCStoreRequest(item));
            }
            // Send the request.
            client.Send(dicomStation.IPAddress, dicomStation.Port, false, _callingAE, dicomStation.AE);
        }

        public void Upload(string filename, DicomStation dicomStation)
        {
            Upload(new string[] { filename }, dicomStation);
        }


        public async Task UploadAsync(IEnumerable<string> filenames, DicomStation dicomStation)
        {
            var client = new DicomClient();
            // Create request and push to queue.
            foreach (var item in filenames)
            {
                client.AddRequest(new DicomCStoreRequest(item));
            }
            // Send the request.
            await client.SendAsync(dicomStation.IPAddress, dicomStation.Port, false, _callingAE, dicomStation.AE);
        }

        public async Task UploadAsync(string filename, DicomStation dicomStation)
        {
            await UploadAsync(new string[] { filename }, dicomStation);
        }
    }
}
