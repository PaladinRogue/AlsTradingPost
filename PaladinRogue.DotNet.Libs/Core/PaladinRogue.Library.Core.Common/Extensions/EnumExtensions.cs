using System;
using System.ComponentModel;
using System.Linq;

namespace PaladinRogue.Library.Core.Common.Extensions
{
	public static class Extensions
	{
		public static int ToInt<TValue>(this TValue value) where TValue : Enum
			=> (int)(object)value;

		public static string GetEnumDescription<TEnum>(this TEnum item)
			=> item.GetType()
				   .GetField(item.ToString())
				   .GetCustomAttributes(typeof(DescriptionAttribute), false)
				   .Cast<DescriptionAttribute>()
				   .SingleOrDefault()?.Description ?? string.Empty;
	}
}