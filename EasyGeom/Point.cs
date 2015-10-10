namespace EasyGeom
{
	public struct Point2D
	{
		public double X { get; set; }
		public double Y { get; set; }

		public Point2D( double x, double y )
			: this()
		{
			X = x;
			Y = y;
		}

		public double DistanceTo( Point2D pt )
		{
			double distance = (pt - this).Length();

			return distance;
		}

		public double DistanceTo( Line2D line )
		{
			return DistanceBetween( this, line );
		}

		public static double DistanceBetween( Point2D point, Line2D line )
		{
			Point2D  linePoint = line.PointAt( 0.0 );
			Vector2D lineDir   = line.Direction();

			Vector2D v = point - linePoint;
			Vector2D projVector = v.ProjectOnto( lineDir );

			double distance = (v - projVector).Length();

			return distance;
		}

		#region Operator overloads

		public static Point2D operator +( Point2D a, Point2D b )
		{
			return new Point2D( a.X + b.X, a.Y + b.Y );
		}

		public static Vector2D operator -( Point2D a, Point2D b )
		{
			return new Vector2D( a.X - b.X, a.Y - b.Y );
		}

		public static Point2D operator *( double c, Point2D pt )
		{
			return new Point2D( c * pt.X, c * pt.Y );
		}

		public static Point2D operator /( Point2D pt, double c )
		{
			double cInverse = 1.0 / c;
			return new Point2D( cInverse * pt.X, cInverse * pt.Y );
		}

		public static bool operator ==( Point2D a, Point2D b )
		{
			return a.X == b.X && a.Y == b.Y;
		}

		public static bool operator !=( Point2D a, Point2D b )
		{
			return !(a == b);
		}

		public override bool Equals( object obj )
		{
			return (obj is Point2D) && (this == (Point2D) obj);
		}

		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode();
		}

		#endregion
	}

	public struct Point3D
	{
		public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }

		public Point3D( double x, double y, double z )
			: this()
		{
			X = x;
			Y = y;
			Z = z;
		}

		public double DistanceTo( Point3D pt )
		{
			double distance = (pt - this).Length();

			return distance;
		}

		#region Operator overloads

		public static Point3D operator +( Point3D a, Point3D b )
		{
			return new Point3D( a.X + b.X, a.Y + b.Y, a.Z + b.Z );
		}

		public static Vector3D operator -( Point3D a, Point3D b )
		{
			return new Vector3D( a.X - b.X, a.Y - b.Y, a.Z - b.Z );
		}

		public static Point3D operator *( double c, Point3D pt )
		{
			return new Point3D( c * pt.X, c * pt.Y, c * pt.Z );
		}

		public static Point3D operator /( Point3D pt, double c )
		{
			double cInverse = 1.0 / c;
			return new Point3D( cInverse * pt.X, cInverse * pt.Y, cInverse * pt.Z );
		}

		public static bool operator ==( Point3D a, Point3D b )
		{
			return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
		}

		public static bool operator !=( Point3D a, Point3D b )
		{
			return !(a == b);
		}

		public override bool Equals( object obj )
		{
			return (obj is Point3D) && (this == (Point3D) obj);
		}

		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
		}

		#endregion
	}
}

