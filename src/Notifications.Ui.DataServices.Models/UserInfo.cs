using System;

namespace Notifications.Ui.DomainModels
{
	public class UserInfo : IEquatable<UserInfo>
	{
		public string DisplayName { get; set; }
		public string PrincipalName { get; set; }

		public override string ToString()
		{
			return $"{DisplayName}";
		}

		public bool Equals(UserInfo other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return DisplayName == other.DisplayName && PrincipalName == other.PrincipalName;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((UserInfo) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((DisplayName != null ? DisplayName.GetHashCode() : 0) * 397) ^ (PrincipalName != null ? PrincipalName.GetHashCode() : 0);
			}
		}
	}
}