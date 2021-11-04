<img src="img/readme-cover.jpg" alt="medline pubmed banner" align="center" />

# Medline Pubmed Data Extractor
A simple console application that will process a given Medline Pubmed dataset (XML) and generate TSV files with given attributes.

## Introduction
This .NET 5 console application will iterate through a given set of Medline Pubmed dataset from a given starting point.

Please note that this is a naive implementation using LINQ to XML ibrary. I have tested this for current latest as of `22/12/2020` which contains `33M` records. You can expect it to finish processing all records within an hour.

The official Pubmed baseline dataset can be downloaded from [here](https://ftp.ncbi.nlm.nih.gov/pubmed/baseline/).

Currently, the application extracts the following attributes:

| Pubmed Attr.                      | Output Attr. | Example                                                                                                                                                        |
|-----------------------------------|--------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------|
| PMID                              | ID           | 1329680                                                                                                                                                        |
| ArticleTitle                      | Title        | Rabbit esophageal cell cytoplasmic pH regulation: role of Na+-H+ antiport and H+-dependent HCO3- transport systems.                                            |
| Abstract                          | Abstract     | N/A                                                                                                                                                            |
| MedlineJournalInfo.Country        | Country      | Belgium                                                                                                                                                        |
| Journal.Title                     | JournalName  | Annales de la Societe belge de medecine tropicale                                                                                                              |
| Journal.JournalIssue.PubDate.Year | Year         | 1992                                                                                                                                                           |
| MeshHeadingList                   | Mesh         | Animals;Attitude to Health;Community Participation;Cote d'Ivoire;Culture;Ethnic Groups;Female;Humans;Insect Control;Male;Trypanosomiasis, African;Tsetse Flies |
| Article.AuthorList                | Authors      | C Laveissière;H H Meda                                                                                                                                         |

## Usage

1. Clone this repo, 
2. Open in VS 2019 or Rider
3. Configure the input and output paths (either passing them as command line args or manually)
4. Hit F5

or if you are using the console,

```bash
dotnet build
dotnet run -p MedlineExtractor "path/to/input/folder" "path/to/out/folder"
```

## Running the sample

Change the following parameters in Program.cs. This sample XML is from 44th file of the sequence and contains an example with MathML tags.

```csharp
...
private const int Start = 44;
private const int End = 44;
...
```

You could also specify where to write the output TSV file. By default it will write to the `Debug` folder.

## Improvements
Many improvements can be done as follows.

 - [x] Make the file paths configurable
 - [ ] Performance improvements
 - [ ] Documentation
 - [ ] Gracefully handle errors/nulls
 - [x] Break the LINQ query to smaller chunks
 - [ ] Add unit tests
 - [ ] Support multiple output formats (eg. JSON, CSV etc.)

## Related Blog Post
TBD

## Limitations

 - This application does not support MathML tags embedded in ArticleTitle. We simply parse it as plain text for now.

## Questions? Bugs? Suggestions for Improvement?
Having any issues or troubles getting started? [Get in touch with me](https://sahansera.dev/contact/) 

## Support
Has this Project helped you learn something new? or helped you at work? Do Consider Supporting.

<a href="https://www.buymeacoffee.com/sahan" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-orange.png" alt="Buy Me A Coffee" width="200"  ></a>

## Share it!
Please share this Repository within your developer community, if you think that this would make a difference! Cheers.

## About the Author
### Sahan Serasinghe
- Blogs at [sahansera.dev](https://sahansera.dev/)
- Twitter - [_SahanSera](https://www.twitter.com/_SahanSera)
- Linkedin - [Sahan Serasinghe](https://www.linkedin.com/in/sahanserasinghe/)
