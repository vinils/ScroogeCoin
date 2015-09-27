using System;

namespace GoofyCoin2015
{
    public class Transfers : TransferHashed
    {
        private TransferList chain;

        public Transfers(TransferHashed transHashed, TransferList transferChain)
            : base(transHashed.Info, transHashed.Hash)
        {
            this.chain = transferChain;
        }

        public TransferHashed this[TransferHashed trans]
        {
            get
            {
                foreach (var ret in chain)
                    if (ret.Hash == trans.Hash)
                        return ret;

                return null;
            }
        }

        public TransferHashed this[TransferInfo trans]
        {
            get
            {
                var transHashed = new TransferHashed(trans);

                foreach (var ret in chain)
                    if (ret.Hash == transHashed.Hash)
                        return ret;

                return null;
            }
        }

        public Transfers PayTo(TransferInfo transInfo)
        {
            var trans = chain.Payto(transInfo);
            return new Transfers(trans, trans);
        }

        public virtual Boolean isChainNotNull()
        {
            return chain != null;
        }

        public override void CheckTransfer()
        {
            if (!isChainNotNull())
                throw new Exception("Transfer chain must be informed.");

            chain.CheckTransfer();
        }

        public virtual void CheckTransfers()
        {
            foreach (var trans in chain)
            {
                trans.CheckTransfer();
            }
        }
    }
}
