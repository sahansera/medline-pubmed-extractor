namespace MedlineExtractor.Models
{
    public class Output
    {
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Mesh { get; set; }
        public string Country { get; set; }
        public string Authors { get; set; }
        public string JournalName { get; set; }
        public string Year { get; set; }
        public string PubMedId { get; set; }

        public override string ToString()
        {
            return $"{PubMedId}\t{Title}\t{Abstract}\t{Country}\t{JournalName}\t{Year}\t{Mesh}\t{Authors}";
        }
    }
}