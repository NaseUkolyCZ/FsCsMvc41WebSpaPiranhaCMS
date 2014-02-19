<%@ Application Inherits="FsWeb.Global" Language="C#" %>
<script Language="C#" RunAt="server">

  protected void Application_Start(Object sender, EventArgs e) {
      base.Start();

      FsCsMvc41WebSpa.RouteConfig.RegisterRoutes(RouteTable.Routes);
  }

</script>