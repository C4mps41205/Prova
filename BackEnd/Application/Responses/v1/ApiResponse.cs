namespace Application.Responses.v1;

/// <summary>
/// Represents a response from an API, containing a status code, a message, and optional data.
/// </summary>
/// <param name="Status">The HTTP status code of the response.</param>
/// <param name="Message">A message describing the response.</param>
/// <param name="Data">Any data returned by the API.</param>
public class ApiResponse
{
    /// <summary>
    /// Gets or sets the HTTP status code of the response.
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// Gets or sets the message describing the response.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets any data returned by the API.
    /// </summary>
    public object Data { get; set; }
}