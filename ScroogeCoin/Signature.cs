using System;
using System.Security.Cryptography;

namespace ScroogeCoin
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

        public SignedMessage SignMessage(Coin coin)
        {
            return SignMessage((Object) coin);
        }

        public SignedMessage SignMessage(Transaction transaction)
        {
            return SignMessage((Object) transaction);
        }

        private SignedMessage SignMessage(Object obj)
        {
            var bObj = Global.ConvertObjetToArrayByte(obj);

            // signing hash data
            //var msgHashed = new SHA1Managed().ComputeHash(message);
            //var sgndData = dsa.SignHash(msgHashed); 

            var sgndData = dsa.SignData(bObj);
            return new SignedMessage(this, sgndData);
        }
    }
}
