namespace GoofyCoin2015
{
    public class Goofy : Person
    {
        public Goofy()
        {
            Global.GoofyPk = mySignature.PublicKey;
        }


        public Transfers CreateCoin(byte[] ownerPk)
        {
            var goofyTransInfo = new TransferInfoCreateCoin(ownerPk, Counter.Coin);
            var transHashed =  new TransferHashed(goofyTransInfo);
            var goofyList = new TransferListCreateCoin(transHashed);
            return new Transfers(transHashed, goofyList);
        }
    }
}
