using System;
using System.Security.Cryptography;

namespace GoofyCoin2015
{
    public class Signature
    {
        private ECDsaCng dsa;
        protected byte[] publicKey;

        public byte[] PublicKey
        {
            get { return publicKey; }
            protected set { publicKey = value; }
        }

        public Signature(Int32 sizeKey)
        {
            dsa = new ECDsaCng(sizeKey);
            dsa.HashAlgorithm = Global.HashAlgorithm;
            PublicKey = dsa.Key.Export(CngKeyBlobFormat.EccPublicBlob);
        }

        public SignedMessage SignMessage(TransferHashed transfer)
        {
            return SignMessage(transfer.Hash);
        }

        private SignedMessage SignMessage(byte[] hash)
        {
            var sgndData = dsa.SignData(hash);

            return new SignedMessage(publicKey, sgndData);
        }
    }
}
