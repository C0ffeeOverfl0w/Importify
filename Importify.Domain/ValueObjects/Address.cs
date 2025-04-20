namespace Importify.Domain.ValueObjects;

/// <summary>
/// Represents an immutable address with street, city, and postal code information.
/// </summary>
public sealed record Address
{
    /// <summary>
    /// Gets the first line of the address.
    /// </summary>
    public string AddressLine1 { get; init; }

    /// <summary>
    /// Gets the second line of the address, which is optional.
    /// </summary>
    public string AddressLine2 { get; init; }

    /// <summary>
    /// Gets the postal code of the address.
    /// </summary>
    public string Postcode { get; init; }
    private Address() =>
        (AddressLine1, AddressLine2, Postcode) =
        (string.Empty, string.Empty, string.Empty);

    /// <summary>
    /// Initializes a new instance of the <see cref="Address"/> class.
    /// </summary>
    /// <param name="line1">The first line of the address.</param>
    /// <param name="line2">The second line of the address.</param>
    /// <param name="postcode">The postal code of the address.</param>
    private Address(string line1, string line2, string postcode)
    {
        AddressLine1 = line1;
        AddressLine2 = line2;
        Postcode = postcode;
    }

    /// <summary>
    /// Creates a new <see cref="Address"/> instance with the specified details.
    /// </summary>
    /// <param name="line1">The first line of the address.</param>
    /// <param name="line2">The second line of the address.</param>
    /// <param name="postcode">The postal code of the address.</param>
    /// <returns>A new <see cref="Address"/> instance.</returns>
    /// <exception cref="ArgumentException">Thrown when required fields are missing or invalid.</exception>
    public static Address Create(string line1, string line2, string postcode)
    {
        if (string.IsNullOrWhiteSpace(line1))
            throw new ArgumentException("Address line 1 is required.", nameof(line1));

        if (string.IsNullOrWhiteSpace(postcode))
            throw new ArgumentException("Postcode is required.", nameof(postcode));

        return new Address(line1, line2, postcode);
    }

    /// <summary>
    /// Attempts to parse the provided address details into an <see cref="Address"/> instance.
    /// </summary>
    /// <param name="line1">The first line of the address.</param>
    /// <param name="line2">The second line of the address.</param>
    /// <param name="postcode">The postal code of the address.</param>
    /// <param name="address">When this method returns, contains the parsed <see cref="Address"/>
    /// instance, if successful; otherwise, null.</param>
    /// <returns><c>true</c> if the parsing was successful; otherwise, <c>false</c>.</returns>
    public static bool TryParse(string? line1, string? line2, string? postcode, out Address? address)
    {
        address = null;

        if (string.IsNullOrWhiteSpace(line1) || string.IsNullOrWhiteSpace(postcode))
            return false;

        address = new Address(line1.Trim(), line2?.Trim() ?? string.Empty, postcode.Trim());
        return true;
    }

    /// <summary>
    /// Returns a string representation of the address.
    /// </summary>
    /// <returns>A string that represents the address.</returns>
    public override string ToString() => $"{AddressLine1}, {AddressLine2}, {Postcode}";
}