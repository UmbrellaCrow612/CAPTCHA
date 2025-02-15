namespace CAPTCHA.Core.Models
{
    public class TileCAPTCHA
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// When we make the matrix well make it into a string to store it like [[1,2,3,4],[1,2,3,4],[1,2,3,4]]as a string
        /// </summary>
        public required string AnswerMatrixAsJson { get; set; }

        /// <summary>
        /// If the current captcha has ben used already to stop others from using it again
        /// </summary>
        public bool IsUsed { get; set; } = false;


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

        public bool IsAnswerCorrect(string jsonMatrix)
        {
            return string.Equals(AnswerMatrixAsJson, jsonMatrix);
        }
    }
}
