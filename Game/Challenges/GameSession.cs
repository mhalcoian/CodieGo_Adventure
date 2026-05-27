using CodieGo_Adventure.DTO;
using CodieGo_Adventure.Game.Challenges;

public class GameSession
{
    private readonly List<ChallengeDefinition> _challenges;
    private readonly HashSet<int> _skipped = new();

    private DayOfWeek today = DateTime.Now.DayOfWeek;
    private int _completedCount = 0;

    public int numberOfSkips { get; private set; } = 1;
    public int Score { get; private set; }

    public GameSession()
    {
        var rnd = new Random();

        _challenges = ChallengePool.All
            .Where(x => x.AvailableDays.Contains(today))
            .OrderBy(x => rnd.Next())
            .ToList();

        LoadNext();
    }

    public ChallengeDefinition? Current { get; private set; }

    public bool GameEnded => _completedCount >= 5;

    public void Complete()
    {
        if (Current == null)
            return;

        _completedCount++;
        _skipped.Add(Current!.Id);
        Score = Current.ScorePerDifficulty;

        LoadNext();
    }

    public void Skip()
    {
        if (Current == null)
            return;

        if (numberOfSkips <= 0)
        {
            numberOfSkips = 0;
            return;
        }

        numberOfSkips--;
        _skipped.Add(Current!.Id);
        LoadNext();
    }

    private void LoadNext()
    {
        if (GameEnded)
        {
            Current = null;
            return;
        }

        Current = _challenges.FirstOrDefault(c => !_skipped.Contains(c.Id));
    }
}
