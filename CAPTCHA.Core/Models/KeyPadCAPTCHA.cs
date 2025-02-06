namespace CAPTCHA.Core.Models
{
    public class KeyPadCAPTCHA
    {
        /// <summary>
        /// Used as the joining factor between strings
        /// </summary>

        private readonly string _sep = "$$";

        /// <summary>
        /// The Separator used to join the answer ID's together
        /// </summary>
        public string GetSeparator()
        {
            return _sep;
        }

        /// <summary>
        /// Unique ID
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Each elements ID joined together in order of the sequense 
        /// </summary>
        /// <remarks>
        /// For example if the keypad they need to enter in order is shown as 123456, we would send over
        /// the game id and children buttons - these will have the id of the child and number to display 
        /// then ui side they click each child's store the id's they clicked in order 
        /// send it over to backend which will take them and compare it to the stored order by 
        /// un compressing the concated answer string
        /// </remarks>
        public required string AnswerInPlainText { get; set; }

        /// <summary>
        /// Check if the provided list of selected ID are correct to the answer
        /// </summary>
        public bool IsAnswerCorrect(ICollection<string> selectedIdsInOrder)
        {
            var answer = AnswerInPlainText.Split(_sep);

            return answer.SequenceEqual(selectedIdsInOrder);
        }

        public ICollection<KeyPadChild> Children { get; set; } = [];
    }

    /// <summary>
    /// A button in the UI to be clicked
    /// </summary>
    public class KeyPadChild
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string VisualText { get; set; }
    }
}
