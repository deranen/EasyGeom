using System;
using System.Collections.Generic;

namespace EasyGeom
{
	public class Matrix
	{
		double[,] _coeffs;

		public int RowCount {
			get { return _coeffs.GetLength( 0 ); }
		}

		public int ColCount {
			get { return _coeffs.GetLength( 1 ); }
		}

		public Matrix( int nRows, int nCols )
		{
			if( nRows <= 0 || nCols <= 0 ) {
				throw new ArgumentException(
					"The row and column count of a matrix must be at least 1." );
			}

			_coeffs = new double[nRows, nCols];

			for( int i = 0; i < Math.Min(nRows, nCols); i++ ) {
				_coeffs[i, i] = 1.0;
			}
		}

		public Matrix( double[,] coeffs )
		{
			int nRows = coeffs.GetLength( 0 );
			int nCols = coeffs.GetLength( 1 );

			if( nRows <= 0 || nCols <= 0 ) {
				throw new ArgumentException(
					"The row and column count of a matrix must be at least 1." );
			}

			_coeffs = new double[nRows, nCols];

			for( int i = 0; i < nRows; i++ ) {
				for( int j = 0; j < nCols; j++ )
				{
					_coeffs[i, j] = coeffs[i, j];
				}
			}
		}

		public Matrix( Matrix mat )
		{
			_coeffs = new double[ mat.RowCount, mat.ColCount ];

			for( int i = 0; i < mat.RowCount; i++ ) {
				for( int j = 0; j < mat.ColCount; j++ )
				{
					_coeffs[i, j] = mat[i, j];
				}
			}
		}

		public virtual double this[ int i, int j ]
		{
			get { return _coeffs[i, j]; }
			set { _coeffs[i, j] = value; }
		}

		public virtual double this[ MatrixCoefficient c ]
		{
			get { return this[c.I, c.J]; }
			set { this[c.I, c.J] = value; }
		}

		#region Arithmetic operators

		public static Matrix operator +( Matrix left, Matrix right )
		{
			if( left.RowCount != right.RowCount || left.ColCount != right.ColCount ) {
				throw new IncompatibleMatrixDimensionsException();
			}

			Matrix result = new Matrix( left.RowCount, left.ColCount );

			for( int i = 0; i < left.RowCount; i++ ) {
				for( int j = 0; j < left.ColCount; j++ ) {
					result[i, j] = left[i, j] + right[i, j];
				}
			}

			return result;
		}

		public static Matrix operator -( Matrix left, Matrix right )
		{
			if( left.RowCount != right.RowCount || left.ColCount != right.ColCount ) {
				throw new IncompatibleMatrixDimensionsException();
			}

			Matrix result = new Matrix( left.RowCount, left.ColCount );

			for( int i = 0; i < left.RowCount; i++ ) {
				for( int j = 0; j < left.ColCount; j++ ) {
					result[i, j] = left[i, j] - right[i, j];
				}
			}

			return result;
		}

		public static Matrix operator *( double c, Matrix mat )
		{
			Matrix result = new Matrix( mat.RowCount, mat.ColCount );

			for( int i = 0; i < mat.RowCount; i++ ) {
				for( int j = 0; j < mat.ColCount; j++ ) {
					result[i, j] = c * mat[i, j];
				}
			}

			return result;
		}

		public static Matrix operator /( Matrix mat, double c )
		{
			double cInverse = 1.0 / c;

			return cInverse * mat;
		}

		public static Matrix operator *( Matrix left, Matrix right )
		{
			if( left.ColCount != right.RowCount ) {
				throw new IncompatibleMatrixDimensionsException();
			}

			int newRowCount = left.RowCount;
			int newColCount = right.ColCount;

			Matrix result = new Matrix( newRowCount, newColCount );

			for( int i = 0; i < newRowCount; i++ ) {
				for( int j = 0; j < newColCount; j++ ) {
					double coeff = 0.0;

					for( int c = 0; c < left.ColCount; c++ ) {
						coeff += left[i, c] * right[c, j];
					}

					result[i, j] = coeff;
				}
			}

			return result;
		}

		#endregion

