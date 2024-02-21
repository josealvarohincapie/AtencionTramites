Imports System.Diagnostics.CodeAnalysis

Namespace AtencionTramites.Servicios.Utilidades.Excepciones

    ''' <summary>
    ''' Clase que maneja las excepciones. 
    ''' </summary>
    ''' <paramname=""></param>
    ''' <returns></returns>
    ''' <CreatedBy>EALVAREZT</CreatedBy>
    ''' <CreationDate>21/02/2024</CreationDate>
    ''' <ModifiedBy></ModifiedBy>
    ''' <ModificationDate></ModificationDate>
    Public Class ExcepcionOperacion
        Inherits ExcepcionBase

#Region "Variables"

        Private _operacion As String
        Private _codigo As Integer

        Public Property Operacion As String
            Get
                Return _operacion
            End Get
            Set(value As String)
                _operacion = value
            End Set
        End Property
        Public Property Codigo As Integer
            Get
                Return _codigo
            End Get
            Set(value As Integer)
                _codigo = value
            End Set
        End Property

#End Region

#Region "Constructor"

        ''' <summary>
        ''' Constructor para excepciones de operacion. 
        ''' </summary>
        ''' <paramname="message"> string --- Mensaje</param>
        ''' <paramname="codigo"> int --- Código interno de excepción</param>
        ''' <paramname="data"> object --- objeto con información util sobre la excepcion</param>
        ''' <paramname="innerException">Exception --- Inner exception</param>
        ''' <returns></returns>
        ''' <CreatedBy>EALVAREZT</CreatedBy>
        ''' <CreationDate>21/02/2024</CreationDate>
        ''' <ModifiedBy></ModifiedBy>
        ''' <ModificationDate></ModificationDate>
        Public Sub New(message As String, codigo As Integer, data As Object, Optional innerException As Exception = Nothing)
            MyBase.New(message, data, innerException)
            _codigo = codigo
            _operacion = GetCallerMethod()
        End Sub

#End Region

#Region "Métodos"

        ''' <summary>
        ''' Metodo que realiza los llamados a las excepciones. 
        ''' </summary>
        ''' <paramname=""></param>
        ''' <returns>string</returns>
        ''' <CreatedBy>EALVAREZT</CreatedBy>
        ''' <CreationDate>21/02/2024</CreationDate>
        ''' <ModifiedBy></ModifiedBy>
        ''' <ModificationDate></ModificationDate>
        <SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")>
        <SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")>
        Public Function GetCallerMethod() As String
            Dim st As StackTrace = New StackTrace()
            Dim sf = st.GetFrame(2)

            Return sf.GetMethod().Name
        End Function
#End Region

    End Class
End Namespace
