using System.IO;
using System.Linq;

namespace PrimeDeals.Data.Persistence
{
   public class FileSystemPersistor : IPersistor
   {
      private string _storageLocation; //e.g. Storage subfolder, read-write access required

      public FileSystemPersistor(string storageLocation)
      {
         _storageLocation = storageLocation;
         Directory.CreateDirectory(_storageLocation); //make sure the storage location is valid, i.e. the subfolder exists
      }

      /// <summary>
      /// Save JSON string in a text file with .json extension.
      /// </summary>
      /// <param name="storageId">Name of the file (less extension) to create (overwrite if exists).</param>
      /// <param name="json">JSON string representing object to save.</param>
      public void PersistJson(string storageId, string json)
      {
         using (var streamWriter = File.CreateText(GetStorageFileName(storageId)))
         {
            streamWriter.Write(json);
            streamWriter.Flush();
         }
      }


      /// <summary>
      /// Retrieve JSON string from a text file with .json extension and located in Storage subfolder.
      /// </summary>
      /// <param name="storageId">Name of the file (less extension) to read from.</param>
      /// <returns>JSON string representing serialized object; null if retrieval failed.</returns>
      public string RetrieveJson(string storageId)
      {
         try
         {
            using (var streamReader = File.OpenText(GetStorageFileName(storageId)))
            {
               return streamReader.ReadToEnd();
            }
         }
         catch  //no file for the broker
         {
            return null;
         }
      }

      /// <summary>
      /// Clear the storage, i.e. remove all files from the Storage subfolder.
      /// </summary>
      public void Reset()
      {
         Directory.GetFiles(_storageLocation).ToList().ForEach(File.Delete);
      }

      private string GetStorageFileName(string storageId) => $"{_storageLocation}{Path.DirectorySeparatorChar}{storageId}.json";
   }
}
