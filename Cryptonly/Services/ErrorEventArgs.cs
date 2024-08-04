/// <summary>
/// Provides error data in the CoinCapRepository class.
/// </summary>
public class ErrorEventArgs : EventArgs
{
    public string MethodName { get; }
    public string Message { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorEventArgs"/> class.
    /// </summary>
    /// <param name="methodName">The name of the method where the error occurred.</param>
    /// <param name="message">The error message.</param>
    public ErrorEventArgs(string methodName, string message)
    {
        MethodName = methodName;
        Message = message;
    }
}