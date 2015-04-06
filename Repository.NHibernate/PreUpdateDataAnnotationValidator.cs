using System.ComponentModel.DataAnnotations;
using NHibernate.Event;

namespace Repository.NHibernate
{
	public class PreUpdateDataAnnotationValidator : IPreUpdateEventListener
	{
		/// <summary>
		/// Return true if the operation should be vetoed
		/// </summary>
		/// <param name="event"/>
		public bool OnPreUpdate(PreUpdateEvent @event)
		{
			Validator.ValidateObject(@event.Entity, new ValidationContext(@event.Entity, null, null), true);
			return false;
		}
	}
}
