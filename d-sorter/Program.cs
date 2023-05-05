using System;
using System.Collections;
using System.Linq;
using d_sorter;
using Microsoft.Extensions.DependencyInjection;

var filePath = "E:\\domains.txt";
var sortedDirectoryPath = "E:\\sorted\\";

//SortDomainsFromFileToFolder(filePath, sortedDirectoryPath);

DomainGenerator.GenerageAvailibleDomains(generatedDomainFilePath: "E:\\shortRuDomain.txt", 
    registredDomainsFilePath: "E:\\sorted\\ru.txt", 
    topDomainName: "ru",
    maxNameLenght: 3);







static void SortDomainsFromFileToFolder(string filePath, string sortedDirectoryPath)
{
    var storage = new DomainStorage(sortedDirectoryPath);
    int lineNum =0;
    using (var stream = File.Open(filePath, FileMode.Open))
    {


        var streamReader = new StreamReader(stream);
        string line = string.Empty;

        while ((line = streamReader.ReadLine()) != null)
        {
            if (lineNum % 100000 == 0)
            {
                Console.WriteLine(lineNum);
            }
            lineNum++;

            storage.Add(line);
        }

        storage.WtiteBufferToDisk();
    }
    Console.WriteLine("Sorting finished");
    Console.ReadLine();
}

