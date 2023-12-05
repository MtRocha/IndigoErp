// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



//Funcoes de Validação Geral
function ValidateText(text) {

    var regex = new RegExp("\^[\"\';:]+$")

    if (regex.test(item))
    {
        return true
    }
    else {
        return false
    }

}

function ValidateInt(value, isLowerThanZero, isEqualToZero) {

    if ((value == 0 && !isEqualToZero) || (value < 0 && !isLowerThanZero)) {
        return true;
    }
    else { return false; }


}
//------------------------------------------------------------------------------//

//Funcao de validação de Equipamento
function ValidateEquip()
function EditEquip() {

    window.location("/Equip/")

}
