namespace EasyGeom
{
	public class MatrixCoeff
	{
		public int I { get; private set; }
		public int J { get; private set; }
		public double Value { get; private set; }

		public MatrixCoeff( int i, int j, double value )
		{
			I = i;
			J = j;
			Value = value;
		}
	}
	
}
