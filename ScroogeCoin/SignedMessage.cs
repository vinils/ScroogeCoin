using System;
using System.Security.Cryptography;

namespace GoofyCoin2015
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

        public SignedMessage(byte[] publicKey, byte[] signedMsg)
        {
            PublicKey = publicKey;
            SignedMsg = signedMsg;
        }

        public Boolean isValidSignedMsg(TransferHashed message)
        {
            return isValidSignedMsg(message.Hash);
        }

        private Boolean isValidSignedMsg(byte[] hash)
        {
            Boolean bReturn;

            using (var dsa = new ECDsaCng(CngKey.Import(PublicKey, CngKeyBlobFormat.EccPublicBlob)))
            {
                dsa.HashAlgorithm = Global.HashAlgorithm;
                bReturn = dsa.VerifyData(hash, SignedMsg);
            }

            return bReturn;
        }
    }
}
