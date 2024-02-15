Public Class ComunicacionRecibida

    Public Id As Long
    Public Proceso As String
    Public Incidente As Integer
    Public Creado As Boolean
    Public FechaRadicacion As DateTime
    Public NumeroRadicado As String
    Public NumeroReferenciaRelacionada As String
    Public TipoIdentificacion As String
    Public NumeroIdentificacion As Long
    Public NombreRazonSocial As String
    Public Asunto As Integer
    Public DescripcionAsunto As String
    Public FunsionarioAsignado As String
    Public DocumentHandle As Long
    Public IdAnexos As String
    Public NombreAnexos As String
    Public CantidadAnexos As Integer
    Public MostrarAnexos As Boolean

    Public ReadOnly Property FILEID As String
        Get
            Return Id.ToString()
        End Get
    End Property

    Public ReadOnly Property FECHA As String
        Get
            Return FechaRadicacion.ToString("dd/MM/yyyy h:mm tt")
        End Get
    End Property

    Public ReadOnly Property NUMERO_RADICADO As String
        Get
            Return "[" + NumeroRadicado + "]"
        End Get
    End Property

    Public ReadOnly Property DETALLE_ASUNTO As String
        Get
            Return DescripcionAsunto + " - " + NumeroReferenciaRelacionada
        End Get
    End Property

    Public ReadOnly Property FUNCIONARIO As String
        Get
            Return FunsionarioAsignado
        End Get
    End Property

    Public ReadOnly Property CLIENTE As String
        Get
            Return TipoIdentificacion + " " + NumeroIdentificacion.ToString() + " - " + NombreRazonSocial
        End Get
    End Property

    Public ReadOnly Property DETALLE_ANEXOS As String
        Get
            Dim html As String = ""
            For Each a In Anexos
                If MostrarAnexos Then
                    html += "<input type='button' value='" + a.Nombre + "' onclick='return VerAnexo(""" + a.Id + """)' /><br />"
                Else
                    html += "<input type='button' value='" + a.Nombre + "' disabled /><br />"
                End If
            Next
            Return html
        End Get
    End Property

    Public ReadOnly Property Anexos As List(Of AnexoComunicacion)
        Get
            Dim ids As String() = IdAnexos.Split(New String() {"|"}, StringSplitOptions.RemoveEmptyEntries)
            Dim nombres As String() = NombreAnexos.Split(New String() {"|"}, StringSplitOptions.RemoveEmptyEntries)
            Return ids.Select(Function(id, index) New AnexoComunicacion With {.Id = New Simple3Des("NkSN3.5sAAlsp").EncryptData(Proceso + "|" + Incidente.ToString() + "|" + id), .Nombre = nombres(index)}).ToList()
        End Get
    End Property

End Class

Public Class AnexoComunicacion
    Public Id As String
    Public Nombre As String
End Class

Public Class IncidenteComunicacionRecibida
    Public ComunicacionRecibida As Long
    Public Proceso As String
    Public Incidente As Integer
    Public Resumen As String
    Public Comentarios As String
End Class