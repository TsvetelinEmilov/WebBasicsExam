namespace SharedTrip.Services
{
    using System.Collections.Generic;
    using SharedTrip.Models.Users;
    using System.Text.RegularExpressions;

    using static Data.DataConstants;
    using System.Linq;
    using SharedTrip.Models.Trips;
    using System.Globalization;
    using System;

    public class Validator : IValidator
    {
      
        public ICollection<string> ValidateUser(RegisterUserFormModel user)
        {
            var errors = new List<string>();

            if (user.Username.Length < UsernameMinLength ||
                user.Username.Length > DefaultMaxLength)
            {
                errors.Add($"Username '{user.Username}' is not valid. It must be between {UsernameMinLength} and {DefaultMaxLength} characters long.");
            }

            if (!Regex.IsMatch(user.Email, UserEmailRegularExpression))
            {
                errors.Add($"Email '{user.Email}' is not a valid e-mail address.");
            }

            if (user.Password.Length < PasswordMinLength ||
                user.Password.Length > DefaultMaxLength)
            {
                errors.Add($"The provided password is not valid. It must be between {PasswordMinLength} and {DefaultMaxLength} characters long.");
            }

            if (user.Password.Any(x => x == ' '))
            {
                errors.Add($"The provided password cannot contain whitespaces.");
            }

            if (user.Password != user.ConfirmPassword)
            {
                errors.Add("Password and its confirmation are different.");
            }

            return errors;
        }

        public ICollection<string> ValidateTrip(AddTripModel trip)
        {
            var errors = new List<string>();

            if (!DateTime.TryParseExact(trip.DepartureTime, DateFormat, null,
               DateTimeStyles.None, out _))
            {
                errors.Add("Date format should be: 'dd.MM.yyyy HH:mm'");
            }

            if (trip.Seats < SeatsMinCount || trip.Seats > SeatsMaxCount)
            {
                errors.Add($"Invalid seats number. Seats count must be between {SeatsMinCount} and {SeatsMaxCount}.");
            }

            if (trip.Description.Length > DescriptionMaxLength)
            {
                errors.Add($"The Description must not be longer than {DescriptionMaxLength} characters.");
            }

            if (trip.Description.Length < 1)
            {
                errors.Add($"The Description cannot be empty");
            }

            if (!string.IsNullOrWhiteSpace(trip.ImagePath) &&
                !Uri.IsWellFormedUriString(trip.ImagePath, UriKind.Absolute))
            {
                errors.Add("Invalid URL Address!");
            }

            return errors;
        }

    }
}
