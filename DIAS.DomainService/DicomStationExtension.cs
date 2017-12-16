using Dicom.Network;
using System.Threading.Tasks;

namespace DIAS.DomainService
{
    /// <summary>
    /// DicomStation extentsion for testing connection.
    /// </summary>
    public static class DicomStationExtension
    {
        /// <summary>
        /// Test if the dicom station can be connected. 
        /// Throw exception if the connecting fails.
        /// </summary>
        /// <param name="dicomStation"><see cref="DataModel.DicomStation"/></param>
        public static void TestConnection(this DicomStation dicomStation)
        {
            var client = new DicomClient();
            client.NegotiateAsyncOps();
            client.AddRequest(new DicomCEchoRequest());
            client.Send(dicomStation.IPAddress, dicomStation.Port, false, "Test", dicomStation.AE);
        }

        /// <summary>
        /// Test if the dicom station can be connected. 
        /// Throw exception if the connecting fails.
        /// </summary>
        /// <param name="dicomStation"><see cref="DataModel.DicomStation"/></param>
        public static async Task TestConnectionAsync(this DicomStation dicomStation)
        {
            var client = new DicomClient();
            client.NegotiateAsyncOps();
            client.AddRequest(new DicomCEchoRequest());
            await client.SendAsync(dicomStation.IPAddress, dicomStation.Port, false, "Test", dicomStation.AE);
        }
    }
}
