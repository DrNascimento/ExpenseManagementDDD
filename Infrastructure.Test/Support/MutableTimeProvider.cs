namespace Infrastructure.Test.Support;

public class MutableTimeProvider(DateTimeOffset now) : TimeProvider
{
    private DateTimeOffset _now = now;

    public MutableTimeProvider() : this(DateTimeOffset.UtcNow)
    { }

    public override DateTimeOffset GetUtcNow() => _now;

    public void Advance(TimeSpan delta) => _now = _now.Add(delta);

    public void Set(DateTimeOffset now) => _now = now;
}
