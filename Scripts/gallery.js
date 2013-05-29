function getElementLeft(elm) {
    var x = 0;

    //set x to elm’s offsetLeft
    x = elm.offsetLeft;

    //set elm to its offsetParent
    elm = elm.offsetParent;

    //use while loop to check if elm is null
    // if not then add current elm’s offsetLeft to x
    //offsetTop to y and set elm to its offsetParent

    while (elm != null) {
        x = parseInt(x) + parseInt(elm.offsetLeft);
        elm = elm.offsetParent;
    }
    return x;
}

function getElementTop(elm) {
    var y = 0;

    //set x to elm’s offsetLeft
    y = elm.offsetTop;

    //set elm to its offsetParent
    elm = elm.offsetParent;

    //use while loop to check if elm is null
    // if not then add current elm’s offsetLeft to x
    //offsetTop to y and set elm to its offsetParent

    while (elm != null) {
        y = parseInt(y) + parseInt(elm.offsetTop);
        elm = elm.offsetParent;
    }

    return y;
}

function Large(obj) {
    var imgbox = document.getElementById("imgbox");
    imgbox.style.visibility = 'visible';
    var img = document.createElement("img");
    img.src = obj.src;
    var height = obj.height;
    var width = obj.width;
    img.style.width = width * 2;
    img.style.height = height * 2;

    if (img.addEventListener) {
        img.addEventListener('mouseout', Out, false);
    } else {
        img.attachEvent('onmouseout', Out);
    }
    imgbox.innerHTML = '';
    imgbox.appendChild(img);
    imgbox.style.left = (getElementLeft(obj) - 50) + 'px';
    imgbox.style.top = (getElementTop(obj) - 50) + 'px';
}


function Out() {
    document.getElementById("imgbox").style.visibility = 'hidden';
}


function unloadPopupBox() {	// TO Unload the Popupbox
    $('#popup_box').fadeOut("fast");
    $('#popup_box').find('img').remove();
    $("#MainContainer").css({ // this is just for style		
        "opacity": "1"
    });
}

function loadPopupBox(img) {// To Load the Popupbox
   
    $('#popup_box').append("<img src='"+img+"'/>");
    $('#popup_box').fadeIn("slow");
    
    $("#MainContainer").css({ // this is just for style
        "opacity": "0.3"
    });
}