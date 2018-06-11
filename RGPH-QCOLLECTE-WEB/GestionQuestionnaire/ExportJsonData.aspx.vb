Imports Microsoft
Imports System.Data
Imports System.Collections.Generic
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer
Imports Telerik.Web.UI
Imports RGPH_QCOLLECTE_Library

Partial Class GestionQuestionnaire_ExportJsonData
    Inherits System.Web.UI.Page

    Dim User_Connected As Cls_User          ' INSTANCE DE LA CLASSE UTILISATEUR - UTILISER POUR L'UTILISATEUR EN SESSION 

    Private Sub GestionQuestionnaire_ExportJsonData_Load(sender As Object, e As EventArgs) Handles Me.Load
        LOAD_ALL_DATA()
    End Sub

#Region "Other Method - MessageToShow"
    Private Sub MessageToShow(ByVal _message As String, Optional ByVal E_or_S As String = "E", Optional ByVal ShowPopUp As Boolean = True)
        Panel_Msg.Visible = True
        GlobalFunctions.Message_Image(Image_Msg, E_or_S)
        Label_Msg.Text = _message
        If ShowPopUp Then
            'RadAjaxManager1.ResponseScripts.Add("alert('" & [Global].GetTextFromHtml(_message).Replace("'", "\'") & "');")
            'Dialogue.alert([Global].GetTextFromHtml(_message))
        End If
        If E_or_S = "S" Then
            Style_Division(DIV_Msg, "alert alert-success alert-dismissable")
            Style_Division(Icon_Msg, "fa  fa-thumbs-up")
        Else
            Style_Division(DIV_Msg, "alert alert-danger alert-dismissable")
            Style_Division(Icon_Msg, "fa  fa-thumbs-down")
        End If
    End Sub
#End Region

