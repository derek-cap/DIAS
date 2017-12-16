using Dicom.Log;
using Dicom.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIAS.DomainService
{ 
    /// <summary>
    /// Class representing to provide Dicom C-Store service.
    /// </summary>
    public class DicomCStoreProvider : DicomService, IDicomServiceProvider, IDicomCEchoProvider, IDicomCStoreProvider
    {
        public DicomCStoreProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log)
            : base(stream, fallbackEncoding, log)
        {
        }

        #region Dicom Store Provider
        public DicomCStoreResponse OnCStoreRequest(DicomCStoreRequest request)
        {
            throw new NotImplementedException();
        }

        public void OnCStoreRequestException(string tempFileName, Exception e)
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
        #endregion

    
    }
}
