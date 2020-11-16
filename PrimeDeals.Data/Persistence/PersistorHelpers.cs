using Newtonsoft.Json;

namespace PrimeDeals.Data.Persistence
{
   //TODO: consider adding these method to IPersistor interface, possibly replacing existing methods .
   internal static class PersistorHelpers
   {
      /// <summary>
      /// Restore object of given type from persistent storage
      /// </summary>
      /// <typeparam name="T">Type of the object, e.g. Broker.</typeparam>
      /// <param name="persistor"></param>
      /// <param name="storageId">Storage Id, e.g. BrokerState_B101.</param>
      /// <returns>Restored object or null if not found.</returns>
      internal static T RestoreObject<T>(this IPersistor persistor, string storageId) where T : class
      {
         var json = persistor.RetrieveJson(storageId);
         return json == null ? null : JsonConvert.DeserializeObject<T>(json);
      }


      /// <summary>
      /// Save object to persistent storage
      /// </summary>
      /// <param name="persistor"></param>
      /// <param name="storageId">Storage Id, e.g. BrokerState_B101.</param>
      /// <param name="objectToPersist"></param>
      internal static void PersistObject(this IPersistor persistor, string storageId, object objectToPersist)
      {
         persistor.PersistJson(storageId, JsonConvert.SerializeObject(objectToPersist));
      }
   }
}
