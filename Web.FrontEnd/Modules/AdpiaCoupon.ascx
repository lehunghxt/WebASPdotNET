<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdpiaCoupon.ascx.cs" Inherits="Web.FrontEnd.Modules.AdpiaCoupon" %>

 <h1 title="<%=Title%>"><strong> <%=Title%></strong></h1>
<div class="row">
    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
        <label for="cbDevice">Nền tảng hỗ trợ:</label>
        <select id="cbDevice" class="form-control" onchange="Filter();">
            <option value="">Tất cả thiết bị</option>
            <option value="2">Áp dụng trên App</option>
            <option value="3">Áp dụng trên Website</option>
        </select>
    </div>
    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
        <label for="cbWebsite">Website:</label>
        <select id="cbWebsite" class="form-control" onchange="Filter();">
            <option value="">Tất cả</option>
        </select>
    </div>
    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
        <label for="cbCategory">Danh mục:</label>
        <select id="cbCategory" class="form-control" onchange="Filter();">
            <option value="">Tất cả</option>
        </select>
    </div>
</div>
<br />
<div id="coupon_area" class="row">

</div>

<script>
    var data = JSON.parse('<%=Data %>');
    var coupons = data.data.items;
    var merchants = new Set(coupons.map(function (item) { return item.MERCHANT_ID; }));
    var categories = new Set(coupons.map(function (item) { return item.CATEGORY_NAME; }));

    merchants.forEach(function (item) { $("#cbWebsite").append(new Option(item, item)); });
    categories.forEach(function (item) { $("#cbCategory").append(new Option(item, item)); });

    ShowCoupon();

    function Filter()
    {
        coupons = data.data.items;

        var device = $("#cbDevice").val();
        if (device) {
            coupons = coupons.filter(function (obj) {
                return obj.DEVICE === device;
            });
        }

        var website = $("#cbWebsite").val();
        if (website) {
            coupons = coupons.filter(function (obj) {
                return obj.MERCHANT_ID === website;
            });
        }

        var category = $("#cbCategory").val();
        if (category) {
            coupons = coupons.filter(function (obj) {
                return obj.CATEGORY_NAME === category;
            });
        }

        ShowCoupon();
    }

    function ShowCoupon()
    {
        $("#coupon_area").empty();
        if (coupons.length == 0) {
            $("#coupon_area").html("<p>Không tìm thấy mã giảm giá phù họp.</p>");
        }
        else
        {
            coupons.forEach(function (item) {
                var startdate = (new Date(item.START_DATE * 1000)).toLocaleDateString("vi-VN");
                var content = "<div class='col-lg-4 col-md-4 col-sm-6 col-xs-2' style='margin-bottom: 20px'><div style='border: 1px solid #ccc; padding:10px'>";
                content += "<div style='font-size:12pt'><strong>Ưu đãi:</strong><font style='color:red'> " + item.DISCOUNT + "</font></div>";
                content += "<div style='font-size:12pt'><strong>Mã giảm giá:</strong><a target='_blank' href='" + item.AFF_LINK + "' title='" + item.TITLE + "'> " + (item.DISCOUNT_CODE ? item.DISCOUNT_CODE : "") + "</a></div>";
                content += "<div style='font-size:12pt'><strong>Áp dụng tại:</strong><a target='_blank' href='" + item.AFF_LINK + "' title='" + item.TITLE + "'> " + item.MERCHANT_ID + "</a></div>";
                content += "<div style='font-size:12pt'><strong>Hiệu lực lúc:</strong> " + startdate.substring(0, startdate.length - 5) + "</div>";
                content += "<div style='font-size:12pt'>" + (item.DESCRIPTION.length > 100 ? item.DESCRIPTION.substring(100) + "..." : item.DESCRIPTION) + "</div>";
                content += "</div></div>";
                $("#coupon_area").append(content);
            });
           
        }
    }
</script>

