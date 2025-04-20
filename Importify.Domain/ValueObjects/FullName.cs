namespace Importify.Domain.ValueObjects;

/// <summary>
/// Represents a full name value object consisting of a first name and a last name.
/// </summary>
public sealed record FullName
{
    /// <summary>
    /// Gets the first name.
    /// </summary>
    public string FirstName { get; }

    /// <summary>
    /// Gets the last name.
    /// </summary>
    public string LastName { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FullName"/> class with the specified first and last names.
    /// </summary>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    /// <summary>
    /// Creates a new <see cref="FullName"/> instance with the specified first and last names.
    /// </summary>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    /// <returns>A new <see cref="FullName"/> instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the first name or last name is null, empty, or whitespace.</exception>
    public static FullName Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty.", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty.", nameof(lastName));

        return new FullName(firstName, lastName);
    }

    /// <summary>
    /// Attempts to create a <see cref="FullName"/> instance from the specified first and last names.
    /// </summary>
    /// <param name="first">The first name.</param>
    /// <param name="last">The last name.</param>
    /// <param name="name">When this method returns, contains the created <see cref="FullName"/>
    /// instance if the operation succeeded; otherwise, null.</param>
    /// <returns><c>true</c> if the operation succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParse(string? first, string? last, out FullName? name)
    {
        name = null;

        if (string.IsNullOrWhiteSpace(first) || string.IsNullOrWhiteSpace(last))
            return false;

        name = new FullName(first.Trim(), last.Trim());
        return true;
    }

    /// <summary>
    /// Returns a string representation of the full name.
    /// </summary>
    /// <returns>A string in the format "FirstName LastName".</returns>
    public override string ToString() => $"{FirstName} {LastName}";
}