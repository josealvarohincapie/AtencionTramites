Namespace DB

    Public Class Utilidad

        Public Shared Function ParseInteger(ByVal o As Object) As Integer
            If IsDBNull(o) Or Not TypeOf o Is Integer Then
                Return 0
            Else
                Return DirectCast(o, Integer)
            End If
        End Function

        Public Shared Function ParseString(ByVal o As Object) As String
            If IsDBNull(o) Or Not TypeOf o Is String Then
                Return ""
            Else
                Return DirectCast(o, String)
            End If
        End Function

        Public Shared Function ParseBoolean(ByVal o As Object) As Boolean
            If IsDBNull(o) Or Not TypeOf o Is Boolean Then
                Return False
            Else
                Return DirectCast(o, Boolean)
            End If
        End Function

        Public Shared Function ParseDecimal(ByVal o As Object) As Decimal
            If IsDBNull(o) Or Not TypeOf o Is Decimal Then
                Return 0
            Else
                Return DirectCast(o, Decimal)
            End If
        End Function

        Public Shared Function ParseDate(ByVal o As Object) As Date
            If IsDBNull(o) Or Not TypeOf o Is Date Then
                Return Nothing
            Else
                Return DirectCast(o, Date)
            End If
        End Function

        Public Shared Function ParseDateTime(ByVal o As Object) As DateTime
            If IsDBNull(o) Or Not TypeOf o Is DateTime Then
                Return Nothing
            Else
                Return DirectCast(o, DateTime)
            End If
        End Function

        Public Shared Function ParseLong(ByVal o As Object) As Long
            If IsDBNull(o) Or Not TypeOf o Is Long Then
                Return Nothing
            Else
                Return DirectCast(o, Long)
            End If
        End Function



        Public Shared Function IntegerFormat(ByVal i As Integer) As String
            Return i.ToString()
        End Function

        Private Shared Function StringFormat(ByVal s As String) As String
            If s Is Nothing Then
                Return "null"
            Else
                Return "'" + s + "'"
            End If
        End Function

        Private Shared Function BooleanFormat(ByVal b As Boolean) As String
            Return IIf(b, "1", "0")
        End Function

        Private Shared Function DecimalFormat(ByVal d As Decimal) As String
            Return d.ToString().Replace(",", ".")
        End Function

        Private Shared Function DateFormat(ByVal d As Date) As String
            If d = Date.MinValue Then
                Return "null"
            Else
                Return "'" + d.ToString("yyyy-MM-dd") + "'"
            End If
        End Function

    End Class

End Namespace