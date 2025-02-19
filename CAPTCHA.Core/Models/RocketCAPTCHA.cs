namespace CAPTCHA.Core.Models
{
    public class RocketCAPTCHA
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// The matrix used to geenrate the final img and is the game state - read back to a 2d matrix and rocket position is found then moves from UI played
        /// agaisnt it
        /// </summary>
        public string MatrixAsJSON { get; set; } = "";

        public bool IsUsed { get; set; } = false;



        private readonly int MAX_MOVE_COUNT  = 10;

        private byte[] ImageBytes { get; set; } = [];

        private List<List<int>> Matrix { get; set; } = [];


        public void SetImageBytes(byte[] bytes)
        {
            ImageBytes = bytes;
        }

        public byte[] GetImageBytes()
        {
            return ImageBytes;
        }

        public List<List<int>> GetMatrix()
        {
            return Matrix;
        }

        public void SetMatrix(List<List<int>> matrix)
        {
            Matrix = matrix;
        }

        public int GeytMaxMoves()
        {
            return MAX_MOVE_COUNT;
        }
    }

    /// <summary>
    /// Represnets what moves a rocket can make
    /// </summary>
    public enum RocketMoves
    {
        Up = 0, 
        Right = 1,
        Down = 2,
        Left = 3,
    }

    /// <summary>
    /// What items are placed on the board
    /// </summary>
    public enum RocketBoardItems
    {
        EmptySpace = 0,
        RocketPosition = 1,
        Meteor = 2,
        TargetGoal = 3,
    }
}
