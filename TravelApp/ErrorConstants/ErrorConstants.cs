namespace TravelApp.ErrorConstants
{   
    /// <summary>
    /// This class holds Error constants messages.
    /// </summary>
    public static class ErrorConstants
    {
        public static class GlobalErrorConstants
        {
            public const string somethingWrong = "Something went wrong, please try again!";
        }

        public static class UserErrorConstants
        {
            public const string invalidLogin = "Invalid Login";
            public const string unAvailableUserName = "This UserName is unavailable. Please try another one";
            public const string unAvailableEmail = "This Email is unavailable. Please try another one";

        }

        public static class JourneyErrorConstants
        {
            public const string startBeforeToday = "Start Date of the Journey must be after today";
            public const string endBeforeStart = "Start Date must be before End Date";
            public const string wrongNumberDays = "Wrong number of days";

        }

        public static class RequestErrorConstants
        {
            public const string wrongNumberOfPeople = "Number of people must be greater or equal to available";
        }

    }
}
