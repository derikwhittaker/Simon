namespace Dimesoft.Simon.Domain.Model
{
    public class GameLevel
    {
        public void Reset()
        {
            Level = 0;
            Moves = 0;
        }

        public bool IsReset
        {
            get { return Moves == 0 && Level == 0; }
        }

        public int Moves { get; set; }

        public int Level { get; set; }
    }
}