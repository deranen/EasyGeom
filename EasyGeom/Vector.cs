using System;

namespace EasyGeom
{
	public struct Vector2D
	{
		public double X { get; set; }
		public double Y { get; set; }

		public static Vector2D ZeroVector = new Vector2D( 0.0, 0.0 );

		public Vector2D( double x, double y )
			: this()
		{
			X = x;
			Y = y;
		}

		#region Operator overloads

		public static Vector2D operator +( Vector2D a, Vector2D b )
		{
			return new Vector2D( a.X + b.X, a.Y + b.Y );
		}

		public static Vector2D operator -( Vector2D a, Vector2D b )
		{
			return new Vector2D( a.X - b.X, a.Y - b.Y );
		}

		public static Vector2D operator *( double c, Vector2D vec )
		{
			return new Vector2D( c * vec.X, c * vec.Y );
		}

		public static Vector2D operator /( Vector2D vec, double c )
		{
			double cInverse = 1.0 / c;
			return new Vector2D( cInverse * vec.X, cInverse * vec.Y );
		}

		public static bool operator ==( Vector2D a, Vector2D b )
		{
			return a.X == b.X && a.Y == b.Y;
		}

		public static bool operator !=( Vector2D a, Vector2D b )
		{
			return !(a == b);
		}

		public override bool Equals( object obj )
		{
			return (obj is Vector2D) && (this == (Vector2D) obj);
		}

		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode();
		}

		#endregion

		public bool IsZeroVector()
		{
			return (this == ZeroVector);
		}

		public double Length()
		{
			return Math.Sqrt( X*X + Y*Y );
		}

		public double LengthSquared()
		{
			return X*X + Y*Y;
		}

		public void Normalize()
		{
			if( IsZeroVector() ) {
				throw new ZeroVectorException( "Can't normalize the zero-vector." );
			}

			this /= Length();
		}

		public double DistanceTo( Vector2D vec )
		{
			double distance = (vec - this).Length();

			return distance;
		}

