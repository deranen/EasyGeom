using System;

namespace EasyGeom
{
	public class Line2D
	{
		readonly Point2D  _p; // Point on line
		readonly Vector2D _d; // Line direction (normalized)

		public Line2D( Point2D a, Point2D b )
		{
			if( a == b ) {
				throw new ArgumentException("Can't construct a Line2D using two identical points");
			}

			Vector2D d = Vector2D.Normalize(b - a);

			_p = a;
			_d = d;
		}

		public Line2D( Point2D p, Vector2D d )
		{
			if( d.IsZeroVector() ) {
				throw new ZeroVectorException("Can't construct a Line2D using the zero vector.");
			}

			d.Normalize();

			_p = p;
			_d = d;
		}

		public Vector2D Direction
		{
			get { return _d; }
		}

		public Point2D PointOnLine
		{
			get { return _p; }
		}

		public double DistanceTo( Point2D p )
		{
			return DistanceBetween( p, this );
		}

		public static double DistanceBetween( Point2D p, Line2D l )
		{
			Point2D  linePt    = l.PointOnLine;
			Vector2D ptToPt    = p - linePt;
			Point2D  closestPt = linePt + ptToPt.ProjectionOnto( l.Direction );

			double distance = p.DistanceTo( closestPt );

			return distance;
		}
	}

	public class Line3D
	{
		readonly Point3D  _p; // Point on line
		readonly Vector3D _d; // Line direction (normalized)

		public Line3D( Point3D a, Point3D b )
		{
			if( a == b ) {
				throw new ArgumentException("Can't construct a Line3D using two identical points.");
			}

			var d = Vector3D.Normalize(b - a);

			_p = a;
			_d = d;
		}

		public Line3D( Point3D p, Vector3D d )
		{
			if( d.IsZeroVector() ) {
				throw new ZeroVectorException("Can't construct a Line3D using the zero vector.");
			}

			d.Normalize();

			_p = p;
			_d = d;
		}

		public Vector3D Direction
		{
			get { return _d; }
		}

		public Point3D PointOnLine
		{
			get { return _p; }
		}

		public double DistanceTo( Point3D p )
		{
			return DistanceBetween( p, this );
		}

		public static double DistanceBetween( Point3D p, Line3D l )
		{
			Point3D  linePt    = l.PointOnLine;
			Vector3D ptToPt    = p - linePt;
			Point3D  closestPt = linePt + ptToPt.ProjectionOnto( l.Direction );

			double distance = p.DistanceTo( closestPt );

			return distance;
		}
	}
}

