# Medline Pubmed Extractor
A simple console application that will process a given Medline Pubmed dataset and generate TSV files with given columns.

## Introduction
This .NET 5 console application will iterate through a given set of Medline Pubmed dataset from a given starting point.

Please note that this is a naive implementation using LINQ to XML ibrary. I have tested this for current latest as of `22/12/2020` which contains `33M` records.

The official Pubmed baseline dataset can be downloaded from [here](https://ftp.ncbi.nlm.nih.gov/pubmed/baseline/).

## Usage

1. Clone this repo, 
2. Open in VS 2019 or Rider
3. Configure the input and output paths
4. Hit F5.

or if you are using the console,

```bash
dotnet build
dotnet MedlineExtractor.dll "path/to/input/folder" "path/to/out/folder"
```

## Improvements
Many improvements can be done as follows.

 - [x] Make the file paths configurable
 - [ ] Performance improvements
 - [ ] Documentation
 - [ ] Gracefully handle errors/nulls
 - [ ] Break the LINQ query to smaller chunks

## Related Blog Post
TBD

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
- Twitter - [sahan91](https://www.twitter.com/sahan91)
- Linkedin - [Sahan Serasinghe](https://www.linkedin.com/in/sahanserasinghe/)
