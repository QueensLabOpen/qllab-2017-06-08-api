namespace QLLAB.Models
{
    public class Answer
    {
        public string Url { get; set; }
        public bool IsWinner { get; set; }

        public Answer(string url, bool isWinner) 
        {
            Url = url;
            IsWinner = isWinner;
        }
    }
}
