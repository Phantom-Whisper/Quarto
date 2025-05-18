namespace Manager
{
    public interface IScoreManager
    {
        void AddVictory(IPlayer winner);
        void SaveScores();
        void LoadScores();
        int GetScore(IPlayer player);
    }
}