<script>
    var couponTotalArray;
    getAllDiscountCodeAPI();
    
    function jsonp(url, callback) {
        var callbackName = 'jsonp_callback_' + Math.round(100000 * Math.random());
        window[callbackName] = function(data) {
            delete window[callbackName];
            document.body.removeChild(script);
            callback(data);
        };

        var script = document.createElement('script');
        script.src = url + (url.indexOf('?') >= 0 ? '&' : '?') + 'callback=' + callbackName;
        document.body.appendChild(script);
    }

    async function getAllDiscountCodeAPI() {
        const x1 = await promiseShopee(get_shopee_promo_code_api());
        const x2 = await promiseLazada(get_lazada_promo_code_api());
        const x3 = await promiseSendo(get_sendo_promo_code_api());
        const x4 = await promiseTiki(get_tiki_promo_code_api());
        const x5 = await promiseFahasa(get_fahasa_promo_code_api());

        couponTotalArray = couponTotalArray.substring(0, couponTotalArray.length - 1);
        couponTotalArray = "[" + couponTotalArray + "]";
        couponTotalArray = couponTotalArray.replaceAll(",,", ",");
        couponTotalArray = couponTotalArray.replace("[,", "[");
        couponTotalArray = couponTotalArray.replace(",]", "]");
        couponTotalArray = JSON.parse(couponTotalArray);

        couponTotalArray.sort(function (a, b) {
            date1 = a.end_date.split('/');
            date2 = b.end_date.split('/');
            x = date1[2] + date1[1] + date1[0];
            y = date2[2] + date2[1] + date2[0];
            return x < y ? -1 : x > y ? 1 : 0;
        });

        console.log(couponTotalArray);
    }

    function promiseShopee(f) {
        return new Promise(function (resolve, reject) {
            resolve(f);
        });
    }

    function promiseLazada(f) {
        return new Promise(function (resolve, reject) {
            resolve(f);
        });
    }

    function promiseSendo(f) {
        return new Promise(function (resolve, reject) {
            resolve(f);
        });
    }

    function promiseTiki(f) {
        return new Promise(function (resolve, reject) {
            resolve(f);
        });
    }

    function promiseFahasa(f) {
        return new Promise(function (resolve, reject) {
            resolve(f);
        });
    }

    function get_shopee_promo_code_api() {
        var tmpArray = [];
        var url = "https://data.polyxgo.com/api/v1/datax/shopee_vouchers";
        jsonp('https://jsonp.afeld.me/?callback=&url=' + encodeURIComponent(url), function (res) {
            var data = JSON.parse(res.value);
            data.forEach(function (ev, index) {
                ev.vouchers.forEach(function (ev2, index2) {
                    let mid = "shopee";
                    let title = "";
                    let discount = "";
                    if (ev2.coin_percentage) {
                        title += 'HoÃ n ' + ev2.coin_percentage + '%';
                    } else if (ev2.discount_percentage) {
                        title += 'Giáº£m ' + ev2.discount_percentage + '%';
                    }

                    if (!title) {
                        if (ev2.coin_cap) {
                            title += 'HoÃ n ' + addCommas(ev2.coin_cap) + ' xu';
                        } else if (ev2.discount_value) {
                            title += 'Giáº£m ' + addCommas(ev2.discount_value / 10000) + 'Ä‘';
                        }
                    }

                    if (!title) {
                        title = 'Voucher';
                    }

                    if (ev2.coin_cap) {
                        discount += addCommas(ev2.coin_cap) + ' xu';
                    } else if (ev2.discount_value) {
                        discount += addCommas(ev2.discount_value / 10000) + 'Ä‘';
                    }

                    if (!discount) {
                        if (ev2.coin_percentage) {
                            discount += ev2.coin_percentage + '%';
                        } else if (ev2.discount_percentage) {
                            discount += ev2.discount_percentage + '%';
                        }
                    }

                    if (!discount) {
                        discount = '0Ä‘';
                    }

                    let desc = ev2.usage_terms;
                    var date = new Date(ev2.end_time * 1000);
                    let end_date = (date.getDate() < 10 ? '0' + date.getDate() : date.getDate()) + '/' + (date.getMonth() < 9 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1) + '/' + date.getFullYear();
                    let code = ev2.voucher_code != 'POLYXGO' ? ev2.voucher_code : '';
                    let url = 'https://shopee.vn/search?promotionId=' + ev2.promotionid + '&signature=' + ev2.signature + '&voucherCode=' + ev2.voucher_code;
                    tmpArray.push({ mid: mid, title: title, desc: desc, discount: discount, end_date: end_date, code: code, url: encodeURIComponent(url) });
                });
            });

            var tmp = JSON.stringify(tmpArray);
            tmp = tmp.substring(1, tmp.length - 1);
            var result = tmp + ",";
            couponTotalArray += result;
        });
    }

    function get_lazada_promo_code_api() {
        var tmpArray = [];
        var url = "https://data.polyxgo.com/api/v1/datax/lazada_vouchers";
        jsonp('https://jsonp.afeld.me/?callback=&url=' + encodeURIComponent(url), function (res) {
            var data = JSON.parse(res.value);
            data.forEach(function (ev, index) {
                ev.vouchers.forEach(function (ev2, index2) {
                    let mid = "lazada";
                    let title = "Giáº£m " + addCommas(ev2.amount) + (ev2.amount < 1000 ? '%' : 'Ä‘');
                    let desc = "MÃ£ giáº£m giÃ¡ lazada " + addCommas(ev2.amount) + (ev2.amount < 1000 ? '%' : 'Ä‘') + " Ä‘Æ¡n hÃ ng tá»« " + addCommas(ev2.min_spend) + (ev2.min_spend < 1000 ? '%' : 'Ä‘') + " Ä‘áº·t mua sáº£n pháº©m táº¡i " + ev2.shop_name;
                    let discount = addCommas(ev2.amount) + (ev2.amount < 1000 ? '%' : 'Ä‘');

                    var date = new Date(parseInt(ev2.end_time));
                    let end_date = (date.getDate() < 10 ? '0' + date.getDate() : date.getDate()) + '/' + (date.getMonth() < 9 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1) + '/' + date.getFullYear();
                    let code = ev2.code != 'POLYXGO' ? ev2.code : '';
                    let url = ev2.link;
                    tmpArray.push({ mid: mid, title: title, desc: desc, discount: discount, end_date: end_date, code: code, url: encodeURIComponent(url) });
                });
            });

            var tmp = JSON.stringify(tmpArray);
            tmp = tmp.substring(1, tmp.length - 1);
            var result = tmp + ",";
            couponTotalArray += result;
        });
    }

    function get_sendo_promo_code_api() {
        var tmpArray = [];
        var url = "https://data.polyxgo.com/api/v1/datax/sendo_vouchers";
        jsonp('https://jsonp.afeld.me/?callback=&url=' + encodeURIComponent(url), function (res) {
            var data = JSON.parse(res.value);
            data.forEach(function (ev, index) {
                ev.vouchers.forEach(function (ev2, index2) {
                    let mid = "sendo";
                    let title = ev2.voucher_type == "cashback" ? "HoÃ n " : "Giáº£m " + addCommas(ev2.discount);
                    title += ev2.type == "percent" ? "%" : "Ä‘";
                    let desc = "";
                    if (ev2.voucher_type == "cashback") {
                        desc += "MÃ£ hoÃ n tiá»n sendo ";
                    } else {
                        desc += "MÃ£ giáº£m giÃ¡ sendo ";
                    }
                    desc += addCommas(ev2.discount) + " Ä‘Æ¡n hÃ ng tá»« ";
                    desc += addCommas(ev2.min_order) + (ev2.type == "percent" ? "%" : "Ä‘");
                    let discount = addCommas(ev2.discount) + (ev2.type == "percent" ? "%" : "Ä‘");

                    var date = new Date(parseInt(ev2.end_time * 1000));
                    let end_date = (date.getDate() < 10 ? '0' + date.getDate() : date.getDate()) + '/' + (date.getMonth() < 9 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1) + '/' + date.getFullYear();
                    let code = ev2.code != 'POLYXGO' ? ev2.code : '';
                    let url = ev2.link;
                    tmpArray.push({ mid: mid, title: title, desc: desc, discount: discount, end_date: end_date, code: code, url: encodeURIComponent(url) });
                });
            });

            var tmp = JSON.stringify(tmpArray);
            tmp = tmp.substring(1, tmp.length - 1);
            var result = tmp + ",";
            couponTotalArray += result;
        });
    }

    function get_tiki_promo_code_api() {
        var tmpArray = [];
        var url = "https://data.polyxgo.com/api/v1/datax/tiki_vouchers";
        jsonp('https://jsonp.afeld.me/?callback=&url=' + encodeURIComponent(url), function (res) {
            var data = JSON.parse(res.value);
            data.forEach(function (ev, index) {
                ev.vouchers.forEach(function (ev2, index2) {
                    let mid = "tiki";
                    let title = "Giáº£m " + addCommas(ev2.discount) + (ev2.discount < 1000 ? '%' : 'Ä‘');
                    let desc = ev2.description;
                    let discount = addCommas(ev2.discount) + (ev2.discount < 1000 ? '%' : 'Ä‘');

                    var date = new Date(parseInt(ev2.end_time * 1000));
                    let end_date = (date.getDate() < 10 ? '0' + date.getDate() : date.getDate()) + '/' + (date.getMonth() < 9 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1) + '/' + date.getFullYear();
                    let code = ev2.code != 'POLYXGO' ? ev2.code : '';
                    let url = ev2.link;
                    tmpArray.push({ mid: mid, title: title, desc: desc, discount: discount, end_date: end_date, code: code, url: encodeURIComponent(url) });
                });
            });

            var tmp = JSON.stringify(tmpArray);
            tmp = tmp.substring(1, tmp.length - 1);
            var result = tmp + ",";
            couponTotalArray += result;
        });
    }

    function get_fahasa_promo_code_api() {
        var tmpArray = [];
        var url = "https://data.polyxgo.com/api/v1/datax/fahasa_vouchers";
        jsonp('https://jsonp.afeld.me/?callback=&url=' + encodeURIComponent(url), function (res) {
            var data = JSON.parse(res.value);
            data.forEach(function (ev, index) {
                ev.vouchers.forEach(function (ev2, index2) {
                    let mid = "fahasa";
                    let title = "Giáº£m " + addCommas(ev2.amount) + (ev2.amount < 1000 ? '%' : 'Ä‘');
                    let desc = ev2.condition;
                    let discount = addCommas(ev2.amount) + (ev2.amount < 1000 ? '%' : 'Ä‘');

                    let end_date = ev2.end_time.substring(0, 10);
                    let code = ev2.code != 'POLYXGO' ? ev2.code : '';
                    let url = ev2.link;
                    tmpArray.push({ mid: mid, title: title, desc: desc, discount: discount, end_date: end_date, code: code, url: encodeURIComponent(url) });
                });
            });

            var tmp = JSON.stringify(tmpArray);
            tmp = tmp.substring(1, tmp.length - 1);
            var result = tmp + ",";
            couponTotalArray += result;
        });
    }
</script>