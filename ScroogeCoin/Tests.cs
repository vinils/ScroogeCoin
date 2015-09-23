using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScroogeCoin
{
    public static class Tests
    {
        public static void GoofyCreateAndTansferCoin_SouldHaveValidCoin()
        {
            //Arrange
            var goofySignature = new Signature(256);
            Global.GoofyPk = goofySignature.PublicKey;

            var goofyCoin = new Coin(goofySignature);

            //Act
            var destiny = new Signature(256);
            var trans = new GoofyTransaction(goofyCoin, destiny.PublicKey);

            //Assert

            try
            {
                //if(trans.Coin.Signature.PublicKey != Global.GoofyPk)
                if (!trans.Coin.isGoofyCoin())
                    throw new Exception("This coin doenst belong to Goofy");

                //if(trans.Coin.Signature.isValidSignature(trans.Coin))
                if (!trans.isValidSignedMsg())
                    throw new Exception("This coin signature is invalid");

                //both validation and virtual method
                ((Transaction)trans).CheckTransaction();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void ReceivingAndMaekingTransfer_SouldHaveValidTransaction()
        { 
            //Arrange
            var goofy = new Goofy();
            var person1 = new Signature(256);

            Global.GoofyPk = goofy.PublicKey;

            var trans1 = goofy.CreateCoin(person1.PublicKey);

            //Action
            var sgndTrans1 = person1.SignMessage(trans1);
            var destiny = new Person();
            var trans2 = trans1.Payto(sgndTrans1, destiny.PublicKey);

            //Assert
            try
            {
                //previous.receiverPk != previousTransSignedByMe.PublicKey;
                if(trans2.Previous.TransactionDestinyPk != trans2.PreviousTransSignedByMe.PublicKey)
                    throw new Exception("The transaction dosen't belong to the owner");

                //!previousTransSignedByMe.isValidSignedMsg(previous);
                if (!trans2.PreviousTransSignedByMe.isValidSignedMsg(trans2.Previous))
                    throw new Exception("The previous transaction and his signature dont match");

                //both validation and the virtual methods
                trans2.CheckTransaction();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Attacker change a transfer in the middle of the chain and make the chain invalid
        /// </summary>
        public static void ChengeTransfer_SouldNotAffectTransactionChain()
        {
            //Arrange
            var goofy = new Goofy();
            var changer = new Signature(256);
            var person1 = new Person();
            var person2 = new Person();

            Global.GoofyPk = goofy.PublicKey;
            var trans1 = goofy.CreateCoin(changer.PublicKey);

            var changerSgndTrans = changer.SignMessage(trans1);
            var changerTransaction = trans1.Payto(changerSgndTrans, person1.PublicKey);

            person1.AddTransaction(changerTransaction);

            var tran3 = person1.PayTo(person2.PublicKey);

            //Act
            changerTransaction.TransactionDestinyPk = null;

            //Assert
            try
            {
                person2.AddTransaction(tran3);
            }
            catch 
            {
                Console.WriteLine("Transfer chain is broked because someone change a another transfer in the middle.");
            }
        }

        public static void ReceivingAndMaekingManyTransfer_SouldHaveValidTransactionChain()
        {
            //Arrange
            var goofy = new Goofy();
            var person1 = new Person();
            var person2 = new Person();

            Global.GoofyPk = goofy.PublicKey;

            var trans1 = goofy.CreateCoin(person1.PublicKey);
            person1.AddTransaction(trans1);

            //Action
            var trans2 = person1.PayTo(person2.PublicKey);


            //Assert
            try
            {
                //testing the for loop checktransaction
                person2.AddTransaction(trans2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void DoubleSpendAttack_SouldHaveValidTransactionChain()
        {
            //Arrange
            var goofy = new Goofy();
            var attacker = new Signature(256);

            Global.GoofyPk = goofy.PublicKey;

            var trans1 = goofy.CreateCoin(attacker.PublicKey);

            //Action
            var sgndTrans1 = attacker.SignMessage(trans1);
            var destiny1 = new Person();
            var trans2 = trans1.Payto(sgndTrans1, destiny1.PublicKey);
            var destiny2 = new Person();
            var trans3 = trans1.Payto(sgndTrans1, destiny2.PublicKey);

            //Assert
            try
            {
                if (trans2.isValidSignedMsg() && trans3.isValidSignedMsg())
                    throw new Exception("Its not allowed to double spend the same coin.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
