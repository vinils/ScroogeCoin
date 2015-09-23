using System;
using System.Security.Cryptography;

namespace ScroogeCoin
{
    [Serializable()]
    public class SignedMessage
    {
        private byte[] sgndMsg;
        protected byte[] publicKey;

        public byte[] PublicKey
        {
            get { return publicKey; }
            private set { publicKey = value; }
        }

        public byte[] SignedMsg
        {
            get { return sgndMsg; }
            private set { sgndMsg = value; }
        }

        public SignedMessage(Signature mySignature, byte[] signedMsg)
        {
            PublicKey = mySignature.PublicKey;
            SignedMsg = signedMsg;
        }

        public Boolean isValidSignedMsg(Coin message)
        {
            return isValidSignedMsg((Object)message);
        }

        public Boolean isValidSignedMsg(Transaction message)
        {
            return isValidSignedMsg((Object)message);
        }

        private Boolean isValidSignedMsg(Object obj)
        {
            Boolean bReturn;

            var bObj = Global.ConvertObjetToArrayByte(obj);

            using (var dsa = new ECDsaCng(CngKey.Import(PublicKey, CngKeyBlobFormat.EccPublicBlob)))
            {
                dsa.HashAlgorithm = Global.HashAlgorithm;

                // verifying hashed message
                //bReturn = dsa.VerifyHash(dataHash, SignedMsg);
                bReturn = dsa.VerifyData(bObj, SignedMsg);
            }

            return bReturn;
        }
    }
}
