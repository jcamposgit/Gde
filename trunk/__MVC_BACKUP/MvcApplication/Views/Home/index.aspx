<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ClubStarterKit.Infrastructure.View.BaseView<ClubStarterKit.Web.ViewData.Home.IndexViewData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
    <div class="rightblock">
        
    </div>
    
    <div class="rightblock">
        
    </div>
    
    <div class="rightblock">
        
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="LeftContent" runat="server">
    <div class="leftblock">
        
    </div>
    
    <div id="poll-block" class="leftblock">
        <h2>Poll</h2>
        <div class="dashedline"></div>
        
        <div id="poll">
            <div id="ajax-page-list" rel="{block: 'poll-block'}">
                
            </div>
        </div>
    </div>
    
    
        <div class="leftblock">
             <br />
            
        </div>
    
</asp:Content>
