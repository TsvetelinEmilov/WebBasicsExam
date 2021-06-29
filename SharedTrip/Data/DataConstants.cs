namespace SharedTrip.Data
{
    public class DataConstants
    {
        public const string DateFormat = "dd.MM.yyyy HH:mm";

        public const int DefaultMaxLength = 20;

        public const int UsernameMinLength = 5;

        public const string UserEmailRegularExpression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public const int PasswordMinLength = 6;

        public const int SeatsMinCount = 2;
        public const int SeatsMaxCount = 6;

        public const int DescriptionMaxLength = 80;

    }
}
