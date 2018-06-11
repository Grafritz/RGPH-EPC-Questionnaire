
Imports System.Text.RegularExpressions

Public Enum _GroupeEnum
    Administration = 1
    SecretaireReunion = 2
    Participant = 3
    President_de_reunion = 4
End Enum

Public Enum _TypeDossier
    DETENTION = 1
    PLAINTE = 2
End Enum

Public Enum _SexeEnum
    HOMME = 1
    FEMME = 2
    GARCON = 3
    FILLE = 4
End Enum

Public Enum _StatutDossierEnum
    Ouvert = 1
    Ferme = 2
    Classe_sans_suit = 3
End Enum
Public Enum _StatutPlainteEnum
    Ouverte = 1
    Ferme_resolue = 2
    ferme_non_resolue = 3
End Enum
Public Enum _RecevabiliteEnum
    Recevable = 1
    Irrecevable = 2
    Aletude = 3
End Enum
Public Enum _StatutRecommandationEnum
    EnCours = 1
    Validee = 2
    Rejetee = 3
End Enum

Public Enum _StatutSuiviEnum
    ARealise = 1
    Realise = 2
    NonRealise = 3
End Enum

Public Class Cls_Enumeration


    ''' <summary>Removes the tags from an HTML document.</summary>
    ''' <param name="htmlText">HTML text to parse.</param>
    ''' <returns>The text of an HTML document without tags.</returns>
    ''' <remarks></remarks>
    ''' 
    Public Shared Function GetTextFromHtml(ByVal htmlText As String) As String
        Dim output As String = Regex.Replace(htmlText, "\<[^\>]+\>", "")
        Return output
    End Function

    Public Enum _TypeDossier
        DETENTION = 1
        PLAINTE = 2
    End Enum

    Public Enum TypeQuestion
        QUESTION_CHOIX = 1
        QUESTION_SAISIE = 2
        QUESTION_DATE_MM_AAAA = 3
        QUESTION_CHOIX_COMBO_2 = 4
        QUESTION_CHOIX_PAYS = 5
        QUESTION_CHOIX_COMMUNE = 6
        QUESTION_CHOIX_SECTION_COMMUNALE = 7
        QUESTION_CHOIX_DOMAINE_ETUDE = 8
        QUESTION_CHOIX_INDIVIDU = 9
        QUESTION_CHOIX_ADD_INDIVIDU = 10
        QUESTION_DATE_JJ_MM_AAAA = 11
        QUESTION_NBR_APPAREIL_ET_ANIMAUX = 12
        QUESTION_NBR_GARCON_ET_FILLE = 13
        QUESTION_CHOIX_COMBO1_ET_COMBO2 = 14
        QUESTION_CHOIX_LIER_COMBO1_ET_COMBO2 = 15
        QUESTION_CHOIX_AGE = 16
        QUESTION_CHOIX_LISTE_BOX = 17
        QUESTION_CHOIX_LISTE_BOX_PAYS = 18
        QUESTION_CHOIX_LISTE_BOX_AGE = 19
        QUESTION_CHOIX_LISTE_BOX_DOMAINE_ETUDE = 20
        QUESTION_CHOIX_COMBO1_ET_LISTE_BOX_LIER = 21
        QUESTION_CHOIX_NUMBER_LISTE_BOX = 22
    End Enum

    Public Shared Sexe_String_List() As String = {"Homme", "Femme", "Garçon", "Fille"}
    Public Shared Function Sexe_String(ByVal Statut As Long)
        Dim _Str As String = ""
        Select Case Statut
            Case _SexeEnum.HOMME
                _Str = "Homme"
            Case _SexeEnum.FEMME
                _Str = "Femme"
            Case _SexeEnum.GARCON
                _Str = "Garcon"
            Case _SexeEnum.FILLE
                _Str = "Fille"
        End Select
        Return _Str
    End Function

    Public Shared Sexe_Normal_List() As String = {"Homme", "Femme"}
    Public Shared Function Sexe_Normal(ByVal _id As Long)
        Dim _Str As String = ""
        Select Case _id
            Case _SexeEnum.HOMME
                _Str = "Homme"
            Case _SexeEnum.FEMME
                _Str = "Femme"
        End Select
        Return _Str
    End Function

    Public Shared StatutDossier_List() As String = {"Ouvert", "Fémer", "Classé sans suit"}
    Public Shared Function StatutDossier(ByVal _id As Long)
        Dim _Str As String = ""
        Select Case _id
            Case _StatutDossierEnum.Ouvert
                _Str = "Ouvert"
            Case _StatutDossierEnum.Ferme
                _Str = "Fémer"
            Case _StatutDossierEnum.Classe_sans_suit
                _Str = "Classé sans suit"
        End Select
        Return _Str
    End Function

    Public Shared StatutPlainte_List() As String = {"Ouverte", "Fémée résolue", "Fémée non résolue"}
    Public Shared Function StatutPlainte(ByVal _id As Long)
        Dim _Str As String = ""
        Select Case _id
            Case _StatutPlainteEnum.Ouverte
                _Str = "Ouverte"
            Case _StatutPlainteEnum.Ferme_resolue
                _Str = "Fémée résolue"
            Case _StatutPlainteEnum.ferme_non_resolue
                _Str = "Fémée non résolue"
        End Select
        Return _Str
    End Function

    Public Shared Recevabilite_List() As String = {"Recevable", "Irrecevable", "A l'étude"}
    Public Shared Function Recevabilite(ByVal _id As Long)
        Dim _Str As String = ""
        Select Case _id
            Case _RecevabiliteEnum.Recevable
                _Str = "Recevable"
            Case _RecevabiliteEnum.Irrecevable
                _Str = "Irrecevable"
            Case _RecevabiliteEnum.Aletude
                _Str = "A l'étude"
        End Select
        Return _Str
    End Function

    Public Shared StatutRecommandation_List() As String = {"En Cours", "Validée", "Rejetée"}
    Public Shared Function StatutRecommandation(ByVal _id As Long)
        Dim _Str As String = ""
        Select Case _id
            Case _StatutRecommandationEnum.EnCours
                _Str = "En Cours"
            Case _StatutRecommandationEnum.Validee
                _Str = "Validée"
            Case _StatutRecommandationEnum.Rejetee
                _Str = "Rejetée"
        End Select
        Return _Str
    End Function

    Public Shared StatutSuivi_List() As String = {"A Réalisé", "Réalisé", "Non Réalisé"}
    Public Shared Function StatutSuivi(ByVal _id As Long)
        Dim _Str As String = ""
        Select Case _id
            Case _StatutSuiviEnum.ARealise
                _Str = "A Réalisé"
            Case _StatutSuiviEnum.Realise
                _Str = "Réalisé"
            Case _StatutSuiviEnum.NonRealise
                _Str = "Non Réalisé"
        End Select
        Return _Str
    End Function

    Public Shared Function IconDocument(ByVal _ExtensionDoc As String) As String
        Dim val As String = ""
        Select Case _ExtensionDoc.ToLower
            Case ".pdf", "application/pdf"
                val = "~/images/page_pdf.png"
            Case ".doc", "application/msword", ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                val = "~/images/page_word.png"
            Case ".xls", "application/vnd.ms-excel", ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                val = "~/images/page_excel.png"
            Case ".ppt", "application/vnd.ms-powerpoint", ".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"
                val = "~/images/page_powerpoint.png"
            Case ".jpg", ".jpeg", "image/jpeg", ".png", "image/png", ".bmp", "image/bmp", ".gif", "image/gif"
                val = "~/images/page_Photo.png"
        End Select
        Return val
    End Function


    Public Enum TypeEvaluation
        Entrainement_1 = 1
        Formative_2 = 2
        Sommative_3 = 3
        Observation_4 = 4
    End Enum

    Public Shared Function Get_TypeEvaluation() As List(Of Cls_KeyValue)
        Try
            Dim objs As New List(Of Cls_KeyValue)
            objs.Add(New Cls_KeyValue("Entraînement", "" & TypeEvaluation.Entrainement_1))
            objs.Add(New Cls_KeyValue("Formative", "" & TypeEvaluation.Formative_2))
            objs.Add(New Cls_KeyValue("Sommative", "" & TypeEvaluation.Sommative_3))
            objs.Add(New Cls_KeyValue("Observation", "" & TypeEvaluation.Observation_4))
            Return objs
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Get_TypeEvaluation_String(ByVal IDTypeEvaluation As Long) As String
        Try
            Dim val As String = ""
            Dim objs As List(Of Cls_KeyValue) = Get_TypeEvaluation()
            For Each elem As Cls_KeyValue In objs
                If elem.FieldValue.Equals("" & IDTypeEvaluation) Then
                    val = elem.FieldName
                    Exit For
                End If
            Next
            Return val
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class