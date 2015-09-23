namespace ScroogeCoin
{
    public class Goofy : Person
    {
        public Goofy()
        {
            Global.GoofyPk = mySignature.PublicKey;
        }


        public GoofyTransaction CreateCoin(byte[] ownerPk)
        {
            var goofyCoin = new Coin(mySignature);
            return new GoofyTransaction(goofyCoin, ownerPk);
        }
    }
}
