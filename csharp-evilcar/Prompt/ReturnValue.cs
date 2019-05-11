namespace CsharpEvilcar.Prompt
{
	internal class ReturnValue
	{
		private readonly ErrorCodeFlags flags = ErrorCodeFlags.None;

		internal CaseDescriptor Case2 = default;
		internal virtual ErrorCodeFlags Flags => flags;
		internal virtual string Text => UserMessages.Messages[flags];

		internal ReturnValue(ErrorCodeFlags codeFlags, CaseDescriptor Case = default)
		{
			Case2 = Case;
			flags = codeFlags;
		}
		internal static ReturnValue GetValue(ErrorCodeFlags flags, CaseDescriptor Case = null) => new ReturnValue(flags, Case);

		public static bool operator ==(ReturnValue value, ErrorCodeFlags flag) => value.Flags.HasFlag(flag);
		public static bool operator !=(ReturnValue value, ErrorCodeFlags flag) => !( value == flag );

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
			{
				return true;
			}
			else
			{
				ReturnValue value = obj as ReturnValue;
				if (obj != null && Flags == value.Flags)
				{
					return true;
				}
			}
			return false;
		}

		public override int GetHashCode() => Flags.GetHashCode();

		public static implicit operator bool(ReturnValue value) => value.Flags != ErrorCodeFlags.IsRequestedLogout;
	}
}

