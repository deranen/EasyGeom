using System;

namespace EasyGeom
{
	public class Line2D
	{
		readonly Point2D  _p; // Point on line
		readonly Vector2D _d; // Line direction (normalized)

		public Line2D( Point2D a, Vector2D b )
		{
			if( a == b ) {
				throw ArgumentException("Can't construct a Line2D using two identical points");
			}

			Vector2D d = Vector2D.Normalize(b - a);

			_p = a;
			_d = d;
		}

		public Line2D( Point2D p, Vector2D d )
		{
			if( d.IsZeroVector() ) {
				throw ZeroVectorException("Can't construct a Line2D using the zero vector.");
			}

			d.Normalize();

			_p = p;
			_d = d;
		}

		public Point2D PointAt( double t )
		{
			Point2D p = _p + t * _d;

			return p;
		}

		public Vector2D Direction()
		{
			return _d;
		}
	}

	public class Line3D
	{
		readonly Point3D  _p; // Point on line
		readonly Vector3D _d; // Line direction (normalized)

		public Line3D( Point3D a, Vector3D b )
		{
			if( a == b ) {
				throw ArgumentException("Can't construct a Line3D using two identical points.");
			}

			var d = Vector3D.Normalize(b - a);

			_p = a;
			_d = d;
		}

		public Line3D( Point3D p, Vector3D d )
		{
			if( d.IsZeroVector() ) {
				throw ZeroVectorException("Can't construct a Line3D using the zero vector.");
			}

			d.Normalize();

			_p = p;
			_d = d;
		}

		public Point3D PointAt( double t )
		{
			Point3D p = _p + t * _d;

			return p;
		}

		public Vector3D Direction()
		{
			return _d;
		}
	}
}

