Imports AtencionTramites.Servicios.Utilidades.Mensajes.My.Resources
Imports LogWriterHelper

Namespace Compensar.Servicios.Utilidades.Mensajes

    ''' <summary>
    ''' Clase que maneja el catalogo de mensajes. 
    ''' </summary>
    ''' <param name=""></param>
    ''' <returns></returns>
    ''' <CreatedBy>EALVAREZT</CreatedBy>
    ''' <CreationDate>22/02/2024</CreationDate>
    ''' <ModifiedBy></ModifiedBy>
    ''' <ModificationDate></ModificationDate>
    ''' 
    Public NotInheritable Class CatalogoMensajes

#Region "Variables"

        Private Shared instance As CatalogoMensajes
        Private Shared syncRoot As Object = New Object()
        Private Const MSG_MENSAJE_NO_ENCONTRADO As String = "Mensaje no encontrado. {0}"

        Public Shared ReadOnly Property Instancia As CatalogoMensajes
            Get

                If instance Is Nothing Then

                    SyncLock syncRoot
                        If instance Is Nothing Then instance = New CatalogoMensajes()
                    End SyncLock
                End If

                Return instance
            End Get
        End Property

#End Region

#Region "Métodos"

        Public Shared Sub CatalogoMensajes()

        End Sub

        Public Function GetMensaje(ByVal codigo As String) As String
                Dim descripcion As String
                descripcion = MensajesRecurso.ResourceManager.GetString(codigo)

            If String.IsNullOrEmpty(descripcion) Then
                LogWriter.WriteLog("CatalogoMensaje.GetMensaje", String.Format(MSG_MENSAJE_NO_ENCONTRADO, codigo))
                descripcion = String.Format(MSG_MENSAJE_NO_ENCONTRADO, codigo)
            End If

            Return descripcion
            End Function


#End Region
    End Class
End Namespace