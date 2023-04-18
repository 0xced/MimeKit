//
// InternetAddressListTypeConverterTests.cs
//
// Author: Jeffrey Stedfast <jestedfa@microsoft.com>
//
// Copyright (c) 2013-2023 .NET Foundation and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using System.ComponentModel;
using System.Linq;

using NUnit.Framework;

using MimeKit;

namespace UnitTests;

[TestFixture]
public class InternetAddressListTypeConverterTests
{
	[Test]
	public void TestCanConvert ()
	{
		var converter = TypeDescriptor.GetConverter (typeof(InternetAddressList));
		Assert.True (converter.CanConvertFrom (typeof(string)));
		Assert.True (converter.CanConvertTo (typeof(string)));
	}

	[Test]
	public void TestIsValid ()
	{
		var converter = TypeDescriptor.GetConverter (typeof(InternetAddressList));
		Assert.True (converter.IsValid ("Skye <skye@shield.gov>, Leo Fitz <fitz@shield.gov>, Melinda May <may@shield.gov>"));
	}

	[Test]
	public void TestConvertFromValid ()
	{
		var converter = TypeDescriptor.GetConverter (typeof(InternetAddressList));
		var result = converter.ConvertFrom ("Skye <skye@shield.gov>, Leo Fitz <fitz@shield.gov>, Melinda May <may@shield.gov>");
		Assert.IsInstanceOf<InternetAddressList> (result);
		Assert.AreEqual (new[] { "Skye", "Leo Fitz", "Melinda May" }, ((InternetAddressList)result).Select (e => e.Name));
	}

	[Test]
	public void TestIsNotValid ()
	{
		var converter = TypeDescriptor.GetConverter (typeof(InternetAddressList));
		Assert.False (converter.IsValid (""));
	}

	[Test]
	public void TestConvertFromNotValid ()
	{
		var converter = TypeDescriptor.GetConverter (typeof(InternetAddressList));
		Assert.Throws<ParseException> (() => converter.ConvertFrom (""));
	}
}
