using System;
using System.Collections;

namespace EasyGeom
{
	public class MatrixCoefficient
	{
		public int I { get; private set; }
		public int J { get; private set; }
		public double Value { get; private set; }

		public MatrixCoefficient( int i, int j, double value )
		{
			I = i;
			J = j;
			Value = value;
		}
	}
	
}
