namespace EasyGeom
{
	public struct Point2
	{
		public double X { get; set; }
		public double Y { get; set; }

		public Point2( double x, double y )
			: this()
		{
			X = x;
			Y = y;
		}

		#region Operator overloads

		public static Point2 operator +( Point2 a, Point2 b )
		{
			return new Point2( a.X + b.X, a.Y + b.Y );
		}

		public static Vector2 operator -( Point2 a, Point2 b )
		{
			return new Vector2( a.X - b.X, a.Y - b.Y );
		}

		public static Point2 operator *( double c, Point2 pt )
		{
			return new Point2( c * pt.X, c * pt.Y );
		}

		public static Point2 operator /( Point2 pt, double c )
		{
			double cInverse = 1.0 / c;
			return new Point2( cInverse * pt.X, cInverse * pt.Y );
		}

		public static bool operator ==( Point2 a, Point2 b )
		{
			return a.X == b.X && a.Y == b.Y;
		}

		public static bool operator !=( Point2 a, Point2 b )
		{
			return !(a == b);
		}

		public override bool Equals( object obj )
		{
			return (obj is Point2) && (this == (Point2) obj);
		}

		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode();
		}

		#endregion
	}

	public struct Point3
	{
		public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }

		public Point3( double x, double y, double z )
			: this()
		{
			X = x;
			Y = y;
			Z = z;
		}

		#region Operator overloads

		public static Point3 operator +( Point3 a, Point3 b )
		{
			return new Point3( a.X + b.X, a.Y + b.Y, a.Z + b.Z );
		}

		public static Point3 operator -( Point3 a, Point3 b )
		{
			return new Point3( a.X - b.X, a.Y - b.Y, a.Z - b.Z );
		}

		public static Point3 operator *( double c, Point3 pt )
		{
			return new Point3( c * pt.X, c * pt.Y, c * pt.Z );
		}

		public static Point3 operator /( Point3 pt, double c )
		{
			double cInverse = 1.0 / c;
			return new Point3( cInverse * pt.X, cInverse * pt.Y, cInverse * pt.Z );
		}

		public static bool operator ==( Point3 a, Point3 b )
		{
			return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
		}

		public static bool operator !=( Point3 a, Point3 b )
		{
			return !(a == b);
		}

		public override bool Equals( object obj )
		{
			return (obj is Point3) && (this == (Point3) obj);
		}

		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
		}

		#endregion
	}
}

