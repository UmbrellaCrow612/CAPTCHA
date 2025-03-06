using CAPTCHA.Core.Models;
using CAPTCHA.Core.Options;
using System.Text.Json;

namespace CAPTCHA.Core.Services
{
    public class TileCAPTCHAService
    {
        public TileCAPTCHAOptions DefaultOptions { get; private set; } = new();

        public TileCAPTCHAService() { }

        public TileCAPTCHAService(TileCAPTCHAOptions options)
        {
            DefaultOptions = options;
        }

        public TileCAPTCHAServiceResult GenerateQuestion()
        {
            var result = new TileCAPTCHAServiceResult();
            var baseMatrix = new List<List<int>>();

            // Create the matrix with all zeros
            for (int i = 0; i < DefaultOptions.MatrixColumns; i++)
            {
                baseMatrix.Add([]);
                for (int j = 0; j < DefaultOptions.MatrixRows; j++)
                {
                    baseMatrix[i].Add(0);
                }
            }

            var matrixToSendToClient = baseMatrix.Select(row => new List<int>(row)).ToList();
            result.Matrix = matrixToSendToClient;

            // Add colored tiles
            var random = new Random();
            for (int i = 0; i < DefaultOptions.NumberOfAnswerTiles; i++)
            {
                var colIndex = random.Next(0, DefaultOptions.MatrixColumns);
                var rowIndex = random.Next(0, DefaultOptions.MatrixRows);
                baseMatrix[colIndex][rowIndex] = 1;
            }

            result.CAPTCHA.SetMatrix(baseMatrix);
            result.CAPTCHA.SetImageBytes([.. ImgService.GenerateImg(result.CAPTCHA, DefaultOptions)]);
            result.CAPTCHA.AnswerMatrixAsJson = JsonSerializer.Serialize(baseMatrix);

            result.Succeeded = true;
            return result;
        }
    }

    public class TileCAPTCHAServiceResult
    {
        public bool Succeeded { get; set; } = false;
        public TileCAPTCHA CAPTCHA { get; set; } = new() { AnswerMatrixAsJson = "DEFAULT" };
        public List<List<int>> Matrix { get; set; } = [];
    }
}
