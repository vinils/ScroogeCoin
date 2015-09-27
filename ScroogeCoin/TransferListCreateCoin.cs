using System;

namespace GoofyCoin2015
{
    public class TransferListCreateCoin : TransferList
    {
        public TransferListCreateCoin(TransferHashed trans)
            : base(null, trans)
        {
        }

        public override void CheckTransfer()
        {
            base.CheckTransfer();
        }

        public override void CheckLastTransfer()
        {

        }

        public override Boolean isOwnerTransction()
        {
            return true;
        }

        public override Boolean isValidSignedMsg()
        {
            return true;
        }
    }
}