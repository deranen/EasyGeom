using System;

namespace EasyGeom
{
	public class Plane
	{
		readonly Point3D  _p; // Point on plane
		readonly Vector3D _n; // Plane normal

		public Plane( Point3D p, Vector3D n )
		{
			if( n.IsZeroVector() ) {
				throw ZeroVectorException("Can't construct a Plane using the zero vector.");
			}

			n.Normalize();

			_p = p;
			_n = n;
		}

		public Plane( Point3D a, Point3D b, Point3D c )
		{
			Vector3D u = b - a;
			Vector3D v = c - a;

			Vector3D n = Vector3D.Cross( u, v );

			if( n.IsZeroVector() ) {
				throw ZeroVectorException("Can't construct a Plane using three collinear points.");
			}

			n.Normalize();

			_p = a;
			_n = n;
		}
	}
}

