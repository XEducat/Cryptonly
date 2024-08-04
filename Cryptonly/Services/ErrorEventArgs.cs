public class ErrorEventArgs : EventArgs
{
    public string MethodName { get; }
    public string Message { get; }

    public ErrorEventArgs(string methodName, string message)
    {
        MethodName = methodName;
        Message = message;
    }
}