#Region "Load DATA"
    Private Sub LOAD_ALL_DATA()
        Try
            Dim _action As String = Request.QueryString([Global].ACTION)
            Select Case _action
                Case [Global].DATA_MODULE
                    GetAll_DATA_MODULE()

                Case [Global].DATA_QUESTION
                    GetAll_DATA_QUESTION()

                Case [Global].DATA_REPONSE
                    GetAll_DATA_REPONSE()

                Case [Global].DATA_CATEGORIE_QUESTION
                    GetAll_DATA_CATEGORIE_QUESTION()

                Case [Global].DATA_QUESTION_MODULE
                    GetAll_DATA_QUESTION_MODULE()

                Case [Global].DATA_MODULE_SESSION
                    GetAll_DATA_MODULE_SESSION()

                Case [Global].DATA_QUESTION_MODULE_SESSION
                    GetAll_DATA_QUESTION_MODULE_SESSION()

                Case [Global].DATA_MODULE_QUESTION_SESSION
                    GetAll_DATA_QUESTION_SESSION()

                Case [Global].DATA_MODULE_REPONSES_SESSION
                    GetAll_DATA_REPONSES_SESSION()

            End Select
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            '[Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Public Shared Function TypeSafeConvertion(ByVal Val As Boolean) As Integer
        Dim _val As Long
        If Val Then
            _val = 1
        Else
            _val = 0
        End If
        Return _val
    End Function

    Private Sub GetAll_DATA_MODULE()
        Dim objs As New List(Of Cls_Module)
        Dim Result As String = ""
        Dim ValJson As String = ""
        Try
            'tbl_module.json
            objs = Cls_Module.SearchAll
            With objs
                If .Count > 0 Then
                    For Each item As Cls_Module In objs
                        If Result.Equals("") Then
                            Result = "{"
                            Result &= Chr(13)
                            Result &= """codeModule"":""" & item.CodeModule.Replace("""", "\""") & """"
                            Result &= Chr(13)
                            Result &= ",""nomModule"":""" & item.NomModule.Replace("""", "\""") & """"
                            Result &= Chr(13)
                            Result &= ",""typeModule"":" & item.TypeModule & ""
                            Result &= Chr(13)
                            Result &= ",""description"":""" & item.Description.Replace("""", "\""") & """"
                            Result &= Chr(13)
                            Result &= ",""estActif"":" & IIf(item.EstActif, "true", "false") & ""
                            Result &= Chr(13)
                            Result &= "}"
                        Else
                            Result &= Chr(13)
                            Result &= ",{"
                            Result &= Chr(13)
                            Result &= """codeModule"":""" & item.CodeModule.Replace("""", "\""") & """"
                            Result &= Chr(13)
                            Result &= ",""nomModule"":""" & item.NomModule.Replace("""", "\""") & """"
                            Result &= Chr(13)
                            Result &= ",""typeModule"":" & item.TypeModule & ""
                            Result &= Chr(13)
                            Result &= ",""description"":""" & item.Description.Replace("""", "\""") & """"
                            Result &= Chr(13)
                            Result &= ",""estActif"":" & IIf(item.EstActif, "true", "false") & ""
                            Result &= Chr(13)
                            Result &= "}"
                        End If
                    Next

                    ValJson &= "["
                    ValJson &= Chr(13)
                    ValJson &= Result
                    ValJson &= Chr(13)
                    ValJson &= "]"

                    Response.Buffer = True
                    Response.Charset = ""
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.ContentType = "application/octet-stream"
                    Response.AddHeader("content-disposition", "attachment;filename=tbl_module.json")

                    Response.Write(ValJson)

                    Response.Flush()
                    Response.End()
                End If
            End With


        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            '[Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Private Sub GetAll_DATA_CATEGORIE_QUESTION()
        Dim objs As New List(Of Cls_CategorieQuestion)
        Dim Result As String = ""
        Dim ValJson As String = ""
        Try
            'tbl_categorie_question.json
            objs = Cls_CategorieQuestion.SearchAll
            With objs
                If .Count > 0 Then
                    For Each item As Cls_CategorieQuestion In objs
                        If Result.Equals("") Then
                            Result = "{"
                            Result &= Chr(13)
                            Result &= """codeCategorie"":""" & item.CodeCategorie.Replace("""", "\""") & """"
                            Result &= Chr(13)
                            Result &= ",""categorieQuestion"":""" & item.CategorieQuestion.Replace("""", "\""") & """"
                            Result &= Chr(13)
                            Result &= ",""detailsCategorie"":""" & item.DetailsCategorie.Replace("""", "\""") & """"
                            Result &= Chr(13)
                            Result &= ",""sousDetailsCategorie"":""" & item.SousDetailsCategorie.Replace("""", "\""") & """"
                            Result &= Chr(13)
                            Result &= "}"
                        Else
                            Result &= Chr(13)
                            Result &= ",{"
                            Result &= Chr(13)
                            Result &= """codeCategorie"":""" & item.CodeCategorie.Replace("""", "\""") & """"
                            Result &= Chr(13)
                            Result &= ",""categorieQuestion"":""" & item.CategorieQuestion.Replace("""", "\""") & """"
                            Result &= Chr(13)
                            Result &= ",""detailsCategorie"":""" & item.DetailsCategorie.Replace("""", "\""") & """"
                            Result &= Chr(13)
                            Result &= ",""sousDetailsCategorie"":""" & item.SousDetailsCategorie.Replace("""", "\""") & """"
                            Result &= Chr(13)
                            Result &= "}"
                        End If
                    Next

                    ValJson &= "["
                    ValJson &= Chr(13)
                    ValJson &= Result
                    ValJson &= Chr(13)
                    ValJson &= "]"

                    Response.Buffer = True
                    Response.Charset = ""
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.ContentType = "application/octet-stream"
                    Response.AddHeader("content-disposition", "attachment;filename=tbl_categorie_question.json")

                    Response.Write(ValJson)

                    Response.Flush()
                    Response.End()
                End If
            End With


        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            '[Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Private Sub GetAll_DATA_QUESTION()
        Dim objs As New List(Of Cls_Questions)
        Dim Result As String = ""
        Dim ValJson As String = ""
        Try
            'tbl_questions.json
            objs = Cls_Questions.SearchAll
            With objs
                If .Count > 0 Then
                    For Each item As Cls_Questions In objs
                        If Result.Equals("") Then
                            Result = getStringJSON_Question(item)
                        Else
                            Result &= Chr(13)
                            Result &= "," & getStringJSON_Question(item)
                        End If
                    Next

                    ValJson &= "["
                    ValJson &= Chr(13)
                    ValJson &= Result
                    ValJson &= Chr(13)
                    ValJson &= "]"

                    Response.Buffer = True
                    Response.Charset = ""
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.ContentType = "application/octet-stream"
                    Response.AddHeader("content-disposition", "attachment;filename=tbl_questions.json")

                    Response.Write(ValJson)

                    Response.Flush()
                    Response.End()
                End If
            End With


        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            '[Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Private Sub GetAll_DATA_QUESTION_MODULE()
        Dim objs As New List(Of Cls_Question_Module)
        Dim Result As String = ""
        Dim ValJson As String = ""
        Try
            'tbl_questions_module.json
            objs = Cls_Question_Module.SearchAll
            With objs
                If .Count > 0 Then
                    For Each item As Cls_Question_Module In objs
                        If Result.Equals("") Then
                            Result = "{"
                            Result &= Chr(13)
                            Result &= """ordre"":""" & item.ID & """"
                            Result &= Chr(13)
                            Result &= ",""codeModule"":""" & item.CodeModule.Replace("""", "\""") & """"
                            Result &= Chr(13)
                            Result &= ",""codeQuestion"":""" & item.CodeQuestion.Replace("""", "\""") & """"
                            Result &= Chr(13)
                            Result &= ",""estDebut"":" & IIf(item.EstDebut, "true", "false") & ""
                            Result &= Chr(13)
                            Result &= "}"
                        Else
                            Result &= Chr(13)
                            Result &= ",{"
                            Result &= Chr(13)
                            Result &= """ordre"":""" & item.ID & """"
                            Result &= Chr(13)
                            Result &= ",""codeModule"":""" & item.CodeModule.Replace("""", "\""") & """"
                            Result &= Chr(13)
                            Result &= ",""codeQuestion"":""" & item.CodeQuestion.Replace("""", "\""") & """"
                            Result &= Chr(13)
                            Result &= ",""estDebut"":" & IIf(item.EstDebut, "true", "false") & ""
                            Result &= Chr(13)
                            Result &= "}"
                        End If
                    Next

                    ValJson &= "["
                    ValJson &= Chr(13)
                    ValJson &= Result
                    ValJson &= Chr(13)
                    ValJson &= "]"

                    Response.Buffer = True
                    Response.Charset = ""
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.ContentType = "application/octet-stream"
                    Response.AddHeader("content-disposition", "attachment;filename=tbl_questions_module.json")

                    Response.Write(ValJson)

                    Response.Flush()
                    Response.End()
                End If
            End With


        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            '[Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Private Sub GetAll_DATA_REPONSE()
        Dim objs As New List(Of Cls_Questions_Reponses)
        Dim Result As String = ""
        Dim ValJson As String = ""
        Try
            'tbl_questions_reponses.json
            objs = Cls_Questions_Reponses.SearchAll
            With objs
                If .Count > 0 Then
                    For Each item As Cls_Questions_Reponses In objs
                        If Result.Equals("") Then
                            Result = getStringJSON_Reponse(item)
                        Else
                            Result &= Chr(13)
                            Result &= "," & getStringJSON_Reponse(item)
                        End If
                    Next

                    ValJson &= "["
                    ValJson &= Chr(13)
                    ValJson &= Result
                    ValJson &= Chr(13)
                    ValJson &= "]"

                    Response.Buffer = True
                    Response.Charset = ""
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.ContentType = "application/octet-stream"
                    Response.AddHeader("content-disposition", "attachment;filename=tbl_questions_reponses.json")

                    Response.Write(ValJson)

                    Response.Flush()
                    Response.End()
                End If
            End With


        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            '[Global].WriteError(ex, User_Connected)
        End Try
    End Sub

#Region "SESSION"

    Private Sub GetAll_DATA_MODULE_SESSION()
        Dim objs As New List(Of Cls_Module)
        Dim Result As String = ""
        Dim ValJson As String = ""
        Try
            'tbl_module.json
            'objs = Cls_Module.SearchAll
            If Session([Global].DATA_MODULE_SESSION) IsNot Nothing Then
                objs = CType(Session([Global].DATA_MODULE_SESSION), List(Of Cls_Module))
            End If
            With objs
                If .Count > 0 Then
                    For Each item As Cls_Module In objs
                        If Result.Equals("") Then
                            Result = getStringJSON_Module(item)
                        Else
                            Result &= Chr(13)
                            Result &= "," & getStringJSON_Module(item)
                        End If
                    Next

                    ValJson &= "["
                    ValJson &= Chr(13)
                    ValJson &= Result
                    ValJson &= Chr(13)
                    ValJson &= "]"

                    Response.Buffer = True
                    Response.Charset = ""
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.ContentType = "application/octet-stream"
                    Response.AddHeader("content-disposition", "attachment;filename=tbl_module.json")

                    Response.Write(ValJson)

                    Response.Flush()
                    Response.End()
                End If
            End With


        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            '[Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Private Sub GetAll_DATA_QUESTION_SESSION()
        Dim _questionList As New List(Of Cls_Questions)
        Dim Result As String = ""
        Dim ValJson As String = ""
        Try
            If Session([Global].DATA_MODULE_QUESTION_SESSION) IsNot Nothing Then
                _questionList = CType(Session([Global].DATA_MODULE_QUESTION_SESSION), List(Of Cls_Questions))
            End If
            'tbl_questions.json
            '_questionList = Cls_Questions.SearchAll
            With _questionList
                If .Count > 0 Then
                    For Each item As Cls_Questions In _questionList
                        If Result.Equals("") Then
                            Result = getStringJSON_Question(item)
                        Else
                            Result &= Chr(13)
                            Result &= "," & getStringJSON_Question(item)
                        End If
                    Next

                    ValJson &= "["
                    ValJson &= Chr(13)
                    ValJson &= Result
                    ValJson &= Chr(13)
                    ValJson &= "]"

                    Response.Buffer = True
                    Response.Charset = ""
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.ContentType = "application/octet-stream"
                    Response.AddHeader("content-disposition", "attachment;filename=tbl_questions.json")

                    Response.Write(ValJson)

                    Response.Flush()
                    Response.End()
                End If
            End With


        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            '[Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Private Sub GetAll_DATA_REPONSES_SESSION()
        Dim _reponseList As New List(Of Cls_Questions_Reponses)
        Dim Result As String = ""
        Dim ValJson As String = ""

        Try
            If Session([Global].DATA_MODULE_REPONSES_SESSION) IsNot Nothing Then
                _reponseList = CType(Session([Global].DATA_MODULE_REPONSES_SESSION), List(Of Cls_Questions_Reponses))
            End If
            'tbl_questions_reponses.json
            With _reponseList
                If .Count > 0 Then
                    For Each item As Cls_Questions_Reponses In _reponseList
                        If Result.Equals("") Then
                            Result = getStringJSON_Reponse(item)
                        Else
                            Result &= Chr(13)
                            Result &= "," & getStringJSON_Reponse(item)
                        End If
                    Next

                    ValJson &= "["
                    ValJson &= Chr(13)
                    ValJson &= Result
                    ValJson &= Chr(13)
                    ValJson &= "]"

                    Response.Buffer = True
                    Response.Charset = ""
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.ContentType = "application/octet-stream"
                    Response.AddHeader("content-disposition", "attachment;filename=tbl_questions_reponses.json")

                    Response.Write(ValJson)

                    Response.Flush()
                    Response.End()
                End If
            End With
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Private Sub GetAll_DATA_QUESTION_MODULE_SESSION()
        Dim objs As New List(Of Cls_Question_Module)
        Dim Result As String = ""
        Dim ValJson As String = ""
        Try
            'tbl_questions_module.json
            'objs = Cls_Question_Module.SearchAll
            If Session([Global].DATA_QUESTION_MODULE_SESSION) IsNot Nothing Then
                objs = CType(Session([Global].DATA_QUESTION_MODULE_SESSION), List(Of Cls_Question_Module))
            End If
            With objs
                If .Count > 0 Then
                    For Each item As Cls_Question_Module In objs
                        If Result.Equals("") Then
                            Result = getStringJSON_Question_Module(item)
                        Else
                            Result &= Chr(13)
                            Result &= "," & getStringJSON_Question_Module(item)
                        End If
                    Next

                    ValJson &= "["
                    ValJson &= Chr(13)
                    ValJson &= Result
                    ValJson &= Chr(13)
                    ValJson &= "]"

                    Response.Buffer = True
                    Response.Charset = ""
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.ContentType = "application/octet-stream"
                    Response.AddHeader("content-disposition", "attachment;filename=tbl_questions_module.json")

                    Response.Write(ValJson)

                    Response.Flush()
                    Response.End()
                End If
            End With


        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            '[Global].WriteError(ex, User_Connected)
        End Try
    End Sub
#End Region


    Private Function getStringJSON_Question_Module(item As Cls_Question_Module) As String
        Dim Result As String = "{"
        Result &= Chr(13)
        Result &= """ordre"":""" & item.ID & """"
        Result &= Chr(13)
        Result &= ",""codeModule"":""" & item.CodeModule.Replace("""", "\""") & """"
        Result &= Chr(13)
        Result &= ",""codeQuestion"":""" & item.CodeQuestion.Replace("""", "\""") & """"
        Result &= Chr(13)
        Result &= ",""estDebut"":" & IIf(item.EstDebut, "true", "false") & ""
        Result &= Chr(13)
        Result &= "}"

        Return Result
    End Function

    Private Function getStringJSON_Module(item As Cls_Module) As String
        Dim Result As String = "{"
        Result &= Chr(13)
        Result &= """codeModule"":""" & item.CodeModule.Replace("""", "\""") & """"
        Result &= Chr(13)
        Result &= ",""nomModule"":""" & item.NomModule.Replace("""", "\""") & """"
        Result &= Chr(13)
        Result &= ",""typeModule"":" & item.TypeModule & ""
        Result &= Chr(13)
        Result &= ",""description"":""" & item.Description.Replace("""", "\""") & """"
        Result &= Chr(13)
        Result &= ",""estActif"":" & IIf(item.EstActif, "true", "false") & ""
        Result &= Chr(13)
        Result &= "}"

        Return Result
    End Function

    Private Function getStringJSON_Question(item As Cls_Questions) As String
        Dim Result As String = "{"
        Result &= Chr(13)
        Result &= """codeQuestion"":""" & item.CodeQuestion.Replace("""", "\""") & """"
        Result &= Chr(13)
        Result &= ",""libelle"":""" & item.Libelle.Replace("""", "\""") & """"
        Result &= Chr(13)
        Result &= ",""detailsQuestion"":""" & item.DetailsQuestion.Replace("""", "\""") & """"
        Result &= Chr(13)
        Result &= ",""codeCategorie"":""" & item.CodeCategorie.Replace("""", "\""") & """"
        Result &= Chr(13)
        Result &= ",""nomChamps"":""" & item.NomChamps.Replace("""", "\""") & """"
        Result &= Chr(13)
        Result &= ",""typeQuestion"":" & item.TypeQuestion & ""
        Result &= Chr(13)
        Result &= ",""caratereAccepte"":" & item.CaratereAccepte & ""
        Result &= Chr(13)
        Result &= ",""nbreValeurMinimal"":" & item.NbreValeurMinimal & ""
        Result &= Chr(13)
        Result &= ",""nbreCaratereMaximal"":" & item.NbreCaratereMaximal & ""
        Result &= Chr(13)
        Result &= ",""estSautReponse"":" & IIf(item.EstSautReponse, "true", "false") & ""
        Result &= Chr(13)
        Result &= ",""qPrecedent"":""" & item.QPrecedent.Replace("""", "\""") & """"
        Result &= Chr(13)
        Result &= ",""qSuivant"":""" & item.QSuivant.Replace("""", "\""") & """"
        Result &= Chr(13)
        Result &= "}"

        Return Result
    End Function

    Private Function getStringJSON_Reponse(item As Cls_Questions_Reponses) As String
        Dim Result As String = "{"
        Result &= Chr(13)
        Result &= """codeQuestion"":""" & item.CodeQuestion.Replace("""", "\""") & """"
        Result &= Chr(13)
        Result &= ",""codeUniqueReponse"":""" & item.CodeUniqueReponse.Replace("""", "\""") & """"
        Result &= Chr(13)
        Result &= ",""codeReponse"":""" & item.CodeReponse.Replace("""", "\""") & """"
        Result &= Chr(13)
        Result &= ",""libelleReponse"":""" & item.LibelleReponse.Replace("""", "\""") & """"
        Result &= Chr(13)
        Result &= ",""estEnfant"":" & IIf(item.EstEnfant, "true", "false") & ""
        Result &= Chr(13)
        Result &= ",""avoirEnfant"":" & IIf(item.AvoirEnfant, "true", "false") & ""
        Result &= Chr(13)
        Result &= ",""codeParent"":""" & item.CodeParent.Replace("""", "\""") & """"
        Result &= Chr(13)
        Result &= ",""qPrecedent"":""" & item.QPrecedent.Replace("""", "\""") & """"
        Result &= Chr(13)
        Result &= ",""qSuivant"":""" & item.QSuivant.Replace("""", "\""") & """"
        Result &= Chr(13)
        Result &= "}"

        Return Result
    End Function

    Private Sub GetAll_DATA_MODULE_QUESTION_REPONSE_PAR_MODULE()
        Dim objs As New List(Of Cls_Questions)
        Dim Result As String = ""
        Dim ValJson As String = ""
        Try
            'tbl_questions.json
            objs = Cls_Questions.SearchAll
            With objs
                If .Count > 0 Then
                    For Each item As Cls_Questions In objs
                        If Result.Equals("") Then
                            Result = "{"
                            'Result &= Chr(13)
                            Result &= """codeQuestion"":""" & item.CodeQuestion.Replace("""", "\""") & """"
                            'Result &= Chr(13)
                            Result &= ",""libelle"":""" & item.Libelle.Replace("""", "\""") & """"
                            'Result &= Chr(13)
                            Result &= ",""detailsQuestion"":""" & item.DetailsQuestion.Replace("""", "\""") & """"
                            'Result &= Chr(13)
                            Result &= ",""codeCategorie"":""" & item.CodeCategorie.Replace("""", "\""") & """"
                            'Result &= Chr(13)
                            Result &= ",""nomChamps"":""" & item.NomChamps.Replace("""", "\""") & """"
                            'Result &= Chr(13)
                            Result &= ",""typeQuestion"":" & item.TypeQuestion & ""
                            'Result &= Chr(13)
                            Result &= ",""caratereAccepte"":" & item.CaratereAccepte & ""
                            'Result &= Chr(13)
                            Result &= ",""nbreValeurMinimal"":" & item.NbreValeurMinimal & ""
                            'Result &= Chr(13)
                            Result &= ",""nbreCaratereMaximal"":" & item.NbreCaratereMaximal & ""
                            'Result &= Chr(13)
                            Result &= ",""estSautReponse"":" & IIf(item.EstSautReponse, "true", "false") & ""
                            'Result &= Chr(13)
                            Result &= ",""qPrecedent"":""" & item.QPrecedent.Replace("""", "\""") & """"
                            'Result &= Chr(13)
                            Result &= ",""qSuivant"":""" & item.QSuivant.Replace("""", "\""") & """"
                            'Result &= Chr(13)
                            Result &= "}"
                        Else
                            Result &= Chr(13)
                            Result &= ",{"
                            'Result &= Chr(13)
                            Result &= """codeQuestion"":""" & item.CodeQuestion.Replace("""", "\""") & """"
                            'Result &= Chr(13)
                            Result &= ",""libelle"":""" & item.Libelle.Replace("""", "\""") & """"
                            'Result &= Chr(13)
                            Result &= ",""detailsQuestion"":""" & item.DetailsQuestion.Replace("""", "\""") & """"
                            'Result &= Chr(13)
                            Result &= ",""codeCategorie"":""" & item.CodeCategorie.Replace("""", "\""") & """"
                            'Result &= Chr(13)
                            Result &= ",""nomChamps"":""" & item.NomChamps.Replace("""", "\""") & """"
                            'Result &= Chr(13)
                            Result &= ",""typeQuestion"":" & item.TypeQuestion & ""
                            'Result &= Chr(13)
                            Result &= ",""caratereAccepte"":" & item.CaratereAccepte & ""
                            'Result &= Chr(13)
                            Result &= ",""nbreValeurMinimal"":" & item.NbreValeurMinimal & ""
                            'Result &= Chr(13)
                            Result &= ",""nbreCaratereMaximal"":" & item.NbreCaratereMaximal & ""
                            'Result &= Chr(13)
                            Result &= ",""estSautReponse"":" & IIf(item.EstSautReponse, "true", "false") & ""
                            'Result &= Chr(13)
                            Result &= ",""qPrecedent"":""" & item.QPrecedent.Replace("""", "\""") & """"
                            'Result &= Chr(13)
                            Result &= ",""qSuivant"":""" & item.QSuivant.Replace("""", "\""") & """"
                            'Result &= Chr(13)
                            Result &= "}"
                        End If
                    Next

                    ValJson &= "["
                    ValJson &= Chr(13)
                    ValJson &= Result
                    ValJson &= Chr(13)
                    ValJson &= "]"

                    Response.Buffer = True
                    Response.Charset = ""
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.ContentType = "application/octet-stream"
                    Response.AddHeader("content-disposition", "attachment;filename=tbl_questions.json")

                    Response.Write(ValJson)

                    Response.Flush()
                    Response.End()
                End If
            End With


        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            '[Global].WriteError(ex, User_Connected)
        End Try
    End Sub
#End Region

End Class
