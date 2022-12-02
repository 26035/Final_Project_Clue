using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FinalProject
{
    static class Register
    {
        /// <summary>
        /// Used to save the current board
        /// </summary>
        /// <param name="fileName"> string that represents the name of the file</param>
        /// <param name="board">represents the gameboard</param>
        public static void SaveBoard(string fileName, GameBoard board)
        {
            string saveBoard = "";
            for (int i = 0; i < 24; i++)
            {
                for (int j = 0; j < 24; j++)
                {
                    saveBoard = saveBoard + board.Board[i, j].Path + ";";
                }
                saveBoard = saveBoard + "\n";
            }
            File.WriteAllText(FilePath(fileName), saveBoard);
        }
        /// <summary>
        /// Used to save the data of one player
        /// </summary>
        /// <param name="fileName"> string that represents the name of the file</param>
        /// <param name="player">represents a player</param>
        public static void SavePlayer(string fileName, Player player)
        {
            string position = player.Pos.Row + ";" + player.Pos.Column;
            string handtrail = "";
            for (int i = 0; i < player.Handtrail.Count; i++)
            {
                handtrail = handtrail + player.Handtrail[i].ID;
                if(player.Handtrail.Count-1!=i)
                {
                    handtrail += ";";
                }
            }
            string stillSuspected = "";
            for (int i = 0; i < player.StillSuspected.Count; i++)
            {
                stillSuspected = stillSuspected + player.StillSuspected[i].ID;
                if(player.StillSuspected.Count-1!=i)
                {
                    stillSuspected += ";";
                }
            }
            string allHypothesis = "";
            for (int i = 0; i < player.AllHypothesis.Count; i++)
            {
                for (int j = 0; j < player.AllHypothesis[i].Count; j++)
                {
                    allHypothesis = allHypothesis + player.AllHypothesis[i][j].ID;
                    if(player.AllHypothesis[i].Count-1!=j)
                    {
                        allHypothesis += ";";
                    }
                }
                allHypothesis += "\n";
            }
            string save = player.Name + "\n" + Convert.ToString(player.Id) + "\n" + position + "\n" + player.NumberOfCards + "\n" + handtrail + "\n" + stillSuspected + "\n" + allHypothesis + player.Accusation.ToString();
            File.WriteAllText(FilePath(fileName), save);
        }
        /// <summary>
        /// Used to save the round, the list of player and the order of players 
        /// </summary>
        /// <param name="fileName">string that represents the name of a file</param>
        /// <param name="round">integer that represents the number of turns</param>
        /// <param name="players">represents all the players at the beginning of the game</param>
        /// <param name="runningOrder">represents the current players in the order in which they play</param>
        public static void SaveRound(string fileName, int round, List<Player> players, List<Player> runningOrder)
        {
            string playersId = "";
            for (int i = 0; i < players.Count; i++)
            {
                playersId = playersId + players[i].Id;
                if(players.Count-1!=i)
                {
                    playersId += ";";
                }
            }

            string currentPlayersId = "";
            for (int i = 0; i < runningOrder.Count; i++)
            {
                currentPlayersId = currentPlayersId + runningOrder[i].Id;
                if(runningOrder.Count-1!=i)
                {
                    currentPlayersId += ";";
                }
            }
            string save = round + "\n" + playersId + "\n" + currentPlayersId;
            File.WriteAllText(FilePath(fileName), save);
        }
        /// <summary>
        /// Used to save the three card of the murder
        /// </summary>
        /// <param name="fileName">string that represents the name of the file</param>
        public static void SaveMurderCards(string fileName)
        {
            
            string save =  CardsManager.cardsRooms.CardMurderer.ID + ";" + CardsManager.cardsSuspects.CardMurderer.ID  + ";" + CardsManager.cardsWeapons.CardMurderer.ID;
            File.WriteAllText(FilePath(fileName), save);
        }
        /// <summary>
        /// Used to create a csv file path
        /// </summary>
        /// <param name="fileName">string that represents the name of the file</param>
        /// <returns>string that represents the csv file path</returns>
        public static string FilePath(string fileName)
        {
            string path = Directory.GetCurrentDirectory();
            string fullPath = Path.Combine(path, fileName + ".csv");
            return fullPath;
        }
        /// <summary>
        /// Used to delete files at the end of a game
        /// </summary>
        /// <param name="fileName">string that represents the name of the file</param>
        public static void DeleteFile(string fileName)
        {
            if (File.Exists(FilePath(fileName)))
            {
                File.Delete(FilePath(fileName));
            }
        }

    }
}
