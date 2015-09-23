using System;
using System.Collections;
using System.Collections.Generic;

namespace ScroogeCoin
{
    [Serializable()]
    public class Transaction: IEnumerable<Transaction>
    {
        private Transaction previous;
        private SignedMessage previousTransSignedByMe;
        private byte[] transactionDestinyPk;

        public byte[] TransactionDestinyPk
        {
            get { return transactionDestinyPk; }
            set { transactionDestinyPk = value; }
        }

        public SignedMessage PreviousTransSignedByMe
        {
            get { return previousTransSignedByMe; }
        }

        public Transaction Previous
        {
            get { return previous; }
        }

        public Transaction(Transaction previous, SignedMessage previousTransSignedByMe, byte[] destinyPk)
        {
            this.previous = previous;
            this.previousTransSignedByMe = previousTransSignedByMe;
            this.transactionDestinyPk = destinyPk;
        }

        public Transaction Payto(SignedMessage sgndTrans, byte[] destinyPk)
        {
            return new Transaction(this, sgndTrans, destinyPk);
        }

        public virtual Boolean isValidSignedMsg()
        {
            return previousTransSignedByMe.isValidSignedMsg(previous);
        }

        public virtual void CheckTransaction()
        {
            if (!isOwnerTransction())
                throw new Exception("The transaction dosen't belong to the owner");

            if (!isValidSignedMsg())
                throw new Exception("The signature of the previous transaction and his pk are invalid");
        }

        private Boolean isOwnerTransction()
        {
            return previous.TransactionDestinyPk == previousTransSignedByMe.PublicKey;
        }

        IEnumerator<Transaction> IEnumerable<Transaction>.GetEnumerator()
        {
            Transaction trans = this;

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
