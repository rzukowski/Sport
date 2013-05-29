function validateFileUpload(obj) {
    alert("validate");
    var fileName = new String();
    var fileExtension = new String();

    // store the file name into the variable  
    fileName = obj.value;

    alert(fileName);
 
    // extract and store the file extension into another variable  
    fileExtension = fileName.substr(fileName.length - 3, 3);

    // array of allowed file type extensions  
    var validFileExtensions = new Array("jpg", "png", "gif");

    var flag = false;

    // loop over the valid file extensions to compare them with uploaded file  
    for (var index = 0; index < validFileExtensions.length; index++) {
        if (fileExtension.toLowerCase() == validFileExtensions[index].toString().toLowerCase()) {
            flag = true;
        }
    }

    // display the alert message box according to the flag value  
    if (flag == false) {
        alert('Pliki z rozszerzeniem ".' + fileExtension.toUpperCase() + '" nie są dozwolone.\n\nDo galerii możesz dodać wyłącznie pliki o poniższym rozszerzeniu:\n.jpg\n.png\n.gif\n');
        return false;
    }
    else {
       
        return true;
    }
}
