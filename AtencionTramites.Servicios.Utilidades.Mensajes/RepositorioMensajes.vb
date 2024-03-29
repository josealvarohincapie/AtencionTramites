﻿Namespace AtencionTramites.Servicios.Utilidades.Mensajes
    Module RepositorioMensajes
        Public ReadOnly MSG_BIENVENIDA As String = "Bienvenidos"
        Public ReadOnly MCU188_01 As String = "Ya existe el Codigo o el Nombre para un parametro ingresado en el sistema"
        Public ReadOnly MCOT0002 As String = "Es necesario validar %Nombre de Campo% ya que presenta %error de estructura de datos%, por favor verifique."
        Public ReadOnly MCU188_02_01 As String = "Ya existe otro parámetro creado con el mismo Nombre."
        Public ReadOnly MCU188_02_02 As String = "Ya existe otro parámetro creado con el mismo Código."
        Public ReadOnly MCU0188_04_01 As String = "La 'Fecha Desde' debe ser menor o igual a la 'Fecha Hasta'"
        Public ReadOnly MCU0188_04_02 As String = "No existen registros de log coincidentes con los parámetros ingresados."
        Public ReadOnly MCU0182_01 As String = "Ya existe otro radicador con la misma identificación."
        Public ReadOnly MCU0182_02 As String = "No se encuentra registrado un colaborador con esa información."
        Public Const GEN_0001 As String = "No existe la entidad {0} con el Identificador {1}. Verificar {0}"
        Public Const COL_0001 As String = "El Colaborador no existe en base de datos. Verificar Identificación del Colaborador"
        Public Const COL_0002 As String = "El Contrato que desea eliminar, no está asociado al Colaborador. Verificar Contrato"
        Public Const COL_0003 As String = "La Configuración que desea agregar no existe en un Catálogo Entidad. Verificar Configuración"
        Public Const COL_0004 As String = "El Item que desea agregar, no está asociado a la Configuración. Verificar Item Configuración"
        Public Const COL_0005 As String = "La Configuración seleccionada, no está asociada al Colaborador. Verificar Configuración"
        Public Const COL_0006 As String = "La Configuración que desea eliminar, no está asociado al Colaborador. Verificar Configuración"
        Public Const COL_0007 As String = "El Item de la Configuración que desea eliminar, no está asociado al Colaborador. Verificar Item Configuración"
        Public Const COL_0008 As String = "El Contrato que desea actualizar, no está asociado al Colaborador. Verificar Contrato"
        Public Const COL_0009 As String = "No se encontraron Colaborador/es para el criterio de búsqueda ingresado."
        Public Const COL_0010 As String = "La Configuración que está consultado no existe. Verifique Configuración."
        Public Const COL_0011 As String = "La Configuración que está consultando no está asociada al Colaborador. Verifique Configuración."
        Public Const COL_0012 As String = "Ya existe un Colaborador con el Número de Identificación y Tipo de Identificación indicado. "
        Public Const COL_0013 As String = "Número de Identificación y Tipo de Identificación ya existen combinados previamente en base de datos. Verifique los datos modificados."
        Public Const COL_0014 As String = "La persona que está intentando asociar al grupo familiar, no existe en la base de datos del Cliente. Verifique la combinación: LegalId y Tipo de Identificación."
        Public Const RAD_0001 As String = "El Colaborador no existe en base de datos. Verificar Identificación del Colaborador"
    End Module
End Namespace
