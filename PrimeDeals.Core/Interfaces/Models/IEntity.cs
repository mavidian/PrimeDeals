namespace PrimeDeals.Core.Interfaces.Models
{
   public interface IEntity
   {
      string Id { get; set; }
      string ParentId { get; set; }
   }
}
