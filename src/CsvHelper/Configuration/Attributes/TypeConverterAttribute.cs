﻿// Copyright 2009-2022 Josh Close
// This file is a part of CsvHelper and is dual licensed under MS-PL and Apache 2.0.
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html for MS-PL and http://opensource.org/licenses/Apache-2.0 for Apache 2.0.
// https://github.com/JoshClose/CsvHelper
using CsvHelper.TypeConversion;
using System;

namespace CsvHelper.Configuration.Attributes
{
	/// <summary>
	/// Specifies the <see cref="TypeConverter"/> to use
	/// when converting the member to and from a CSV field.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
	public class TypeConverterAttribute : Attribute, IMemberMapper, IParameterMapper
	{
		/// <summary>
		/// Gets the type converter.
		/// </summary>
		public ITypeConverter TypeConverter { get; private set; }

		/// <summary>
		/// Specifies the <see cref="TypeConverter"/> to use
		/// when converting the member to and from a CSV field.
		/// </summary>
		/// <param name="typeConverterType"></param>
		public TypeConverterAttribute(Type typeConverterType)
		{
			if (typeConverterType == null)
			{
				throw new ArgumentNullException(nameof(typeConverterType));
			}

			TypeConverter = ObjectResolver.Current.Resolve(typeConverterType) as ITypeConverter;
			if (TypeConverter is null)
			{
				throw new ArgumentException($"Type '{typeConverterType.FullName}' does not implement {nameof(ITypeConverter)}");
			}
		}

		/// <summary>
		/// Applies configuration to the given <see cref="MemberMap" />.
		/// </summary>
		/// <param name="memberMap">The member map.</param>
		public void ApplyTo(MemberMap memberMap)
		{
			memberMap.Data.TypeConverter = TypeConverter;
		}

		/// <summary>
		/// Applies configuration to the given <see cref="ParameterMap" />.
		/// </summary>
		/// <param name="parameterMap">The parameter map.</param>
		public void ApplyTo(ParameterMap parameterMap)
		{
			parameterMap.Data.TypeConverter = TypeConverter;
		}
	}
}
