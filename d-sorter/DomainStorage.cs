using System;
using System.IO.Enumeration;

internal class DomainStorage
{
    private string pathToSortFolder;

    public DomainStorage(string pathToSortFolder)
    {
        this.pathToSortFolder = pathToSortFolder;

        if (Directory.Exists(pathToSortFolder) is false)
        {
            Directory.CreateDirectory(pathToSortFolder);
        }
    }
    Dictionary<string, List<string>> storage = new Dictionary<string, List<string>>();
    internal void Add(string line)
    {
        string domainName = string.Empty;
        string topDomain = "unknown";
        if (line.Contains("."))
        {
            int lastDot = line.LastIndexOf('.');
            domainName = line.Substring(0, lastDot);
            topDomain = line.Substring(lastDot + 1, line.Length - lastDot - 1);
        }

        if (storage.ContainsKey(topDomain) is false)
        {
            storage.Add(topDomain, new List<string> { line });
        }
        else
        {
            storage[topDomain].Add(line);
        }

        if (storage[topDomain].Count > 10000000)
        {
            string fileName = pathToSortFolder + topDomain + ".txt";

            SaveToDisk(fileName, storage[topDomain]);
        }

     
    }
    void SaveToDisk(string fileName, List<string> domains)
    {
        File.AppendAllLines(fileName, domains);
        domains.Clear();
    }
    public void WtiteBufferToDisk()
    {
        foreach (var domainList in storage)
        {
            if (domainList.Value.Count > 0)
            {
                string fileName = pathToSortFolder + domainList.Key + ".txt";

                SaveToDisk(fileName, domainList.Value);
            }
        }
    }
}