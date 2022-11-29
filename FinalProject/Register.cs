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

        public static void SavePlayer(string fileName, Player player)
        {
            string position = player.Pos.Row + ";" + player.Pos.Column;
            string handtrail = "";
            for (int i = 0; i < player.Handtrail.Count; i++)
            {
                handtrail = handtrail + player.Handtrail[i].ID + ";";
            }
            string stillSuspected = "";
            for (int i = 0; i < player.StillSuspected.Count; i++)
            {
                stillSuspected = stillSuspected + player.StillSuspected[i].ID + ";";
            }
            string allHypothesis = "";
            for (int i = 0; i < player.AllHypothesis.Count; i++)
            {
                for (int j = 0; j < player.AllHypothesis[i].Count; j++)
                {
                    allHypothesis = allHypothesis + player.AllHypothesis[i][j].ID + ";";
                }
                allHypothesis = "\n";
            }
            string save = player.Name + ";\n" + player.Id.ToString() + ";\n" + position + ";\n" + player.NumberOfCards + ";\n" + handtrail + "\n" + stillSuspected + "\n" + allHypothesis + player.Accusation.ToString() + ";";
            File.WriteAllText(FilePath(fileName), save);
        }
        public static void SaveRound(string fileName, int round, List<Player> players, List<Player> runningOrder)
        {
            string playersId = "";
            for (int i = 0; i < players.Count; i++)
            {
                playersId = playersId + players[i].Id + ";";
            }
            string currentPlayersId = "";
            for (int i = 0; i < players.Count; i++)
            {
                currentPlayersId = currentPlayersId + players[i].Id + ";";
            }
            string save = round + ";\n" + playersId + "\n" + runningOrder;
            File.WriteAllText(FilePath(fileName), save);
        }
        //Register.CreateFileCsv("Joueur"+Convert.ToString(1));
        public static string FilePath(string fileName)
        {
            string path = Directory.GetCurrentDirectory();
            string fullPath = Path.Combine(path, fileName + ".csv");
            return fullPath;
        }

        public static void DeleteFile(string fileName)
        {
            if (File.Exists(FilePath(fileName)))
            {
                File.Delete(FilePath(fileName));
            }
        }

    }
}
