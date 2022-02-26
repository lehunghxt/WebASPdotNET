/* get array of checked checkbox */
function getAllChecked(name) {
    var arr = "";
    $("input[name='" + name + "']:checked").each(function () { arr += this.value + ","; });
    return arr.substr(0, arr.length - 1);
}

/* check all checkbox */
function checkAll(id, name) {
    var flag = $("#" + id).is(':checked');
    $("input[name='" + name + "']").each(function () { this.checked = flag; });
}

//function checkAll(name) {
//  
//    $("input[name='" + name + "']").each(function () { this.checked = false; });
//}
/* has checked */
function hasCheck(name, msg) {
    if ($("input[name='" + name + "']:checked").length == 0) {
        alert(msg);  return false;
    } return true;
}
/* checkByValues */
function checkByValues(name, values) {
    if (values != "") {
        arr = values.split(",");
        $("input[name='" + name + "']").each(function () {
            var val = $(this).val();
            for (var i in arr) {
                if (arr[i] == val) this.checked = true;
            }
        });
    }
}
//------------------------------------------------------------------------------------------------------------------//
function checkForEdit(hidden, name, msg) {
    var str = getAllChecked(name);
    if (str == "") { alert(msg); return false; }
    $("#" + hidden).val(str); return true;
}

function isRoot(id) {
    if ($("td#" + id).html() == "") return true; return false;
}

function checkRoot(name, msg) {
    $("input[name='" + name + "']:checked").each(function () {
        if (isRoot(this.value)) alert(msg); return false;
    });
}


//.................................................................................................................//
function PopupPage(Url, Width, Height) {
    var OffsetHeight = document.body.offsetHeight;
    var OffsettWidth = document.body.offsetWidth;
    var objWindow = window.open(Url, "", "width=" + Width + ",height=" + Height + ",resizable=1,scrollbars=yes,location=0");
    objWindow.moveTo((OffsettWidth - Width) / 2, (OffsetHeight - Height) / 2);
}
function PopupPageWithMenuBar(Url, Width, Height) {
    var OffsetHeight = document.body.offsetHeight;
    var OffsettWidth = document.body.offsetWidth;
    var objWindow = window.open(Url, "", "width=" + Width + ",height=" + Height + ",resizable=1,scrollbars=yes,location=0, menubar=1");
    objWindow.moveTo((OffsettWidth - Width) / 2, (OffsetHeight - Height) / 2);
} 