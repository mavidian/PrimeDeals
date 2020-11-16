using Stateless;
using System;
using System.Linq;

namespace PrimeDeals.Core.Models.Base
{
   /// <summary>
   /// Entity class that contains embedded state mamchine (Stateless); base class for model entities.
   /// </summary>
   /// <typeparam name="TState"></typeparam>
   /// <typeparam name="TTrigger"></typeparam>
   public abstract class SmEntity<TState, TTrigger>
   {
      public StateMachine<TState, TTrigger> SM { get; set; }

      public TState State { get; set; }

      public SmEntity()
      {
         SM = new StateMachine<TState, TTrigger>(() => State, s => State = s);
      }


      /// <summary>
      /// Auxiliary method to accompany state machine transition on console output
      /// </summary>
      /// <param name="trigger"></param>
      public void SmFireWithTrace(TTrigger trigger)
      {
         Console.WriteLine($"<<< BEFORE: {SM.State}");
         Console.Write($"    Executing {trigger}");
         try
         {
            SM.Fire(trigger);
            Console.WriteLine(" - success!");
         }
         catch (InvalidOperationException)
         {
            string permittedTriggers;
            switch (SM.PermittedTriggers.Count())
            {
               case 0: //final state
                  permittedTriggers = "no triggers are";
                  break;
               case 1:
                  permittedTriggers = $" only {SM.PermittedTriggers.First()} is";
                  break;
               default:
                  permittedTriggers = $" only {string.Join(" or ", SM.PermittedTriggers)} are";
                  break;
            }
            Console.WriteLine($" - ERROR (in {SM.State} state, {permittedTriggers} allowed!)");
         }
         Console.WriteLine($">>> AFTER : {SM.State}");
      }
   }
}
