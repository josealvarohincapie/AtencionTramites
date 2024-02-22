Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace AtencionTramites.Modelos
    Public Class BpmDTO
        Public Property NumeroIncidente As Long?
        Public Property NumeroRadicacionPostulacion As Long?
        Public Property DominioRed As String
        Public Property UsuarioBPM As String
        Public Property PassBPM As String
        Public Property [Step] As String
        Public Property TareaId As String
        Public Property FechaVencimientoBPM As DateTime?
        Public Property FechaExtendidaBPM As DateTime?
        Public Property NombreProceso As String
        Public Property NombreEstado As String
        Public Property AsignadoA As String
        Public Property strTaskXml As String
        Public Property UsuarioDefaultBPM As String
        Public Property UsuarioOc As String
        Public Property NumeroVersion As String
        Public Property EstadoNegado As String
        Public Property Taskid_CancelarPostulacion As String
        Public Property Taskid_NegarPostulacion As String
        Public Property Taskid_PendienteAclarar As String
    End Class
End Namespace

