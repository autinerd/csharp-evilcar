namespace CsharpEvilcar.Prompt
{
	internal class ReturnValue
	{
		private readonly ErrorCodeFlags flags = ErrorCodeFlags.None;

		internal CaseTypes.Base Case = null;
		internal virtual ErrorCodeFlags Flags => flags;
		internal virtual string Text => UserMessages.Messages[flags];
		internal ReturnValue(CaseTypes.Base Case = null) => this.Case = Case;
		internal ReturnValue(ErrorCodeFlags codeFlags = ErrorCodeFlags.None, CaseTypes.Base Case = null)
		{
			this.Case = Case;
			flags = codeFlags;
		}
		internal static ReturnValue Execute(ReturnValue i) => i;
		internal static ReturnValue Execute(out ReturnValue r, ReturnValue i) => r = i;
		internal static ReturnValue Execute(ReturnValue i, CaseTypes.Base C)
		{
			i.Case = C;
			return i;
		}
		internal static ReturnValue GetValue(ErrorCodeFlags flags = ErrorCodeFlags.None, CaseTypes.Base Case = null) => new ReturnValue(flags, Case);

		public static bool operator ==(ReturnValue value, ErrorCodeFlags flag) => value.Flags.HasFlag(flag);
		public static bool operator !=(ReturnValue value, ErrorCodeFlags flag) => !(value == flag);

		public override bool Equals(object obj) => ReferenceEquals(this, obj)
				? true
				:  obj is ReturnValue 
					? Flags == ( obj as ReturnValue ).Flags
					: false;

		public override int GetHashCode() => Flags.GetHashCode();
	}
}

