using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MediumAI : MonoBehaviour
{
    private string[,] grid; // Grille du morpion
    private string playerSymbol; // Symbole du joueur

    

    // Méthode pour que l'IA choisisse le prochain mouvement
    public (int,int) ChooseNextMove(string[,] currentGrid, string currentPlayerSymbol)
    {
        grid = currentGrid;
        playerSymbol = currentPlayerSymbol;

        // Vérifier s'il y a un mouvement gagnant disponible
        (int,int)? winningMove = CheckForWinningMove(playerSymbol);
        if (winningMove != null)
            return ((int, int))winningMove;

        // Vérifier s'il y a un mouvement à bloquer
        (int,int)? blockingMove = CheckForBlockingMove(playerSymbol == "X" ? "O" : "X");
        if (blockingMove != null)
            return ((int, int))blockingMove;

        // Choisir un mouvement aléatoire
        
        return ChooseRandomMove();
    }

    // Méthode pour vérifier s'il y a un mouvement gagnant disponible
    private (int, int)? CheckForWinningMove(string symbol)
    {
        // Vérifie les lignes
        for (int row = 0; row < 3; row++)
        {
            if (grid[row, 0] == symbol && grid[row, 1] == symbol && grid[row, 2] == "")
                return (row, 2);
            if (grid[row, 0] == symbol && grid[row, 1] == "" && grid[row, 2] == symbol)
                return (row, 1);
            if (grid[row, 0] == "" && grid[row, 1] == symbol && grid[row, 2] == symbol)
                return (row, 0);
        }

        // Vérifie les colonnes
        for (int col = 0; col < 3; col++)
        {
            if (grid[0,
                     col] == symbol && grid[1, col] == symbol && grid[2, col] == "")
                return (2, col);
            if (grid[0, col] == symbol && grid[1, col] == "" && grid[2, col] == symbol)
                return (1, col);
            if (grid[0, col] == "" && grid[1, col] == symbol && grid[2, col] == symbol)
                return (0, col);
        }

        // Vérifie les diagonales
        if (grid[0, 0] == symbol && grid[1, 1] == symbol && grid[2, 2] == "")
            return (2, 2);
        if (grid[0, 0] == symbol && grid[1, 1] == "" && grid[2, 2] == symbol)
            return (1, 1);
        if (grid[0, 0] == "" && grid[1, 1] == symbol && grid[2, 2] == symbol)
            return (0, 0);

        if (grid[0, 2] == symbol && grid[1, 1] == symbol && grid[2, 0] == "")
            return (2, 0);
        if (grid[0, 2] == symbol && grid[1, 1] == "" && grid[2, 0] == symbol)
            return (1, 1);
        if (grid[0, 2] == "" && grid[1, 1] == symbol && grid[2, 0] == symbol)
            return (0, 2);

        return null; // Aucun mouvement gagnant trouvé
    }

    // Méthode pour vérifier s'il y a un mouvement à bloquer
    private (int, int)? CheckForBlockingMove(string opponentSymbol)
    {
        // Vérifie les lignes
        for (int row = 0; row < 3; row++)
        {
            if (grid[row, 0] == opponentSymbol && grid[row, 1] == opponentSymbol && grid[row, 2] == "")
                return (row, 2);
            if (grid[row, 0] == opponentSymbol && grid[row, 1] == "" && grid[row, 2] == opponentSymbol)
                return (row, 1);
            if (grid[row, 0] == "" && grid[row, 1] == opponentSymbol && grid[row, 2] == opponentSymbol)
                return (row, 0);
        }

        // Vérifie les colonnes
        for (int col = 0; col < 3; col++)
        {
            if (grid[0, col] == opponentSymbol && grid[1, col] == opponentSymbol && grid[2, col] == "")
                return (2, col);
            if (grid[0, col] == opponentSymbol && grid[1, col] == "" && grid[2, col] == opponentSymbol)
                return (1, col);
            if (grid[0, col] == "" && grid[1, col] == opponentSymbol && grid[2, col] == opponentSymbol)
                return (0, col);
        }

        // Vérifie les diagonales
        if (grid[0, 0] == opponentSymbol && grid[1, 1] == opponentSymbol && grid[2, 2] == "")
            return (2, 2);
        if (grid[0, 0] == opponentSymbol && grid[1, 1] == "" && grid[2, 2] == opponentSymbol)
            return (1, 1);
        if (grid[0, 0] == "" && grid[1, 1] == opponentSymbol && grid[2, 2] == opponentSymbol)
            return (0, 0);

        if (grid[0, 2] == opponentSymbol && grid[1, 1] == opponentSymbol && grid[2, 0] == "")
            return (2, 0);
        if (grid[0, 2] == opponentSymbol && grid[1, 1] == "" && grid[2, 0] == opponentSymbol)
            return (1, 1);
        if (grid[0, 2] == "" && grid[1, 1] == opponentSymbol && grid[2, 0] == opponentSymbol)
            return (0, 2);

        return null; // Aucun mouvement à bloquer trouvé
    }


    // Méthode pour choisir un mouvement aléatoire parmi les cases vides
    private (int, int) ChooseRandomMove()
    {
        // Liste des coordonnées des cases vides
        var emptyCells = new List<(int, int)>();

        // Parcours de la grille pour trouver les cases vides
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (grid[row, col] == "")
                {
                    emptyCells.Add((row, col));
                }
            }
        }

        // Choix aléatoire d'une case vide
        int randomIndex = UnityEngine.Random.Range(0, emptyCells.Count);
        return emptyCells[randomIndex];
    }
}
