namespace PrimeDeals.Core.Models.Base
{
   public class Address
   {
      private string _state;

      public string Street { get; set; }
      public string City { get; set; }
      public string State { get { return _state;  } set { _state = value.ToUpper();  } }
      public string Zip { get; set; }
   }
}
