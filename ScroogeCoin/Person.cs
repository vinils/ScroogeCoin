using System.Collections.Generic;
using System.Linq;

namespace ScroogeCoin
{
    public class Person
    {
        private static readonly int sizeKey = 256;
        private List<Transfers> wallet = new List<Transfers>();
        protected Signature mySignature = new Signature(sizeKey);

        public byte[] PublicKey
        {
            get { return mySignature.PublicKey; }
        }

        public Person()
        {
        }

        public void AddTransfer(Transfers trans)
        {
            CheckTransfers(trans);
            wallet.Add(trans);
        }

        public Transfers PayTo(byte[] publicKey)
        {
            var trans = wallet.Last();
            var sgndTrans = mySignature.SignMessage(trans);
            var transInfo = new TransferInfo(sgndTrans, publicKey);
            var paidTransfer = trans.PayTo(transInfo);
            wallet.Remove(trans);

            return paidTransfer;
        }

        public virtual void CheckTransfers(Transfers trans)
        {
            trans.CheckTransfers();
        }
    }
}
