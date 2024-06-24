public static class EmailValidator
{
    /// <summary>
    ///  ref.: https://html.spec.whatwg.org/multipage/forms.html#valid-e-mail-address (HTML5 living standard, willful violation of RFC 3522)
    /// </summary>
    public static readonly string EmailValidation_Regex = @"^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";

    public static readonly Regex EmailValidation_Regex_Compiled = new Regex(EmailValidation_Regex, RegexOptions.IgnoreCase);

    public static readonly string[] AllowedDomains = [
        "gmail.com", "yandex.ru", "yahoo.com",
        "vk.com", "mail.ru", "bk.ru", "inbox.ru",
        "rambler.ru", "outlook.com", "googlemail.com"]
    /// <summary>
    /// Checks if the given e-mail is valid using various techniques
    /// </summary>
    /// <param name="email">The e-mail address to check / validate</param>
    /// <param name="useRegEx">TRUE to use the HTML5 living standard e-mail validation RegEx, FALSE to use the built-in validator provided by .NET (default: FALSE)</param>
    /// <returns>TRUE if the e-mail address is valid, FALSE otherwise.</returns>
    public static bool IsValidEmailAddress(string email, bool useRegEx = false)
    {
        var isValid = useRegEx
            // see RegEx comments
            ? email is not null && EmailValidation_Regex_Compiled.IsMatch(email)

            // ref.: https://stackoverflow.com/a/33931538/1233379
            : new EmailAddressAttribute().IsValid(email);

        if (isValid)
        {
            var arr = email.ToLower().Split('@', StringSplitOptions.RemoveEmptyEntries);
            isValid = arr.Length == 2 && arr[1].Contains(".") && AllowedDomains.Contains(arr[1]);
        }
        return isValid;
    }
}
