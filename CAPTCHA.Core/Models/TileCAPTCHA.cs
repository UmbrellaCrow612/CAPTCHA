namespace CAPTCHA.Core.Models
{
    public class TileCAPTCHA
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// The target answer they need to achieve. This is done by generating an image with <see cref="Services.ImgService"/>.
        /// </summary>
        public byte[] AnswerImageBytes { get; set; } = [];

        /// <summary>
        /// The base tiles sent across.
        /// </summary>
        /// <remarks>
        /// This is a 2D list representing the grid of tiles. By default, all values are set to 0, indicating unselected tiles.
        /// The front end updates the indices to 1 to represent selected tiles.
        /// To verify the answer, we simply compare the provided matrix against this one.
        /// </remarks>
        public List<List<int>> Matrix { get; set; } =
        [
        ];

        /// <summary>
        /// Checks if the given matrix matches the expected Matrix.
        /// </summary>
        /// <param name="matrix">The user's selected tile matrix.</param>
        /// <returns>True if the matrices are identical; otherwise, false.</returns>
        public bool IsAnswerCorrect(List<List<int>> matrix)
        {
            if (matrix.Count != Matrix.Count || matrix.Any(row => row.Count != Matrix[0].Count))
            {
                return false;
            }

            for (int i = 0; i < Matrix.Count; i++)
            {
                for (int j = 0; j < Matrix[i].Count; j++)
                {
                    if (matrix[i][j] != Matrix[i][j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
