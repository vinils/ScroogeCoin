using System;
using System.Collections;
using System.Collections.Generic;

namespace GoofyCoin2015
{
    public class TransferList : TransferHashed, IEnumerable<TransferList>
    {
        protected TransferList previous;

        public TransferList Previous
        {
            get { return previous; }
        }

        public TransferList(TransferList previous, TransferHashed transHashed)
            : base(transHashed.Info, transHashed.Hash)
        {
            this.previous = previous;
        }

        protected TransferList(TransferList previous, TransferInfo trans)
            : base(trans)
        {
            this.previous = previous;
        }

        public TransferList Payto(TransferInfo trans)
        {
            return new TransferList(this, trans);
        }

        public override void CheckTransfer()
        {
            base.CheckTransfer();
            CheckLastTransfer();
        }

        public virtual void CheckLastTransfer()
        {
            if (!isOwnerTransction())
                throw new Exception("Transfer dosen't belong to the owner.");

            if (!isValidSignedMsg())
                throw new Exception("Signature of the previous transfer and his pk are invalid.");
        }

        public virtual Boolean isOwnerTransction()
        {
            return isSignerPreviousTransactoin(previous.info.DestinyPk);
        }

        public virtual Boolean isValidSignedMsg()
        {
            return isValidSignedMsg(previous);
        }

        IEnumerator<TransferList> IEnumerable<TransferList>.GetEnumerator()
        {
            TransferList trans = this;

            do
            {
                yield return trans;
                trans = trans.previous;
            } while (trans != null);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
