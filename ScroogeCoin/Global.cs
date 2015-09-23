using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace ScroogeCoin
{
    public static class Global
    {
        public static byte[] GoofyPk;
        public static readonly CngAlgorithm HashAlgorithm = CngAlgorithm.Sha256;

        public static byte[] ConvertObjetToArrayByte(Object obj)
        {
            byte[] bObj;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                bObj = ms.ToArray();
            }

            var test = ConvertArrayByteToObjet(bObj);

            return bObj;
        }

        public static Object ConvertArrayByteToObjet(byte[] byts)
        {
            Object obj;

            using (var memStream = new System.IO.MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(byts, 0, byts.Length);
                memStream.Seek(0, System.IO.SeekOrigin.Begin);
                obj = binForm.Deserialize(memStream);
            }

            return obj;
        }
    }
}
