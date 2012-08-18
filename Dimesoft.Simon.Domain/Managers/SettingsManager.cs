using Dimesoft.Simon.Domain.Model;

namespace Dimesoft.Simon.Domain.Managers
{
    public class SettingsManager
    {
        private DifficultyLevel _defaultDifficultyLevel;
        private GameMode _defaultGameMode;
        private bool _defaultPlaySoundOption;

        public DifficultyLevel DefaultDifficultyLevel
        {
            get { return _defaultDifficultyLevel; }
            set { _defaultDifficultyLevel = value; }
        }

        public GameMode DefaultGameMode
        {
            get { return _defaultGameMode; }
            set { _defaultGameMode = value; }
        }

        public bool DefaultPlaySoundOption
        {
            get { return _defaultPlaySoundOption; }
            set { _defaultPlaySoundOption = value; }
        }
    }
}
