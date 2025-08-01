namespace GamesRental.GCommon
{
    public static class ValidationConstants
    {
        // Game
        public const int GameTitleMinLength = 1;
        public const int GameTitleMaxLength = 100;
        public const int GameDescriptionMinLength = 10;
        public const int GameDescriptionMaxLength = 1000;
        public const int GameImageUrlMinLength = 3;
        public const int GameImageUrlMaxLength = 200;

        // Genre
        public const int GenreNameMaxLength = 50;

        // Platform
        public const int PlatformNameMaxLength = 50;

        // Review
        public const int ReviewContentMaxLength = 1000;
    }
}
