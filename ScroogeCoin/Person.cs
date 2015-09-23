using System.Collections.Generic;
using System.Linq;

namespace ScroogeCoin
{
    public class Person
    {
        private static readonly int sizeKey = 256;
        private List<Transaction> wallet = new List<Transaction>();
        protected Signature mySignature = new Signature(sizeKey);

        public byte[] PublicKey
        {
            get { return mySignature.PublicKey; }
        }

        public Person()
        {
        }

        public void AddTransaction(Transaction trans)
        {
            CheckTransaction(trans);
            wallet.Add(trans);
        }

        public Transaction PayTo(byte[] publicKey)
        {
            var trans = wallet.Last();
            var sgndTrans = mySignature.SignMessage(trans);
            var paidTransaction = trans.Payto(sgndTrans, publicKey);
            wallet.Remove(trans);

            return paidTransaction;
        }

        private void CheckTransaction(Transaction transaction)
        {
            foreach(var trans in transaction)
            {
                trans.CheckTransaction();
            }
        }
    }
}
