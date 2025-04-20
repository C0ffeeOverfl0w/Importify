namespace Importify.Domain.ValueObjects;

/// <summary>
/// Represents an email value as a value object.
/// </summary>
public sealed record Email
{
    /// <summary>
    /// Gets the email value as a string.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Email"/> class with the specified email value.
    /// </summary>
    /// <param name="value">The email value.</param>
    private Email(string value) => Value = value;

    /// <summary>
    /// Creates a new <see cref="Email"/> instance if the provided value is valid.
    /// </summary>
    /// <param name="value">The email value to validate and create.</param>
    /// <returns>A new <see cref="Email"/> instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the email format is invalid.</exception>
    public static Email Create(string value)
    {
        if (!IsValid(value))
            throw new ArgumentException("Invalid email format.", nameof(value));

        return new Email(value);
    }

    /// <summary>
    /// Attempts to parse the provided string into an <see cref="Email"/> instance.
    /// </summary>
    /// <param name="value">The email value to parse.</param>
    /// <param name="email">The resulting <see cref="Email"/> instance if parsing is successful.</param>
    /// <returns><c>true</c> if parsing is successful; otherwise, <c>false</c>.</returns>
    public static bool TryParse(string? value, out Email? email)
    {
        email = null;

        if (string.IsNullOrWhiteSpace(value))
            return false;

        try
        {
            var addr = new System.Net.Mail.MailAddress(value);
            email = new Email(addr.Address);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Validates whether the provided email value has a valid format.
    /// </summary>
    /// <param name="value">The email value to validate.</param>
    /// <returns><c>true</c> if the email format is valid; otherwise, <c>false</c>.</returns>
    private static bool IsValid(string value)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(value);
            return addr.Address == value;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Returns the string representation of the email value.
    /// </summary>
    /// <returns>The email value as a string.</returns>
    public override string ToString() => Value;
}