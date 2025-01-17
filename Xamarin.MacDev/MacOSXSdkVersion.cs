// 
// MacOSXSdkVersion.cs
//  
// Author: Jeffrey Stedfast <jeff@xamarin.com>
// 
// Copyright (c) 2012 Xamarin Inc.
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

using System;

namespace Xamarin.MacDev
{
	public struct MacOSXSdkVersion : IComparable<MacOSXSdkVersion>, IEquatable<MacOSXSdkVersion>
	{
		int[] version;
		
		public MacOSXSdkVersion (params int[] version)
		{
			if (version == null)
				throw new ArgumentNullException ();
			this.version = version;
		}

		public MacOSXSdkVersion (Version version)
		{
			if (version == null)
				throw new ArgumentNullException ();
			this.version = new [] { version.Major, version.Minor };
		}

		public static MacOSXSdkVersion Parse (string s)
		{
			var vstr = s.Split ('.');
			var vint = new int[vstr.Length];
			for (int j = 0; j < vstr.Length; j++)
				vint[j] = int.Parse (vstr[j]);
			return new MacOSXSdkVersion (vint);
		}
		
		public static bool TryParse (string s, out MacOSXSdkVersion result)
		{
			result = new MacOSXSdkVersion ();
			if (s == null)
				return false;
			var vstr = s.Split ('.');
			var vint = new int[vstr.Length];
			for (int j = 0; j < vstr.Length; j++) {
				int component;
				if (!int.TryParse (vstr[j], out component))
					return false;
				vint[j] = component;
			}
			result.version = vint;
			return true;
		}
		
		public int[] Version { get { return version; } }
		
		public override string ToString ()
		{
			if (IsUseDefault)
				return "";
			var v = new string [version.Length];
			for (int i = 0; i < v.Length; i++)
				v[i] = version[i].ToString ();
			return string.Join (".", v);
		}
		
		public int CompareTo (MacOSXSdkVersion other)
		{
			var x = Version;
			var y = other.Version;
			if (ReferenceEquals (x, y))
				return 0;
			
			if (x == null)
				return -1;
			if (y == null)
				return 1;
			
			for (int i = 0; i < Math.Min (x.Length,y.Length); i++) {
				int res = x[i] - y[i];
				if (res != 0)
					return res;
			}
			return x.Length - y.Length;
		}
		
		public bool Equals (MacOSXSdkVersion other)
		{
			var x = Version;
			var y = other.Version;
			if (ReferenceEquals (x, y))
				return true;
			if (x == null || y == null || x.Length != y.Length)
				return false;
			for (int i = 0; i < x.Length; i++)
				if (x[i] != y[i])
					return false;
			return true;
		}
		
		public override bool Equals (object obj)
		{
			if (obj is MacOSXSdkVersion)
				return Equals ((MacOSXSdkVersion)obj);
			return false;
		}
		
		public override int GetHashCode ()
		{
			unchecked {
				var x = Version;
				int acc = 0;
				for (int i = 0; i < x.Length; i++)
					acc ^= x[i] << i;
				return acc;
			}
		}
		
		public static bool operator == (MacOSXSdkVersion a, MacOSXSdkVersion b)
		{
			return a.Equals (b);
		}
		
		public static bool operator != (MacOSXSdkVersion a, MacOSXSdkVersion b)
		{
			return !a.Equals (b);
		}
		
		public static bool operator < (MacOSXSdkVersion a, MacOSXSdkVersion b)
		{
			return a.CompareTo (b) < 0;
		}
		
		public static bool operator > (MacOSXSdkVersion a, MacOSXSdkVersion b)
		{
			return a.CompareTo (b) > 0;
		}
		
		public static bool operator <= (MacOSXSdkVersion a, MacOSXSdkVersion b)
		{
			return a.CompareTo (b) <= 0;
		}
		
		public static bool operator >= (MacOSXSdkVersion a, MacOSXSdkVersion b)
		{
			return a.CompareTo (b) >= 0;
		}
		
		public bool IsUseDefault {
			get {
				return version == null || version.Length == 0;
			}
		}

		public static MacOSXSdkVersion GetDefault (MacOSXSdk sdk)
		{
			var v = sdk.GetInstalledSdkVersions ();
			return v.Count > 0 ? v [v.Count - 1] : UseDefault;
		}

