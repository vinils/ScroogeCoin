using System;

namespace GoofyCoin2015
{
    class Program
    {
        static void Main(string[] args)
        {
            var goofy = new Goofy();
            var alice = new Person();
            var bob = new Person();
            var clark = new Person();

            //goofy Transfer
            var goofyTrans = goofy.CreateCoin(alice.PublicKey);

            //alice Transfer
            alice.AddTransfer(goofyTrans);
            var aliceTrans = alice.PayTo(bob.PublicKey);


            //bob Transfer
            bob.AddTransfer(aliceTrans);
            var bobTrans = bob.PayTo(clark.PublicKey);

            //clark Transfer
            clark.AddTransfer(bobTrans);


            Tests.GoofyCreateAndTansferCoin_SouldHaveValidCoin();
            Tests.ReceivingAndMaekingTransfer_SouldHaveValidTransfer();
            Tests.ReceivingAndMaekingManyTransfer_SouldHaveValidTransferChain();
            Tests.ChengeTransfer_SouldNotAffectTransferChain();
            Tests.DoubleSpendAttack_SouldHaveValidTransferChain();

            //byte[] publickey;
            //byte[] data;
            //byte[] dataHash;
            //byte[] signature;
            //byte[] signature2;

            //using (var dsa = new ECDsaCng(256))
            //{
            //    dsa.HashAlgorithm = CngAlgorithm.Sha256;
            //    publickey = dsa.Key.Export(CngKeyBlobFormat.EccPublicBlob);

            //    data = new byte[] { 21, 5, 8, 12, 207 };
            //    dataHash = new SHA1Managed().ComputeHash(data);
            //    //dataHash = new SHA256Cng().ComputeHash(data);

            //    signature = dsa.SignData(data);
            //    signature2 = dsa.SignHash(dataHash);
            //}

            //Console.WriteLine(signature.Length);
            //Console.WriteLine(Convert.ToBase64String(signature));

            //using (var dsa = new ECDsaCng(CngKey.Import(publickey, CngKeyBlobFormat.EccPublicBlob)))
            //{
            //    dsa.HashAlgorithm = CngAlgorithm.Sha256;

            //    if (dsa.VerifyData(data, signature))
            //        Console.WriteLine("Data is good");
            //    else
            //        Console.WriteLine("Data is bad");

            //    if (dsa.VerifyHash(dataHash, signature2))
            //        Console.WriteLine("Data is good");
            //    else
            //        Console.WriteLine("Data is bad");

            //}

            //Console.ReadKey();

            //String a = "Hello wrold!";
            //Object b = a;
            //byte[] c;
            //BinaryFormatter bf = new BinaryFormatter();
            //using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            //{
            //    bf.Serialize(ms, b);
            //    c = ms.ToArray();
            //}


            //String d;
            //using (var memStream = new System.IO.MemoryStream())
            //{
            //    var binForm = new BinaryFormatter();
            //    memStream.Write(c, 0, c.Length);
            //    memStream.Seek(0, System.IO.SeekOrigin.Begin);
            //    var obj = binForm.Deserialize(memStream);
            //    d = (String) obj;
            //}

            Console.ReadKey();


            //Org.BouncyCastle.Asn1.X9.X9ECParameters ecp = Org.BouncyCastle.Asn1.Sec.SecNamedCurves.GetByName("secp256k1");
            //ECDomainParameters params = new ECDomainParameters(ecp.Curve, ecp.G, ecp.N, ecp.H);
            //ECPublicKeySpec pubKeySpec = new ECPublicKeySpec(
            //ecp.curve.decodePoint(Hex.decode("045894609CCECF9A92533F630DE713A958E96C97CCB8F5ABB5A688A238DEED6DC2D9D0C94EBFB7D526BA6A61764175B99CB6011E2047F9F067293F57F5")), // Q
        //params);
            //PublicKey pubKey = f.generatePublic(pubKeySpec);


            //var signer = SignerUtilities.GetSigner("ECDSA"); // possibly similar to SHA-1withECDSA
            //signer.Init(false, pubKey);
            //signer.BlockUpdate(plainTextAsBytes, 0, plainTextAsBytes.Length);
            //return signer.VerifySignature(signature);

        }
    }
}
