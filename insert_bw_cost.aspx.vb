Imports System.Data
Partial Class add_request
    Inherits System.Web.UI.Page
    Dim C As New Cls_Data

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("menu") IsNot Nothing Then
                Session("menu") = Request.QueryString("menu")
            End If
            username.Value = Session("uemail")

            Dim strCustomerType As String

            strCustomerType = "select CustomerType_name, BWCostType from dbo.CustomerType where Status = '1' order by CustomerType_order "
            C.SetDropDownList(ddlCustomerType, strCustomerType, "CustomerType_name", "BWCostType")
            SetCost()
            
        End If
    End Sub

    Protected Sub ddlCustomerType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCustomerType.SelectedIndexChanged
        SetCost()
    End Sub

    Public Sub SetCost()
        Dim DT_Utilization As New DataTable
        DT_Utilization = C.GetDataTable("Select * from InternetBWCost where BWCostType = '" & ddlCustomerType.SelectedValue & "'")
        If DT_Utilization.Rows.Count > 0 Then
            txtUtilization.Text = DT_Utilization.Rows(0).Item("Utilization")
            txtDirectTraffic.Text = DT_Utilization.Rows(0).Item("InterDirect")
            txtDomCost.Text = DT_Utilization.Rows(0).Item("DomesticCost")
            txtAllInterCost.Text = DT_Utilization.Rows(0).Item("AllInternationalCost")
            txtTransitCost.Text = DT_Utilization.Rows(0).Item("TransitCost")
            txtNetworkCost.Text = DT_Utilization.Rows(0).Item("NetworkCost")
            txtNetworkPortCost.Text = DT_Utilization.Rows(0).Item("NetworkPortCost")
            txtNOCCost.Text = DT_Utilization.Rows(0).Item("NOCCost")
            txtMinMaxDiffCost.Text = DT_Utilization.Rows(0).Item("MinMaxDiffCost")
        Else
            txtUtilization.Text = "0"
            txtDirectTraffic.Text = "0"
            txtDomCost.Text = "0"
            txtAllInterCost.Text = "0"
            txtTransitCost.Text = "0"
            txtNetworkCost.Text = "0"
            txtNetworkPortCost.Text = "0"
            txtNOCCost.Text = "0"
            txtMinMaxDiffCost.Text = "0"
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If IsNumeric(txtUtilization.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('???????? Utilization ???????????????????????????????!');", True)
            txtUtilization.Focus()
        ElseIf IsNumeric(txtDirectTraffic.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('???????? Direct Traffic ???????????????????????????????!');", True)
            txtDirectTraffic.Focus()
        ElseIf IsNumeric(txtMinMaxDiffCost.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('???????? Min-Max ???????????????????????????????!');", True)
            txtMinMaxDiffCost.Focus()
        ElseIf IsNumeric(txtDomCost.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('???????? Domestic Cost ???????????????????????????????!');", True)
            txtDomCost.Focus()
        ElseIf IsNumeric(txtAllInterCost.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('???????? All International Cost ???????????????????????????????!');", True)
            txtAllInterCost.Focus()
        ElseIf IsNumeric(txtTransitCost.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('???????? Transit Cost ???????????????????????????????!');", True)
            txtTransitCost.Focus()
        ElseIf IsNumeric(txtNetworkCost.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('???????? Network Cost ???????????????????????????????!');", True)
            txtNetworkCost.Focus()
        ElseIf IsNumeric(txtNetworkPortCost.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('???????? Network-Port Cost ???????????????????????????????!');", True)
            txtNetworkPortCost.Focus()
        ElseIf IsNumeric(txtNOCCost.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('???????? NOC ???????????????????????????????!');", True)
            txtNOCCost.Focus()
        Else
            Try
                Dim strSql As String
                Dim DT As DataTable
                strSql = "Update InternetBWCost set Utilization = '" & txtUtilization.Text & "', InterDirect = '" & txtDirectTraffic.Text & "', DomesticCost = '" & txtDomCost.Text & "', AllInternationalCost = '" & txtAllInterCost.Text & "', TransitCost = '" & txtTransitCost.Text & "', NetworkCost = '" & txtNetworkCost.Text & "', NetworkPortCost = '" & txtNetworkPortCost.Text & "', NOCCost = '" & txtNOCCost.Text & "', MinMaxDiffCost = '" & txtMinMaxDiffCost.Text & "' where BWCostType = '" & ddlCustomerType.SelectedValue & "' "
                C.ExecuteNonQuery(strSql)
                DT = C.GetDataTable("select * from dbo.List_Service where ISNULL(Document_No,'') = '' ")
                If DT.Rows.Count > 0 Then
                    strSql = "Update dbo.List_Service set INLUtilization = '" & txtUtilization.Text & "', InterUtilization = '" & txtUtilization.Text & "', IPVUtilization = '" & txtUtilization.Text & "', InterDirect = '" & txtDirectTraffic.Text & "', DomesticCost = '" & txtDomCost.Text & "', AllInternationalCost = '" & txtAllInterCost.Text & "', TransitCost = '" & txtTransitCost.Text & "', NetworkCost = '" & txtNetworkCost.Text & "', NetworkPortCost = '" & txtNetworkPortCost.Text & "', NOCCost = '" & txtNOCCost.Text & "' where ISNULL(Document_No,'') = '' "
                    C.ExecuteNonQuery(strSql)
                End If
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertSuccess('???????????????????????????????????????????', function() { focus;window.location.href='insert_bw_cost.aspx?menu=insert'; });", True)
            Catch ex As Exception
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertError('????????????????????????????????????????????????????', function() { focus; });", True)
            End Try
        End If
    End Sub
End Class
