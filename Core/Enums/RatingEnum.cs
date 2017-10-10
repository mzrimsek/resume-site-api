using System.Collections.Generic;

namespace Core.Enums
{
    public class RatingEnum
    {
        private static readonly RatingEnum Ok = new RatingEnum(1, "Beginner");
        private static readonly RatingEnum Good = new RatingEnum(2, "Intermediate");
        private static readonly RatingEnum Great = new RatingEnum(3, "Experienced");

        public int Key { get; private set; }
        public string Display { get; private set; }
        private RatingEnum(int key, string display)
        {
            Key = key;
            Display = display;
        }

        public static IEnumerable<RatingEnum> GetAll()
        {
            return new List<RatingEnum>() { Ok, Good, Great };
        }
    }
}