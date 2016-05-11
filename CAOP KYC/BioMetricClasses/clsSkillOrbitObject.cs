using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BioMetricClasses
{

    [Serializable]
    public class clsSkillOrbitObject
    {

        public string CNIC { get; set; }
        public string TOTAccount { get; set; } //Type Of Account
        public string ContactNumber { get; set; } //Type Of Account
        public string UserId { get; set; } //Type Of Account
        public string BranchCode { get; set; } //Type Of Account
        public string NameOfArea { get; set; } //Type Of Account
        public int? AccountId { get; set; }
        public int? CIF { get; set; }


        public static string ObjectName = "";
        public static string redirectPage = "";


        private static byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        private static Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();

            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);

            return obj;
        }


        public static string get_clsSkillOrbitObject_STR(clsSkillOrbitObject Object)
        {
            string Base64String = null;
            try
            {
                byte[] objByte = ObjectToByteArray(Object);
                Base64String = Convert.ToBase64String(objByte);
                // Base64String = Base64String.Replace(" ", "+");
                return Base64String;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static clsSkillOrbitObject get_clsSkillOrbitObject_OBJ(string Base64String)
        {
            clsSkillOrbitObject clsSkillOrbitobj = null;
            try
            {
                Base64String = Base64String.Replace(" ", "+");
                byte[] toDecodeByte = Convert.FromBase64String(Base64String);
                clsSkillOrbitobj = (clsSkillOrbitObject)ByteArrayToObject(toDecodeByte);
                return clsSkillOrbitobj;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
