namespace Domain.SeedWorks
{
	public class Entity : IEntity
	{

		#region Static Member(s)
		public static bool operator ==(Entity leftObject, Entity rightObject)
		{
			if (leftObject is null && rightObject is null)
			{
				return true;
			}

			if (leftObject is null && rightObject is not null)
			{
				return false;
			}

			if (leftObject is not null && rightObject is null)
			{
				return false;
			}

			return leftObject.Equals(rightObject);
		}
		public static bool operator !=(Entity leftObject, Entity rightObject)
		{
			return !(leftObject == rightObject);
		}
		#endregion /Static Member(s)


		protected Entity()
		{
		}

		private Guid _id;
		public Guid Id { get => _id; protected set => _id = value; }

		int? _requestedHashCode;

		public override bool Equals(object otherobj)
		{
			if (otherobj is null)
			{
				return false;
			}

			if (otherobj is not Entity)
			{
				return false;
			}

			if (ReferenceEquals(this,otherobj))
			{
				return true;
			}

			var otherobjEntity = otherobj as Entity;

			if (otherobjEntity is null)
			{
				return false;
			}

			if (otherobjEntity.GetRealType() != GetType())
			{
				return false;
			}

			if (GetType() == otherobjEntity.GetType())
			{
				if (IsTransient() || otherobjEntity.IsTransient())
				{
					return false;
				}
				else
				{
					return Id == otherobjEntity.Id;
				}
			}

			return false;
		}

		public override int GetHashCode()
		{
			if (IsTransient() == false)
			{
				if (_requestedHashCode.HasValue == false)
				{
					_requestedHashCode = this.Id.GetHashCode() ^ 31;
				}

				// XOR for random distribution. See:
				// https://docs.microsoft.com/archive/blogs/ericlippert/guidelines-and-rules-for-gethashcode
				return _requestedHashCode.Value;
			}
			else
			{
				return base.GetHashCode();
			}
		}

		private bool IsTransient()
		{
			return Id == default;
		}

		/// <summary>
		/// For EF Core!
		/// </summary>
		private System.Type GetRealType()
		{
			System.Type type = GetType();

			if (type.ToString().Contains("Castle.Proxies."))
			{
				return type.BaseType;
			}

			return type;
		}

	}
}