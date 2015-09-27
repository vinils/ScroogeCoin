using System;
using System.Security.Cryptography;

namespace ScroogeCoin
{
    public class TransferHashed
    {
        protected TransferInfo info;
        protected byte[] hash;

        public byte[] Hash
        {
            get { return hash; }
            set { hash = value; }
        }

        public TransferInfo Info
        {
            get { return info; }
            set { info = value; }
        }

        public TransferHashed(TransferInfo info, byte[] hash)
        {
            this.info = info;
            this.Hash = hash;
        }

        public TransferHashed(TransferInfo info)
        {
            this.info = info;
            this.Hash = HashTransfer(Info);
        }

        public virtual Boolean isInfoNotNull()
        {
            return info != null;
        }

        public virtual Boolean isHashNotNull()
        {
            return hash != null;
        }

        public virtual Boolean isValidHash()
        {
            byte[] comperHash = HashTransfer(info);

            for (int x = 0; x < hash.Length; x++)
            {
                if (hash[x] != comperHash[x])
                {
                    return false;
                }
            }  

            return true;
        }

        public virtual Boolean isValidSignedMsg(TransferHashed previous)
        {
            return info.PreviousTransSignedByMe.isValidSignedMsg(previous);
        }

        public virtual Boolean isSignerPreviousTransactoin(byte[] ownerPk)
        {
            return info.PreviousTransSignedByMe.PublicKey == ownerPk;
        }

        public virtual void CheckTransfer()
        {
            if (!isInfoNotNull())
                throw new Exception("Transfer informations must be informed");

            info.CheckTransfer();

            if (!isHashNotNull())
                throw new Exception("The hash of the transfer must be informed.");
            if (!isValidHash())
                throw new Exception("The hash of this transfer is invalid.");
        }

        private byte[] HashTransfer(TransferInfo transInfo)
        {
            var bTrans = Global.ConvertObjetToArrayByte(transInfo);
            
            return new SHA1Managed().ComputeHash(bTrans);
        }
   }
}