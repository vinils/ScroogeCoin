using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScroogeCoin
{
    public class TransferSignature
    {
        protected TransferHashed transferHashed;
        protected byte[] scroogeSignature;

        public TransferSignature(TransferHashed transHashed, byte[] scroogeSign)
        {
            this.transferHashed = transHashed;
            this.scroogeSignature = scroogeSign;
        }

        public TransferSignature(TransferInfo transInfo, byte[] scroogeSign)
            : this(new TransferHashed(transInfo), scroogeSign)
        {
        }

        public virtual Boolean isValidSignedMsg()
        {
            return true;
            //return previousTransSignedByMe.isValidSignedMsg(previous);
        }

        protected virtual Boolean isSignerPreviousTransactoin()
        {
            return true;
            //return previousTransSignedByMe.PublicKey == Global.GoofyPk;
        }
    }
}
