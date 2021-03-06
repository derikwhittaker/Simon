namespace Dimesoft.Simon.Domain.Model
{
    public enum DifficultyLevel
    {
        Unknown = 0,
        Easy = 1,
        Medium = 2,
        Hard = 3
    }

    public enum GameMode
    {
        Unknown = 0,
        Normal = 1
    }

    public enum GameTile
    {
        Unknown = 0,
        TopLeft = 1,
        TopRight = 2,
        BottomLeft = 3,
        BottomRight = 4
    }

    public enum GamePlayStatus
    {
        Unknown = 0,
        Notstarted = 1,
        BeingPlayed = 2,
        Paused = 3,
        Completed = 4,
    }

    public enum AttemptResult
    {
        Unknown = 0 ,
        Valid = 1,
        InValid = 2
    }
}