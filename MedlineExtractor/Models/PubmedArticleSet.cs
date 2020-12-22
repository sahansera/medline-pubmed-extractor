using System.Collections.Generic;
using System.Xml.Serialization;

namespace MedlineExtractor.Models
{
    [XmlRoot("PubmedArticleSet")]
    public class PubmedArticleSet : List<PubmedArticle> {}

    public class PubmedArticle
    {
        public MedlineCitation MedlineCitation { get; set; }
    }

    public class MedlineCitation
    {
        public string PMID { get; set; }
        
        public Article Article { get; set; }
        
        public MedlineJournalInfo MedlineJournalInfo { get; set; }

        public List<MeshHeading> MeshHeadingList { get; set; }
    }

    public class MeshHeading
    {
        public string DescriptorName { get; set; }
    }

    public class MedlineJournalInfo
    {
        public string Country { get; set; }

        public override string ToString()
        {
            return Country ?? "";
        }
    }

    public class Journal
    {
        public string Title { get; set; }

        public JournalIssue JournalIssue { get; set; }
    }

    public class JournalIssue
    {
        public PubDate PubDate { get; set; }
    }

    public class PubDate
    {
        public string Year { get; set; }
    }

    public class Article
    {
        public string ArticleTitle { get; set; }
        
        public Journal Journal { get; set; }

        public List<Author> AuthorList { get; set; }

        public Abstract Abstract { get; set; }
    }

    public class Abstract
    {
        public string AbstractText { get; set; }

        public override string ToString()
        {
            return AbstractText ?? "";
        }
    }

    public class Author
    {
        public string LastName { get; set; }
        public string ForeName { get; set; }

        public override string ToString()
        {
            return $"{ForeName} {LastName}";
        }
    }
}