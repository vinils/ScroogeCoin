using System;

namespace ScroogeCoin
{
    [Serializable()]
    public class TransferInfoCreateCoin: TransferInfo
    {
        private Int32 coinId;

        public TransferInfoCreateCoin(byte[] destinyPk, Int32 coinId)
            : base(null, destinyPk)
        {
            this.coinId = coinId;
        }
        public virtual Boolean isValidCoinId()
        {
            return coinId >= 0;
        }

        public override void CheckTransfer()
        {
            if(!isValidCoinId())
                throw new Exception("Coin id must be informed.");
            if (!isDestinyPkNotNull())
                throw new Exception("Destiny public key must be informed.");
        }
    }
}