		public static double Dot( Vector2D a, Vector2D b )
		{
			return a.X * b.X + a.Y * b.Y;
		}
	}

	public struct Vector3D
	{
		public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }

		public static Vector3D ZeroVector = new Vector3D( 0.0, 0.0, 0.0 );

		public Vector3D( double x, double y, double z )
			: this()
		{
			X = x;
			Y = y;
			Z = z;
		}

		#region Operator overloads

		public static Vector3D operator +( Vector3D a, Vector3D b )
		{
			return new Vector3D( a.X + b.X, a.Y + b.Y, a.Z + b.Z );
		}

		public static Vector3D operator -( Vector3D a, Vector3D b )
		{
			return new Vector3D( a.X - b.X, a.Y - b.Y, a.Z - b.Z );
		}

		public static Vector3D operator *( double c, Vector3D vec )
		{
			return new Vector3D( c * vec.X, c * vec.Y, c * vec.Z );
		}

		public static Vector3D operator /( Vector3D vec, double c )
		{
			double cInverse = 1.0 / c;
			return new Vector3D( cInverse * vec.X, cInverse * vec.Y, cInverse * vec.Z );
		}

		public static bool operator ==( Vector3D a, Vector3D b )
		{
			return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
		}

		public static bool operator !=( Vector3D a, Vector3D b )
		{
			return !(a == b);
		}

		public override bool Equals( object obj )
		{
			return (obj is Vector3D) && (this == (Vector3D) obj);
		}

		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
		}

		#endregion

		public bool IsZeroVector()
		{
			return (this == ZeroVector);
		}

		public double Length()
		{
			return Math.Sqrt( X*X + Y*Y + Z*Z );
		}

		public double LengthSquared()
		{
			return X*X + Y*Y + Z*Z;
		}

		public void Normalize()
		{
			if( IsZeroVector() ) {
				throw new ZeroVectorException( "Can't normalize the zero-vector." );
			}

			this /= Length();
		}

		public double DistanceTo( Vector3D vec )
		{
			double distance = (vec - this).Length();

			return distance;
		}

		public Vector3D ProjectionOnto( Vector3D vec )
		{
			return Project( this, vec );
		}

		public static double Dot( Vector3D a, Vector3D b )
		{
			return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
		}

		public static Vector3D Cross( Vector3D a, Vector3D b )
		{
			double x = a.Y * b.Z - a.Z * b.Y;
			double y = a.Z * b.X - a.X * b.Z;
			double z = a.X * b.Y - a.Y * b.X;

			return new Vector3D( x, y, z );
		}

		public static double AngleBetween( Vector3D a, Vector3D b )
		{
			if( a.IsZeroVector() || b.IsZeroVector() ) {
				throw new ZeroVectorException( "Can't measure angle with the zero-vector." );
			}

			double dot = Dot( a, b );

			if( dot == 0.0 ) {
				return Math.PI / 2.0;
			}

			double lengthA = a.Length();
			double lengthB = b.Length();

			double cosineOfAngle = dot / (lengthA * lengthB);
			double angle = Math.Acos( cosineOfAngle );

			return angle;
		}

		public static Vector3D Project( Vector3D a, Vector3D b )
		{
			if( a.IsZeroVector() ) {
				throw new ZeroVectorException( "Can't project with the zero-vector." );
			}
			if( b.IsZeroVector() ) {
				throw new ZeroVectorException( "Can't project onto the zero-vector." );
			}

			double dotNumer = Dot( a, b );

			if( dotNumer == 0.0 ) {
				return ZeroVector;
			}

			double dotDenom = Dot( b, b );

			Vector3D vecProjection = (dotNumer / dotDenom) * b;

			return vecProjection;
		}
	}

	public class Vector
	{
		double[] _coeffs;

		public Vector( int size )
		{
			if( size <= 0 ) {
				throw new ArgumentException( "Size of vector must be at least 1." );
			}

			_coeffs = new double[size];
		}

		#region Properties

		public int Size {
			get { return _coeffs.Length; }
		}

		public double this[ int index ] {
			get { return _coeffs[index]; }
			set { _coeffs[index] = value; }
		}

		#endregion

		#region Operator overloads

		public static Vector operator +( Vector a, Vector b )
		{
			if( a.Size != b.Size ) {
				throw new IncompatibleVectorSizesException();
			}

			Vector result = new Vector( a.Size );

			for( int i = 0; i < a.Size; i++ ) {
				result[i] = a[i] + b[i];
			}

			return result;
		}

		public static Vector operator -( Vector a, Vector b )
		{
			if( a.Size != b.Size ) {
				throw new IncompatibleVectorSizesException();
			}

			Vector result = new Vector( a.Size );

			for( int i = 0; i < a.Size; i++ ) {
				result[i] = a[i] - b[i];
			}

			return result;
		}

		public static Vector operator *( double c, Vector vec )
		{
			Vector result = new Vector( vec.Size );

			for( int i = 0; i < vec.Size; i++ ) {
				result[i] = c * vec[i];
			}

			return result;
		}

		public static Vector operator /( Vector vec, double c )
		{
			if( c == 0.0 ) {
				throw new DivideByZeroException();
			}

			Vector result = new Vector( vec.Size );

			double cInverse = 1.0 / c;

			for( int i = 0; i < vec.Size; i++ ) {
				result[i] = cInverse * vec[i];
			}

			return result;
		}

		#endregion

		public double Length()
		{
			return Math.Sqrt( LengthSquared() );
		}

		public double LengthSquared()
		{
			double lengthSquared = 0.0;

			for( int i = 0; i < Size; i++ )
			{
				double coeff = _coeffs[i];
				lengthSquared += coeff * coeff;
			}

			return lengthSquared;
		}

		public static double Dot( Vector a, Vector b )
		{
			if( a.Size != b.Size ) {
				throw new IncompatibleVectorSizesException();
			}

			double dot = 0.0;

			for( int i = 0; i < a.Size; i++ ) {
				dot += a[i] * b[i];
			}

			return dot;
		}

	}

	public class ZeroVectorException : Exception
	{
		public ZeroVectorException()
		{}

		public ZeroVectorException( string message )
			: base( message )
		{}
	}

	public class IncompatibleVectorSizesException : Exception
	{
		public IncompatibleVectorSizesException()
		{}

		public IncompatibleVectorSizesException( string message )
			: base( message )
		{}
	}
}