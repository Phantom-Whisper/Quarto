namespace Manager
{
    public interface IScoreManager
    {
        void AddVictory(IPlayer winner);
        void SaveScores(string filepath);
        void LoadScores(string filepath);
        int GetScore(IPlayer player);
    }
}
