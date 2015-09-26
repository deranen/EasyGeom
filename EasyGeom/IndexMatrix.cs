namespace EasyGeom
{
	public class IndexMatrix : Matrix
	{
		readonly int[] _rowIndices;
		readonly int[] _colIndices;

		public IndexMatrix( Matrix matrix )
			: base( matrix )
		{
			_rowIndices = new int[ RowCount ];
			_colIndices = new int[ ColCount ];

			for( int i = 0; i < RowCount; i++ ) {
				_rowIndices[i] = i;
			}

			for( int i = 0; i < ColCount; i++ ) {
				_colIndices[i] = i;
			}
		}

		public double this[int i, int j]
		{
			get {
				return base[_rowIndices[i], _colIndices[j]];
			}
			set {
				base[_rowIndices[i], _colIndices[j]] = value;
			}
		}

		public void SwapRows( int i, int ii )
		{
			int tmp = _rowIndices[i];
			_rowIndices[i] = _rowIndices[ii];
			_rowIndices[ii] = tmp;
		}

		public void AddRowMultiple( int destRow, double multiple, int sourceRow )
		{
			for( int j = 0; j < ColCount; j++ ) {
				this[destRow, j] += multiple * this[sourceRow, j];
			}
		}
	}
}

