<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DashMenu_Securite.ascx.vb" Inherits="UserControl_DashMenu_1" %>
<!-- Sidebar user panel -->
<div class="user-panel">
    <div class="pull-left image">
        <asp:Image ID="ImageParticipant" runat="server" CssClass="img-circle" AlternateText=""></asp:Image>
    </div>
    <div class="pull-left info">
        <p>
            <asp:Label ID="LabelPrenomParticipant" runat="server" />
        </p>
        <a href="#"><i class="fa fa-circle text-success"></i>En ligne</a>
    </div>
</div>
<ul class="sidebar-menu">
    <%--<li class="treeview" runat="server" id="LI_TREE_PLANETSERVICE509">
        <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/GestionPlansEtServices/Frm_PlanRezo509.aspx"
            ToolTip="PLAN & SERVICES">
        <i class="fa fa-briefcase"></i> <span> PLAN & SERVICES 509 </span>
        <i class="fa fa-angle-left pull-right"></i>
        </asp:HyperLink>
        <ul class="treeview-menu">
            <li runat="server" id="LI_Frm_PlanRezo509">
                <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/GestionPlansEtServices/Frm_PlanRezo509.aspx">
                    <i class="fa fa-briefcase fa-cogs"></i> Plans
                </asp:HyperLink>
            </li>
            <li runat="server" id="LI_Frm_ServicesRezo509">
                <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/GestionPlansEtServices/Frm_ServicesRezo509.aspx">
                    <i class="fa fa-briefcase fa-cogs"></i> Services Rezo509
                </asp:HyperLink>
            </li>
        </ul>
    </li>--%>
    <%--<li class="treeview" runat="server" id="LiPanel_GESTION_MAILLING">
        <asp:HyperLink ID="HyperLink_Wfrm_Mailling" runat="server" NavigateUrl="~/GestionMailling/CampagneListing.aspx?SECURITY=1">
            <i class="fa fa-briefcase"></i> MASSE MAILING <i class="fa fa-angle-left pull-right"></i>
        </asp:HyperLink>
        <ul class="treeview-menu">
            <li runat="server" id="LI_GestionMailling">
                <asp:HyperLink ID="HyperLink_GestionMailling" runat="server"
                    NavigateUrl="~/GestionMailling/CampagneListing.aspx?SECURITY=1">
                    <i class="fa fa-envelope"></i> Masse Mailing
                </asp:HyperLink></li>
            <li runat="server" id="LI_GestionGroupeContact">
                <asp:HyperLink ID="HyperLink_GestionGroupeContact" runat="server"
                    NavigateUrl="~/GestionMailling/GestionGroupeContact.aspx">
                    <i class="fa fa-group"></i> Groupes Contacts
                </asp:HyperLink></li>
            <li runat="server" id="LI_GestionContact">
                <asp:HyperLink ID="HyperLink_GestionContact" runat="server"
                    NavigateUrl="~/GestionMailling/GestionContact.aspx">
                    <i class="fa fa-book"></i> Contacts
                </asp:HyperLink></li>
            <li runat="server" id="LI_GestionContactToGroupe">
                <asp:HyperLink ID="HyperLink_GestionContactToGroupe" runat="server"
                    NavigateUrl="~/GestionMailling/GestionContactToGroupe.aspx">
                    <i class="fa fa-tasks"></i> Assignation Contact / Groupe
                </asp:HyperLink></li>
            <li runat="server" id="LI_GestionHeaderFooterLogo">
                <asp:HyperLink ID="HyperLink_GestionHeaderFooterLogo" runat="server"
                    NavigateUrl="~/GestionMailling/GestionHeaderFooter.aspx">
                    <i class="fa fa-wrench"></i> Paramètres E-Mail
                </asp:HyperLink></li>
        </ul>
    </li>--%>
    <%--<li class="treeview" runat="server" id="LI_TREE_ANNONCE509">
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/GestionAnnonces/AnnonceListing.aspx?SECURITY=1"
            ToolTip="Annonces">
        <i class="fa fa-bullhorn"></i> <span> ANNONCES 509 </span>
        <i class="fa fa-angle-left pull-right"></i>
        </asp:HyperLink>
        <ul class="treeview-menu">
            <li runat="server" id="Li_AnnonceListing">
                <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/GestionAnnonces/AnnonceListing.aspx?SECURITY=1">
                    <i class="fa fa-bullhorn"></i>
                    <asp:Label ID="Label_MyAnnonces" runat="server" Text="Annonces" />
                </asp:HyperLink>
            </li>
            <li runat="server" id="li_Frm_AnnonceCategorieListing">
                <asp:HyperLink ID="HyperLinkFrm_AnnonceCategorieListing" runat="server" NavigateUrl="~/GestionAnnonces/MyAnnonceCategorieListing.aspx"> 
                    <i class="fa fa-bullhorn fa-cogs"></i> Catégorie Annonce
                </asp:HyperLink>
            </li>
            <li runat="server" id="li_Frm_AnnonceSousCategorieListing">
                <asp:HyperLink ID="HyperLinkFrm_AnnonceSousCategorieListing" runat="server" NavigateUrl="~/GestionAnnonces/Frm_AnnonceSousCategorieListing.aspx"> 
                    <i class="fa fa-bullhorn fa-cogs"></i> Sous-Catégorie Annonce
                </asp:HyperLink>
            </li>
            <li runat="server" id="li_Frm_DeviseListing">
                <asp:HyperLink ID="HyperLinkFrm_DeviseListing" runat="server" NavigateUrl="~/GestionAnnonces/Frm_DeviseListing.aspx"> 
                    <i class="fa fa-money fa-cogs"></i> Devise
                </asp:HyperLink>
            </li>
        </ul>
    </li>--%>
    <%--<li class="treeview" runat="server" id="LI_TREE_JOBREZO509">
        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Parametres/Wfrm_OffreEmploi.aspx"
            ToolTip="Annonces">
        <i class="fa fa-asterisk"></i> <span> OFFRES D'EMPLOI </span>
        <i class="fa fa-angle-left pull-right"></i>
        </asp:HyperLink>
        <ul class="treeview-menu">
            <li runat="server" id="li_OffreEmploi">
                <asp:HyperLink ID="HyperLink_MyOffreEmploi" runat="server" NavigateUrl="~/GestionOffreEmploi/OffreEmploiListing.aspx?SECURITY=1">
                    <i class="fa fa-folder-open"></i>
                    <asp:Label ID="Label_MyOffreEmploi" runat="server" Text="Offres d'Emploi" />
                </asp:HyperLink></li>
            <li runat="server" id="LI_HyperLink_OffreEmploi">
                <asp:HyperLink ID="HyperLink_OffreEmploi" runat="server" NavigateUrl="~/Parametres/Wfrm_OffreEmploi.aspx">
                       <i class="fa fa-asterisk"></i> Offres d'Emploi
                </asp:HyperLink>
            </li>
        </ul>
    </li>--%>
    <%--<li class="treeview" runat="server" id="LI_HyperLink_Validation">
        <asp:HyperLink ID="HyperLink_Validation" runat="server" NavigateUrl="~/Parametres/Wfrm_Annonce.aspx"
            ToolTip="Annonces">
        <i class="fa fa-lock"></i> <span> VALIDATION </span>
        <i class="fa fa-angle-left pull-right"></i>
        </asp:HyperLink>
        <ul class="treeview-menu">
            <li runat="server" id="LI_HyperLink_Wfrm_Annonce">
                <asp:HyperLink ID="HyperLink_Wfrm_Annonce" runat="server" NavigateUrl="~/Parametres/Wfrm_Annonce.aspx"
                    ToolTip="Annonces"><span class="r"></span><span
                                        class="t">Annonces</span></asp:HyperLink></li>
            <li runat="server" id="LI_HyperLink_AppelOffres">
                <asp:HyperLink ID="HyperLink_AppelOffres" runat="server" Text="Institutions" NavigateUrl="~/Parametres/Wfrm_Institutions.aspx"
                    ToolTip="Institution"><span class="r"></span><span
                                        class="t">Appel d'Offres</span></asp:HyperLink></li>
            <li runat="server" id="LI_HyperLink_Wfrm_Institutions">
                <asp:HyperLink ID="HyperLink_Wfrm_Institutions" runat="server" Text="Institutions"
                    NavigateUrl="~/Presentations/MyInstitutionListing.aspx?SECURITY=1" ToolTip="Institution">
                    <i class="fa  fa-home"></i> Institutions</asp:HyperLink></li>
            <li runat="server" id="LI_HyperLink_Wfrm_CurriculumVitae_Abonne">
                <asp:HyperLink ID="HyperLink_Wfrm_CurriculumVitae_Abonne" runat="server" Text="Curriculum Vitae Abonne"
                    NavigateUrl="~/Parametres/Wfrm_CurriculumVitae_Abonne.aspx" ToolTip="Curriculum Vitae"><span class="r"></span><span
                                        class="t">Curriculum Vitae Abonné</span></asp:HyperLink></li>
        </ul>
    </li>--%>
    <li class="treeview" runat="server" id="LI_PARAMETRES">
        <asp:HyperLink ID="HyperLink_Parametre" runat="server" NavigateUrl="~/GestionQuestionnaire/Frm_QuestionsListing.aspx">                
            <i class="fa fa-lock"></i> <span> PARAMETRES </span>
            <i class="fa fa-angle-left pull-right"></i>
        </asp:HyperLink>
        <ul class="treeview-menu">
            <li runat="server" id="LI_HyperLink_DomaineActivite">
                <asp:HyperLink ID="HyperLink_DomaineActivite" runat="server" NavigateUrl="~/GestionQuestionnaire/Frm_QuestionsListing.aspx">
                    <i class="fa fa-home"></i> Font Office
                </asp:HyperLink></li>
        </ul>
    </li>
    <li class="treeview" runat="server" id="liPANEL_GESTION_SECURITE">
        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Administration/Wfrm_User.aspx">
        <i class="fa fa-lock"></i> <span>GESTION SECURITE</span> 
        <i class="fa fa-angle-left pull-right"></i>
        </asp:HyperLink>
        <ul class="treeview-menu">
            <li runat="server" id="liPAGE_GESTION_UTILISATEUR">
                <asp:HyperLink ID="HyperLink_Wfrm_User" runat="server" NavigateUrl="~/Administration/Wfrm_User.aspx"
                    ToolTip="Utilisateurs"> <i class="fa fa-users"></i> <span>Utilisateurs</span>
                </asp:HyperLink>
            </li>
            <li runat="server" id="liPAGE_UTILISATEURS_CONNECTES">
                <asp:HyperLink ID="HyperLink_Wfrm_UserConnected" runat="server" NavigateUrl="~/Administration/Wfrm_UserConnected.aspx"
                    ToolTip="liste des Utilisateurs connectés (aujourd'hui)"> 
                    <i class="fa fa-users"></i><span>Utilisateurs connectés</span>
                </asp:HyperLink>
            </li>
            <li runat="server" id="liPAGE_ACTIVITES_UTILISATEUR">
                <asp:HyperLink ID="HyperLink_Wfrm_ActiviteUtilisateur" runat="server" NavigateUrl="~/Administration/Wfrm_ActiviteUtilisateur.aspx"
                    ToolTip="liste des opérations effectuées sur le système par les Utilisateurs">
                    <i class="fa fa-users"></i><span>Activités Utilisateurs</span>
                </asp:HyperLink>
            </li>
            <li runat="server" id="liPAGE_MODULES">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Administration/Wfrm_Module.aspx">
                    <i class="fa fa-suitcase"></i><span>Modules</span>
                </asp:HyperLink>
            </li>
            <li runat="server" id="liPAGE_GESTION_OBJET">
                <asp:HyperLink ID="HyperLink_Wfrm_Object" runat="server" NavigateUrl="~/Administration/Wfrm_Object.aspx"
                    ToolTip="Objet Système">
                    <i class="fa fa-tags"></i><span>Objets Système</span>
                </asp:HyperLink>
            </li>
            <li runat="server" id="liPAGE_GESTION_GROUPE_UTILISATEUR">
                <asp:HyperLink ID="HyperLink_Wfrm_Group" runat="server" NavigateUrl="~/Administration/Wfrm_Group.aspx"
                    ToolTip="Groupe Utilisateur">
                     <i class="fa fa-tags"></i><span>Groupe d'Utilisateurs</span>
                </asp:HyperLink>
            </li>
            <li runat="server" id="liPAGE_GESTION_TACHES_UTILISATEURS">
                <asp:HyperLink ID="HyperLink_Wfrm_Taches_User" runat="server" NavigateUrl="~/Administration/Wfrm_Taches_User.aspx"
                    ToolTip="Taches des Utilisateur">
                    <i class="fa fa-check-square-o"></i><span>Tâches des Utilisateurs</span>
                </asp:HyperLink>
            </li>
            <li runat="server" id="liPAGE_PRIVILEGE_GROUPE">
                <asp:HyperLink ID="HyperLink_Wfrm_Groupe_Right" runat="server" NavigateUrl="~/Administration/Wfrm_Groupe_Right.aspx"
                    ToolTip="Tâches Groupe d'Utilisateur">
                    <i class="fa fa-check-square"></i><span>Tâches Groupe d'Utilisateurs</span>
                </asp:HyperLink>
            </li>
            <li runat="server" id="liPAGE_PRIVILEGES_DES_TACHES_UTILISATEURS">
                <asp:HyperLink ID="HyperLink_Wfrm_RightObjets" runat="server" NavigateUrl="~/Administration/Wfrm_RightObjets.aspx"
                    ToolTip="Droits des Tâches">
                    <i class="fa fa-check-circle"></i><span>Droits des Tâches</span>
                </asp:HyperLink>
            </li>
            <li runat="server" id="liLogErreur">
                <asp:HyperLink ID="HyperLink_LogErreur" runat="server" NavigateUrl="~/Administration/Log.aspx?SECURITY=1"> 
                    <i class="fa fa-bookmark"></i> LOG Erreur
                </asp:HyperLink>
            </li>
            <%--<li runat="server" id="li1">
                <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/VersionMobile/VersionMobileListing.aspx"> 
                    <i class="fa fa-bookmark"></i> Gestion Version Mobile
                </asp:HyperLink>
            </li>--%>
        </ul>
    </li>
    <%--<li class="treeview" runat="server" id="li_GESTIONAPI">
        <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/VersionMobile/VersionMobileListing.aspx">
        <i class="fa fa-lock"></i> <span>GESTION API</span> 
        <i class="fa fa-angle-left pull-right"></i>
        </asp:HyperLink>
        <ul class="treeview-menu">
            <li runat="server" id="liFrm_ChantListing" title="">
                <asp:HyperLink ID="LinkFrm_ChantListing" runat="server" > 
                    <i class="fa  fa-check-circle"></i> <span>Chants Listing</span>
                </asp:HyperLink>
            </li>
            <li runat="server" id="li5">
                <asp:HyperLink ID="LinkFrm_GroupeListing" runat="server" NavigateUrl="~/Frm_GroupeListing.aspx">
                    <i class="fa  fa-check-circle"></i> <span> Groupe Chantant Listing</span>
                </asp:HyperLink>
            </li>
            <li runat="server" id="liVersionMobileListing">
                <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="~/VersionMobile/VersionMobileListing.aspx"> 
                    <i class="fa fa-mobile"></i>  <span>Gestion version Android</span>
                </asp:HyperLink>
            </li>
            <li runat="server" id="liLink_APISurRezo509">
                <asp:HyperLink ID="Link_APISurRezo509" runat="server" NavigateUrl="~/Feed/ApiDashBord.aspx">
                    <i class="fa fa-check-circle"></i><span>API</span>
                </asp:HyperLink>
            </li>
        </ul>
    </li>--%>
</ul>
