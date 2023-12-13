// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Funcoes de Validação Geral
function ValidateText(text) {
    var regex = new RegExp("\^[\"\';:]+$")

    if (regex.test(text) || text == '') {
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
function ValidateEquip() {
    const form = document.getElementById('form')

    const nome = document.getElementById('nome')
    const numeroSerie = document.getElementById('numeroSerie')
    const marca = document.getElementById('marca')
    const modelo = document.getElementById('modelo')
    const setor = document.getElementById('setor')

    const nomeInput = document.getElementById('nomeInput')
    const numeroSerieInput = document.getElementById('numeroSerieInput')
    const marcaInput = document.getElementById('marcaInput')
    const modeloInput = document.getElementById('modeloInput')
    const setorInput = document.getElementById('setorInput')

    var erro;

    if (ValidateText(nomeInput.value)) {
        erro = nome.querySelector('.alert-danger')
        erro.style.display = 'flex'
    }
    else {
        erro = nome.querySelector('.alert-danger')
        erro.style.display = 'none'
    }

    if (ValidateText(numeroSerieInput.value)) {
        erro = numeroSerie.querySelector('.alert-danger')
        erro.style.display = 'flex'
    }
    else {
        erro = numeroSerie.querySelector('.alert-danger')
        erro.style.display = 'none'
    }

    if (ValidateText(marcaInput.value)) {
        erro = marca.querySelector('.alert-danger')
        erro.style.display = 'flex'
    }
    else {
        erro = marca.querySelector('.alert-danger')
        erro.style.display = 'none'
    }

    if (ValidateText(modeloInput.value)) {
        erro = modelo.querySelector('.alert-danger')
        erro.style.display = 'flex'
    }
    else {
        erro = modelo.querySelector('.alert-danger')
        erro.style.display = 'none'
    }

    if (setorInput.selectedOptions[0].text == "Setor") {
        erro = setor.querySelector('.alert-danger')
        erro.style.display = 'flex'
    }
    else if (setorInput.selectedOptions[0].text != "Setor") {
        erro = setor.querySelector('.alert-danger')
        erro.style.display = 'none'
        form.submit()
    }
}
function EditEquip(item) {
    if (confirm("Deseja Realmente Editar Este Equipamento ?"))
        location.href = "/Equip/EditEquip?id=" + item
}

function DeleteEquip(item) {
    if (confirm("Deseja Realmente Excluir Este Equipamento ?"))
        location.href = "/Equip/DeleteEquip?id=" + item
}

function DeleteFail(item) {
    if (confirm("Deseja Realmente Excluir Esta Falha ?"))
        location.href = "DeleteFail?id=" + item
}

function Habilitar(item) {
    item.style.display = "flex"
}

function Desabilitar(item) {
    item.style.display = "none"
}