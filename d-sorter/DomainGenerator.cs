using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d_sorter
{
    public static class DomainGenerator
    {
        static char[] allowedSymbols = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public static void GenerageAvailibleDomains(string generatedDomainFilePath, string registredDomainsFilePath, string topDomainName, int maxNameLenght)
        {
            var domainList = File.ReadAllText(registredDomainsFilePath).Split("\r\n");
            var filtredList = new HashSet<string>();

            foreach (var fullDomainName in domainList)
            {

                if (fullDomainName.Count(x => x == '.') == 1)
                {
                    int lastDot = fullDomainName.LastIndexOf('.');
                    var shortName = fullDomainName.Substring(0, lastDot);
                    //var topDomainName = fullDomainName.Substring(lastDot + 1, fullDomainName.Length - lastDot - 1);

                    if (shortName.Length <= maxNameLenght)
                    {
                        filtredList.Add(fullDomainName);
                    }
                }
            }
            domainList = null;

            List<string> generatedDomainNames = GenerateDomainNames(symbols: allowedSymbols,
                excludedDomains: filtredList,
                topDomainName: topDomainName,
                maxDomainLenght: maxNameLenght,
                startsWith: "");
            File.WriteAllLines(generatedDomainFilePath, generatedDomainNames);
        }

        private static List<string> GenerateDomainNames(char[] symbols, HashSet<string> excludedDomains, string topDomainName, int maxDomainLenght, string startsWith)
        {
            List<string> acceptedDomains = new List<string>();

            foreach (var symbol in symbols)
            {
                var domainName = $"{startsWith}{symbol}";

                if (domainName.Length < maxDomainLenght)
                {
                    var domainsIncoming = GenerateDomainNames(symbols, excludedDomains, topDomainName, maxDomainLenght, domainName);
                    acceptedDomains.AddRange(domainsIncoming);
                }
                else
                {
                    var fqdn = $"{domainName}.{topDomainName}";
                    if (excludedDomains.Contains(fqdn) is false)
                        acceptedDomains.Add(fqdn);
                }
            }
            return acceptedDomains;
        }
    }
}
