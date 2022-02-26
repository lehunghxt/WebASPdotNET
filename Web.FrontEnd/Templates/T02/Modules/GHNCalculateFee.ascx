<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/GHNCalculateFee.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.GHNCalculateFee" %>

<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">    
    <style>
        .custom-combobox {
            position: relative;
            display: inline-block;
        }
        .ui-autocomplete {
            max-height: 300px;max-width: 400px;
            overflow-y: auto;
            /* prevent horizontal scrollbar */
            overflow-x: hidden;
            margin-bottom:20px;
          }
        .custom-combobox-toggle {
            position: absolute;
            top: 0;
            bottom: 0;
            margin-left: -1px;
            padding: 0;
        }
        .custom-combobox-input {
            margin: 0;
            padding: 5px 10px;
        }

        .main-content table td{padding:5px; border:none}
        .weight{margin:0px !important}
   </style>
<div class="row" style="padding:10px">
    <div class="col-md-8 col-md-offset-2">
        <table width="100%" style="background:none">
            <tr>
                <td style="width:195px">Khối lượng hàng (gram):</td>






                <td colspan="2"><asp:TextBox ID="txtWeight" runat="server" TextMode="Number" CssClass="form-control weight" Width="100%" placeholder="Khối lượng (gram)" Text="500"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Giao đến:</td>
                <td><asp:DropDownList ID="ddlToDistrictID" ClientIDMode="Static" CssClass="form-control" runat="server" placeholder="Giao đến"></asp:DropDownList></td>
            <td style="text-align:right"><asp:LinkButton ID="btnFind" runat="server" Onclick="btnFind_Click" CssClass="input-group-btn">
                        <button class="btn btn-warning" type="button"> Tính phí </button>
</asp:LinkButton></td>
            </tr>
            <tr><td colspan="3" style="height:30px"></td></tr>

            <%if (District != null)
                    { %>
            <tr>
                <td colspan="3">Khu vực giao hàng: <strong> <%=District.Type == 1 ? "Nội thành" : District.Type == 2 ? "Ngoại thành 1" : "Ngoại thành 2"%></strong></td>
            </tr>
            <%}
                    if (Data != null)
                    {
                        foreach (var item in Data)
                        {%>
            <tr>
                <td>Gói cước: <strong><%=item.Name %></strong></td>
                <td colspan="2">Cước: <strong><%=String.Format("{0:0,0}", item.ServiceFee) %></strong> &nbsp &nbsp &nbsp &nbsp &nbsp Dự kiến giao: <strong><%=String.Format("{0:dd/MM/yyyy}", item.ExpectedDeliveryTime) %></strong></td>
            </tr>
            <%}
    }%>
            <tr>
                <td colspan="3" style="color:red; text-align:center; padding-top:20px">Kết quả chỉ mang tính chất tham khảo.<br /> Phí vận chuyển phụ thuộc vào khối lượng và kích thước hàng hóa thực tế.</td>
                </tr>
        </table>
        
</div>
    </div>
<script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>  

<script type="text/javascript">
        (function ($) {
            $.widget("custom.combobox", {
                _create: function () {
                    this.wrapper = $("<span>")
                      .addClass("custom-combobox")
                      .insertAfter(this.element);

                    this.element.hide();
                    this._createAutocomplete();
                    this._createShowAllButton();
                },

                _createAutocomplete: function () {
                    var selected = this.element.children(":selected"),
                      value = selected.val() ? selected.text() : "";

                    this.input = $("<input>")
                      .appendTo(this.wrapper)
                      .val(value)
                      .attr("title", "")
                      .addClass("custom-combobox-input ui-widget ui-widget-content ui-state-default ui-corner-left")
                      .autocomplete({
                          delay: 0,
                          minLength: 0,
                          source: $.proxy(this, "_source")
                      })
                      .tooltip({
                          tooltipClass: "ui-state-highlight"
                      });

                    this._on(this.input, {
                        autocompleteselect: function (event, ui) {
                            ui.item.option.selected = true;
                            this._trigger("select", event, {
                                item: ui.item.option
                            });
                        },

                        autocompletechange: "_removeIfInvalid"
                    });
                },

                _createShowAllButton: function () {
                    var input = this.input,
                      wasOpen = false;

                    $("<a>")
                      .attr("tabIndex", -1)
                      .attr("title", "Hiển thị tất cả")
                      .tooltip()
                      .appendTo(this.wrapper)
                      .button({
                          icons: {
                              primary: "ui-icon-triangle-1-s"
                          },
                          text: false
                      })
                      .removeClass("ui-corner-all")
                      .addClass("custom-combobox-toggle ui-corner-right")
                      .mousedown(function () {
                          wasOpen = input.autocomplete("widget").is(":visible");
                      })
                      .click(function () {
                          input.focus();

                          // Close if already visible
                          if (wasOpen) {
                              return;
                          }

                          // Pass empty string as value to search for, displaying all results
                          input.autocomplete("search", "");
                      });
                },

                _source: function (request, response) {
                    var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
                    response(this.element.children("option").map(function () {
                        var text = $(this).text();
                        if (this.value && (!request.term || matcher.test(text)))
                            return {
                                label: text,
                                value: text,
                                option: this
                            };
                    }));
                },

                _removeIfInvalid: function (event, ui) {

                    // Selected an item, nothing to do
                    if (ui.item) {
                        return;
                    }

                    // Search for a match (case-insensitive)
                    var value = this.input.val(),
                      valueLowerCase = value.toLowerCase(),
                      valid = false;
                    this.element.children("option").each(function () {
                        if ($(this).text().toLowerCase() === valueLowerCase) {
                            this.selected = valid = true;
                            return false;
                        }
                    });

                    // Found a match, nothing to do
                    if (valid) {
                        return;
                    }

                    // Remove invalid value
                    this.input
                      .val("")
                      .attr("title", "Không tìm thấy " + value)
                      .tooltip("open");
                    this.element.val("");
                    this._delay(function () {
                        this.input.tooltip("close").attr("title", "");
                    }, 2500);
                    this.input.autocomplete("instance").term = "";
                },

                _destroy: function () {
                    this.wrapper.remove();
                    this.element.show();
                }
            });
        })(jQuery);

        $("#ddlToDistrictID").combobox();
        $("#toggle").click(function () {
            $("#ddlToDistrictID").toggle();
        });
    </script>