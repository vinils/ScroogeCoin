using System;

namespace ScroogeCoin
{
    [Serializable()]
    public class Coin
    {
        private Int32 coinId;
        [field: NonSerializedAttribute()]
        private SignedMessage signature;

        public Coin(Signature mySignature)
        {
            coinId = Counter.Coin;
            Signature = mySignature.SignMessage(this);
        }

        public SignedMessage Signature
        {
            get { return signature; }
            set { signature = value; }
        }

        public Boolean isGoofyCoin()
        {
            return Signature.PublicKey == Global.GoofyPk;
        }
        public Boolean isValidSignature()
        {
            return Signature.isValidSignedMsg(this);
        }
    }
}
