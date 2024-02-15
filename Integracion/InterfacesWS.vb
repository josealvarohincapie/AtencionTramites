Imports System.Configuration.ConfigurationManager
Imports System.Xml
Imports LogWriterHelper

Public Class InterfacesWS

    Public Function ConsultaClienteT24(ByVal numDocumento As String, ByRef bActivo As Boolean, ByRef strNombre As String, ByRef strPais As String, ByRef strCiudad As String, ByRef strEmail As String, ByRef strError As String) As Boolean
        Dim bResultado As Boolean
        Try
            bActivo = False
            Dim strUrl = AppSettings("ServicioT24")
            Dim sBody As String = "<?xml version=""1.0"" encoding=""utf-8""?>"
            ' Servicio Anterior
            'sBody &= "<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" "
            'sBody &= "xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" "
            'sBody &= "xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" "
            'sBody &= "xmlns:t24=""T24WebServicesImpl"">"
            'sBody &= "<soap:Body><t24:ULTCLIENTESVERIFICA><WebRequestCommon>"
            'sBody &= "<userName>" & AppSettings("UserNameT24") & "</userName>"
            'sBody &= "<password>" & AppSettings("PasswordT24") & "</password>"
            'sBody &= "<company /></WebRequestCommon><ULTCLIENTESVERIFICAType><enquiryInputCollection>"
            'sBody &= "<columnName>COL.NUM.ID</columnName>"
            'sBody &= "<criteriaValue>" & numDocumento & "</criteriaValue>"
            'sBody &= "<operand>EQ</operand>"
            'sBody &= "</enquiryInputCollection></ULTCLIENTESVERIFICAType></t24:ULTCLIENTESVERIFICA></soap:Body>"
            'sBody &= "</soap:Envelope>"
            ' Servicio Nuevo
            sBody &= "<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" "
            sBody &= "xmlns:ult=""http://temenos.com/ULT-WS""><soapenv:Header/>"
            sBody &= "<soapenv:Body><ult:ULTCLIENTESVERIFICA><WebRequestCommon><company/>"
            sBody &= "<password>" & AppSettings("PasswordT24") & "</password>"
            sBody &= "<userName>" & AppSettings("UserNameT24") & "</userName>"
            sBody &= "</WebRequestCommon><ULTCLIENTESVERIFICAType><enquiryInputCollection>"
            sBody &= "<columnName>COL.NUM.ID</columnName>"
            sBody &= "<criteriaValue>" & numDocumento & "</criteriaValue>"
            sBody &= "<operand>EQ</operand>"
            sBody &= "</enquiryInputCollection></ULTCLIENTESVERIFICAType></ult:ULTCLIENTESVERIFICA></soapenv:Body>"
            sBody &= "</soapenv:Envelope>"

            Dim wsHttpWebRequest As System.Net.HttpWebRequest
            wsHttpWebRequest = CType(System.Net.WebRequest.Create(strUrl), System.Net.HttpWebRequest)
            wsHttpWebRequest.Headers.Add("SOAPAction", "")
            wsHttpWebRequest.ContentType = "text/xml;charset=UTF-8"
            wsHttpWebRequest.Method = "POST"
            Dim encoding As New System.Text.UTF8Encoding
            'LogWriter.WriteLog("Info-Request", sBody)
            Dim data As Byte() = encoding.GetBytes(sBody)
            wsHttpWebRequest.ContentLength = data.Length

            Dim newStream As System.IO.Stream = wsHttpWebRequest.GetRequestStream()
            newStream.Write(data, 0, data.Length)
            newStream.Close()
            Dim myRes As System.Net.WebResponse = wsHttpWebRequest.GetResponse()
            Dim respStream As System.IO.Stream = myRes.GetResponseStream()
            Dim reader As System.IO.StreamReader = New System.IO.StreamReader(respStream, System.Text.Encoding.UTF8)
            Dim strXml As String = reader.ReadToEnd

            'LogWriter.WriteLog("Info-Response", strXml)
            Dim xmlDoc As New XmlDocument
            xmlDoc.LoadXml(strXml)
            Try
                ' Datos
                ' Anterior Servicio
                Dim node As XmlNode = xmlDoc.LastChild.FirstChild.FirstChild.LastChild.FirstChild.FirstChild
                'strNombre = node.Item("COLSOCIALNAME").InnerText
                'strEmail = node.Item("EMAIL1").InnerText
                'strCiudad = node.Item("CITYNAME").InnerText
                'strPais = node.Item("COUNTRYNAME").InnerText
                ' Nuevo Servicio
                strNombre = node.Item("ns4:COLSOCIALNAME").InnerText
                strEmail = node.Item("ns4:EMAIL1").InnerText
                strCiudad = node.Item("ns4:CITYNAME").InnerText
                strPais = node.Item("ns4:COUNTRYNAME").InnerText
                bActivo = True
            Catch err As Exception
                LogWriter.WriteLog("InterfacesWS", err)
            End Try
            myRes.Close()
            wsHttpWebRequest = Nothing
            bResultado = True
        Catch err As Exception
            LogWriter.WriteLog("InterfacesWS", err)
            strError = err.Message
            bResultado = False
        End Try
        Return bResultado
    End Function

    Public Function ConsultaVIGIA(ByVal login As String, ByVal strNombre As String, ByVal numIdentificacion As String, ByRef numLista As String, ByRef strUrl As String, ByRef strError As String) As Boolean
        Dim bResultado As Boolean
        Dim wsLisEsp As New VIGIA.ConsultaIndividual
        wsLisEsp.Timeout = AppSettings("CentinelaTimeOut")
        wsLisEsp.Url = AppSettings("ServicioListasEspeciales")
        Try
            Dim strUsuario As String = IIf(AppSettings("UserLogged") = "0", AppSettings("UserNameVIGIA"), login)
            Dim strClave As String = AppSettings("PasswordVIGIA")
            Dim strXml As String
            Dim numResultado As Integer
            Dim numPorcentaje As Integer
            Dim strResultado As String
            Dim xdoc As New XmlDocument
            strXml = "<REQUEST_DEROGATORIOS>"
            strXml &= "<NOMBRECOMPLETO>" & strNombre & "</NOMBRECOMPLETO>"
            strXml &= "<NUMIDENTIFICACION>" & numIdentificacion & "</NUMIDENTIFICACION>"
            strXml &= "<USUARIO>" & strUsuario & "</USUARIO>"
            strXml &= "<CLAVE>" & strClave & "</CLAVE>"
            strXml &= "</REQUEST_DEROGATORIOS>"
            ' <RESPONSE_DEROGATORIOS>
            '  <RESULTADO>
            '     0,1,2
            '            '    0: NO HUBO COINCIDENCIA
            '            '    1: COINCIDENCIA POR NOMBRE
            '            '    2: COINCIDNECIA POR IDENTIFICACION
            '            '    3: ERROR EN ENTRADA
            '            '   4: NO AUTORIZADO
            '</RESULTADO>
            '  <CAMPOCOINCIDENCIA>
            '         VACIO, IDENTIFICACION, NOMBRE
            '</CAMPOCOINCIDENCIA>
            '  <PORCENTAJECOINCIDENCIA>
            '     100, 0.100
            '</PORCENTAJECOINCIDENCIA>
            '  <URLPDF>
            '   URL PDF
            '   </URLPDF>
            '   <TIPOLISTA>
            '      CODIGO DEL TIPO 1 / RESTRICTIVA  2 / INFORMATIVA
            '</TIPOLISTA>
            ' <DETALLE>
            '            ' Registros de resultado de la consulta realizada
            '           <REGISTRO>
            '            <NOMBRE> </NOMBRE>
            '            <OBSERVACION> </OBSERVACION>
            '            <LISTA> </LISTA>
            '            </REGISTRO> ………..
            '</DETALLE>
            '</RESPONSE_DEROGATORIOS>
            strResultado = wsLisEsp.CallConsultaIndividual(strXml)
            xdoc.LoadXml(strResultado)
            numResultado = xdoc.SelectSingleNode("//RESPONSE_DEROGATORIOS/RESULTADO").InnerText
            strUrl = wsLisEsp.Url.Substring(0, wsLisEsp.Url.Replace("//", String.Empty).IndexOf("/") + 2)
            strUrl &= xdoc.SelectSingleNode("//RESPONSE_DEROGATORIOS/URLPDF").InnerText
            If numResultado = 0 Then
                numLista = 0 ' SIN COINCIDENCIA
            Else
                numPorcentaje = xdoc.SelectSingleNode("//RESPONSE_DEROGATORIOS/PORCENTAJECOINCIDENCIA").InnerText
                If numPorcentaje = 100 Then
                    numLista = 1 ' TOTAL
                Else
                    numLista = 2 ' PARCIAL
                End If
            End If
            bResultado = True
        Catch err As Exception
            LogWriter.WriteLog("InterfacesWS", err)
            bResultado = False
            strError = err.Message
        End Try
        Return bResultado
    End Function

End Class