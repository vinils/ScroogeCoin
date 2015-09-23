using System;
using System.Collections;
using System.Collections.Generic;

namespace ScroogeCoin
{
    [Serializable()]
    public class GoofyTransaction: Transaction
    {
        private Coin coin;

        public Coin Coin
        {
            get { return coin; }
        }

        public GoofyTransaction(Coin coin, byte[] destinyPk)
            : base(null, null, destinyPk)
        {
            this.coin = coin;
        }

        public override Boolean isValidSignedMsg()
        {
            return coin.isValidSignature();
        }

        public override void CheckTransaction()
        {
            if (!isGoofyCoin())
                throw new Exception("This coin don't belong to Goofy");

            if (!isValidSignedMsg())
                throw new Exception("This coin signature is invalid");
        }

        private Boolean isGoofyCoin()
        {
            return coin.isGoofyCoin();
        }
    }
}
