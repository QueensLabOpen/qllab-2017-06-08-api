namespace QLLAB.Models
{
    public class Answer
    {
        public string Url { get; set; }
        public string[] Tags { get; set; }

        public Answer(string url, string[] tags) 
        {
            Url = url;
            Tags = tags;
        }
    }
}
