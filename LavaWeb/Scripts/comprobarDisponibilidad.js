function comprobarDisponibilidad(idx=null) {
    selectClear();
    fechaEntrad = $("#fechaEntrada").val();
    fechaSalid = $("#fechaSalida").val();;
    var serviceURL = '/Home/Disponibilidad';
    if (fechaEntrad !== "" && fechaSalid !== "") {
        $.ajax({
            type: "POST",
            url: serviceURL,
            data: param = { fechaEntrada: fechaEntrad, fechaSalida: fechaSalid, id: idx },
            dataType: "json",
            success: successFunc,
            error: errorFunc
        });

    }

}

function successFunc(data, status) {

    data.forEach(agregarItemSelect);
}

function errorFunc() {
    alert('Hubo un error buscando dormis disponibles');
}

function agregarItemSelect(item)
{
    $('#dormi').append($('<option>', {
        value: item,
        text: item
    }));
}

function selectClear()
{
    $('#dormi').html("");
    agregarItemSelect("---")
}
