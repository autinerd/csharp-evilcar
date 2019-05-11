using System.Collections.Generic;

namespace CsharpEvilcar.Prompt
{
	internal class ReturnValue
	{
		internal CaseDescriptor Case = default;
		internal ErrorCodeFlags Flags { get; } = ErrorCodeFlags.None;
		internal string Text => UserMessages.Messages[Flags];

		internal IEnumerable<object> Options { get; }

		internal ReturnValue(ErrorCodeFlags codeFlags, CaseDescriptor Case = default, params object[] options)
		{
			this.Case = Case;
			Flags = codeFlags;
			Options = options;
		}
		internal static ReturnValue GetValue(ErrorCodeFlags flags, CaseDescriptor Case = null, params object[] options) => new ReturnValue(flags, Case, options);

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

