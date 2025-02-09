using CAPTCHA.Core.Models;
using CAPTCHA.Core.Options;

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

            return result;
        }
    }

    public class RocketCAPTCHAServiceResult
    {
        public RocketCAPTCHA CAPTCHA { get; set; } = new();
    }
}
