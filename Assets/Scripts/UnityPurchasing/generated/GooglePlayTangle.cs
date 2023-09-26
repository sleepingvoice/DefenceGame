// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("FFKfSlSMaASKnwPgf1rIldBp2fr5sLK8GmWb7RclyZFMTroMkIV0kcYNd2rwdmtuUdTSLXy3VT0o3KtYDb88Hw0wOzQXu3W7yjA8PDw4PT7gqzfN1KDbW7BlqbXygVYlsoFdhpXJxwhl5J5gFX0IvQbKwe3ryI0ZvzwyPQ2/PDc/vzw8PZOORsHJSzqdPaj2MXy8quAKu6XrXFzuTEU+GcYEn2ZYbLASgg470iS16upLPUUeHR4v2Xc5rxTl3pB9lmLre4Tuwl+lxHEcgt+Vid+h6jFIT+bJNIJ01G+U95i60NkoWkA8c2ts41qEf8F34+zvbLMd4tulrE3SYMtNu//P3NzlzqJqyop5u22nBeU5T2mb2DxxVL6teUeoGzoIAj8+PD08");
        private static int[] order = new int[] { 6,3,7,6,7,11,6,8,13,11,12,13,13,13,14 };
        private static int key = 61;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
