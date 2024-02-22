Namespace AtencionTramites.Servicios.Utilidades.Excepciones

    ''' <summary>
    ''' Clase que maneja las excepciones de validación. 
    ''' </summary>
    ''' <paramname=""></param>
    ''' <returns></returns>
    ''' <CreatedBy>EALVAREZT</CreatedBy>
    ''' <CreationDate>21/02/2024</CreationDate>
    ''' <ModifiedBy></ModifiedBy>
    ''' <ModificationDate></ModificationDate>
    Public Class ExcepcionValidacion
        Inherits ExcepcionBase
#Region "Variables"

        Public Overridable Property data As Dictionary(Of String, String)

#End Region

#Region ""

        ''' <summary>
        ''' Constructor de la clase. 
        ''' </summary>
        ''' <paramname="message">string</param>
        ''' <paramname="data">object</param>
        ''' <paramname="innerException">Exception</param>
        ''' <returns></returns>
        ''' <CreatedBy>EALVAREZT</CreatedBy>
        ''' <CreationDate>21/02/2024</CreationDate>
        ''' <ModifiedBy></ModifiedBy>
        ''' <ModificationDate></ModificationDate>
        Public Sub New(message As String, data As Object, Optional innerException As Exception = Nothing)
            MyBase.New(message, data, innerException)
            Me.data = New Dictionary(Of String, String)()
        End Sub

        ''' <summary>
        ''' Constructor de la clase. 
        ''' </summary>
        ''' <paramname="message">string</param>
        ''' <returns></returns>
        ''' <CreatedBy>EALVAREZT</CreatedBy>
        ''' <CreationDate>21/02/2024</CreationDate>
        ''' <ModifiedBy></ModifiedBy>
        ''' <ModificationDate></ModificationDate>

        Public Sub New(message As String)
            MyBase.New(message)
            data = New Dictionary(Of String, String)()
        End Sub

#End Region

    End Class
End Namespace