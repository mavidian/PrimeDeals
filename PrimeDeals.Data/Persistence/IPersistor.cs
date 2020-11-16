namespace PrimeDeals.Data.Persistence
{
   /// <summary>
   /// Storage mechanism for JSON strings representing serialized objects.
   /// </summary>
   public interface IPersistor
   {
      /// <summary>
      /// Save JSON representing serialized object to perstistent storage.
      /// </summary>
      /// <param name="storageId">ID (storage-wide) under which to save the object.</param>
      /// <param name="json">JSON string representing object to save.</param>
      void PersistJson(string storageId, string json);
      /// <summary>
      /// Retrieve (from perstistent storage) JSON representing serialized object.
      /// </summary>
      /// <param name="storageId">Unique ID (storage-wide) of the object to retrieve.</param>
      /// <returns>JSON string retrieved from storage; null if retrieval failed.</returns>
      string RetrieveJson(string storageId);
      /// <summary>
      /// Clear storage of any elements that have been persisted.
      /// </summary>
      void Reset();
   }
}
