namespace CAPTCHA.Core.Models
{
    public class TileCAPTCHA
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// When we make the matrix well make it into a string to store it like [[1,2,3,4],[1,2,3,4],[1,2,3,4]]as a string
        /// </summary>
        public required string AnswerMatrixAsPlainText { get; set; }


        /// <summary>
        /// The base tiles sent across.
        /// </summary>
        /// <remarks>
        /// This is a 2D list representing the grid of tiles. By default, all values are set to 0, indicating unselected tiles.
        /// The front end updates the indices to 1 to represent selected tiles.
        /// To verify the answer, we simply compare the provided matrix against this one.
        /// </remarks>
        private List<List<int>> Matrix { get; set; } = [];
        public void SetMatrix(List<List<int>> matrix)
        {
            Matrix = matrix;
        }
        public List<List<int>> GetMatrix()
        {
            return Matrix;
        }

        /// <summary>
        /// The target answer they need to achieve. This is done by generating an image with <see cref="Services.ImgService"/>.
        /// </summary>
        private byte[] AnswerImageBytes = [];
        public void SetImageBytes(byte[] bytes)
        {
            AnswerImageBytes = bytes;
        }
        public byte[] GetImageBytes()
        {
            return AnswerImageBytes;
        }

        // Change to matcj how we store it make it a string and just do a string comp
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
