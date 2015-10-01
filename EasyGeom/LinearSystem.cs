namespace EasyGeom
{
	public class LinearSystem
	{
		Matrix _a;

		public LinearSystem( Matrix a )
		{
			_a = new Matrix( a );
		}

		public Matrix GetEchelonForm()
		{
			if( !IsEchelonForm() ) {
				ToEchelonForm();
			}

			return new Matrix( _a );
		}

		bool IsEchelonForm()
		{
			return _a.IsEchelonForm();
		}

		void ToEchelonForm()
		{
			int rowCount = _a.RowCount;
			int colCount = _a.ColCount;

			var matrix = new IndexMatrix( _a );

			for( int i = 0, j = 0; i < rowCount && j < colCount; i++, j++ )
			{
				int currentRow = i;

				if( !FindPivotIndex( matrix, ref i, ref j ) ) {
					break;
				}

				if( i > currentRow ) {
					matrix.SwapRows( i, currentRow );
					i = currentRow;
				}

				double pivotCoeff = matrix[i, j];

				for( int ii = i + 1; ii < rowCount; ii++ )
				{
					double coeff = matrix[ii, j];

					if( coeff == 0.0 ) {
						continue;
					}

					double multiple = coeff / pivotCoeff;

					matrix.AddRowMultiple( ii, -multiple, i );

					matrix[ii, j] = 0.0;
				}
			}

			foreach( var c in matrix.CoefficientIterator() ) {
				_a[c] = c.Value;
			}
		}

		static bool FindPivotIndex( Matrix matrix, ref int i, ref int j )
		{
			for( int jj = j; jj < matrix.ColCount; jj++ ) {
				for( int ii = i; ii < matrix.RowCount; ii++ )
				{
					if( matrix[ii, jj] != 0.0 ) {
						i = ii;
						j = jj;
						return true;
					}
				}
			}

			return false;
		}
	}


	

	
}

