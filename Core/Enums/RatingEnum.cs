using System.Collections.Generic;

namespace Core.Enums
{
    public class RatingEnum
    {
        public static RatingEnum OK = new RatingEnum(1, "Beginner");
        public static RatingEnum GOOD = new RatingEnum(2, "Intermediate");
        public static RatingEnum GREAT = new RatingEnum(3, "Experienced");

        public int Key { get; private set; }
        public string Display { get; private set; }
        public RatingEnum(int key, string display)
        {
            Key = key;
            Display = display;
        }

        public static IEnumerable<RatingEnum> GetAll()
        {
            return new List<RatingEnum>() { OK, GOOD, GREAT };
        }
    }
}