		public MacOSXSdkVersion ResolveIfDefault (MacOSXSdk sdk)
		{
			return IsUseDefault ? GetDefault (sdk) : this;
		}

		public static readonly MacOSXSdkVersion UseDefault = new MacOSXSdkVersion (new int[0]);
		
		public static readonly MacOSXSdkVersion V10_0 = new MacOSXSdkVersion (10, 0);
		public static readonly MacOSXSdkVersion V10_1 = new MacOSXSdkVersion (10, 1);
		public static readonly MacOSXSdkVersion V10_2 = new MacOSXSdkVersion (10, 2);
		public static readonly MacOSXSdkVersion V10_3 = new MacOSXSdkVersion (10, 3);
		public static readonly MacOSXSdkVersion V10_4 = new MacOSXSdkVersion (10, 4);
		public static readonly MacOSXSdkVersion V10_5 = new MacOSXSdkVersion (10, 5);
		public static readonly MacOSXSdkVersion V10_6 = new MacOSXSdkVersion (10, 6);
		public static readonly MacOSXSdkVersion V10_7 = new MacOSXSdkVersion (10, 7);
		public static readonly MacOSXSdkVersion V10_8 = new MacOSXSdkVersion (10, 8);
		public static readonly MacOSXSdkVersion V10_9 = new MacOSXSdkVersion (10, 9);
		public static readonly MacOSXSdkVersion V10_10 = new MacOSXSdkVersion (10, 10);
		public static readonly MacOSXSdkVersion V10_11 = new MacOSXSdkVersion (10, 11);
		public static readonly MacOSXSdkVersion V10_12 = new MacOSXSdkVersion (10, 12);
		public static readonly MacOSXSdkVersion V10_13 = new MacOSXSdkVersion (10, 13);
        	public static readonly MacOSXSdkVersion V10_14 = new MacOSXSdkVersion(10, 14);
		public static readonly MacOSXSdkVersion V10_15 = new MacOSXSdkVersion(10, 15);

        	public static readonly MacOSXSdkVersion Cheetah = V10_0;
		public static readonly MacOSXSdkVersion Puma = V10_1;
		public static readonly MacOSXSdkVersion Jaguar = V10_2;
		public static readonly MacOSXSdkVersion Panther = V10_3;
		public static readonly MacOSXSdkVersion Tiger = V10_4;
		public static readonly MacOSXSdkVersion Leopard = V10_5;
		public static readonly MacOSXSdkVersion SnowLeopard = V10_6;
		public static readonly MacOSXSdkVersion Lion = V10_7;
		public static readonly MacOSXSdkVersion MountainLion = V10_8;
		public static readonly MacOSXSdkVersion Mavericks = V10_9;
		public static readonly MacOSXSdkVersion Yosemite = V10_10;
		public static readonly MacOSXSdkVersion ElCapitan = V10_11;
		public static readonly MacOSXSdkVersion Sierra = V10_12;
		public static readonly MacOSXSdkVersion HighSierra = V10_13;
        	public static readonly MacOSXSdkVersion Mojave = V10_14;
		public static readonly MacOSXSdkVersion Catalina = V10_15;
		
        	public static string VersionName (MacOSXSdkVersion version)
		{
			string versionName;
			switch (version.ToString ()) {
			case "10.0":
				versionName = "Cheetah";
				break;
			case "10.1":
				versionName = "Puma";
				break;
			case "10.2":
				versionName = "Jaguar";
				break;
			case "10.3":
				versionName = "Panther";
				break;
			case "10.4":
				versionName = "Tiger";
				break;
			case "10.5":
				versionName = "Leopard";
				break;
			case "10.6":
				versionName = "Snow Leopard";
				break;
			case "10.7":
				versionName = "Lion";
				break;
			case "10.8":
				versionName = "Mountain Lion";
				break;
			case "10.9":
				versionName = "Mavericks";
				break;
			case "10.10":
				versionName = "Yosemite";
				break;
			case "10.11":
				versionName = "El Capitan";
				break;
			case "10.12":
				versionName = "Sierra";
				break;
			case "10.13":
				versionName = "High Sierra";
				break;
			case "10.14":
				versionName = "Mojave";
				break;
			case "10.15":
				versionName = "Catalina";
				break;
			default:
				versionName = string.Empty;
				break;
			}
			return versionName;
		}
	}
}
