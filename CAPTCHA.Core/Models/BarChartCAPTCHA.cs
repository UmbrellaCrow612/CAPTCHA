namespace CAPTCHA.Core.Models
{
    /// <summary>
    /// Data sent to render a chart js bar chart small and raounded version and model saved in DB
    /// </summary>
    public class BarChartCAPTCHA
    {
        /// <summary>
        /// ID of this captcha
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// When the random data is generated to be sent to UI to render it stores the lowest point
        /// </summary>
        public required double LowestPointAnswer { get; set; }

        /// <summary>
        /// When the random data is generated to be sent to UI to render it stores the highest point
        /// </summary>
        public required double HighestPointAnswer { get; set; }

        /// <summary>
        /// See if the answers picked by the user from the graph for the <see cref="LowestPointAnswer"/> and <see cref="HighestPointAnswer"/> for this captcha
        /// </summary>
        /// <param name="high">The high value they picked graph</param>
        /// <param name="low">The low value they picked from the graph</param>
        /// <returns>True or false if they are correct</returns>
        public bool CheckAnswer(double high, double low)
        {
            return LowestPointAnswer == low && HighestPointAnswer == high;
        }


        /// <summary>
        /// Temp Data sent to the UI to be rendered with chart js bar chart 
        /// </summary>
        private BarChartData Data { get; set; } = new();
        public BarChartData GetData()
        {
            return Data;
        }
        public void SetData(BarChartData data)
        {
            Data = data;
        }
    }

    /// <summary>
    /// Not saved in Db just sent to the client to use to render in the shape of char js bar chart what they need
    /// </summary>
    public class BarChartData
    {
        /// <summary>
        /// Labels used in the x axis 
        /// </summary>
        public string[] Labels { get; set; } = [];

        /// <summary>
        /// The data values
        /// </summary>
        public BarChartDataSet[] BarChartDataSets { get; set; } = [];
    }

    /// <summary>
    /// Not saved but sent to client as part of <see cref="BarChartData"/>
    /// </summary>
    public class BarChartDataSet
    {
        public required string Label { get; set; }
        public List<int> Data { get; set; } = [];
        public required string BorderColor { get; set; }
        public required string BackgroundColor { get; set; }
        public int BorderWidth { get; set; }
        public int BorderRadius { get; set; }
        public bool BorderSkipped { get; set; }
    }
}
