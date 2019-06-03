using Microsoft.Azure.Documents;

using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDBSecondaryIndex
{
    class Program
    {
        
        static void Main(string[] args)
        {
            DocumentClient client = new DocumentClient(
                new Uri("https://<NAME OF COSMOSDB ACCOUNT>.documents.azure.com:443/"), 
                "<KEY>");

            var t = client.ReadDocumentCollectionAsync(
                UriFactory.CreateDocumentCollectionUri(
                    "<NAME OF DATABASE","<NAME OF COLLECTION>"));
            t.Wait();
            var collection = (DocumentCollection)t.Result;
            // Add composite index to the policy
            Collection <CompositePath> compositePaths = new Collection<CompositePath>();
            //if you have query select * from c where c.Field1="x" and c.Field2="y" then
            //specify Path="Field1" in the first line & Path="Field2" in the second field
            //This is irrespective of Field1 or Field2 being a partition key

            compositePaths.Add(new CompositePath() { Path = "/<NAME OF Field>", Order = CompositePathSortOrder.Ascending });
            compositePaths.Add(new CompositePath() { Path = "/<NAME OF Field>", Order = CompositePathSortOrder.Ascending });
            collection.IndexingPolicy.CompositeIndexes.Add(compositePaths);
            

        }
    }
}