		public void Transpose()
		{
			for( int i = 0; i < RowCount - 1; i++ ) {
				for( int j = i + 1; j < ColCount; j++ )
				{
					double tmp = this[i, j];
					this[i, j] = this[j, i];
					this[j, i] = tmp;
				}
			}
		}

		public bool IsSquare()
		{
			return RowCount == ColCount;
		}

		public bool IsIdentity()
		{
			if( !IsSquare() ) {
				throw new NonSquareMatrixException();
			}

			foreach( var c in CoefficientIterator() )
			{
				bool ok = (c.I == c.J ? c.Value == 1.0 : c.Value == 0.0);

				if( !ok ) {
					return false;
				}
			}

			return false;
		}

		public bool IsSymmetric()
		{
			if( !IsSquare() ) {
				throw new NonSquareMatrixException();
			}

			foreach( var c in UpperTriangleIterator() ) {
				if( this[c.I, c.J] != this[c.J, c.I] ) {
					return false;
				}
			}

			return true;
		}

		public bool IsUpperTriangular()
		{
			if( !IsSquare() ) {
				throw new NonSquareMatrixException();
			}

			foreach( var c in LowerTriangleIterator() ) {
				if( c.Value != 0.0 ) {
					return false;
				}
			}

			return true;
		}

		public bool IsLowerTriangular()
		{
			if( !IsSquare() ) {
				throw new NonSquareMatrixException();
			}

			foreach( var c in UpperTriangleIterator() ) {
				if( c.Value != 0.0 ) {
					return false;
				}
			}

			return true;
		}

		public bool IsDiagonal()
		{
			if( !IsSquare() ) {
				throw new NonSquareMatrixException();
			}

			foreach( var c in CoefficientIterator() )
			{
				if( c.I != c.J && c.Value != 0.0 ) {
					return false;
				}
			}

			return true;
		}

		public bool IsEchelonForm()
		{
			for( int i = 0, j = 0; i < RowCount && j < ColCount; j++ )
			{
				bool pivotFound = (this[i, j] != 0.0);

				// Make sure rest of the column is all zeros
				for( int ii = i + 1; ii < RowCount; ii++ ) {
					if( this[ii, j] != 0.0 ) {
						return false;
					}
				}

				if( pivotFound ) {
					i++;
				}
			}

			return true;
		}

		#region Iterators

		public IEnumerable<MatrixCoefficient> CoefficientIterator()
		{
			for( int i = 0; i < RowCount; i++ ) {
				for( int j = 0; j < ColCount; j++ ) {
					yield return new MatrixCoefficient( i, j, this[i, j] );
				}
			}
		}

		public IEnumerable<MatrixCoefficient> DiagonalIterator()
		{
			int diagonalLength = Math.Min( RowCount, ColCount );

			for( int d = 0; d < diagonalLength; d++ ) {
				yield return new MatrixCoefficient( d, d, this[d, d] );
			}
		}

		public IEnumerable<MatrixCoefficient> UpperTriangleIterator()
		{
			if( !IsSquare() ) {
				throw new NonSquareMatrixException();
			}

			for( int i = 0; i < RowCount - 1; i++ ) {
				for( int j = i + 1; j < ColCount; j++ ) {
					yield return new MatrixCoefficient( i, j, this[i, j] );
				}
			}
		}

		public IEnumerable<MatrixCoefficient> LowerTriangleIterator()
		{
			if( !IsSquare() ) {
				throw new NonSquareMatrixException();
			}

			for( int i = 1; i < RowCount; i++ ) {
				for( int j = 0; j < i; j++ ) {
					yield return new MatrixCoefficient( i, j, this[i, j] );
				}
			}
		}

		#endregion
	}


	public class NonSquareMatrixException : Exception
	{
		public NonSquareMatrixException()
		{}

		public NonSquareMatrixException( string message )
			: base( message )
		{}
	}

	public class IncompatibleMatrixDimensionsException : Exception
	{
		public IncompatibleMatrixDimensionsException()
		{}

		public IncompatibleMatrixDimensionsException( string message )
			: base( message )
		{}
	}
}

