using CAPTCHA.Core.Models;
using CAPTCHA.Core.Options;
using System.Text.Json;

namespace CAPTCHA.Core.Services
{
    public class RocketCAPTCHAService
    {
        public RocketCAPTCHAOptions Options { get; set; } = new();

        public RocketCAPTCHAService() { }

        public RocketCAPTCHAService(RocketCAPTCHAOptions options)
        {
            Options = options;
        }


        public RocketCAPTCHAServiceResult GenerateQuestion()
        {
            var result = new RocketCAPTCHAServiceResult();

            List<List<int>> matrix = [];

            // create base matrix
            for (int i = 0; i < Options.MatrixRows; i++)
            {
                matrix.Add([]);
                for (int j = 0; j < Options.MatrixColumns; j++)
                {
                    matrix[i].Add((int)RocketBoardItems.EmptySpace);
                }
            }

            var random = new Random();

            // add starting pos of rocket
            var rocketColIndex = random.Next(0, Options.MatrixColumns);
            var rocketRowIndex = random.Next(0, Options.MatrixRows);
            matrix[rocketRowIndex][rocketColIndex] = (int)RocketBoardItems.RocketPosition;


            // place goal 
            while (true)
            {
                var ITT = 0;
                var goalColIndex = random.Next(0, Options.MatrixColumns);
                var goalRowIndex = random.Next(0, Options.MatrixRows);

                ITT += 1;
                if (ITT >= 10) break;

                if (matrix[goalRowIndex][goalColIndex] == (int)RocketBoardItems.EmptySpace)
                {
                    matrix[goalRowIndex][goalColIndex] = (int)RocketBoardItems.TargetGoal;
                    break;
                }
            }

            // Add Meteors
            for (int i = 0; i < Options.NumberOfMeteorsToPlace; i++)
            {
                int ITT = 0;
                while (true)
                {
                    ITT++;
                    if (ITT >= 10) break;
                    var meteorColIndex = random.Next(0, Options.MatrixColumns);
                    var meteorRowIndex = random.Next(0, Options.MatrixRows);

                    if (matrix[meteorRowIndex][meteorColIndex] == (int)RocketBoardItems.EmptySpace)
                    {
                        matrix[meteorRowIndex][meteorColIndex] = (int)RocketBoardItems.Meteor;
                        break;
                    }
                }
            }

            result.CAPTCHA.SetMatrix(matrix);
            result.CAPTCHA.SetImageBytes([.. ImgService.GenerateImg(result.CAPTCHA, Options)]);
            result.CAPTCHA.MatrixAsJSON = JsonSerializer.Serialize(matrix);

            return result;
        }

        public static bool CanMovesReachGoal(List<int> moves, List<List<int>> matrix, int rocketColIndex, int rocketRowIndex)
        {
            var colIndex = rocketColIndex;
            var rowIndex = rocketRowIndex;

            for (int i = 0; i < moves.Count; i++)
            {
                switch (moves[i])
                {
                    case (int)RocketMoves.Up:
                        if (rowIndex - 1 < 0) // Check if moving up is out of bounds
                        {
                            return false;
                        }
                        rowIndex--; // Move up
                        break;

                    case (int)RocketMoves.Right:
                        if (colIndex + 1 >= matrix[rowIndex].Count) // Check if moving right is out of bounds
                        {
                            return false;
                        }
                        colIndex++; // Move right
                        break;

                    case (int)RocketMoves.Down:
                        if (rowIndex + 1 >= matrix.Count) // Check if moving down is out of bounds
                        {
                            return false;
                        }
                        rowIndex++; // Move down
                        break;

                    case (int)RocketMoves.Left:
                        if (colIndex - 1 < 0) // Check if moving left is out of bounds
                        {
                            return false;
                        }
                        colIndex--; // Move left
                        break;

                    default:
                        // Handle unexpected move values
                        return false;
                }

                // Check the new position after the move
                var itemAtNewIndex = matrix[rowIndex][colIndex];
                if (itemAtNewIndex == (int)RocketBoardItems.Meteor)
                {
                    return false; // Hit a meteor
                }
                if (itemAtNewIndex == (int)RocketBoardItems.TargetGoal)
                {
                    return true; // Reached the goal
                }
            }

            // If all moves are processed and the goal is not reached
            return false;
        }

        public static (int row, int col) FindRocketPosition(List<List<int>> matrix)
        {
            for (int i = 0; i < matrix.Count; i++) // Iterate over rows
            {
                for (int j = 0; j < matrix[i].Count; j++) // Iterate over columns
                {
                    if (matrix[i][j] == (int)RocketBoardItems.RocketPosition)
                    {
                        return (i, j); // Return row and column index immediately
                    }
                }
            }

            return (-1, -1); // Return (-1, -1) if rocket is not found
        }
    }

    public class RocketCAPTCHAServiceResult
    {
        public RocketCAPTCHA CAPTCHA { get; set; } = new();
    }
}
