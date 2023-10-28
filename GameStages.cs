namespace GameNamespace;

static class GameStages {
    private const int MIN_LENGTH_OF_START_WORD = 8;
    private const int MAX_LENGTH_OF_WORD = 30;
    private const int MIN_LENGTH_OF_PLAYER_WORD = 0;

    /// <summary>
    /// Launches the game
    /// </summary>
    public static void LaunchGame() {
        string? startWord = InputOutput.WordInput("Start word: ", MIN_LENGTH_OF_START_WORD, MAX_LENGTH_OF_WORD);
        int timeForRound = InputOutput.NumberInput("Enter time for round(sec): ") * 1000;

        GameRound(startWord, timeForRound);
        Console.WriteLine("Game has ended!\nDo you want to restart the game?");
        GameEnded();
    }

    /// <summary>
    /// Manages a round of the game and should it be restarted or not.
    /// </summary>
    /// <param name="startWord">Main word of the game.</param>
    /// <param name="timeForRound">Time for player to input his word.</param>
    /// <param name="roundNumber">Number of round.</param>
    static void GameRound(string? startWord, int timeForRound, int roundNumber = 1) {
        string? firstPlayerWord, secondPlayerWord;

        InputOutput.RoundInfo(roundNumber);

        firstPlayerWord = InputOutput.TimeoutInput(timeForRound, "First player's word: ", MIN_LENGTH_OF_PLAYER_WORD, MAX_LENGTH_OF_WORD);
        secondPlayerWord = InputOutput.TimeoutInput(timeForRound, "Second player's word: ", MIN_LENGTH_OF_PLAYER_WORD, MAX_LENGTH_OF_WORD);

        if (firstPlayerWord.Equals(secondPlayerWord, StringComparison.OrdinalIgnoreCase)) {
            Console.WriteLine("Words must be different!");
            GameRound(startWord, timeForRound, roundNumber);
            return;
        }
        
        if (!GameHelper.CompairingWords(startWord, firstPlayerWord, secondPlayerWord))
            return;

        GameRound(startWord, timeForRound, roundNumber + 1);
    }

    /// <summary>
    /// Manages should the game be restarted or finished.
    /// </summary>
    static void GameEnded() {
        string? endPar = InputOutput.WordInput("Choose the option (restart, exit): ");

        switch (endPar) {
            case "restart": LaunchGame();
            break;
            case "exit": return;
            default: 
                Console.WriteLine("Invalid input!");
                GameEnded();
            break;
        }
    }
}