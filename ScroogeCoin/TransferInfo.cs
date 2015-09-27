using System;

namespace GoofyCoin2015
{
    [Serializable()]
    public class TransferInfo
    {
        private SignedMessage previousTransSignedByMe;
        private byte[] destinyPk;

        public byte[] DestinyPk
        {
            get { return destinyPk; }
            set { destinyPk = value; }
        }

        public SignedMessage PreviousTransSignedByMe
        {
            get { return previousTransSignedByMe; }
        }

        public TransferInfo(SignedMessage previousTransSignedByMe, byte[] destinyPk)
        {
            this.previousTransSignedByMe = previousTransSignedByMe;
            this.destinyPk = destinyPk;
        }

        protected virtual Boolean isValidSignedMsg(TransferHashed previous)
        {
            return previousTransSignedByMe.isValidSignedMsg(previous);
        }

        protected virtual Boolean isSignerPreviousTransactoin(byte[] ownerPk)
        {
            return previousTransSignedByMe.PublicKey == ownerPk;
        }

        public virtual Boolean isPrepreviousTransSignedByMeNotNull()
        {
            return previousTransSignedByMe != null;
        }

        public virtual Boolean isDestinyPkNotNull()
        {
            return destinyPk != null;
        }

        public virtual void CheckTransfer()
        {
            if (!isPrepreviousTransSignedByMeNotNull())
                throw new Exception("Signed previous transfer must be informed.");

            if (!isDestinyPkNotNull())
                throw new Exception("Destiny public key must b informed.");
        }
    }
}
