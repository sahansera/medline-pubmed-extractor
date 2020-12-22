using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using MedlineExtractor.Models;

namespace MedlineExtractor
{
    class Program
    {
        // Input and Output folder paths
        private static string _inputFolder = "";
        private static string _outputFolder = "";
        
        // Start and End of the file sequence
        private const int Start = 44;
        private const int End = 44;
        
        // File name prefix
        private const string Prefix = "pubmed21n";

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                _inputFolder = args[0] ?? "";
                _outputFolder = args[1] ?? "";
            }
            
            for (var i = Start; i <= End; i++)
            {
                var filename = i.ToString("0000");
                Process($"{Prefix}{filename}");
            }
        }

        private static void Process(string fileName)
        {
            var fullPath = $"{_inputFolder}{fileName}.xml";
            var doc = XDocument.Load(fullPath);
            var articleElements = doc.Descendants(nameof(PubmedArticle));

            var articles = articleElements
                .Select(e =>
                {
                    var medlineCitation = e.Element(nameof(MedlineCitation));
                    Console.WriteLine($"Processing {medlineCitation.Element(nameof(MedlineCitation.PMID))?.Value} in {fileName}");
                    return new PubmedArticle
                    {
                        MedlineCitation = new MedlineCitation
                        {
                            PMID = medlineCitation.Element(nameof(MedlineCitation.PMID))?.Value,
                            MedlineJournalInfo = new MedlineJournalInfo
                            {
                                Country = medlineCitation.Element(nameof(MedlineJournalInfo))?.Element(nameof(MedlineJournalInfo.Country))?.Value
                            },
                            Article = new Article
                            {
                                ArticleTitle = medlineCitation.Element(nameof(Article))?.Element(nameof(Article.ArticleTitle))?.Value,
                                Abstract = new Abstract
                                {
                                    AbstractText = medlineCitation.Element(nameof(Article))?.Element(nameof(Abstract))?.Element(nameof(Abstract.AbstractText))?.Value
                                },
                                Journal = new Journal
                                {
                                    Title = medlineCitation.Element(nameof(Article))?.Element(nameof(Journal))?.Element(nameof(Journal.Title))?.Value,
                                    JournalIssue = new JournalIssue
                                    {
                                        PubDate = new PubDate
                                        {
                                            Year = medlineCitation.Element(nameof(Article))?.Element(nameof(Journal))?.Element(nameof(Journal.JournalIssue))?.Element(nameof(Journal.JournalIssue.PubDate))?.Element(nameof(Journal.JournalIssue.PubDate.Year))?.Value
                                        }
                                    },
                                },
                                AuthorList = medlineCitation.Element(nameof(Article))?.Element(nameof(Article.AuthorList))?.Descendants(nameof(Author)).Select(author => new Author
                                {
                                    ForeName = author.Element(nameof(Author.ForeName))?.Value,
                                    LastName = author.Element(nameof(Author.LastName))?.Value
                                }).ToList()
                            },
                            MeshHeadingList = medlineCitation.Element(nameof(MedlineCitation.MeshHeadingList))?.Descendants(nameof(MeshHeading)).Select(mesh => new MeshHeading
                            {
                                DescriptorName = mesh.Element(nameof(MeshHeading.DescriptorName))?.Value
                            }).ToList()
                        }
                    };
                });
            var reps = articles.ToList();
            var output = reps.Select(article => new Output
            {
                PubMedId = article.MedlineCitation.PMID,
                Abstract = article.MedlineCitation.Article.Abstract?.AbstractText,
                Title = article.MedlineCitation.Article.ArticleTitle,
                Authors = string.Join(";", (article.MedlineCitation.Article.AuthorList ?? new List<Author>()).Select(x => x.ToString())),
                Country = article.MedlineCitation.MedlineJournalInfo.Country,
                Mesh = string.Join(";", (article.MedlineCitation.MeshHeadingList ?? new List<MeshHeading>()).Select(x => x.DescriptorName)),
                Year = article.MedlineCitation.Article.Journal.JournalIssue.PubDate.Year,
                JournalName = article.MedlineCitation.Article.Journal.Title
            }).ToList();
            
            Write(output, fileName);
            Console.WriteLine($"Completed {fileName}");
        }

        private static void Write(IEnumerable<Output> output, string fileName)
        {
            var headings = new[]{"Id", "Title", "Abstract", "Country", "JournalName", "Year", "Mesh", "Authors"};
            
            using (var outputFile = new StreamWriter(Path.Combine(_outputFolder, $"{fileName}.tsv")))
            {
                outputFile.WriteLine(string.Join("\t", headings));
                foreach (var line in output)
                    outputFile.WriteLine(line);
            }
        }
    }
}
