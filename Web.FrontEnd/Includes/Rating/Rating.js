var sMax; // Isthe maximum number of stars
var preSet; // Is the PreSet value onces a selection has been made

// Rollover for image Stars //
function rating(num) {
    sMax = 0; // Isthe maximum number of stars
    for (var n = 0; n < num.parentNode.childNodes.length; n++) {
        if (num.parentNode.childNodes[n].nodeName == "A") {
            sMax++;
        }
    }

    var rid = num.parentNode.id.split('_');
    if (rid[0] == "1") {
        var s = num.id.replace(rid[1] + "_", ''); // Get the selected star
        for (var i = 1; i <= sMax; i++) {
            if (i <= s) {
                document.getElementById(rid[1] + "_" + i).className = "on";
            } else {
                document.getElementById(rid[1] + "_" + i).className = "";
            }
        }
    }
}

function current(star, rate, max) {
    for (var i = 1; i <= max; i++) {
        if (i <= rate) document.getElementById(star + "_" + i).className = "on";
        else document.getElementById(star + "_" + i).className = "";

    }

    if (rate <= max) {
        var me = document.getElementById(star + "_" + rate);
        me.parentNode.setAttribute("onmouseout", "current('" + star + "'," + rate + "," + max + ")");
    }

    if (preSet == null) {
        preSet = rate;
    }
}

// For when you roll out of the the whole thing //
function off(me) {
    var rid = me.parentNode.id.split('_');
    if (rid[0] == "1") {
        var vote = document.getElementById(rid[1] + '_' + preSet);
        rating(vote);
    }
}

// When you actually rate something //
function rateIt(me) {
    var rid = me.parentNode.id.split('_');
    if (rid[0]=="1") {
        me.parentNode.id = "0_" + rid[1];
        rating(me);
        sendRate(me);
    }
}

function generate_stars(max, attach) {
    //get div container
    var container = document.getElementById(attach);
    if (container != null) {
        container.id = "1_" + attach;
        for (var i = 1; i <= max; i++) {
            //create star
            var a = document.createElement("a");
            a.setAttribute("onclick", "rateIt(this)");
            a.setAttribute("id", attach + "_" + i);
            //set events
            a.setAttribute("onmouseover", "rating(this)");
            a.setAttribute("onmouseout", "off(this)");
            //append child to contaner
            container.appendChild(a);
        }
    }
}