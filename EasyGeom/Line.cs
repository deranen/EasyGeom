using System;

namespace EasyGeom
{
	public class Line2D
	{
		readonly Point2D  _p;
		readonly Vector2D _d;

		public Line2D( Point2D a, Vector2D b )
		{
			if( a == b ) {
				throw ArgumentException("Can't construct a Line2D using two identical points");
			}

			_p = a;
			_d = Vector2D.Normalize(b - a);
		}

		public Line2D( Point2D p, Vector2D d )
		{
			if( d.IsZeroVector() ) {
				throw ZeroVectorException("Can't construct a Line2D using the zero vector");
			}

			d.Normalize();

			_p = p;
			_d = d;
		}
	}

	public class Line3D
	{
		readonly Point3D  _p;
		readonly Vector3D _d;

		public Line3D( Point3D a, Vector3D b )
		{
			if( a == b ) {
				throw ArgumentException("Can't construct a Line3D using two identical points");
			}

			_p = a;
			_d = Vector3D.Normalize(b - a);
		}

		public Line3D( Point3D p, Vector3D d )
		{
			if( d.IsZeroVector() ) {
				throw ZeroVectorException("Can't construct a Line3D using the zero vector");
			}

			d.Normalize();

			_p = p;
			_d = d;
		}
	}
}

