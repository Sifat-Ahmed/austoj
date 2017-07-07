<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="index" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">


<head runat="server">

    
    <link href="StyleSheet.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/highlight.js/9.12.0/styles/default.min.css"/>
    <script src="//cdnjs.cloudflare.com/ajax/libs/highlight.js/9.12.0/highlight.min.js"></script>
    <link rel="stylesheet" href="/path/to/styles/default.css"/>
    <script src="/path/to/highlight.pack.js"></script>
    <script>hljs.initHighlightingOnLoad();</script>

    <title>AUST OJ</title>

</head>

<body>

    <div class="website-title" >
            AUST Online Judge BETA v1.0    
     </div>
    <div class="developer-title">
        <h4>Developed By</h4>
            <a href="https://facebook.com/sifat.iv">Sifat Ahmed</a>
            <a href="https://www.facebook.com/asif.shaafi">Asif Imtiaz Shaafi</a>
            <a href="https://www.facebook.com/Sayef.Reyadh.Khan">Sayef Reyadh Khan</a>
    </div>


    <form id="form" runat="server">
        <div class="container">
            <div class="code-ext">
                <p>Choose code type</p>
                <asp:DropDownList ID="codeList" runat="server">
                    <asp:ListItem Value=".c">C</asp:ListItem>
                    <asp:ListItem Value=".cpp">C++</asp:ListItem>
                    <asp:ListItem Value=".java">Java</asp:ListItem>
                </asp:DropDownList>
                <div class="name" style="margin-left:20px;">
                    <p>------------------For JAVA Class Name Must BE *** Main ****------------------------------------------------------------------------------------------------STDIN------------------------------------</p>
                </div>
            </div>

            <div class = "code-body" style="display:inline-block;">
                <div style="float:left;">
                    <asp:TextBox ID="codeText" runat="server" Height="400px" TabIndex="4" TextMode="MultiLine" Width="750px" Font-Size="X-Large"></asp:TextBox>
                </div>
                <div class = "sideBar" style="float:left;">
                    <asp:TextBox ID="codeInput" runat="server" Text="" Height="400px" Width="400px" TextMode="MultiLine" Font-Size="X-Large" ></asp:TextBox>
                </div>
                
                <asp:Button ID="submitBtn" runat="server" style="margin-left: 627px" Text="Submit" OnClick="Button_click" Height="40px" Width="130px" Font-Size="X-Large"/>
            </div>
            
            
        </div>
        
        <div class = "code-body" style="width:90%;padding:50px; margin-left:auto; margin-right:auto;">
            
            <div style="margin-top:20px;">
                <asp:Label ID="filename" runat="server" Text="File name:" Font-Size="X-Large"></asp:Label>        
           </div>
            
           <div style="margin-top:20px;">
                <asp:Label ID="compile_status" runat="server" Text="Compile Status:" Font-Size="X-Large"></asp:Label>
            </div>

            <div style="margin-top:20px;">
                <asp:Label ID="compile_error" runat="server" Text="Compile Error:" Font-Size="X-Large"></asp:Label>               
            </div>

            <div style="margin-top:20px;">
                <asp:Label ID="run_status" runat="server" Text="Run status:" Font-Size="X-Large"></asp:Label>               
            </div>

            <div style="margin-top:20px;">
                <asp:Label ID="run_error" runat="server" Text="Run Error: " Font-Size="X-Large"></asp:Label>
            </div>

            <div style="margin-top:20px;">
                <asp:Label ID="memory" runat="server" Text="Memory Limit: " Font-Size="X-Large"></asp:Label>
            </div>

            <div style="margin-top:20px;">
                <asp:Label ID="runtime" runat="server" Text="Run Error:" Font-Size="X-Large"></asp:Label>
            </div>

            <div style="margin-top:20px;">
                <asp:Label ID="codeTxt" runat="server" Font-Size="X-Large" Text="Code: "></asp:Label>
                <pre><code style="font-size:20px;">
                    <%= "\n" + stat.Code %>
                </code></pre>
            </div>

            <div style="margin-top:20px;">
                <asp:Label ID="stdin" runat="server" Text="Stdin:" Font-Size="X-Large"></asp:Label>
                    <pre style="font-size:20px;">
                        <%= "\n" + stat.STDIN %>
                    </pre>
           </div>
            <div style="margin-top:20px;">
               <asp:Label ID="stdout" runat="server" Text="Stdout:" Font-Size="X-Large"></asp:Label>
                    <pre style="font-size:20px;">
                        <%= "\n" + stat.STDOUT %>
                    </pre>
            </div>
        </div>

    </form>
</body>
</html>
