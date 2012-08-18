namespace Dimesoft.Simon.Domain.Model
{
    public class Player
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class DefaultSettings
    {
        public int DefaultGameLevel { get; set; }

        public int DefaultGameMode { get; set; }

        public bool DefaultPlaySoundOption { get; set; }
    }

    public class MoveResult
    {
        public AttemptResult AttemptResult { get; set; }

        public bool IsAtEndOfSequence { get; set; }
    }
}