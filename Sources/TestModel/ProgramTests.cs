using Xunit;
using System;
using System.IO;
using ConsoleApp;

public class ProgramTests
{
    [Fact]
    public void Menu_ShouldReturnValidChoice()
    {
        // Simule l'entrée utilisateur "1"
        using var input = new StringReader("1\n");
        using var output = new StringWriter();
        Console.SetIn(input);
        Console.SetOut(output);

        // Rendre Menu public ou internal pour le test
        int result = Program.Menu();

        Assert.Equal(1, result);
    }

    [Fact]
    public void ChooseDifficulty_ShouldReturnRulesBeginner_WhenInputIs1()
    {
        using var input = new StringReader("1\n");
        Console.SetIn(input);

        var rules = Program.ChooseDifficulty();

        Assert.IsType<RulesBeginner>(rules);
    }

    [Fact]
    public void ChooseDifficulty_ShouldReturnRules_WhenInputIs2()
    {
        using var input = new StringReader("2\n");
        Console.SetIn(input);

        var rules = Program.ChooseDifficulty();

        Assert.IsType<Rules>(rules);
    }

    [Fact]
    public void ChooseDifficulty_ShouldReturnRulesAdvanced_WhenInputIs3()
    {
        using var input = new StringReader("3\n");
        Console.SetIn(input);

        var rules = Program.ChooseDifficulty();

        Assert.IsType<RulesAdvanced>(rules);
    }
    [Fact]
    public void CreatePlayers_ShouldCreateHumanAndAI_WhenSolo()
    {
        using var input = new StringReader("TestUser\n");
        Console.SetIn(input);

        IPlayer[] players = new IPlayer[2];
        Program.CreatePlayers(true, players);

        Assert.IsType<HumanPlayer>(players[0]);
        Assert.IsType<DumbAIPlayer>(players[1]);
        Assert.Equal("TestUser", players[0].Name);
    }
    [Fact]
    public void Menu_ShouldHandleInvalidInput_ThenReturnValidChoice()
    {
        using var input = new StringReader("abc\n0\n1\n");
        using var output = new StringWriter();
        Console.SetIn(input);
        Console.SetOut(output);

        int result = Program.Menu();

        Assert.Equal(1, result);
    }

    [Fact]
    public void ChooseDifficulty_ShouldReturnRules_WhenInputIsInvalid()
    {
        using var input = new StringReader("invalid\n");
        Console.SetIn(input);

        var rules = Program.ChooseDifficulty();

        Assert.IsType<Rules>(rules);
    }

    [Fact]
    public void CreatePlayers_ShouldDefaultName_WhenInputIsEmpty()
    {
        using var input = new StringReader("\n");
        Console.SetIn(input);

        IPlayer[] players = new IPlayer[2];
        Program.CreatePlayers(true, players);

        Assert.Equal("Player1", players[0].Name);
        Assert.IsType<DumbAIPlayer>(players[1]);
    }

    [Fact]
    public void CreatePlayers_ShouldCreateTwoHumanPlayers_WhenNotSolo()
    {
        using var input = new StringReader("Alice\nBob\n");
        Console.SetIn(input);

        IPlayer[] players = new IPlayer[2];
        Program.CreatePlayers(false, players);

        Assert.IsType<HumanPlayer>(players[0]);
        Assert.IsType<HumanPlayer>(players[1]);
        Assert.Equal("Alice", players[0].Name);
        Assert.Equal("Bob", players[1].Name);
    }

    [Fact]
    public void CreatePlayers_ShouldDefaultNames_WhenBothInputsEmpty()
    {
        using var input = new StringReader("\n\n");
        Console.SetIn(input);

        IPlayer[] players = new IPlayer[2];
        Program.CreatePlayers(false, players);

        Assert.Equal("Player1", players[0].Name);
        Assert.Equal("Player2", players[1].Name);
    }

}
