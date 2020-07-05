using Dicom.Log;
using Dicom.Network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.DomainService
{
    /// <summary>
    /// Class represening to provider C-Find service.
    /// </summary>
    public class DicomCFindProvider : DicomService, IDicomServiceProvider, IDicomCEchoProvider, IDicomCFindProvider
    {
        public DicomCFindProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log)
            : base(stream, fallbackEncoding, log)
        {
        }

        #region Dicom Find Provider
        public IEnumerable<DicomCFindResponse> OnCFindRequest(DicomCFindRequest request)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region CEchoProvider
        public DicomCEchoResponse OnCEchoRequest(DicomCEchoRequest request)
        {
            throw new NotImplementedException();
        }     
        #endregion

        #region Service provider
        public void OnConnectionClosed(Exception exception)
        {
            throw new NotImplementedException();
        }

        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            throw new NotImplementedException();
        }

        public void OnReceiveAssociationReleaseRequest()
        {
            throw new NotImplementedException();
        }

        public void OnReceiveAssociationRequest(DicomAssociation association)
        {
            throw new NotImplementedException();
        }

        public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
            throw new NotImplementedException();
        }

        public Task OnReceiveAssociationReleaseRequestAsync()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
