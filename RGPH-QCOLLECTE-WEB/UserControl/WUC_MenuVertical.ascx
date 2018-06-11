<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WUC_MenuVertical.ascx.vb" Inherits="UserControl_WUC_MenuVertical" %>
<div id="SideBarNav_menu" runat="server" class="cl-sidebar">
    <div class="cl-sidebar" data-position="right" data-step="1" data-intro="<strong>Fixed Sidebar</strong> <br/> It adjust to your needs.">
        <div class="cl-toggle"><i class="fa fa-bars"></i></div>
        <div class="cl-navblock">
            <div class="menu-space">
                <div class="content">
                    <div class="side-user">
                        <div class="avatar">
                            <asp:Image ID="ImageParticipant" runat="server" Width="50px" AlternateText="" ImageUrl="~/CleanZone/images/avatar1_50.jpg"></asp:Image>
                        </div>
                        <div class="info">
                            <a href="#">
                                <asp:Label ID="LabelPrenomParticipant" runat="server" Text="IHSI" /></a>
                            <img src="../CleanZone/images/state_online.png" alt="Status" />
                            <span>Online</span>
                        </div>
                    </div>
                    <ul class="cl-vnavigation">
                        <li><a href="#"><i class="fa fa-home"></i><span>Dashboard</span></a>
                        </li>
                        <li runat="server" id="LIFrm_FormationListing1">
                            <a href="#"><i class="fa fa-folder-o"></i><span>Gestion Formation</span></a>
                            <ul class="sub-menu">
                                <li runat="server" id="LIFrm_FormationListing">
                                    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Formation/Frm_FormationListing.aspx">
                                <i class="fa fa-book"></i> Formation Planification
                                    </asp:HyperLink></li>
                                <li runat="server" id="LIFrm_PageFormateur">
                                    <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/Formation/Frm_PageFormateur.aspx">
                                <i class="fa fa-book"></i> Formation<i class="fa fa-angle-double-right"></i>Formateur
                                    </asp:HyperLink></li>
                                <li runat="server" id="LIFrm_PageResultatExamenParticipant">
                                    <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="~/Formation/Frm_PageResultatExamenParticipant.aspx">
                                <i class="fa fa-book"></i> Formation<i class="fa fa-angle-double-right"></i>Résultats
                                    </asp:HyperLink></li>
                                <li runat="server" id="LIFrm_PageEvaluationExamenParticipant">
                                    <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/Formation/Frm_PageEvaluationExamenParticipant.aspx">
                                <i class="fa fa-trophy"></i> Evaluation<i class="fa fa-angle-double-right"></i>Participant
                                    </asp:HyperLink></li>
                            </ul>
                        </li>
                        <li runat="server" id="LIParent_Frm_StatutListing">
                            <a href="#"><i class="fa fa-cogs"></i><span>Paramètre Formation</span></a>
                            <ul class="sub-menu">
                                <li runat="server" id="LIChild_Frm_StatutListing">
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Formation/Frm_StatutListing.aspx">
                                <i class="fa fa-gear"></i> Statut
                                    </asp:HyperLink></li>
                                <li runat="server" id="LIFrm_QuestionListing">
                                    <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/Formation/Frm_QuestionListing.aspx">
                                <i class="fa fa-gear"></i> Question Listing
                                    </asp:HyperLink></li>
                                <li runat="server" id="LIFrm_ExamenListing">
                                    <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Formation/Frm_ExamenListing.aspx">
                                <i class="fa fa-gear"></i> Examen Listing
                                    </asp:HyperLink></li>
                                <li runat="server" id="LIFrm_Formulaire_ExamenListing">
                                    <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/Formation/Frm_Formulaire_ExamenListing.aspx">
                                <i class="fa fa-gear"></i> Formulaire Examen Listing
                                    </asp:HyperLink></li>
                                <li runat="server" id="LIFrm_PlanificationFormulaireExamen_PersonnelListing">
                                    <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/Formation/Frm_PlanificationFormulaireExamen_PersonnelListing.aspx">
                                <i class="fa fa-gear"></i> Planification des Examens
                                    </asp:HyperLink></li>
                            </ul>
                        </li>

                        <li runat="server" id="liPANEL_GESTION_SECURITE">
                            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Administration/Wfrm_User.aspx">
        <i class="fa fa-lock"></i> GESTION SECURITE 
                            </asp:HyperLink>
                            <ul class="sub-menu">
                                <li runat="server" id="liPAGE_GESTION_UTILISATEUR">
                                    <asp:HyperLink ID="HyperLink_Wfrm_User" runat="server" NavigateUrl="~/Administration/Wfrm_User.aspx"> <i class="fa fa-users"></i> <span>Utilisateurs</span>
                                    </asp:HyperLink>
                                </li>
                                <li runat="server" id="liPAGE_UTILISATEURS_CONNECTES">
                                    <asp:HyperLink ID="HyperLink_Wfrm_UserConnected" runat="server" NavigateUrl="~/Administration/Wfrm_UserConnected.aspx"> 
                    <i class="fa fa-users"></i><span>Utilisateurs connectés</span>
                                    </asp:HyperLink>
                                </li>
                                <li runat="server" id="liPAGE_ACTIVITES_UTILISATEUR">
                                    <asp:HyperLink ID="HyperLink_Wfrm_ActiviteUtilisateur" runat="server" NavigateUrl="~/Administration/Wfrm_ActiviteUtilisateur.aspx">
                    <i class="fa fa-users"></i><span>Activités Utilisateurs</span>
                                    </asp:HyperLink>
                                </li>
                                <li runat="server" id="liPAGE_MODULES">
                                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Administration/Wfrm_Module.aspx">
                    <i class="fa fa-suitcase"></i><span>Modules</span>
                                    </asp:HyperLink>
                                </li>
                                <li runat="server" id="liPAGE_GESTION_OBJET">
                                    <asp:HyperLink ID="HyperLink_Wfrm_Object" runat="server" NavigateUrl="~/Administration/Wfrm_Object.aspx">
                    <i class="fa fa-tags"></i><span>Objets Système</span>
                                    </asp:HyperLink>
                                </li>
                                <li runat="server" id="liPAGE_GESTION_GROUPE_UTILISATEUR">
                                    <asp:HyperLink ID="HyperLink_Wfrm_Group" runat="server" NavigateUrl="~/Administration/Wfrm_Group.aspx">
                     <i class="fa fa-tags"></i><span>Groupe d'Utilisateurs</span>
                                    </asp:HyperLink>
                                </li>
                                <li runat="server" id="liPAGE_GESTION_TACHES_UTILISATEURS">
                                    <asp:HyperLink ID="HyperLink_Wfrm_Taches_User" runat="server" NavigateUrl="~/Administration/Wfrm_Taches_User.aspx">
                    <i class="fa fa-check-square-o"></i><span>Tâches Utilisateurs</span>
                                    </asp:HyperLink>
                                </li>
                                <li runat="server" id="liPAGE_PRIVILEGE_GROUPE">
                                    <asp:HyperLink ID="HyperLink_Wfrm_Groupe_Right" runat="server" NavigateUrl="~/Administration/Wfrm_Groupe_Right.aspx">
                    <i class="fa fa-check-square"></i><span>Tâches Groupe d'Utilisateurs</span>
                                    </asp:HyperLink>
                                </li>
                                <li runat="server" id="liPAGE_PRIVILEGES_DES_TACHES_UTILISATEURS">
                                    <asp:HyperLink ID="HyperLink_Wfrm_RightObjets" runat="server" NavigateUrl="~/Administration/Wfrm_RightObjets.aspx">
                    <i class="fa fa-check-circle"></i><span>Droits des Tâches</span>
                                    </asp:HyperLink>
                                </li>
                                <li runat="server" id="li7">
                                    <a href="#" onclick="Open_Window('../_GUIDE_OPC-SM_1.0/GU_URES.html', 'GU_URES', 800, 650);">
                                        <i class="fa fa-book"></i><span>Aides</span>
                                    </a>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="text-right collapse-button" style="padding: 7px 9px;">
                <%--<input type="text" class="form-control search" placeholder="Search..." />--%>
                <button type="button" id="sidebar-collapse" class="btn btn-default" style=""><i style="color: #fff;" class="fa fa-angle-left"></i></button>
            </div>
        </div>
    </div>
</div>
<!-- END SideBarNav_menu -->
