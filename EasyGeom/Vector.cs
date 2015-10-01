using System;
using System.Diagnostics;

namespace EasyGeom
{
	public struct Vector2
	{
		public double X { get; set; }
		public double Y { get; set; }

		public static Vector2 ZeroVector = new Vector2( 0.0, 0.0 );

		public Vector2( double x, double y )
			: this()
		{
			X = x;
			Y = y;
		}

		#region Operator overloads

		public static Vector2 operator +( Vector2 a, Vector2 b )
		{
			return new Vector2( a.X + b.X, a.Y + b.Y );
		}

		public static Vector2 operator -( Vector2 a, Vector2 b )
		{
			return new Vector2( a.X - b.X, a.Y - b.Y );
		}

		public static Vector2 operator *( double c, Vector2 vec )
		{
			return new Vector2( c * vec.X, c * vec.Y );
		}

		public static Vector2 operator /( Vector2 vec, double c )
		{
			double cInverse = 1.0 / c;
			return new Vector2( cInverse * vec.X, cInverse * vec.Y );
		}

		public static bool operator ==( Vector2 a, Vector2 b )
		{
			return a.X == b.X && a.Y == b.Y;
		}

		public static bool operator !=( Vector2 a, Vector2 b )
		{
			return !(a == b);
		}

		public override bool Equals( object obj )
		{
			return (obj is Vector2) && (this == (Vector2) obj);
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

		public double DistanceTo( Vector2 vec )
		{
			double distance = (vec - this).Length();

			return distance;
		}

		public static double Dot( Vector2 a, Vector2 b )
		{
			return a.X * b.X + a.Y * b.Y;
		}
	}

	public struct Vector3
	{
		public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }

		public static Vector3 ZeroVector = new Vector3( 0.0, 0.0, 0.0 );

		public Vector3( double x, double y, double z )
			: this()
		{
			X = x;
			Y = y;
			Z = z;
		}

		#region Operator overloads

		public static Vector3 operator +( Vector3 a, Vector3 b )
		{
			return new Vector3( a.X + b.X, a.Y + b.Y, a.Z + b.Z );
		}

		public static Vector3 operator -( Vector3 a, Vector3 b )
		{
			return new Vector3( a.X - b.X, a.Y - b.Y, a.Z - b.Z );
		}

		public static Vector3 operator *( double c, Vector3 vec )
		{
			return new Vector3( c * vec.X, c * vec.Y, c * vec.Z );
		}

		public static Vector3 operator /( Vector3 vec, double c )
		{
			double cInverse = 1.0 / c;
			return new Vector3( cInverse * vec.X, cInverse * vec.Y, cInverse * vec.Z );
		}

		public static bool operator ==( Vector3 a, Vector3 b )
		{
			return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
		}

		public static bool operator !=( Vector3 a, Vector3 b )
		{
			return !(a == b);
		}

		public override bool Equals( object obj )
		{
			return (obj is Vector3) && (this == (Vector3) obj);
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

		public double DistanceTo( Vector3 vec )
		{
			double distance = (vec - this).Length();

			return distance;
		}

		public Vector3 ProjectionOnto( Vector3 vec )
		{
			return Project( this, vec );
		}

		public static double Dot( Vector3 a, Vector3 b )
		{
			return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
		}

		public static Vector3 Cross( Vector3 a, Vector3 b )
		{
			double x = a.Y * b.Z - a.Z * b.Y;
			double y = a.Z * b.X - a.X * b.Z;
			double z = a.X * b.Y - a.Y * b.X;

			return new Vector3( x, y, z );
		}

		public static double AngleBetween( Vector3 a, Vector3 b )
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

		public static Vector3 Project( Vector3 a, Vector3 b )
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

			Vector3 vecProjection = (dotNumer / dotDenom) * b;

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