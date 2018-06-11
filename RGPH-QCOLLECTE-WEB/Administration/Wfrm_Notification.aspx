<%@ Page Title=".: Rezo509 - Notification :." ValidateRequest="false" Language="VB" MasterPageFile="~/MasterPages/MasterPage_Security.master" AutoEventWireup="false" CodeFile="Wfrm_Notification.aspx.vb" Inherits="Administration_Wfrm_Notification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function ShowAddUpdateForm(strPage, tmpW, tmpH) {
                var oWindow = window.radopen(strPage, "AddUpdateDialog");
                //oWindow.set_autoSize(true);
                oWindow.SetSize(tmpW, tmpH);
                document.getElementById("txtWindowPage").value = strPage;
                if (oWindow) {
                    if (!oWindow.isClosed()) {
                        oWindow.center();
                        var bounds = oWindow.getWindowBounds();
                        oWindow.moveTo(bounds.x + 'px', "50px");
                    }
                }
                return false;
            }
            function RadWindowClosing() {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Reload");
            }

            function RadWindowClientResizeEnd() {
                var manager = GetRadWindowManager();
                var window1 = manager.getActiveWindow();
                window1.center();
                var bounds = window1.getWindowBounds();
                window1.moveTo(bounds.x + 'px', "50px");
            }

            function refreshMe() {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Reload");
            }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="LabelPhotoAll" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
    <Msg:msgBox ID="Dialogue" runat="server" />
    <!-- DEBUT FORMAT TEXT AREA -->
    <script type="text/javascript" src="../JavaScript/tiny_mce_4.0/tinymce.min.js"></script>
    <script type="text/javascript">
        tinymce.init({
            selector: "textarea",
            plugins: [
                        "advlist autolink autosave link image lists charmap print preview hr anchor pagebreak spellchecker",
                        "searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking",
                        "table contextmenu directionality emoticons template textcolor paste textcolor"
            ],

            toolbar1: "BtnNameDest | newdocument fullpage | bold italic underline strikethrough | forecolor backcolor | alignleft aligncenter alignright alignjustify | table | bullist numlist | styleselect formatselect fontselect fontsizeselect |searchreplace | link unlink anchor image media inserttime | charmap emoticons | print fullscreen code preview |  hr removeformat | subscript superscript | ltr rtl | spellchecker | visualchars visualblocks nonbreaking template pagebreak restoredraft",
            //toolbar1: "newdocument fullpage | bold italic underline strikethrough | alignleft aligncenter alignright alignjustify | styleselect formatselect fontselect fontsizeselect",
            //toolbar2: "cut copy paste | searchreplace | bullist numlist | outdent indent blockquote | undo redo | link unlink anchor image media code | inserttime preview | forecolor backcolor",
            //toolbar3: "table | hr removeformat | subscript superscript | charmap emoticons | print fullscreen | ltr rtl | spellchecker | visualchars visualblocks nonbreaking template pagebreak restoredraft",

            menubar: false,
            toolbar_items_size: 'small',

            style_formats: [
                        { title: 'Bold text', inline: 'b' },
                        { title: 'Red text', inline: 'span', styles: { color: '#ff0000' } },
                        { title: 'Red header', block: 'h1', styles: { color: '#ff0000' } },
                        { title: 'Example 1', inline: 'span', classes: 'example1' },
                        { title: 'Example 2', inline: 'span', classes: 'example2' },
                        { title: 'Table styles' },
                        { title: 'Table row 1', selector: 'tr', classes: 'tablerow1' }
            ],

            templates: [
                        { title: 'Test template 1', content: 'Test 1' },
                        { title: 'Test template 2', content: 'Test 2' }
            ],
            setup: function (ed) {
                // Add a custom button
                ed.addButton('BtnNameDest', {
                    title: 'Ajouter le Nom du destinataire',
                    image: '../Images/user_.png',
                    onclick: function () {
                        // Add you own code to execute something on click
                        ed.focus();
                        ed.selection.setContent('_NOMCOMPLET');
                    }
                });
            }
        });
    </script>
    <!-- END FORMAT TEXT AREA -->
    
    <section class="content-header">
        <h1>
            <i class="fa  fa-bullhorn"></i>
            <asp:Label ID="Label_Titre" runat="server" Text="NOTIFICATION " />
            <small>
                <asp:Label ID="LabelNumeroDeReference" runat="server" /></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Accueil</a></li>
            <li class="active">NOTIFICATION</li>
        </ol>
    </section>
    
    <section class="content" style="padding-top: 0px;">    
            <Msg:msgBox ID="MsgBox1" runat="server" />
            <asp:Panel runat="server" ID="Panel_Msg" Visible="false">
                <div id="DIV_Msg" runat="server" class="alert alert-warning alert-dismissable">
                    <i id="Icon_Msg" runat="server" class="fa fa-warning"></i>
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                    <asp:Image ID="Image_Msg" runat="server" />
                    <asp:Label ID="Label_Msg" runat="server" Text=""></asp:Label>
                </div>
            </asp:Panel>
        
                <!-- DEBUT Notification Send Email -->
                <asp:Panel ID="Panel_NotificationSendEmail" Visible="true" runat="server" > 
                    
                <div class="box-body">
                    <div class="form-group">
                        <div class="col-sm-12">
                            <label>
                                <asp:Label ID="Label10" runat="server" >De :</asp:Label>
                                <asp:Label ID="Label_VotreEmail" runat="server" Text="info@rezo509.com" Style="color:green;" />
                            </label>
                        </div>
                        
                        <div class="col-sm-12">
                            <label>
                                        <asp:Label ID="Label6" runat="server" Text="Email destination(à):" />
                                        <asp:Label ID="Label_EmailDestination" runat="server" Style="font-weight: 700" Text="0" />
                                        <asp:RequiredFieldValidator ID="RFVtxt_EmailTo" runat="server" ControlToValidate="txt_EmailTo"
                                            ErrorMessage="Adresse email destination obligatoire" SetFocusOnError="True" 
                                            ValidationGroup="GPSendNotification" Style="color: #FF0000" />
                                        <%--<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="Server"
                                            HighlightCssClass="validatorCalloutHighlight" TargetControlID="RFVtxt_EmailTo" />--%>
                            </label>
                            <asp:TextBox ID="txt_EmailTo" runat="server" CssClass="form-control" Width="100%" />
                        </div>
                        
                        <div class="col-sm-12">
                            <label>
                                <asp:Label ID="Label12" runat="server" >Messages :</asp:Label>
                            </label>
                            <asp:TextBox ID="txt_Notes" runat="server" TextMode="MultiLine" CssClass="form-control"
                                            Style="width: 100%; height: 200px;">
                                        </asp:TextBox>
                        </div>
                    </div>
                </div> 
                    <div style="clear: both;"></div>
                    
                <!-- /.box-body -->
                <div class="box-footer">
                    <asp:LinkButton ID="Btn_SendNotification" runat="server" CssClass="btn btn-primary"
                        ValidationGroup="GPSendNotification">
                     <i class="fa fa-save" ></i> Envoyer la notification
                    </asp:LinkButton>
                    
                    &nbsp;
                    <asp:LinkButton ID="Btn_Annuler" CausesValidation="false" runat="server"
                        CssClass="btn btn-primary">
                         <i class="fa  fa-reply-all" ></i> Annuler
                    </asp:LinkButton>
                </div>
                <!-- /.box-footer--> 
                </asp:Panel>
                <!-- FIN -->
    </section>
</asp:Content>

