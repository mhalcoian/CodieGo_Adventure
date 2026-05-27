public static class GameSessionStore
{
    private static GameSession? _session;

    public static GameSession Get()
    {
        if (_session == null)
            _session = new GameSession();

        return _session;
    }

    public static void Reset()
    {
        _session = new GameSession();
    }
}
