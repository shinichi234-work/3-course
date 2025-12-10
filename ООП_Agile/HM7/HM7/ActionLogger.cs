namespace HM7;

public class ActionLogger
{
    private LogHandler? _logHandler;

    public LogHandler? Handler
    {
        get => _logHandler;
        set => _logHandler = value;
    }

    public void AddHandler(LogHandler handler)
    {
        if (handler == null)
        {
            throw new ArgumentNullException(nameof(handler));
        }

        _logHandler += handler;
    }

    public void RemoveHandler(LogHandler handler)
    {
        if (handler == null)
        {
            throw new ArgumentNullException(nameof(handler));
        }

        _logHandler -= handler;
    }

    public void Run(ActionContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        _logHandler?.Invoke(context);
    }
}