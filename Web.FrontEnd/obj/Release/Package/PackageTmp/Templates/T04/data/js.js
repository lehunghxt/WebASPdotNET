    //IMG



    function w3_open() {
    document.getElementById("mySidebar").style.display = "block";
    document.getElementById("myOverlay").style.display = "block";
}
 
function w3_close() {
    document.getElementById("mySidebar").style.display = "none";
    document.getElementById("myOverlay").style.display = "none";
}

//window.onscroll = function () { myFunction();}
//function myFunction() {
//    var navbar = document.getElementById("navbar");
//    var sticky = navbar.offsetTop;
//          if (window.pageYOffset >= sticky) {
//            navbar.classList.add("sticky")
//          } else {
//            navbar.classList.remove("sticky");
//          }
//        }

//back to top
function topFunction() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}