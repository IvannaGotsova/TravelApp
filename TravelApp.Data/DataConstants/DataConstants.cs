namespace TravelApp.Data.DataConstants
{
    /// <summary>
    /// This class holds constants for the database entities.
    /// </summary>
    public class DataConstants
    {    
        public class CountryConstants
        {
            public const int CountryMinLengthName = 3;
            public const int CountryMaxLengthName = 100;
            public const int CountryMinLengthDescription = 5;
            public const int CountryMaxLengthDescription = 10000;
            public const int CountryMinLengthImage = 5;
            public const int CountryMaxLengthImage = 10000;
            public const string CountryMinPopulation = "-9223372036854775808";
            public const string CountryMaxPopulation = "9223372036854775807";
            public const string CountryMinArea = "-9223372036854775808";
            public const string CountryMaxArea = "9223372036854775807";
        }

        public class TownConstants
        {
            public const int TownMinLengthName = 3;
            public const int TownMaxLengthName = 100;
            public const int TownMinLengthImage = 5;
            public const int TownMaxLengthImage = 10000;
            public const int TownMinLengthDescription = 5;
            public const int TownMaxLengthDescription = 10000;
            public const string TownMinPopulation = "-9223372036854775808";
            public const string TownMaxPopulation = "9223372036854775807";
            public const string TownMinArea = "-9223372036854775808";
            public const string TownMaxArea = "9223372036854775807";

        }

        public class TripConstants
        {
            public const int TripMinLengthTitle = 1;
            public const int TripMaxLengthTitle = 1000;
            public const int TripMinLengthUser = 3;
            public const int TripMaxLengthUser = 100;
            public const int TripMinRating = 0;
            public const int TripMaxRating = 10;
        }

        public class PostConstants
        {
            public const int PostMinLengthTitle = 1;
            public const int PostMaxLengthTitle = 1000;
            public const int PostMinLengthDescription = 5;
            public const int PostMaxLengthDescription = 10000;
            public const int PostMinLengthImage = 5;
            public const int PostMaxLengthImage = 10000;
        }

        public class CommentConstants
        {
            public const int CommentMinLengthTitle = 5;
            public const int CommentMaxLengthTitle = 1000;
            public const int CommentMinLengthDescription = 5;
            public const int CommentMaxLengthDescription = 10000;
            public const int CommentMinLengthAuthor = 5;
            public const int CommentMaxLengthAuthor = 20;

        }

        public class JourneyConstants
        {
            public const int JourneyMinLengthDescription = 5;
            public const int JourneyMaxLengthDescription = 10000;
            public const int JourneyMinLengthTitle = 1;
            public const int JourneyMaxLengthTitle = 1000;
            public const int JourneyMinNumberPeople = 1;
            public const int JourneyMaxNumberPeople = 100;
            public const int JourneyMinLengthImage = 5;
            public const int JourneyMaxLengthImage = 10000;
            public const int JourneyMinDays = 1;
            public const int JourneyMaxDays = 365;
        }


        public class ApplicationUserConstants
        {
            public const int UserMinLengthUsername = 5;
            public const int UserMaxLengthUsername = 20;

            public const int UserMinLengthEmail = 10;
            public const int UserMaxLengthEmail = 60;

            public const int UserMinLengthPassword = 5;
            public const int UserMaxLengthPassword = 20;

            public const int UserMinLengthFirstName = 5;
            public const int UserMaxLengthFirstName = 20;

            public const int UserMinLengthLastName = 5;
            public const int UserMaxLengthLastName = 20;
        }

        public class RequestConstants
        {
            public const int RequestMinLengthDescription = 5;
            public const int RequestMaxLengthDescription = 10000;
            public const int RequestMinLengthTitle = 1;
            public const int RequestMaxLengthTitle = 1000;
            public const int RequestMinNumberPeople = 1;
            public const int RequestMaxNumberPeople = 100;
            public const int RequestMinDays = 1;
            public const int RequestMaxDays = 365;
        }
    }
}
