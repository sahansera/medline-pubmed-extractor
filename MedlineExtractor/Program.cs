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
        private const string InputFolder = "";
        private const string OutputFolder = "";
        private const int Start = 1;
        private const int End = 1062;

        static void Main(string[] args)
        {
            for (var i = Start; i <= End; i++)
            {
                var filename = i.ToString("0000");
                Process($"pubmed21n{filename}");
            }
        }

        private static void Process(string fileName)
        {
            var fullPath = $"{InputFolder}{fileName}.xml";
            var doc = XDocument.Load(fullPath);
            var articleElements = doc.Descendants("PubmedArticle");

            var articles = articleElements
                .Select(e =>
                {
                    var medlineCitation = e.Element("MedlineCitation");
                    Console.WriteLine($"Processing {medlineCitation.Element("PMID")?.Value} in {fileName}");
                    return new PubmedArticle
                    {
                        MedlineCitation = new MedlineCitation
                        {
                            PMID = medlineCitation.Element("PMID")?.Value,
                            MedlineJournalInfo = new MedlineJournalInfo
                            {
                                Country = medlineCitation.Element("MedlineJournalInfo")?.Element("Country")?.Value
                            },
                            Article = new Article
                            {
                                ArticleTitle = medlineCitation.Element("Article")?.Element("ArticleTitle")?.Value,
                                Abstract = new Abstract
                                {
                                    AbstractText = medlineCitation.Element("Article")?.Element("Abstract")?.Element("AbstractText")?.Value
                                },
                                Journal = new Journal
                                {
                                    Title = medlineCitation.Element("Article")?.Element("Journal")?.Element("Title")?.Value,
                                    JournalIssue = new JournalIssue
                                    {
                                        PubDate = new PubDate
                                        {
                                            Year = medlineCitation.Element("Article")?.Element("Journal")?.Element("JournalIssue")?.Element("PubDate")?.Element("Year")?.Value
                                        }
                                    },
                                },
                                AuthorList = medlineCitation.Element("Article")?.Element("AuthorList")?.Descendants("Author").Select(author => new Author
                                {
                                    ForeName = author.Element("ForeName")?.Value,
                                    LastName = author.Element("LastName")?.Value
                                }).ToList()
                            },
                            MeshHeadingList = medlineCitation.Element("MeshHeadingList")?.Descendants("MeshHeading").Select(mesh => new MeshHeading
                            {
                                DescriptorName = mesh.Element("DescriptorName")?.Value
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
            
            using (var outputFile = new StreamWriter(Path.Combine(OutputFolder, $"{fileName}.tsv")))
            {
                outputFile.WriteLine(string.Join("\t", headings));
                foreach (var line in output)
                    outputFile.WriteLine(line);
            }
        }
    }
}
