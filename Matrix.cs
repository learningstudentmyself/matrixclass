using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathHelper
{
    /// <summary>
    /// Represents a Matrix.
    /// Author : Learning Student
    /// </summary>   
    [Serializable]
    public class Matrix
    {
        #region "Variables and constructor"
        private double[][] value;
        private double det = 0;
        /// <summary>
        /// Gets number of rows in Matrix.
        /// </summary>
        public int NumberOfRows
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets number of columns in Matrix.
        /// </summary>
        public int NumberOfColumns
        {
            get;
            private set;
        }


        /// <summary>
        /// Initializes an empty matrix with given dimensions.
        /// </summary>
        /// <param name="row">Number of rows.</param>
        /// <param name="col">Number of columns.</param>
        public Matrix(int row, int column)
        {
            NumberOfRows = row;
            NumberOfColumns = column;
            det = 0;
            value = new double[row][];

            for (int i = 0; i < NumberOfRows; i++)
                value[i] = new double[NumberOfColumns];
        }
        #endregion

        #region "Private methods"
        /// <summary>
        /// Gets determinant of matrix having order 2.
        /// </summary>
        /// <returns></returns>
        private double GetDeterminant2By2()
        {
            if (NumberOfRows != 2 || NumberOfColumns != 2)
                throw new Exception("Number of rows or columns not equal to 2 .");
            return (this[0, 0] * this[1, 1] - this[1, 0] * this[0, 1]);
        }

        /// <summary>
        /// Gets determinant of matrix having order 3.
        /// </summary>
        /// <returns></returns>
        private double GetDeterminant3By3()
        {
            if (NumberOfRows != 3 || NumberOfColumns != 3)
                throw new Exception("Number of rows or columns not equal to 3.");
            return ((this[0, 0] * (this[1, 1] * this[2, 2] - this[1, 2] * this[2, 1]))
                           - (this[0, 1] * (this[1, 0] * this[2, 2] - this[2, 0] * this[1, 2]))
                           + (this[0, 2] * (this[1, 0] * this[2, 1] - this[1, 1] * this[2, 0]))
                    );

        }
        #endregion

        #region "Public Methods"

        /// <summary>
        /// Returns Identity matrix of given order.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static Matrix GetIdentityMatrix(int order)
        {
            Matrix mat = new Matrix(order, order);
            for (int i = 0; i < order; i++)
                for (int j = 0; j < order; j++)
                    mat[i, j] = i == j ? 1 : 0;
            return mat;
        }

        /// <summary>
        /// Returns matrix having all the elements zero of given order.
        /// </summary>
        /// <param name="rowsCount"></param>
        /// <param name="columnCount"></param>
        /// <returns></returns>
        public static Matrix GetZeroMatrix(int rowsCount, int columnCount)
        {
            Matrix mat = new Matrix(rowsCount, columnCount);
            for (int i = 0; i < rowsCount; i++)
                for (int j = 0; j < columnCount; j++)
                    mat[i, j] = 0;
            return mat;
        }

        /// <summary>
        /// Gets a sub-matrix skipping the specified row and column. Matrix should be square.
        /// </summary>
        /// <param name="initRow">Row number to skip.</param>
        /// <param name="initColumn">Column number to skip.</param>
        /// <returns>A matrix with one decremented order from original.</returns>
        public Matrix GetMinorMatrix(int initRow, int initColumn)
        {
            Matrix subMat = new Matrix(NumberOfRows - 1, NumberOfColumns - 1);
            int resultRow = 0, resultColumn = 0;
            for (int i = 0; i < NumberOfRows; i++)
            {
                if (i == initRow)
                    continue;

                resultColumn = 0;
                for (int j = 0; j < NumberOfColumns; j++)
                {
                    if (j == initColumn)
                        continue;

                    subMat[resultRow, resultColumn] = this[i, j];
                    resultColumn++;

                    if (resultColumn == NumberOfRows)
                        break;
                }
                resultRow++;
                if (resultRow == NumberOfRows)
                    break;
            }
            return subMat;
        }

        /// <summary>
        /// Gets or Sets the value with specified position. 
        /// </summary>
        /// <param name="row">Row position.</param>
        /// <param name="column">Column position.</param>
        /// <returns>Value of that position.</returns>
        public double this[int row, int column]
        {
            get
            {
                if (row >= this.NumberOfRows)
                    throw new Exception("Row index out of range.");

                if (column >= this.NumberOfColumns)
                    throw new Exception("Column index out of range.");

                return value[row][column];
            }
            set
            {
                if (row >= this.NumberOfRows)
                    throw new Exception("Row index out of range.");

                if (column >= this.NumberOfColumns)
                    throw new Exception("Column index out of range.");
                this.value[row][column] = value;
            }
        }

        /// <summary>
        /// Copies and sets current matrix from specified matrix.
        /// </summary>
        /// <param name="mat">To be copied from matrix.</param>
        public void SetMatrix(Matrix mat)
        {
            this.NumberOfRows = mat.NumberOfRows;
            this.NumberOfColumns = mat.NumberOfColumns;
            this.value = mat.value;
            this.det = 0;
        }

        /// <summary>
        /// Returns an equivalent matrix with data.
        /// </summary>
        /// <returns>Copied matrix.</returns>
        public Matrix Copy()
        {
            Matrix A = new Matrix(this.NumberOfRows, this.NumberOfColumns);
            A.value = this.value;
            return A;
        }

        /// <summary>
        /// Returns an equivalent matrix without data.
        /// </summary>
        /// <returns></returns>
        public Matrix Clone()
        {
            return (new Matrix(this.NumberOfRows, this.NumberOfColumns));
        }

        /// <summary>
        ///Set the Matrix from string. String matrix must be in format [{1,2,3}{4,5,6}{7,8,9}], which is square matrix of order 3.
        /// </summary>
        /// <param name="matrixString"></param>
        public void SetMatrix(string matrixString)
        {
            try
            {
                matrixString = matrixString.Trim().Replace("[", string.Empty).Replace("]", string.Empty).Replace("}", "~").Replace("{", string.Empty);

                string[] columns;
                string[] rows = matrixString.Split('~');

                int numRows = rows.Count();
                int numColumns = rows[0].Split(',').Count();
                int iter1 = 0, iter2 = 0;

                Matrix result = new Matrix(numRows, numColumns);

                foreach (string row in rows)
                {
                    columns = row.Split(',');
                    iter2 = 0;
                    foreach (string column in columns)
                    {
                        this[iter1, iter2++] = int.Parse(column);
                    }
                }

                SetMatrix(result);
            }
            catch (Exception ex)
            {
                throw new Exception("The string matrix is not in correct format. : " + ex.Message + " : " + ex.StackTrace);
            }
        }

        /// <summary>
        /// Checks if specified matrix is equal to current matrixS.
        /// </summary>
        /// <param name="obj">Matrix to be compared.</param>
        /// <returns>Returns true if equal else returns false.</returns>
        public override bool Equals(object obj)
        {
            try
            {
                if (obj.GetType() != typeof(Matrix))
                {
                    throw new Exception("The value obj must be type of Matrix class.");
                }
                else
                {
                    Matrix A = (Matrix)obj;
                    if (A.NumberOfRows != this.NumberOfRows || A.NumberOfColumns != this.NumberOfColumns)
                        throw new Exception("Number of rows and columns must match to compare.");

                    for (int i = 0; i < NumberOfRows; i++)
                        for (int j = 0; j < NumberOfColumns; j++)
                            if (this[i, j] != A[i, j])
                                return false;
                }
                return true;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Serves as a hash function for matrix.
        /// </summary>
        /// <returns>A hash code for current Matrix.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Converts the matrix into string.
        /// </summary>
        /// <returns>Matrix in string.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < this.NumberOfRows; i++)
            {
                sb.Append("{");
                for (int j = 0; j < this.NumberOfColumns; j++)
                {
                    sb.Append(this[i, j]);
                    if (j != NumberOfColumns - 1)
                        sb.Append(", ");
                }
                sb.Append("}");
                if (i != NumberOfRows - 1)
                    sb.Append(Environment.NewLine);
            }
            sb.Append("]");

            return sb.ToString();
        }

        /// <summary>
        /// Adds the specified matrix with current matrix.
        /// </summary>
        /// <param name="toBeAdded"></param>
        /// <returns></returns>
        public void Add(Matrix toBeAdded)
        {
            if (NumberOfRows != toBeAdded.NumberOfRows)
                throw new Exception("Row count doesnot match. Can't add.");
            if (NumberOfColumns != toBeAdded.NumberOfColumns)
                throw new Exception("Column count doesnot match. Can't add.");

            for (int i = 0; i < NumberOfRows; i++)
            {
                for (int j = 0; j < NumberOfColumns; j++)
                {
                    this[i, j] += toBeAdded[i, j];
                }
            }
        }

        /// <summary>
        /// Adds the given value to all the elements of matrix.
        /// </summary>
        /// <param name="value"></param>
        public void Add(double value)
        {
            for (int i = 0; i < NumberOfRows; i++)
            {
                for (int j = 0; j < NumberOfColumns; j++)
                {
                    this[i, j] += value;
                }
            }
        }

        /// <summary>
        /// Subtracts the given value to all the elements of matrix.
        /// </summary>
        /// <param name="value"></param>
        public void Subtract(double value)
        {
            for (int i = 0; i < NumberOfRows; i++)
            {
                for (int j = 0; j < NumberOfColumns; j++)
                {
                    this[i, j] -= value;
                }
            }
        }

        /// <summary>
        /// Subtracts the specified matrix from the matrix.
        /// </summary>
        /// <param name="toBeSubtracted"></param>
        public void Subtract(Matrix toBeSubtracted)
        {
            if (NumberOfRows != toBeSubtracted.NumberOfRows)
                throw new Exception("Row count doesnot match. Can't subtract.");
            if (NumberOfColumns != toBeSubtracted.NumberOfColumns)
                throw new Exception("Column count doesnot match. Can't subtract.");

            for (int i = 0; i < NumberOfRows; i++)
            {
                for (int j = 0; j < NumberOfColumns; j++)
                {
                    this[i, j] = this[i, j] - toBeSubtracted[i, j];
                }
            }
        }

        /// <summary>
        /// Multiplies the given matrix with the specified matrix.
        /// </summary>
        /// <param name="toBeMultiplied"></param>
        public void Multiply(Matrix toBeMultiplied)
        {
            if (toBeMultiplied.NumberOfRows != NumberOfColumns)
                throw new Exception("Row and Column count doesnot match. Can't multiply.");

            Matrix C = new Matrix(this.NumberOfRows, toBeMultiplied.NumberOfColumns);
            for (int i = 0; i < NumberOfRows; i++)
            {
                for (int j = 0; j < NumberOfColumns; j++)
                {
                    C[i, j] = 0; ;
                    for (int k = 0; k < NumberOfColumns; k++)
                    {
                        C[i, j] += value[i][k] * toBeMultiplied.value[k][j];
                    }
                }
            }

            SetMatrix(C);
        }

        /// <summary>
        /// Multiply the given value to all the elements of matrix.
        /// </summary>
        /// <param name="value"></param>
        public void Multiply(double value)
        {
            for (int i = 0; i < NumberOfRows; i++)
            {
                for (int j = 0; j < NumberOfColumns; j++)
                {
                    this[i, j] *= value;
                }
            }
        }

        /// <summary>
        /// Divide the given value to all the elements of matrix.
        /// </summary>
        /// <param name="value"></param>
        public void Divide(double value)
        {
            if (value.Equals(0)) throw new Exception("Divident cannot be zero.");

            for (int i = 0; i < NumberOfRows; i++)
            {
                for (int j = 0; j < NumberOfColumns; j++)
                {
                    this[i, j] /= value;
                }
            }
        }

        /// <summary>
        /// Returns the determinant of the matrix.
        /// </summary>
        /// <returns></returns>
        public double GetDeterminant()
        {
            if (!this.IsSquare())
                throw new Exception("Matrix should be square.");

            if (NumberOfRows == 2)
                return GetDeterminant2By2();
            if (NumberOfRows == 3)
                return GetDeterminant3By3();

            for (int i = 0; i < NumberOfRows; i++)
            {
                det += Math.Pow(-1, (i + 2)) * this[0, i] * (this.GetMinorMatrix(0, i).GetDeterminant());
            }
            return det;
        }

        /// <summary>
        /// Interchanges the rows and corresponding columns.
        /// </summary>
        public void Transpose()
        {
            Matrix result = new Matrix(NumberOfColumns, NumberOfRows);

            for (int i = 0; i < NumberOfRows; i++)
                for (int j = 0; j < NumberOfColumns; j++)
                    result[j, i] = this[i, j];

            SetMatrix(result);
        }

        /// <summary>
        /// Returns the cofactor of the element of specified position.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public double GetCofactor(int row, int column)
        {
            if (!this.IsSquare())
                throw new Exception("Matrix should be square.");

            if (NumberOfRows == 2)
                return GetDeterminant2By2();

            return Math.Pow(-1, (row + column + 2)) * (this.GetMinorMatrix(row, column).GetDeterminant());
        }

        /// <summary>
        /// Returns matrix of cofactors.
        /// </summary>
        /// <returns></returns>
        public Matrix GetMatrixOfCofactor()
        {
            if (!this.IsSquare())
                throw new Exception("Matrix should be square.");

            Matrix mat = new Matrix(NumberOfRows, NumberOfColumns);
            for (int i = 0; i < NumberOfRows; i++)
                for (int j = 0; j < NumberOfColumns; j++)
                    mat[i, j] = GetCofactor(i, j);

            return mat;

        }

        /// <summary>
        /// Returns adjoint of matrix. The matrix should be square.
        /// </summary>
        /// <returns></returns>
        public Matrix GetAdjoint()
        {
            if (!this.IsSquare())
                throw new Exception("Matrix should be square.");
            Matrix result = this.GetMatrixOfCofactor();
            result.Transpose();
            return result;
        }

        /// <summary>
        /// Returns inverse of matrix.
        /// </summary>
        /// <returns></returns>
        public Matrix GetInverse()
        {
            Matrix result = this.GetAdjoint();
            result.Divide(this.GetDeterminant());
            return result;
        }

        /// <summary>
        /// Invert the matrix.
        /// </summary>
        public void Invert()
        {
            Matrix result = new Matrix(this.NumberOfRows, this.NumberOfColumns);
            result = this.GetAdjoint();
            result.Divide(this.GetDeterminant());
            SetMatrix(result);
        }

        /// <summary>
        /// Checks whether the matrix is singular.
        /// Any square matrix having determinant zero is singular.
        /// </summary>
        /// <returns></returns>
        public bool IsSingular()
        {
            return this.GetDeterminant() == 0;
        }

        /// <summary>
        /// Checks whether the matrix is singular.
        /// If number of rows and columns are equal, then the matrix is square.
        /// </summary>
        /// <returns></returns>
        public bool IsSquare()
        {
            return this.NumberOfRows == this.NumberOfColumns;
        }

        /// <summary>
        /// Checks whether the matrix is a Row Matrix.
        /// If matrix has only one row, then the matrix is Row Matrix.
        /// </summary>
        /// <returns></returns>
        public bool IsRow()
        {
            return this.NumberOfRows == 1;
        }

        /// <summary>
        /// Checks whether the matrix is a Column Matrix.
        /// If matrix has only one column, then the matrix is Column Matrix.
        /// </summary>
        /// <returns></returns>
        public bool IsColumn()
        {
            return this.NumberOfColumns == 1;
        }

        /// <summary>
        /// Checks whether a matrix is Null or Zero.
        /// If all elements of matrix are zero, then the matrix is Null or Zero.
        /// </summary>
        /// <returns></returns>
        public bool IsNullOrZero()
        {
            for (int i = 0; i < this.NumberOfRows; i++)
                for (int j = 0; j < this.NumberOfColumns; j++)
                    if (this[i, j] != 0)
                        return false;
            return true;
        }

        /// <summary>
        /// Check whether a matrix is Diagolal.
        /// If non diagonal elements are zero, then the square matrix is Diagonal.
        /// </summary>
        /// <returns></returns>
        public bool IsDiagonal()
        {
            if (!this.IsSquare()) return false;
            for (int i = 0; i < this.NumberOfRows; i++)
                for (int j = 0; j < this.NumberOfColumns; j++)
                {
                    if (i != j)
                        if (this[i, j] != 0)
                            return false;
                }
            return true;
        }

        /// <summary>
        /// Checks whether the matrix is Identity. 
        /// If diagonal elements are unity (1) and non-diagonal elements are zero, then the square matrix is Identity or Unit Matrix.
        /// </summary>
        /// <returns></returns>
        public bool IsIdentity()
        {
            if (!this.IsSquare()) return false;
            for (int i = 0; i < this.NumberOfRows; i++)
                for (int j = 0; j < this.NumberOfColumns; j++)
                {
                    if (i == j)
                    {
                        if (this[i, j] != 1)
                            return false;
                    }
                    else
                    {
                        if (this[i, j] != 0)
                            return false;
                    }
                }
            return true;
        }

        /// <summary>
        /// Checks whether the matrix is Symmetric.
        /// If for all values of i and j, a[i,j] = a[j,i], then the square matrix is Symmetric matrix.
        /// </summary>
        /// <returns></returns>
        public bool IsSymmetric()
        {
            if (!this.IsSquare()) return false;
            for (int i = 0; i < this.NumberOfRows; i++)
                for (int j = 0; j < this.NumberOfColumns; j++)
                {
                    if (this[i, j] != this[j, i])
                        return false;
                }
            return true;
        }

        /// <summary>
        /// Checks whether the matrix is Skew Symmetric.
        /// If for all values of i and j, a[i,j] = -a[j,i] and all diagonal elements are zero, then the square matrix is Symmetric matrix.</summary>
        /// <returns></returns>
        public bool IsSkewSymmetric()
        {
            if (!this.IsSquare()) return false;
            for (int i = 0; i < this.NumberOfRows; i++)
                for (int j = 0; j < this.NumberOfColumns; j++)
                {
                    if (i == j)
                        if (this[i, j] != 0)
                            return false;

                    if (this[i, j] != -1 * this[j, i])
                        return false;

                }
            return true;
        }

        /// <summary>
        /// Checks whether the matrix is Upper Triangular. 
        /// If all elements below the diagonal is zero, then the square matrix is Upper Triangular.
        /// </summary>
        /// <returns></returns>
        public bool IsUpperTriangular()
        {
            if (!this.IsSquare()) return false;

            int colCount = 0;
            for (int i = 1; i < this.NumberOfRows; i++)
            {
                for (int j = 0; j <= colCount && colCount < this.NumberOfColumns - 1; j++)
                {
                    if (this[i, j] != 0)
                        return false;
                }
                colCount++;
            }
            return true;
        }

        /// <summary>
        ///  Checks whether the matrix is Lower Triangular. 
        ///  If all elements above the diagonal is zero, then the square matrix is Lower Triangular.</summary>
        /// <returns></returns>
        public bool IsLowerTriangular()
        {
            if (!this.IsSquare()) return false;

            int colCount = 1;
            for (int i = 0; i < this.NumberOfRows - 1; i++)
            {
                for (int j = colCount; colCount < this.NumberOfColumns; j--)
                {
                    if (this[i, j] != 0)
                        return false;
                }
                colCount++;
            }
            return true;
        }

        /// <summary>
        ///  Checks whether the matrix is Orthogonal. 
        ///  If product of the matrix to its transpose is unit or identity matrix, then the square matris is Orthogonal.</summary>
        /// <returns></returns>
        public bool IsOrthogonal()
        {
            if (!this.IsSquare()) return false;

            Matrix mat1 = this.Copy();
            Matrix mat2 = this.Copy();

            mat2.Transpose();
            mat1.Multiply(mat2);

            return mat1.IsIdentity();
        }
        #endregion
    }
}